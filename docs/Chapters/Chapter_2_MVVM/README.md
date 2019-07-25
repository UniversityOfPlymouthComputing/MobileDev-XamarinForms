[Contents](/docs/README.md)

----

# Model-View-ViewModel (MVVM)
In the previous example, a Model-View-Controller architecture was used. For simple applications, this works well with the following caveats:

- It does not tends to scale well as controllers get complicated
- The controller is tightly coupled with the view, making it difficult to unit test

Despite this, many excellent applications have been written with MVC. 

> The Model-View-ViewModel (MVVM) architecture is similar to MVC, only it is not tightly coupled to the view making it easier to test.

[From the Microsoft Documentation](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/xaml/xaml-basics/data-bindings-to-mvvm) (accessed 24/07/2019)

> The Model-View-ViewModel (MVVM) architectural pattern was invented with XAML in mind. The pattern enforces a separation between three software layers — the XAML user interface, called the View; the underlying data, called the Model; and an intermediary between the View and the Model, called the ViewModel. The View and the ViewModel are often connected through data bindings defined in the XAML file. The BindingContext for the View is usually an instance of the ViewModel.

_From: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/xaml/xaml-basics/data-bindings-to-mvvm (accessed 24/07/2019)_

The key point here is the connection between the View Model (which probably sounds like a Controller at this point) and the View: _they are not as tightly coupled as MVC_.

- The View knows something of the ViewModel, but the ViewModel does not know any specifics about the View
- The View Model knows something of the Model, but the Model knows nothing about the ViewModel

This is summarised in the following figure.

![MVVM Layers](img/mvvm-visibility.png)

The View Model and Model objects will contain no knowledge or reference to UI types. Only the view (XAML and code-behind) has knowledge of such types. The View Model and Model objects can therefore be compiled as part of a simple unit testing or even command line project.

This is achieved through the use of a _binding layer_ between the ViewModel and the View. Xamarin.Forms comes with a baked-in binding mechanism, but others are available. This is illustrated in the next figure:

![MVVM](img/mvvm.png)

Consider two possible scenarios:

1. The user makes a change to a UI element that has a property bound to a ViewModel property. This automatically causes an update to the ViewModel property without the need to write any code. The setter of that property may (or may not) then process the value and pass it to the Model via a public API. A common task for ViewModel is data conversion and updates to the Model.
2. A waiting network connection (invoked from within the Model) returns a new value and asynchronously updates a value in the model. This is advertised as an event. The ViewModel is listening for such events and so will observe the change. It then might change one of the bound properties which in-turn, provokes an automatic update in the UI

> The ViewModel is therefore the arbitrator between events in the View and events in the Model, marshalling data between them.

**Application State**
The Models define the _application state_ - Within the Model objects are the actual data, plus any methods that operate on the data. The ViewModel should not contan any domain specific data. A good Model should be self contained and highly testable.

**UI State**
The ViewModel also has state, but it is nothing to do with the domain Model data. These are typically properties relating to UI State, such as whether a Label is visible (`bool`), the selected row of a table (`int`) etc. A good view model will also be highly testable. In fact one of the objectives is to be able to simulate UI logic (clicks, text input etc.) through calling methods on the ViewModel, via a unit testing framework and without the need to link in UI libraries.

_You need to see it to fully appreciate this_

So how does this work? Starting with the interface between the ViewModel and View, some key conceptual points to note are as follows:

- Through "bindings", _properties_ of user interface objects will be bound to properties in the view model. This means when one is changed, the other may be automatically updated without having to write any code.
   - Often there is a one-to-one mapping, such as the `Text` propery of a `Label` (type `string`) to a `string` property in the view model.
   - Where there is no one-to-one type mapping, a _Value Converter_ can be inserted between them so that the ViewModel can avoid using UI types. 
- Bindings can be uni-directional or bi-directional. 
    - For uni-directional bindings, you also have control in which direction changes are propagated.
