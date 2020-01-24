[back](listview-tapped.md)

---

# ListView - Item Selection

The example for this section is found in the [/code/Chapter4/ListView/C_SimpleListView_Selection](/code/Chapter4/ListView/C_SimpleListView_Selection) folder.

> * Build and run the code
> * Click on a row
> * Click on the same row twice
> * Toggle the switch
> * Click on the same row twice
> * Familiarize yourself with the code

Selection is different from tapped. When you touch on a row, the tapped event is always invoked. When selection mode is used, the selected event (or command) is invoked only when the selected row changes.

## MainPage.xaml.cs
There are some interesting changes in the XAML file in this example.

### Toggle Switch
First there is addition of a toggle switch, bound to the boolean property `SelectionModeOn` in the ViewModel:

```XML
<Switch x:Name="SelectSwitch" IsToggled="{Binding SelectionModeOn, Mode=TwoWay}" HorizontalOptions="Start" VerticalOptions="Center"/>
```

### ListView Changes

The `ListView` has also been modified:

```XML
    <ListView ItemsSource="{Binding Planets}"
                x:Name="PlanetListView"
                SelectedItem="{Binding SelectedString}"
                SelectionMode="{Binding SelectionModeOn, Converter={StaticResource bool2mode}, Mode=TwoWay }"
                HorizontalOptions="Center"
                VerticalOptions="CenterAndExpand"/>
```                  

The `SelectedItem` (the row highlighted) is bound to the property `SelectedString`. 

> What is bound? 
>
> The _data behind the selected row_ (type `string` in this case), and not a visual object such the table view cell. The is data that was read from the `Planets` collection. This is very convenient.

To use selection, it needs to be enabled. This is controlled by the switch, via the ViewModel.

* The `SelectionMode` property is an enumerated type  `ListViewSelectionMode` that can be set to either `ListViewSelectionMode.Single` or `ListViewSelectionMode.None`. 
* The switch value `IsToggled` is type `bool`

We could have created a property on the ViewModel of type `ListViewSelectionMode`, but it's preferable to expose more general types to the binding layer, in this case `bool`.

We clearly cannot bind `SelectionMode` directly to a property of type `bool` as the types do not match. Instead, we insert a converter between the two as follows:

```XML
SelectionMode="{Binding SelectionModeOn, Converter={StaticResource bool2mode}, Mode=TwoWay }"
```

To do this, we create a value converter.

### BoolToSelectionModeConverter
The converter is a simple class which implements the interface `IValueConverter`. The source is shown below:

```C#
    public class BoolToSelectionModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? ListViewSelectionMode.Single : ListViewSelectionMode.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((ListViewSelectionMode)value == ListViewSelectionMode.Single);
        }
    }
```

* The `Convert` method converts `ListViewSelectionMode` (target property type) to `bool` (source property type)

* `ConvertBack` does the converse.

Back in the XAML, `BoolToSelectionModeConverter` is instantiated in the resource dictionary for the page:

```XML
        ...
        xmlns:local="clr-namespace:SimpleListView"
        ...

    <ContentPage.Resources>
        ...
        <ResourceDictionary>
            <local:BoolToSelectionModeConverter x:Key="bool2mode"/>
        </ResourceDictionary>
        ...
```        

> If you want to see how a binding converter is specified in C#, the documentation for the `Binding` class has a nice example:
>
> [Microsoft Documentation - Binding Constructors](https://docs.microsoft.com/dotnet/api/xamarin.forms.binding.-ctor?view=xamarin-forms#Xamarin_Forms_Binding__ctor_System_String_Xamarin_Forms_BindingMode_Xamarin_Forms_IValueConverter_System_Object_System_String_System_Object_)

## MainPage.xaml.cs
The code behind contains the event handler for selection.
```C#
    private async void PlanetListView_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
    {
        //If nothing is selected, there is nothing to do
        if (e.SelectedItem == null) return;

        //Extract data
        string itemString = (string)e.SelectedItem;
        int selectedRow = e.SelectedItemIndex;

        //Update ViewModel
        await vm.ItemSelectionChangedAsync(row: selectedRow, planetString: itemString);
    }
```

This is similar in the way the tapped event was handled. The selected data is passed by parameter to the ViewModel as we did with the tapped event.

The event handler is specified in the page constructor

```C#
    ...
    PlanetListView.ItemSelected += PlanetListView_ItemSelectedAsync;
    ...
```

> However, unless you specifically want an event handler,  the data behind the selected row can be directly bound to a property on the view model (see `SelectedString`).
>
> **A word of caution** - `SelectedItem` defaults to `null` until the user performs a selection.


## MainPageViewModel
The view model has been updated to handle the selected item event.

```C#
    public async Task ItemSelectionChangedAsync(int row, string planetString)
    {
        SelectedRow = row;
        SelectionCount += 1;
        await _viewHelper.TextPopup("Selection Changed: ", $"{planetString} on row {row}");
    }
```

A property `SelectedString` was also added to demonstrate binding to `SelectedItem`. Note it is type `string`, the type of the data in the item source collection.:

```C#
    public string SelectedString
    {
        get => _selectedString;
        set
        {
            if (_selectedString == value) return;

            _selectedString = value;

            //Update UI
            TitleString = _selectedString ?? "Nothing Selected";
        }
    }
```

Note also how *`null` coalescing* is used to provide a default string when nothing has been selected:

```C#
TitleString = _selectedString ?? "Nothing Selected";
```


## Summary
In this section we introduced the notion of row selection. This optional feature of `ListView` enabled the user to select and highlight a single row in the list. When (and only when) the user changes the selection does an event occur.

A benefit of selection is that an event is only generated when there is a change. You can also bind the `SelectedItem` property to the ViewModel and not even have an event handler.

A downside of selection is that the bindable `SelectedItem` is `null`, so we need to be careful.

It is interesting that there is no `Command` property related to item selection for `ListView`. There is a command for pull-to-refresh (something you can experiment with).

Next, we turn our attention back to the model data and consider again what happens when it changes.

---

[Next - Adding Data](listview-add.md)

