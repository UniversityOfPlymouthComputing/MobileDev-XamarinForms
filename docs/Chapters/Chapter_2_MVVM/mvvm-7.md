[Contents](README.md)

----

[Prev](mvvm-6.md)

## Part 7 - Hooking up to the Cloud
[Part 7 is here](/code/Chapter2/Bindings/HelloBindings-07). Inspect and familiarise yourself with the code fully before proceeding. There is some preliminary work you need to do before you can run it.

In this last section, I've done something that might seem a bit ambitious: I've stored the list of sayings in the cloud (jargon for someone elses computer), and I'm going to use Azure Functions to retrieve them. This would allow me to add more sayings without app updates. Also, as long as we moderate usage, it's free, and we like free.

### Setting up an Azure Function

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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "LookupSaying")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

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

Note how this function is stateless. You pass in parameters, you get back a response. There is no _remembering_. The _state_ is held by the client. To facilitate this, this function requres a single parameter `index` to specify which saying is being requested. The response is some XML which includes the saying and the total number of sayings available.

So, for a READ (GET), we might issue:

`https:\\<url-to-function>?index=1`
 
If index is within range the response will be XML of the form:

[TBD]



### Updating the Model
The biggest change to this project is in the model. The class has been split into three:

- An abstract base class `SayingsAbstractModel`
- Two child classes: `RemoteModel` and `MockedRemoteModel`

The mocked model is provided as an alternative to using an acutal network. Is also simulates some of the network failings that might occur (handled in the next section).

Once networking is introduced, the following need to be taken into account:

- Fetching the next piece of data will take an unpredictable amount of time and so will be done **asynchronously**
- The model needs to communicate when a network trasnaction is waiting for a response
- The model also needs to communicate whether the transaction was successful, and if not, why not.
- The model also needs to convert the retrieved payload of data (XML) into a data structure
- Initial values of data also need to be considered - it might not be good etiquette to pull data without involvement from the user  

The very asynchronous nature of network communication almost always adds a significiant degree of complexity to any application. We now see why `await`, `async`, `try` and `catch` are so important.

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

        //Specific implmentation for fetching a saying from the network (Azure)
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

        protected abstract Task<PayLoad> FetchPayloadAsync(int WithIndex = 0);
    }
```

It looks worse than it really is and you'd probably be advised to study this code fairly closely.

First, there are a number of bindable properties that all follow the same pattern

- `CurrentSaying : string` - Holds the current saying to be displayed
- `SayingNumber : int` - The number in the sequence of sayings
- `IsRequestingFromNetwork : bool` - True while waiting for a network response to respond
- `HasData : bool` - True once the first fetch has been successfully completed



[ TO BE CONTINUED ]


----

[Contents](README.md)