- Content pages and UI Components have a property called the `BindingContext` - this typically references the ViewModel
    - It is often the case that the View instantiates the ViewModel.
- It is also possible to bind UI elements to other UI elements (no view model involved)
- There can be multiple View Models and Models for any given View

For the interface between the ViewModel and Model, some key conceptual points to note are as follows:

- It is often the case that the ViewModel instantiates the Model
- The link between them may be limited to the ViewModel calling synchronous (public) APIs on the Model and handling any returned values.
    - If the returned values result in changea to any bounded properties in the ViewModel, then the UI may be automatically updated.
- The ViewModel can also invoke Asynchronous methods on the Model - the call-back is typically performed using .NET events

Ok, that's a lot of stuff and I suspect it does not yet hold much meaning until you see it in practise. For this, we need a simple example to illustrate all the key points.

# The "Wise Sayings" Application

Like the examples before it, the example that is developed in this section is also trivially simple. This is on purpose. 

> Anticipate the difficult by managing the easy. Lao Tzu

It starts with the familiar MVC architecture, and is evolved incrementally to a testable MVVM archirecture with Model data being pulled from the cloud (Azure Function). Although the resulting MVVM mode is longer and possibly overkill for such a simple application, it is hopefully illustrative. You can then apply it yourself to more real-world applications that scale beyond the trivial.

 
## Part 1 - Start with Familiar Code

We begin with using a coding style that most people new to Xamarin are familiar with. It follows the pattern used in the BMI application. Creating event handlers to update the UI state and model data.

All the code is available on the GitHub site. It is **strongly suggested** you open and examine each example as we progress.

[Part 1 is here](/code/Chapter2/Bindings/HelloBindings-01)

### The Button Event Handler
The view is defined in XAML along with some code-behind to manage the UI logic

Looking at the XAML and code-behind, there are some key points to note.


When the `Button` is clicked, it invokes the event handler `MessageButton_Clicked`
```C#
  private void MessageButton_Clicked(object sender, EventArgs e)
  {
      MessageLabel.Text = Sayings[next];
      next = (next + 1) % Sayings.Count;
  }
```  
The event handler is specified in XAML using the `Clicked` property as follows
```XAML
  <Button x:Name="MessageButton"
          Text="Click Me" 
          HorizontalOptions="Center" 
          VerticalOptions="CenterAndExpand"
          Clicked="MessageButton_Clicked"
          />
```

### The Toggle Switch Event Handler
The binary toggle switch is of type Switch, which is instantiated in XAML. 

```XAML
  <Switch x:Name="ToggleSwitch"  
          HorizontalOptions="Center"
          VerticalOptions="End"
          IsToggled="true"
          Toggled="ToggleSwitch_Toggled"
          />
```

Note the event handler `Toggled` is set to the `ToggleSwitch_Toggled` method in the code behind:

```C#
  private void ToggleSwitch_Toggled(object sender, ToggledEventArgs e)
  {
      MessageLabel.IsVisible = ToggleSwitch.IsToggled;
      MessageButton.IsEnabled = ToggleSwitch.IsToggled;
  }
```
Note the role is simply to enable / disable the Switch and the Message label. This is UI state. 

### The Model Data
The model data can be considered to be the instance variables `Sayings` and `next`. These are not yet properties, and yes, it would be better practise to make them properties..but all in good time.

## Part 2 - Binding Between UI Elements using Code
[Part 2 is here](/code/Chapter2/Bindings/HelloBindings-02). Build and run this to see what it does. Note the strange behaviour once you get back to the first saying. This was added to illustrate a point and will be removed later.

This step is simply to illustrate the mechanism of two-way _binding_. I have purposely used code to set up the bindings as it exposes the APIs that are leveraged by XAML in subsequent sections. This can be very helpful for demystifying what is going on.

To begin with, a three way binding will be set up between the `MessageLabel`, the `ToggleSwitch` and the `MessageButton`.           

We will bind `ToggleSwitch.IsToggled` to both the `MessageLabel.IsVisible` and `MessageButton.IsEnabled`. We will also set this up initially as a two-way binding. _A change in one will result in an automatic change in the others_. 

