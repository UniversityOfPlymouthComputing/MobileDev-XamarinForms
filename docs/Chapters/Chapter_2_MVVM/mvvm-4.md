[Contents](README.md)

----


[Prev](mvvm-3.md)


## Part 4 - Type Conversion
[Part 4 is here](/code/Chapter2/Bindings/HelloBindings-04). Build and run this to see what it does. Inspect and familiarize yourself with the code fully before proceeding.

Firstly, a new bindable property `SayingNumber` of type `int` has been addded to the model. Take a look at the model class to see the code (it's very much like the other properties).

What is most different are two new bindings in the code-behind the XAML. The first is to bind the `Text` property (string) of the button to the `SayingNumber` (int). What probably stands out here is that the types are not the same. This is such a common scenario (many properties are strings), an additional conversion string can be passed as an additional parameter. For this example, no more work is needed!

```C#
   MessageButton.SetBinding(Button.TextProperty, "SayingNumber", BindingMode.OneWay, null, "Saying {0:d}");
```

What is not shown are a number of hidden parameters with default values. 

> **TASK** - retype the line above and note all the parameters revealed by intellisense. The parameter labels will help you explore the full API.

If we were to write the complete method, it would read:

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

You can read more about value converters in the [Microsoft Documentation](https://docs.microsoft.com/xamarin/xamarin-forms/app-fundamentals/data-binding/converters)


## Self-Study Task - Binding in Code
Create yourself a new project. Add a Slider and a label to the page. Set the maximum value of the slider to 100. Can you use bindings to display the slider value using the label?

Although you can setup direct bindings between UI elements, it is suggested you create an integer property as a go-between.

Remember:

* Create a Model class that has a public integer property and implements the interface `INotifyPropertyChanged`
* Instantiate the model class and hook up the bindings from the code behind.
* You don't strictly need a value converter as the binding layer will invoke ToString() automatically
* Try using the `stringFormat` property to only display to one decimal place (you will need to research how to create format strings)

Make reference to the notes up to this point to assist. I would not expect you to memorize everything!

 [Next](mvvm-5.md)

----

[Contents](README.md)
