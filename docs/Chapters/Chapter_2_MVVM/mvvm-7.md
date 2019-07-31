[Contents](README.md)

----

[Prev](mvvm-6.md)

## Part 7 - Hooking up to the Cloud
[Part 7 is here](/code/Chapter2/Bindings/HelloBindings-07). Inspect and familiarise yourself with the code fully before proceeding. There is some preliminary work you need to do before you can run it.

In this section, I've done something that might seem a bit ambitious: I've stored the list of sayings in the cloud (jargon for someone elses computer), and I'm going to use Azure Functions to retrieve them. This would allow me to add more sayings without app updates. Also, as long as we moderate usage, it's free, and we like free.

Another option is to run Azure functions _locally_. I will start with this, and if you wish to host on the cloud, you can do so.

### Pre-requisites
To run Azure functions locally, you need to meet some pre-requisites first:

For both Mac and PC, the following apply:

1. Ideally, if you don't already have it, [install the Node Package Manager (npm)](https://nodejs.org/en/download/)
You can test if you already have it by opening a command shell / terminal and typing `npm`
1. Install the Azure Functions Core Tools by typeing `npm install -g azure-functions-core-tools`



### Setting up an Azure Function
Starting with [Part 6](/code/Chapter2/Bindings/HelloBindings-06), we're going to add an Azure Function to the project.




[TO BE DONE]

The function we are going to use is as follows:

```C#
    public static class Function1 
    {
        private static List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo",
            "Make it So!"
        };
        private static int Count => Function1.Sayings.Count;

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "LookupSaying")] HttpRequest req)
        {
            string name = req.Query["index"];
            bool success = int.TryParse(name, out int index);
            if (success)
            {
                if ((index >= 0) && (index < Function1.Count))
                {
                    PayLoad p = new PayLoad
                    {
                        From = Count,
                        Saying = Sayings[index]
                    };
                    
                    return (ActionResult)new OkObjectResult(p.ToXML());
                }
                else
                {
                    return new BadRequestObjectResult("Index out of range. Please use an index from 0.." + Count);
                }

            } else
            {
                 return new BadRequestObjectResult("The query string parameter index must be an integer");
            }

        }
    }
```

Note how this function is _stateless_, i.e. it has no memory of previous calls. It's a pure function where you pass in parameters, you get back a response. There is no _remembering_ of past calls (or who made them) by the server. Any _state_ must be held by the client. To facilitate this, this function takes a single parameter `index`, provided by the client, to specify which saying is being requested. The response is some XML which includes the saying and the total number of sayings available.

So, for a READ (GET), we might issue:

`https:\\<url-to-function>?code=<id from azure>&index=1`
 
If index is within range the response will be XML of the form:

[TBD]

The XML data is produced using a technique known as _serialisation_. This behaviour (and the ability to de-serialise) is encapsulated in the `PayLoad` class

```C#
    public class PayLoad
    {
        public string Saying { get; set; }
        public int From { get; set; }
        public PayLoad()
        {
        }
        public string ToXML()
        {
            var xmlSerializer = new XmlSerializer(this.GetType());
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }

        }
        public static PayLoad FromXML(string text)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PayLoad));
            using (StringReader textReader = new StringReader(text))
            {
                return xmlSerializer.Deserialize(textReader) as PayLoad;
            }
        }
    }
```

Yes I could have used JSON, but as the data is small and XML is baked right in, then XML is fine. The great thing is this exact same code is used on the server and in the client (it is help in a shared library).


### Updating the Model
The biggest change to this project is in the client model. The class has now been split into three:

- An abstract base class `SayingsAbstractModel` containing most of the code
- Two thin child classes: `RemoteModel` and `MockedRemoteModel` 

The mocked model provides an alternative to using an acutal network. Is simulates the delay and some of the network failings that might occur (handled in the next section). This is useful for testing.

Once networking is introduced, the following need to be taken into account:

- Fetching the next piece of data will take an unpredictable amount of time and so will be done **asynchronously**
- The model needs to communicate when a network transaction is waiting for a response
- The model also needs to communicate whether the transaction was successful, and if not, why not.
- The model also needs to convert the retrieved payload of data (XML) into a data structure
- Initial values of data also need to be considered - it might not be good etiquette to pull data without involvement from the user  

> The very asynchronous nature of network communication almost always adds a significiant degree of complexity to any application. 

We will also see why `await`, `async`, `try` and `catch` are so important.

Let's examing the base-class first

```C#
    public abstract class SayingsAbstractModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //The number of strings in the collection
        public int Count { get; set; } = 0;

        //The currently fetched saying
        protected string _currentSaying = "Ye Olde Wise Sayings";
        public string CurrentSaying
        {
            get => _currentSaying;
            protected set
            {
                if (!_currentSaying.Equals(value))
                {
                    _currentSaying = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSaying)));
                }
            }
        }

        //The position of the saying in the list
        protected int _sayingNumber = 0;
        public int SayingNumber
        {
            get => _sayingNumber;
            protected set
            {
                if (value != _sayingNumber)
                {
                    _sayingNumber = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SayingNumber)));
                }
            }
        }

        //Set true while waiting on a network transaction
        protected bool _isRequestingFromNetwork = false;
        public bool IsRequestingFromNetwork
        {
            get => _isRequestingFromNetwork;
            protected set
            {
                if (value != _isRequestingFromNetwork)
                {
                    _isRequestingFromNetwork = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRequestingFromNetwork)));
                }
            }
        }

        //Set true if valid data has been acquired
        protected bool _hasData = false;
        public bool HasData
        {
            get => _hasData;
            protected set
            {
                if (_hasData != value)
                {
                    _hasData = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasData)));
                }
            }
        }

        //Fetches next saying from the network
        public async Task<(bool success, string ErrorString)> NextSaying()
        {
            //Perform fetch from a network
            int n = SayingNumber;
            n = HasData ? (n + 1) % Count : 0;
            IsRequestingFromNetwork = true;
            (bool success, string ErrStr) = await FetchSayingAsync(n);
            IsRequestingFromNetwork = false;
            return (success, ErrStr);
        }

        //Wrapper around the specific implmentation for fetching a saying 
        protected async Task<(bool success, string status)> FetchSayingAsync(int WithIndex = 0)
        {
            try
            {
                PayLoad p = await FetchPayloadAsync(WithIndex);
                if (p != null)
                {
                    Count = p.From;
                    CurrentSaying = p.Saying;
                    SayingNumber = WithIndex;
                    HasData = true;
                    return (success: HasData, status: "OK");
                }
                else
                {
                    HasData = false;
                    return (success: HasData, status: "Invalid Response");
                }
            }
            catch
            {
                HasData = false;
                return (success: HasData, status: "Permission Denied");
            }
        }

        //Specific implmentation for fetching a saying (either from the Azure network or mocked)
        protected abstract Task<PayLoad> FetchPayloadAsync(int WithIndex = 0);
    }
```

It looks worse than it really is and you'd probably be advised to study this code fairly closely.

First, a large bulk of this code is made from a set of bindable properties that all follow the same pattern

- `CurrentSaying : string` - Holds the current saying to be displayed
- `SayingNumber : int` - The number in the sequence of sayings
- `IsRequestingFromNetwork : bool` - True while waiting for a network response to respond
- `HasData : bool` - True once the first fetch has been successfully completed

All the above are updated asynchronously. By making these bindable, this helps separate concerns and decouple from the rest of the application. The model does not need to know about _what_ is observing these bindings, or how they will be displayed.

To trigger a network connection there is _one_ simple public API

```C#
     //Fetches next saying from the network
     public async Task<(bool success, string ErrorString)> NextSaying()
     {
         //Perform fetch from a network
         int n = SayingNumber;
         n = HasData ? (n + 1) % Count : 0;
         IsRequestingFromNetwork = true;
         (bool success, string ErrStr) = await FetchSayingAsync(n);
         IsRequestingFromNetwork = false;
         return (success, ErrStr);
     }
```        