1. A change to `TooglSwitch.IsToggled` will _automatically_ change both `MesageLabel.IsVisible` and `MessageButton.IsEnabled`
1. A change to `MesageLabel.IsVisible` will _automatically_ change both `ToogleSwitch.IsToggled`. This in turn will activate (1)
1. A change to `MesageButton.IsEnabled` will _automatically_ change both `ToogleSwitch.IsToggled`. This in turn will activate (1)

### Bindings and Relationship Types
The figure below captures the necessary relationships to establish a binding between two properties

![Binding](img/binding.png)

Becoming familiar with the notation is important here as it can otherwise get confusing. Some points to observe:

- The _Target_ is typically a UI object, and must interit from `BindableObject` (which provides the property `BindingContext`)
- The `BindingContext` is a reference to the source - the source can be any type of object (hence is more loosely coupled)
- A binding is setup between specified properties of the source and target objects
    - The target property is of type BindableProperty
    - The source property is loosely specified by name (as a string). 
    
As you can probably infer, the requirements for the target are much more constrained than the source. The target does not know the concrete type of the source or it's bound property (just it's name). This means _the source can by almost any type object_.  It is commonly either a ViewModel or another UI component. Equally the source property is only known by name (type `string`). Behind the scenes, something known as [reflection](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/reflection) will be used to find a property matching this name at run-time and bind to it.

### Binding Properties of the Switch to the Label and Button
The first point to note is that the event handler for the Switch has been removed. We still have the button handler (for now).

```XAML
     <Switch x:Name="ToggleSwitch"  
             HorizontalOptions="Center"
             VerticalOptions="End"
             IsToggled="true"
             />
```     

Next, look at the code-behind where the bindings are set up in code.

```C#
  public MainPage()
  {
      InitializeComponent();

      MessageLabel.BindingContext = ToggleSwitch; //Source
      MessageLabel.SetBinding(Label.IsVisibleProperty, "IsToggled", BindingMode.TwoWay);

      MessageButton.BindingContext = ToggleSwitch;
      MessageButton.SetBinding(Button.IsEnabledProperty, "IsToggled", BindingMode.TwoWay);
  }
```        

The `Switch` will be the source object. The `MessageLabel` and `MessageButton` will be targets. This creates a one-to-many realationship

![OneToMany](img/one-to-many.png)

Note that _a target object can only have one source_, so this topology makes sense. Had we made the Switch the target, we would have a problem. In code we can set the source for each target by specifying the `BindingContext` property. 

```C#
   ...
   MessageLabel.BindingContext = ToggleSwitch;
   ...
   MessageButton.BindingContext = ToggleSwitch;
   ...
```

Here the _targets_ are `MessageLabel` and `MessageButton`. These meet all requirements: both are UI objects, so inherit from `BindableObject` and have properties of type `BinableProperty` (discussed below). The source object (instance of `Switch`) is a reference type, so derived from object, so that's fine. 

Now for the interesting bit, the [SetBinding](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.bindableobjectextensions.setbinding?view=xamarin-forms) API.
```C#
   ...
   MessageLabel.SetBinding(Label.IsVisibleProperty, "IsToggled", BindingMode.TwoWay);
   ...
   MessageButton.SetBinding(Button.IsEnabledProperty, "IsToggled", BindingMode.TwoWay);
   ...
```

You always start with the target, or put another way, you always _set the binding on the target_ (to rememer this, I like to visualise a cross-hair on each of the UI components I want to bind to). 

![BindingTheMessageLabel](img/binding-label-to-switch.png)
_Binding `ToggleSwitch.IsToggled` to `MessageLabel.IsVisible`. Green is related to the source and blue is related to the target_

![BindingTheMessageButton](img/binding-button-to-switch.png)
_Binding `ToggleSwitch.IsToggled` to `MessageButton.IsEnabled`_

Consider each parameter in turn:

