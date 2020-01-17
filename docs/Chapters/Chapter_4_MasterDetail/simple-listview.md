[Back](listview.md)

---

# Simple ListView Example
Let's start with an example and work through the key points. This is found in the [/code/Chapter4/ListView/A_SimpleListView](/code/Chapter4/ListView/A_SimpleListView) folder.

> * Build and run the code
> * Click on a row
> * Familiarize yourself with the code, especially `MainPage.xaml`, `MainPage.xaml.cs` and `MainPageViewModel.cs`

Note that we are now sticking to a consistent pattern by binding the UI to a ViewModel. In this case, `MainPageViewModel`.

## MainPage.xaml
Consider the extract below:

```C#
    <StackLayout>    
        <Label Text="{Binding TitleString}" />

        <ListView ItemsSource="{Binding Planets}"
                  x:Name="PlanetListView"
                  HorizontalOptions="Center"
                  VerticalOptions="CenterAndExpand"/>
    </StackLayout>  
```

An instance of `ListView` is added to the view hierarchy. The critical attribute is `ItemSource` which is bound to a *collection* of data (such as a `List<>`) and not a single value as we've seen previously.

> It is important to note that the `ListView` does not encapsulate the data being displayed. Instead, you provide a reference to the data using the `ItemSource` property, of type `IEnumerable`

The default behavior is to display a single string in each row. The `ListView` will read individual data values (as needed) from `ItemSource` and by default invoke `ToString()` on each data element.

The `ItemSource` is bound to a property named `Planets`. This is a property of the `BindingContext`, which in this example, is set in the code-behind.

### MainPage.xaml.cs
The constructor contains the code to set up the binding context of `MainPage`

```C#
    ...

    vm = new MainPageViewModel(this);
    BindingContext = vm;

    ...
```

As in previous examples, the `BindingContext` is reference to a ViewModel object.

> Remember that the child elements of the page (including `ListView`) will inherit the page `BindingContext` (although you can override it of course).

Let's now drill down and look inside the ViewModel.

### MainPageViewModel.cs
To keep this example concise, there is no separate Model object. As we saw in the previous section, the  `ItemSource` property of `ListView` is bound to the `Planets` property of the ViewModel, which is shown below:

```C#
    // ItemSource binds to an IEnumerable
    public List<string> Planets
    {
        get => _planets;
        set
        {
            if (_planets == value) return;
            _planets = value;
            OnPropertyChanged();
        }
    }
```

Note it is a **collection type** that implements the `IEnumerable` interface. In this case the type `List<string>` was used, which is one of the C#.NET collection types. 

In the constructor of the ViewModel, we see this list being initialized:

```C#
    Planets = new List<string>
    {
        "Mercury",
        "Venus",
        "Jupiter",
        "Earth",
        "Mars",
        "Saturn",
        "Pluto"
    };
```

> When the `ListView` is loaded, it will read data from `ItemSource` for display purposes. **It does not make a copy of the data**, but always references the source data. This way, _there is only ever one copy of the data_ (which is good from a data consistency perspective).
>
If the data should change **and** `ListView` is notified of this change (see later), the `ListView` is capable of updating itself. 

## Updating the ListView
However, as we will see, `List<>` is sometimes not the best choice.

### EXPERIMENT
Return to the code-behind, and note the following code in the constructor:

```C#
//5 second timer
tmr = new Timer(5000);
tmr.Elapsed += Tmr_Elapsed;
tmr.AutoReset = true;
tmr.Enabled = true;
```

This invokes the method `Tmr_Elapsed` every 5 seconds. Now consider the code in `Tmr_Elapsed`

```C#
private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
{
_tickCount++;

//VER1 - update content of the List
Planets.Add($"Timer Fired {_tickCount} times");
Planets[0] = "First Item Updated";

//VER2 - replace the entire list (expensive)
/*
//Make a copy
List<string> newList = new List<string>(Planets);

//Add new row
newList.Add($"Timer Fired {_tickCount} times");

//Update the complete List with a new one
Planets = newList;
*/

TitleString = $"Timer fired {_tickCount} times";
}
```

> You can see that we are actually appending the list every 5 seconds. This change is reflected in the `TitleString` property, but never in the ListView.

To quote the documentation:

>The default visualization also presents a problem with changes. ListView can't detect changes to the underlying data even if you're using property notification changes. ListView calls ToString only once when it first displays the row. It doesn't realize it needs to call ToString every time the underlying data changes.
>[Reference](https://docs.microsoft.com/learn/modules/display-collections-in-xamarin-forms-apps-with-listview/5-customize-listview-rows)

Even if you add the following line, it has no effect:
```C#
    OnPropertyChanged(nameof(Planets));
```

* Now comment out the code for VER1 and uncomment the code for VER2.
* Run again and wait a few seconds to see any updates.

You will notice that the table now updates, but only replacing the complete `List<string>` with a new one. This is not very efficient!

One issue is that `List<>` does not notify the binding layer when its content changes. This can be overcome by using a different collection object, and we will address this later.

---

[Next - Tapped Event](listview-tapped.md)
