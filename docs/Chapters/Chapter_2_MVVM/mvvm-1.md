[Contents](README.md)

----
 
[Prev](Introduction.md)

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
The model data can be considered to be the instance variables `Sayings` and `next`. These are not yet properties, and yes, it would be better practice to make them properties..but all in good time.



 [Next](mvvm-2.md)


----

[Contents](README.md)