- The first parameter is the **target property** of type `BindableProperty`. On inspecton, the code might seem confusing (because it is!). For a start, _static properties_ on the target type. 
    - For any (bindable) property, there will be a static class property of the same name + suffix `Property`.
    - You always pass the static property (never an instance property)
    - Why? Err... I'll get back to you on that ok?
- The second property is the _name_ of the **source property**, specified as a `string`.    
- Finally, there is the direction. This enumerable type is can set to:
    - Default
    - TwoWay (changes are communicated in both direction)
    - OneWay (changes are only communicated from source to taget)
    - OneWayToSource (changes are only communicated from target to source)
    - OneTime (changes only communicated when the `BindingContext` changes)

### Two-Way Binding in Action
Run the application and click the button until it recycles back to the first message. Let's examine what happens in the code at this point:

```C#
     private void MessageButton_Clicked(object sender, EventArgs e)
     {
         MessageLabel.Text = Sayings[next];
         next = (next + 1) % Sayings.Count;

         //Sneaky trick
         if (next == 0)
         {
             MessageLabel.IsVisible = false;
         }
     }
```

The line of interest is this `MessageLabel.IsVisible = false;` Note that `MessageLabel` is a binding _target_. However, the binding between the switch and label was set to `BindingMode.TwoWay`. 

- By changing `MessageLabel.IsVisible`, the two-way binding automatically changes `ToggleSwitch.IsToggled`, which in turn changes `MessageButton.IsEnabled`.
- You might be worried that this could get into a ever-lasting loop. You would be right to be concerned and later we will have to be mindful to avoid such a trap. However, when binding between UI elements, checks are put in place to only update a bound property if it's value is actually going to change.

## Part 3 - This is not MVVM
[Part 3 is here](/code/Chapter2/Bindings/HelloBindings-03). Build and run this to see what it does. Inspect and familiarise yourself with the code fully before proceeding.

Confirm the following:

- A Model class has been added (and will be discussed)
- In the code-behind, and instance of the Model class has been allocated `Model DataModel = new Model();`
- The `BindingContext` for all the bound UI properties are referencing the model object `DataModel`
    - I would add that binding directly to a Model is NOT MVVM, and is only temporary

Again for illustrative purposes, we will skip the ViewModel layer for now and bind directly to a Model.

### One-Way Bindings
In the code-behind the XAML, we see a new property `DataModel` (type `Model`) has been added. For now, we will instantiate the model here

```C#
   Model DataModel = new Model();
```

As much as it pains me to do so, the model object can be used as a binding source.

```C#
   ...
   ToggleSwitch.BindingContext = DataModel;
   MessageButton.BindingContext = DataModel;
   MessageLabel.BindingContext = DataModel;
   ...
```

The UI properties are now bound to properties on the model:

```C#
   ...
   ToggleSwitch.SetBinding(Switch.IsToggledProperty,  "IsTrue", BindingMode.OneWayToSource);
   MessageButton.SetBinding(Button.IsEnabledProperty, "IsTrue", BindingMode.OneWay);
   MessageLabel.SetBinding(Label.IsVisibleProperty,   "IsTrue", BindingMode.OneWay);
   MessageLabel.SetBinding(Label.TextProperty, "CurrentSaying", BindingMode.OneWay);   
   ...
```

Note the following:
- The source is now the data model and the targets are all UI components (as is often the case).
- The target (UI) properties `ToggleSwitch.IsToggled`, `MessageButton.IsEnabled` and `MessageLabel.IsVisible` are all bound to a single  property data model property `DataModel.IsTrue` (type `bool`).
- The binding directions have been set to one-way
    - By default, changes in the source will invoke changes in the targets 
    - The exception is the switch (`OneWayToSource`), where changes in the UI component invoke changes in the model

An additional binding has been added: `MessageLabel.Text` is bound to `DataModel.CurrentSaying`

```C#
   MessageLabel.SetBinding(Label.TextProperty, "CurrentSaying", BindingMode.OneWay);
```

