[Prev - Basic Navigation Part 2](basic_navigation_2.md)

---

## Basic Navigation 3 - Using Binding
Open the solution in the folder [BasicNavigation-3-Binding](/code/Chapter3/NavigationControllers/1-View_Based/BasicNavigation-3-Binding)

Run the code, and try editing the data using both the `Slider` control (for the year) and the `Entry` control (for the name).

This time you will find the data being updated throughout the application. The edits are currently immediate, with no option to undo or cancel.

### `FirstPage` - binding to the code-behind
There are some subtle but important changes to the XAML

For the `ContentPage` element, the attribute `x:Name="ThisPage"` is defined. Using this, we can refer to the page itself from within the XAML as follows:

```XML
<Label BindingContext="{x:Reference ThisPage}"
        Text="{Binding Year, StringFormat='(c) {0:F0}'}"
        x:Name="YearLabel"
        Grid.Row="1" Grid.Column="0"
        />
<!-- Again, binding to properties in the code behind -->
<Label BindingContext="{x:Reference ThisPage}"
        Text="{Binding Name}"
        Grid.Row="1" Grid.Column="1"
        />
```

> Remember that the `FirstPage` class definition is split across two files: the XAML file and a code-behind. The reference `x:ThisPage` includes both.

In the code behind we see that the properties `Year` and `Name` both call `OnPropertyChanged()` when they are updated. This way, any changes to these properties will be immediately reflected in the UI.

```C#
public string Name
{
    get => _name;
    set
    {
        if (_name == value) return;

        _name = value;
        OnPropertyChanged();    //Update binding layer
    }
}

public int Year
{
    get => _year;
    set
    {
        if (_year == value) return;

        _year = value;
        OnPropertyChanged();    //Update binding layer
    }
}
```

> Note that `ContentPage` conveniently inherits an implementation of `OnPropertyChanged()`.

The way data is now passed forwards and backwards is based on the following observation:

> If the `FirstPage` UI elements can bind to the `Year` and `Name` properties in the code-behind, then so can the UI elements in the next page. 

Look in the button event handler for `FirstPage`, and we note that the `BindingContext` property of the next page (type `YearEditPage`) is set to `FirstPage`. 

```C#
private async void EditButton_Clicked(object sender, EventArgs e)
{
    //No need to pass data forward
    var nextPage = new YearEditPage();
    //Set this object as the binding context
    nextPage.BindingContext = this;
    //Navigate
    await Navigation.PushAsync(nextPage, true);
}
```

This makes the `YearEditPage` fairly simple.

### `YearEditPage` - binding to data from the previous page
Let's first look at the XAML

```XML
<Label Text="{Binding Name}" x:Name="NameLabel"/>

<Label BindingContext="{x:Reference YearSlider}"
        Text="{Binding Value, StringFormat='The year is {0:F0}'}" />
<Slider 
        x:Name="YearSlider"
        Maximum="2100"
        Minimum="1900"
        Value="{Binding Year}"
        MinimumTrackColor="Blue"
        MaximumTrackColor="Red"
/>

<Button Text="Edit Name"
        x:Name="EditButton"/>

```

* The top text label is bound to the `Name` property
* The slider `Value` is bound to the `Year` property
* The second text label is bound to the slider value

In the code behind, all the properties have been removed (not needed) leaving just the event handler:

```C#
private async void EditButton_Clicked(object sender, EventArgs e)
{
    var nextPage = new NameEditPage();
    nextPage.BindingContext = this.BindingContext;
    await Navigation.PushAsync(nextPage, true);
}
```

This again sets the `BindingContext` of the next view to be the same as it's own (the previous view `FirstPage`).

### `NameEditPage` - binding to the `FirstPage`
The `NameEditPage` is similar to the previous page, only that it only binds to the `Name` property in the XAML

```XML
<Entry Placeholder="Name Cannot be Blank"
        x:Name="NameEntry"
        VerticalOptions="StartAndExpand"
        HorizontalTextAlignment="Center"
        ClearButtonVisibility="WhileEditing"
        Text="{Binding Name}" />

<!-- For View to View binding, use x:Reference to reference another view object -->
<Label BindingContext="{x:Reference NameEntry}"
        Text="{Binding Text}"
        VerticalOptions="StartAndExpand"
        HorizontalTextAlignment="Center" />
```

Similarly the code-behind has no properties of it's own. It simply contains the code the save button event handler

```C#
public NameEditPage()
{
    InitializeComponent();
    SaveButton.Clicked += SaveButton_Clicked;
}

// ************************** Event Handlers ***************************
private async void SaveButton_Clicked(object sender, EventArgs e)
{
    Console.WriteLine("Save Clicked");
    await Navigation.PopToRootAsync();
}
```

There are some observations about this approach:

* The edits in one page are bound to the data in another. This loses the idea of a page being self-contained / stand-alone
* Changes to the UI elements have an immediate effect on the original model data, with no opportunity to cancel changes once an edit is made. 

To address the above is going to require some additional work. The next couple sections go some way to resolving these, but as we shall see, an MVVM pattern may be preferred.

--- 

[Next - Basic Navigation Part 4](basic_navigation_4.md)