The centrepiece of this is `(bool success, string ErrStr) = await FetchSayingAsync(n);` 

- `await` does **not** block the main thread. Instead it yelds to the main thread event queue. Once `FetchSayingAsync` returns a result, a call-back to the point where it yeided will be added to the event queue of the main thread.
- Around this the property `IsRequestingFromNetwork` is set. Remember that `IsRequestingFromNetwork` is bindable.
- The return result is a tuple containing a boolean (indicating success or failure) and a string (indicating the status)

Let's drill down into `FetchSayingAsync` (which is NOT public)

```C#
     protected async Task<(bool success, string status)> FetchSayingAsync(int WithIndex = 0)
     {
         try
         {
             PayLoad p = await FetchPayloadAsync(WithIndex);
             if (p != null)
             {
                 Count = p.From;
                 CurrentSaying = p.Saying;
                 SayingNumber = WithIndex;
                 HasData = true;
                 return (success: HasData, status: "OK");
             }
             else
             {
                 HasData = false;
                 return (success: HasData, status: "Invalid Response");
             }
         }
         catch
         {
             HasData = false;
             return (success: HasData, status: "Permission Denied");
         }
     }
```

Key to this is the method call `PayLoad p = await FetchPayloadAsync(WithIndex);`. This method is not implemented in the base class, but in a child.

- Note again how the bindable properties `CurrentSaying`, `SayingNumber` and `HasData` are all set here. This gives updates to any object that chooses to observe these changes
- We might describe this as a _decorator_ for `FetchPayloadAsync`, performing actions before and after it is invoked.
- The actions here focus on updating state

Finally, we drill down to the innermost method, `FetchPayloadAsync(WithIndex)`. This is implementation specific and is found in the child classes `RemoteModel` and `MockedRemoteModel`

#### RemoteModel
This is the actual model class that will interact with Azure Functions. 

```C#
    public class RemoteModel : SayingsAbstractModel
    {

        //URL string for the remote server
        protected const string Url = "https://sayingsfunctionappplymouth.azurewebsites.net/api/LookupSaying?index=";

        //Dynamically allocated HTTP client for performing a network connection
        protected static HttpClient _client;
        protected static HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                    _client.DefaultRequestHeaders.Add("x-functions-key", "Z8W37szNxA5mdRmDkblGr/3fimj3IPojd6l9tDTBo4pgyHRtklovAA==");
                }
                return _client;
            }
        }

        protected override async Task<PayLoad> FetchPayloadAsync(int WithIndex = 0)
        {
            string result = await Client.GetStringAsync(Url + WithIndex);
            PayLoad p = PayLoad.FromXML(result);
            return p;
        }
    }
```

- The security key (yours will be different) is stored in the HTTP header
- Note how we only create one instance of HTTP client (it is a `static` property)
- This code can focus on one task, to fetch the payload. It does not need to concern itself with bound properties. It can be easily substitured with something to mimick it.

#### MockedRemoteModel
This class mocks a real network interface. This takes more work that using the real thing!

```C#
    public class MockedRemoteModel : SayingsAbstractModel
    {
        private static List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo",
            "Make it So!",
            "never show this",
        };

        private byte errorMode = 0;

        protected override async Task<PayLoad> FetchPayloadAsync(int WithIndex = 0)
        {
            await Task.Delay(1000);

            //Every last one simulate a network error or invalid response (unable to parse XML)
            if (WithIndex == Sayings.Count - 1)
            {
                if ((errorMode++) % 2 == 1)
                {
                    return null;
                }
                else
                {
                    throw new HttpRequestException();
                }
            }

            PayLoad p = new PayLoad
            {
                Saying = Sayings[WithIndex],
                From = Sayings.Count
            };
            return p;
        }
    }
```