What this means is that any change to the model `CurrentSaying` property will reflect in the `MessageLabel`. The current saying is updated when the button is clicked. We still have the event handler for the button, but it has now been reduced down to a single statement.

```C#
  private void MessageButton_Clicked(object sender, EventArgs e)
  {
      DataModel.NextMessage();
  }
```        

The view behind code is now looking a little thinner. The data model now contains most of the application logic. Let's look at that next.

### Creating Bindable Properties
Recall that the model is now the binding source for all UI components. It exposes two properties used in the bindings,`IsTrue` and `CurrentSaying`. How does this work? Well, you may be pleased to know that it takes very little code to make the binding magic work!

First, look at the class declaration:
```C#
   ...
    class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
   ...
```

Critcally, it implements the interface `INotifyPropertyChanged`. This means there will be a compiler error unless the interface requirements are met. There is only one:

```C#
   public event PropertyChangedEventHandler PropertyChanged;
```  
   
This event is what the binding mechanism uses to listen for changes. It is the developers responsibility to signal an event when a change occues, as shown in the following extract:

```C#
     string _currentSaying = "Welcome to Xamarin Forms!";
     public string CurrentSaying
     {
         get => _currentSaying;
         set
         {
             if (!value.Equals(_currentSaying))
             {
                 _currentSaying = value;
                 if (PropertyChanged != null)
                 {
                     PropertyChanged(this, new PropertyChangedEventArgs("CurrentSaying"));
                 }
             }
         }
     }
```

Here we see the setter and getter for the variable backed property `CurrentSaying`. Look closely at the setter. If (and only if) a new value is set, then the following is executed:

```C#
   PropertyChanged(this, new PropertyChangedEventArgs("CurrentSaying"));
```

If you recall the button event handler, it called the following method on the model: 

```C#
     public void NextMessage()
     {
         CurrentSaying = Sayings[next];
         next = (next+ 1) % Sayings.Count;
     }
```

What have we achieved?

> By simply setting `CurrentSaying`, the UI automatically updates, but without reference to the UI anywhere in the model code.

The `IsTrue` property is even more impressive. Three separate UI properies are bound to this property (here they are again)

```C#
   ToggleSwitch.SetBinding(Switch.IsToggledProperty,  "IsTrue", BindingMode.OneWayToSource);
   MessageButton.SetBinding(Button.IsEnabledProperty, "IsTrue", BindingMode.OneWay);
   MessageLabel.SetBinding(Label.IsVisibleProperty,   "IsTrue", BindingMode.OneWay);
```   

Again, key is the line in the setter that reads:

```C#
   PropertyChanged(this, new PropertyChangedEventArgs("IsTrue"));
```

