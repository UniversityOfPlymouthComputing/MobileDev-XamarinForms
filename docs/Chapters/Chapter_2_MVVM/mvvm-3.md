[Contents](/docs/README.md)

----

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

----
[Contents](/docs/README.md)
