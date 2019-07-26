[Contents](README.md)

----

[Prev](mvvm-4.md)

## Part 5 - Commanding and the ViewModel
[Part 5 is here](/code/Chapter2/Bindings/HelloBindings-05). Build and run this to see what it does. Inspect and familiarise yourself with the code fully before proceeding.

The time has finally to adopt MVVM

- The Model currently has properties related to UI State only
- It's only going to get worse!

There are two aims in mind:
- To unit test the model and consider only domain specific data issues. Ideally, the code would be pure C#.NET
- To unit test the ViewModel and test UI logic. Ideally, there will be no dependency on UI components in Xamarin.Forms

A bit of code refactoring is there needed!

### A Cleaner Model Class
Here is the new Model class, with all the 
```C#
    class Model : INotifyPropertyChanged
    {
        //Keeping this around - I will need it later ;)
        public event PropertyChangedEventHandler PropertyChanged;

        private List<string> Sayings = new List<string>
        {
            "May the Force be With You",
            "Live long and prosper",
            "Nanoo nanoo"
        };

        public int SayingNumber { get; private set; } //Index of which saying to use
        public string CurrentSaying { get; private set;  }
        public void NextMessage()
        {
            SayingNumber = (SayingNumber + 1) % Sayings.Count;
            CurrentSaying = Sayings[SayingNumber];
        }

        public Model()
        {
            CurrentSaying = Sayings[0];
        }
    }
```

All the binding related code has been removed. I've kept in `INotifyPropertyChanged` but it's not needed at this stage (you should see a sqiggle in the editor suggesting that `PropertyChanged` is unused).

Reflecting on this code, it's now very simple - it simply stores data and has one method (`NextMessage()`). This is simple to unit test!

### Introducing the ViewModel
Now we introduce the middle layer - the ViewModel.  First, let's examine the complete class:

```C#
   class MainPageViewModel : INotifyPropertyChanged
    {
        private Model DataModel = new Model();
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ButtonCommand { get; private set; }

        public MainPageViewModel()
        {
            ButtonCommand = new Command(execute: () =>
            {
                DataModel.NextMessage();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentSaying"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SayingNumber"));
            }, canExecute: () => this.UIVisible);
        }

        public int SayingNumber => DataModel.SayingNumber;
        public string CurrentSaying => DataModel.CurrentSaying;

        bool _visible = true;
        public bool UIVisible
        {
            get => _visible;
            set
            {
                if (value != _visible)
                {
                    _visible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UIVisible"));
                    ((Command)ButtonCommand).ChangeCanExecute();
                }
            }
        }
    }
```

For MVVM, note the following

- **The View is going to bind to the ViewModel and NOT the model**, so once again we implement `INotifyPropertyChanged`.
- The ViewModel will expose bindable properties to align with the bound view properties (albeit with converters in between where necessary) 
- The ViewModel will look after UI state, but not domain data state
- The ViewModel instantiates the `Model` (in this case) 
- The ViewModel coordaintes data flow between Model and View.
- We want to ViewModel to ultimately be unit testable, so we would like to remove event handlers (which have references to UI objects)

In terms of exposing bindable properties to the view, let's do the easy stuff first and peform a simple data pass-through. 

```C#
  public int SayingNumber => DataModel.SayingNumber;
  public string CurrentSaying => DataModel.CurrentSaying;
```        

These are both read-only. Neither of these properties are changed in the view model to there is no value in writing setters. The rest of the changes relate to removal of the event handler and replacement with _commanding_.

### Commanding
The event handler for the button has already been removed. A new property `ButtonCommand` (type `System.Windows.Input.ICommand`) has been added:

```C#
public ICommand ButtonCommand { get; private set; }
```

Let's take a sneaky look at the binding in the code-behind:

```C#
   MessageButton.SetBinding(Button.CommandProperty, "ButtonCommand"); 
```

This is rather nice, as it keeps with the MVVM architecture:

> In short, we have replaced an event handler with a bindable property `ButtonCommand`

I have chosen in instantiate the `Command` in the constructor as follows:

```C#
     public MainPageViewModel()
     {
         ButtonCommand = new Command(execute: () =>
         {
             DataModel.NextMessage();
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentSaying"));
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SayingNumber"));
         }, canExecute: () => this.UIVisible);
     }
```        

This is where all the action takes place! The `Command` class constructor as two important properties: `execute` (type `System.Action`) and `canExecute` (type `System.Func<bool>`). Both of these are types of anonymous function.

When the Command is instantiated, the `canExecute:` property is executed. This simply executes the code `() => this.UIVisible` which returns a bool.
- If a true is returned, the button is enabled
- If a false is returned, the buttis is disabled

When the button is clicked (assuming it is enabled) the bindings will invoke the execute: property.  

```C#
    DataModel.NextMessage();
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentSaying"));
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SayingNumber"));
```             
This first updates the model. In the knowledge that the `CurrentSaying` property and `SayingNumber` property will have changed, so the bindings on these properties are notified so that the UI is updated.

Note how the UI state is managed here. The `IsEnabled` property is also bound via the `ButtonCommand` property. This makes reference to the following property:

```C#
     bool _visible = true;
     public bool UIVisible
     {
         get => _visible;
         set
         {
             if (value != _visible)
             {
                 _visible = value;
                 PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UIVisible"));
                 ((Command)ButtonCommand).ChangeCanExecute();
             }
         }
     }
```        

This property is bound to the switch in the UI. If the switch is changed, the setter will execute. Observe the invokation of `((Command)ButtonCommand).ChangeCanExecute()`. This forces the `ButtonCommand` to reevaluate the canExecute: function and update the `IsEnabled` property on the bounded button.

_What a tangled web we weave!_

### The Code Behind
Before we finish, let's look at the Code Behind, which now represents only the View in MVVM

```C#
    public partial class MainPage : ContentPage
    {
        MainPageViewModel ViewModel = new MainPageViewModel();

        public MainPage()
        {
            InitializeComponent();

            //BindingContext is the same for all elements, so can use the containing view
            
            //ToggleSwitch.BindingContext = ViewModel;
            //MessageButton.BindingContext = ViewModel;
            //MessageLabel.BindingContext = ViewModel;
            BindingContext = ViewModel;

            ToggleSwitch.SetBinding(Switch.IsToggledProperty, "UIVisible", BindingMode.OneWayToSource);
            MessageButton.SetBinding(Button.TextProperty, "SayingNumber", BindingMode.OneWay, null, "Saying: {0:d}");
            MessageButton.SetBinding(Button.CommandProperty, "ButtonCommand"); 

            MessageLabel.SetBinding(Label.TextProperty, "CurrentSaying", BindingMode.OneWay);
            MessageLabel.SetBinding(Label.IsVisibleProperty, "UIVisible", BindingMode.OneWay);
            MessageLabel.SetBinding(Label.TextColorProperty, "SayingNumber", BindingMode.OneWay, new ColorConverter());
        }
    }
```

Note how all the view objects shared the same `BindingContext`. In this case, we can simplify matters by simply setting the binding context on the containing view as follows: 
```C#
         //ToggleSwitch.BindingContext = ViewModel;
         //MessageButton.BindingContext = ViewModel;
         //MessageLabel.BindingContext = ViewModel;
         BindingContext = ViewModel;
```            

Some points to note:
- In this example, the ViewModel is instantiated by the View
- The ViewModel is the Binding Context (source) for all targets, so the containing view is used instead
- We added a new binding - the button `Command` property.

In the next section, still keeping these APIs in mind, we remove ALL this code!

 [Next](mvvm-6.md)

----

[Contents](README.md)
