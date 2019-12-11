[Prev - Basic Navigation Part 3](basic_navigation_3.md)

---

## Basic Navigation 4 - Using a Singleton Model
Open the solution in the folder [BasicNavigation-4-Binding](/code/Chapter3/NavigationControllers/1-View_Based/BasicNavigation-4-Singleton)

Run the code, and try editing the data using both the `Slider` control (for the year) and the `Entry` control (for the name).

In the previous section, all model data was contained within view objects (the first Page). This might be fine for very simple projects, but it is suggested it will not scale well.

A popular alternative that deserves a mention is the use of a singleton model object.

> A singleton object is one where only one instance can exist and is typically global in scope.

The class `SingletonModel` is shown below:

```C#
public sealed class SingletonModel : INotifyPropertyChanged
{
    private static SingletonModel _model;
    private string _name = "Anon";
    private int _year = 2021;

    public event PropertyChangedEventHandler PropertyChanged;

    public string Name {
        get => _name;
        set
        {
            if (_name == value) return;
            _name = value;
            OnPropertyChanged();
        }
    }
    public int Year {
        get => _year;
        set
        {
            if (_year == value) return;
            _year = value;
            OnPropertyChanged();
        }
    }

    public static SingletonModel SharedInstance
    {
        get
        {
            if (_model == null)
            {
                _model = new SingletonModel();
            }
            return _model;
        }
    }

    private void OnPropertyChanged([CallerMemberName]string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

Some key points to note:

* This class has a static property `public static SingletonModel SharedInstance`.     * This is used to obtain a reference to the singleton object via `SingletonModel.SharedInstance`
   * This method also instantiates the singleton instance if it does not already exist.
   * It can be accessed from anywhere in the project, making it very simple to read and update

* The constructor is `private` - this prevents additional instances of this object being created elsewhere.

* The class is `sealed`, so cannot be subclassed

* The properties `Name` and `Year` support binding.

Now let's look at how we bind our UI to the singleton object.

## FirstPage
There are some notable changes made to all the XAML files. We need to be able to reference the singleton object in the project.

```XML

...
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:local="clr-namespace:BasicNavigation"
             BindingContext="{x:Static local:SingletonModel.SharedInstance}"
             mc:Ignorable="d"
             Title="First Page"
             x:Class="BasicNavigation.FirstPage">
...
```

First of all, a new namespace is declared: `xmlns:local="clr-namespace:BasicNavigation"`

This is essentially a namespace for any class in current assembly (default) and within in the `BasicNavigation` namespace. Using this, we can set the `BindingContext` for the page:

```XML
...
BindingContext="{x:Static local:SingletonModel.SharedInstance}"
...
```
We use `x:Static` to reference the static property `SingletonModel.SharedInstance`

Having set the `BindingContext` for the page to our singleton object, it is simple to bind UI elements to the respective properties. For example, to display the `Name` property:

```XML
<Label 
        Text="{Binding Year, StringFormat='(c) {0:F0}'}"
        Grid.Row="1" Grid.Column="0"
        />
```        

Much the same is done for the `YearEditPage` and `NameEditPage`. 

> Take a look at both of these and you will notice the same idea repeating.

What is still lacking is the ability to edit the name and cancel and changes. This is addressed in the next example.

--- 

[Next - Basic Navigation Part 5](basic_navigation_5.md)