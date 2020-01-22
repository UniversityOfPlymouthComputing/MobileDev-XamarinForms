[back](listview-tapped.md)

---

# ListView - Adding Data

The example for this section is found in the [/code/Chapter4/ListView/D_SimpleListView_Add](/code/Chapter4/ListView/D_SimpleListView_Add) folder.

> * Build and run the code
> * Click on a row and not the changes in the `ListView`
> * Familiarize yourself with the code

In a previous section, it was observed that changes to our `Planets` collection (type `List<string>`) did not result in an updated `ListView`. In this example, this problem is addressed by simply using a different collection type: `ObservableCollection<string>` which implements `INotifyCollectionChanged`

## MainPageViewModel.cs
Note the `Planets` property has been updated in the ViewModel to use `ObservableCollection<string>`.

```C#
    ...

    private ObservableCollection<string> _planets;
    
    ...
    
    public ObservableCollection<string> Planets
    {
        get => _planets;
        set
        {
            if (_planets == value) return;
            _planets = value;
            OnPropertyChanged();
        }
    }

    ...
```

Performance:

* If you never plan to add or remove data from a collection, you are probably advised to use `List<>` for maximum performance.
* If your collection is mutable, then you need to use one that implements `INotifyCollectionChanged`

If you we look at the tapped event handler in the ViewModel, we see how simple this is:

```C#
        public void UserTappedList(int row, string planetString)
        {
            SelectedRow = row;
            TapCount += 1;
            string item = $"Row {row} tapped";
            Planets.Add(item);
            _viewHelper.ScrollToObject(item);
        }
```

By simply adding an item to the `Planets` collection, the ListView is automatically updated.

## What's going on behind the scenes?
Let's remind ourselves about what is happening here, starting with the `ContentPage` and it's child `ListView`.

```XML
    <ListView ItemsSource="{Binding Planets}"
                x:Name="PlanetListView"
                SelectedItem="{Binding SelectedString}"
                SelectionMode="{Binding SelectionModeOn, Converter={StaticResource bool2mode}, Mode=TwoWay }"
                HorizontalOptions="Center"
                VerticalOptions="CenterAndExpand"/>
```                  

Key Points:

* The `BindingContext` of the `Page` the ViewModel.
* The `ListView` is a child of the `ContentPage`, so it inherits the Binding Context.
* The `ItemSource` of the `ListView` is of type `System.Collections.IEnumerable`. This is bound to the property `Planets` in the ViewModel

When the `ListView` is displayed, it will iterate over it's `ItemSource` for the rows that is wishes to display (or prepare off-screen).

By default, it will render a default "cell" with a single text label property. For each cell, it will call `ToString()` on each element of `ItemSource` to obtain the text for the cell.

As will be seen, there are other cell types we could have used, but for that, we need to do some extra work.

> EXPERIMENT
>
> Modify the solution to use a list of integers instead of strings.
>
> What type is `SelectedItem`?

---

[Next - Cells and Data Templates](listview-templates.md)