There is nothing in the model that either sets or reads `IsTrue` (which strongly suggests this is not it's natural home - back to MVVM again!). Everything is performed using bindings. The toggle switch sets it, while the the message label and button observe it. The `IsTrue` property is simply there as a go-between for some UI state.

** TASK ** Set a break point in the setter for `IsTrue`, debug the code, and click the toggle switch.

## Part 4 - Type Conversion
[Part 4 is here](/code/Chapter2/Bindings/HelloBindings-04). Build and run this to see what it does. Inspect and familiarise yourself with the code fully before proceeding.

Firstly, a new bindable property `SayingNumber` of type `int` has been addded to the model. Take a look at the model class to see the code (it's very much like the other properties).

What is most different are two new bindings in the code-behind the XAML. The first is to bind the `Text` property (string) of the button to the `SayingNumber` (int). What probably stands out here is that the types are not the same. This is such a common scenario (many properties are strings), an additional conversion string can be passed as an additional parameter. For this example, no more work is needed!

```C#
   MessageButton.SetBinding(Button.TextProperty, "SayingNumber", BindingMode.OneWay, null, "Saying {0:d}");
```

What is not shown are a number of hidden parameters with default values. If we were to write the complete method, it would read:

```C#
   MessageButton.SetBinding(targetProperty: Button.TextColorProperty, path: "SayingNumber", mode: BindingMode.OneTime, converter: null, stringFormat: "Saying {0:d}");              )
```

The second binding is more complicated as it uses the `converter` parameter (default `null`). The source property `SayingNumber` (type `int`) is bound to the target label `TextColor` (type `Color`). A format string is not going to help in this case, so instead we create a simple object of type `ColorConverter` to convert between `int` to `Color`

```C#
   MessageLabel.SetBinding(Label.TextColorProperty, "SayingNumber", BindingMode.OneWay, new ColorConverter());
```

### The `TypeConverter` 
The code for the `ColorConverter` class is shown below. 

```C#
    class ColorConverter : IValueConverter
    {
        //Implement this method to convert value to targetType by using parameter and culture.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int v = (int)value;
            Color c;
            switch (v)
            {
                case 0:
                    c = Color.Red;
                    break;
                case 1:
                    c = Color.Gold;
                    break;
                case 2:
                    c = Color.Green;
                    break;
                default:
                    c = Color.Black;
                    break;
            }
            return c;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
```    

Note that it implements the `IValueConverter` interface, which requires the following two methods:

```C#
public object Convert(object value, Type targetType, object parameter, CultureInfo culture); // int to Color
public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture); Color to int
```

We only need the `Convert` method, which converts the source data (`int`) to the target (`Color`). The first parameter is the source data, which is cast immediately to type `int`. The target data is returned from the method. 

You can read more about value converters in the [Microsoft Documentation](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/data-binding/converters)

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

## Part 6 - Bindings in XAML
[Part 6 is here](/code/Chapter2/Bindings/HelloBindings-06). Build and run this to see what it does. Inspect and familiarise yourself with the code fully before proceeding. 

First look at the Code-Behind. Rather than remove code, I have commented out code so you can still see the APIs. This is helpful for following the changes in the XAML.

The new XAML file is shown below:
```XAML
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:local="clr-namespace:HelloBindings;assembly=HelloBindings"
             mc:Ignorable="d"
             x:Class="HelloBindings.MainPage">
            
    <ContentPage.BindingContext>
        <local:MainPageViewModel/>
    </ContentPage.BindingContext>
    
    <ContentPage.Resources>
        <ResourceDictionary>
            <local:ColorConverter x:Key="ColorConv"/>
        </ResourceDictionary>
    </ContentPage.Resources>
    
    <StackLayout Padding="0,40,0,40">
        
        <Label x:Name="MessageLabel" 
               FontSize="Large"
               Text="{Binding Path=CurrentSaying}" 
               IsVisible="{Binding Path=UIVisible}"
               TextColor="{Binding Path=SayingNumber, Converter={StaticResource ColorConv}}"
               HorizontalOptions="Center"
               VerticalOptions="Start" 
               />

        <Button x:Name="MessageButton"
                Text='{Binding Path=SayingNumber, StringFormat="Saying {0:d}"}'
                Command="{Binding Path=ButtonCommand}"
                HorizontalOptions="Center" 
                VerticalOptions="CenterAndExpand"
                />

        <Switch x:Name="ToggleSwitch"  
                HorizontalOptions="Center"
                VerticalOptions="End"
                IsToggled="{Binding Path=UIVisible, Mode=TwoWay}"
                />

    </StackLayout>

</ContentPage>

```

### Instantiating the ViewModel in XAML
The first task is to instantiate a view model to bind to. We can do this using an element property

```XAML
    <ContentPage.BindingContext>
        <local:MainPageViewModel/>
    </ContentPage.BindingContext>
```

Remember - All elements have a namespace prefix. Where one is not specified, it default to the namepace for Xamarin.Forms. Therefore class names such as `Button` and `Label` can be kept concise.

Our view model class `MainPageViewModel` is not part of Forms, XAML or any other framework. It is out own hand-rolled class that is part of the cross platform project with namespace HelloBindings, and in the _assembly_ HelloBindings (you can confirm the namespace from the properties of the cross platform project).

You can create a namespace specially for this purpose as follows:
```XAML
 xmlns:local="clr-namespace:HelloBindings;assembly=HelloBindings"
```
It is called `local` by convention only. You could name it anything you like. 

```XAML
    <ContentPage.BindingContext>
        <local:MainPageViewModel/>
    </ContentPage.BindingContext>
```
is equivalent to writing the following in the `MainPage` class (a subclass of `ContentPage`) 
```C#
   BindingContext = new MainPageViewModel();
```

### Instantiating the ColorConverter class
A similar technique is used to instantiate the `ColorConverter` class needed to convert an `int` to `Color`. We could have done this in the code behind, but luckily we have a tidier way: the `ResourceDictionary`

```XAML
    <ContentPage.Resources>
        <ResourceDictionary>
            <local:ColorConverter x:Key="ColorConv"/>
        </ResourceDictionary>
    </ContentPage.Resources>
```

A `ContentPage` has dictionary of key-value pairs for holding many types of objects referenced in the XAML code. Here the key is "ColorConv" and the associated value is an instance of `ColorConv` (from the cross platform project). We can now obtain a refernce to this object using the key (as we will see below).

### Setting the Bindings
Firt the label used to display the current saying:

```XAML
     <Label x:Name="MessageLabel" 
            FontSize="Large"
            Text="{Binding Path=CurrentSaying}" 
            IsVisible="{Binding Path=UIVisible}"
            TextColor="{Binding Path=SayingNumber, Converter={StaticResource ColorConv}}"
            HorizontalOptions="Center"
            VerticalOptions="Start" 
            />
```
The Text property is bound to the `CurrentSaying` property of the source using the special {Binding..} syntax. The `IsVisible` property is similar.
```XAML
Text="{Binding Path=CurrentSaying}"
IsVisible="{Binding Path=UIVisible}"
```
**TASK** I suggest you refer back to the code equivalent - look at the property names for `SetBinding`

The text colour is a little more involved:
```XAML
TextColor="{Binding Path=SayingNumber, Converter={StaticResource ColorConv}}"
```
The `Converter` property (from the `SetBinding` API) must refer to an instance of ColorConverter. We use the special syntax {StaticResource ..} and a key name to refer to objects in the resource dictionary. 

_Do not forget the comma when providing multiple parameters_ (this one had be going for some time!)

> **Note** - do not confuse {Static ...} and {StaticResource ...}. They look similar, but they are distinctly different.

Next the Button. Again, constrating with the code API, this has few surprises. Note again how for the `Text` property, two parameters are passed. To accomodate the double-quotes, the outer string uses single-quotes

```XAML
     <Button x:Name="MessageButton"
             Text='{Binding Path=SayingNumber, StringFormat="Saying {0:d}"}'
             Command="{Binding Path=ButtonCommand}"
             HorizontalOptions="Center" 
             VerticalOptions="CenterAndExpand"
             />
```                

Finally, the Switch

```XAML

     <Switch x:Name="ToggleSwitch"  
             HorizontalOptions="Center"
             VerticalOptions="End"
             IsToggled="{Binding Path=UIVisible, Mode=TwoWay}"
             />             
```            

I've specified two-way so the switch is initialised in the correct position. 

**TASK**
Take some time to study the XAML in this project, and compare it to the code equivalent from the previous version. Trying experimenting to see how intellisense helps you complete XAML bindings.

## Part 7 - Hooking up to Azure
[Part 7 is here](/code/Chapter2/Bindings/HelloBindings-07). Inspect and familiarise yourself with the code fully before proceeding. There is some preliminary work you need to do before you can run it.

In this last section, I've done something that might seem a bit ambitious: I've stored the list of sayings in the cloud (jargon for someone elses computer), and I'm going to use Azure Functions to retrieve them. This would allow me to add more sayings without app updates. Also, as long as we moderate usage, it's free, and we like free.

[ TO BE CONTINUED ]

----
[Contents](/docs/README.md)