- The network delay is simulated with `await Task.Delay(1000);`
- On the last saying, one of two error types are created:
    - A `null`, which simulates the inability to deserialise the data
    - A network exception (same as that thrown by the real code)

So we know what output to expect, what errors will occur and _the order in which they occur_. This makes deterministic testing much simpler.

### Updating the ViewModel
Now the network layer is built (and tested of course!), we can adapt the viewmodel to handle all the bindable properties. 

> By binding to model properties, we now get _events_ from the model AND the view to manage.

![Model Bindings](img/model-mvvm-bindings.png)

For each property in the Model, there is another in the ViewModel of the same name.

```C#
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private SayingsAbstractModel DataModel { get; }                 //Model object 
        public event PropertyChangedEventHandler PropertyChanged;       //Used to generate events to enable binding to this layer
        public ICommand FetchNextSayingCommand { get; private set; }    //Binable command to fetch a saying

        public MainPageViewModel(SayingsAbstractModel WithModel)
        {
            DataModel = WithModel;
            //Hook up FetchNextSayingCommand property
            FetchNextSayingCommand = new Command(execute: async () => await DoFetchNextMessageCommand(), 
                                              canExecute: () => ButtonEnabled);
            //Hook up event handler for changes in the model
            DataModel.PropertyChanged += OnPropertyChanged;
        }

        //Command to fetch next message - made public to support unit testing
        public async Task DoFetchNextMessageCommand() => await DataModel.NextSaying();

        //Exent handler for all changes on the model
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(DataModel.SayingNumber)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SayingNumber)));
            }
            else if (e.PropertyName.Equals(nameof(DataModel.HasData)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasData)));
            }
            else if (e.PropertyName.Equals(nameof(DataModel.CurrentSaying)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSaying)));
            }
            else if (e.PropertyName.Equals(nameof(DataModel.IsRequestingFromNetwork)))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRequestingFromNetwork)));
                ((Command)FetchNextSayingCommand).ChangeCanExecute();
            }
        }

        //Map through read only acccess to Model properties
        public int SayingNumber => DataModel.SayingNumber;
        public string CurrentSaying => DataModel.CurrentSaying;
        public bool IsRequestingFromNetwork => DataModel.IsRequestingFromNetwork;
        public bool HasData => DataModel.HasData;
        
        //Calculated property for the button canExecute
        public bool ButtonEnabled => UIVisible && !IsRequestingFromNetwork;

        //Bindable property to manage UI state visibility (not to be confused with model based state)
        private bool _uiVisible = true;
        public bool UIVisible
        {
            get => _uiVisible;
            set
            {
                if (value != _uiVisible)
                {
                    _uiVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UIVisible)));
                    ((Command)FetchNextSayingCommand).ChangeCanExecute();
                }
            }
        }

    }
```

First note there is a read-only property for the Model

```C#
   private SayingsAbstractModel DataModel { get; }                 //Model object 
```        
Note the data type is the abstract base-class. This allows us to use polymorphism to perform a run-time switch between different concrete classes (a real and a mocked in this case).

The only place this can be modifed is in the constructor, shown below:

```C#
     public MainPageViewModel(SayingsAbstractModel WithModel)
     {
         DataModel = WithModel;
         //Hook up FetchNextSayingCommand property
         FetchNextSayingCommand = new Command(execute: async () => await DoFetchNextMessageCommand(), 
                                           canExecute: () => ButtonEnabled);
         //Hook up event handler for changes in the model
         DataModel.PropertyChanged += OnPropertyChanged;
     }
```        

- The model is passed as a parameter. This will be from either the View or a unit testing framework.
- The button command is configured 
- The notifications from the Model are connected to the ViewModel by setting `DataModel.PropertyChanged += OnPropertyChanged;`

We have to handle the events ourselves, decide which property has been changed and in most case, simply notify the view layer.

```C#
     private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
     {
         if (e.PropertyName.Equals(nameof(DataModel.SayingNumber)))
         {
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SayingNumber)));
         }
         else if (e.PropertyName.Equals(nameof(DataModel.HasData)))
         {
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasData)));
         }
         else if (e.PropertyName.Equals(nameof(DataModel.CurrentSaying)))
         {
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSaying)));
         }
         else if (e.PropertyName.Equals(nameof(DataModel.IsRequestingFromNetwork)))
         {
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRequestingFromNetwork)));
             ((Command)FetchNextSayingCommand).ChangeCanExecute();
         }
     }  
```

Note the last case: `((Command)FetchNextSayingCommand).ChangeCanExecute();` When the network changes state, so the Button may need to be enabled or disabled.

You may be wondering why we don't update the ViewModel properties in the code above? The next four lines should explain it.

```C#
     //Map through read only acccess to Model properties
     public int SayingNumber => DataModel.SayingNumber;
     public string CurrentSaying => DataModel.CurrentSaying;
     public bool IsRequestingFromNetwork => DataModel.IsRequestingFromNetwork;
     public bool HasData => DataModel.HasData;
```        

This application is so simple, that the ViewModel is simply acting as a go-between in this respect (more complex applications add additional logic of course).

The only exception is the following property:

```C#
   public bool ButtonEnabled => UIVisible && !IsRequestingFromNetwork;
```        
which is a property which indicates if the button should be enabled. This depends on the state of the network and the position of the switch in the UI. Note that `UIVisible` is bound to the switch in the user interface. Back to the constructor as we see this line:

```C#
      FetchNextSayingCommand = new Command(execute: async () => await DoFetchNextMessageCommand(), 
                                        canExecute: () => ButtonEnabled);
```

There are two places where `ButtonEnabled` can possibly change

- The Model event that updates `IsRequestingFromNetwork` 
- The setter for `UIVisible` (should it ever be used)

So `((Command)FetchNextSayingCommand).ChangeCanExecute();` is invoked in both places.

### Updating the View
There is very little change to the View except to pass a parameter to the ViewModel constructor. 

```C#
BindingContext = new MainPageViewModel(new RemoteModel());
```

If you prefer not to use Azure and use a Mocked version, change this to:

```C#
BindingContext = new MainPageViewModel(new MockedRemoteModel());
```

### Testing Locally - Android Emulator Security
By default, the Android emulator will not connect to a cleartext (http) endpoint. To override this, we need to make some edits to the Android project.

In the Android Project, do the following:

1. Add a folder `xml` to the resources folder

2. Add a new XML file `network_security_config.xml` to this folder with the following content:

```XML
<?xml version="1.0" encoding="utf-8"?>
<network-security-config>
    <domain-config cleartextTrafficPermitted="true">
        <domain includeSubdomains="true">10.0.2.2</domain>
    </domain-config>
</network-security-config>
```

> Handy hint. On the Android Emulator, 10.0.2.2 is resolved as the host PC running the emulator.

3. In the Android manifest, set android:networkSecurityConfig. For example:

```XML
<application 	android:label="hello_bindings.Android" 
		android:networkSecurityConfig="@xml/network_security_config"> 
</application>
```


### What is missing?
So far, we've focused mainly on _expected behaviour_ whereby the network is fully connected and the data returned is perfectly formed. What we have not (yet) fully considered are the following:

- The network is unreachable
- The access key has changed
- The function was changed and now returns a different data format (oops!)

For this, we need to look at the values returned from the button command

```C#
   public async Task DoFetchNextMessageCommand() => await DataModel.NextSaying();
```

We are currently ingoring the tuple data `(bool success, string ErrorString)` returned by `NextSaying()` or any exceptions that happen to be thrown.

It is tempting to try and ignore such things, but you can't (or at least should not).

> It is the handing of the unexpected that is often the differentiator of a robust and a weak application

In the next and last section for this example, we do just this.

 [Next](mvvm-8.md)

----

[Contents](README.md)
