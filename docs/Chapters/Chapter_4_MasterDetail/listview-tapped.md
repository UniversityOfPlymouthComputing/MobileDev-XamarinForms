[Back](simple-listview.md)

---

# ListView Tapped Event
Let's start with an example and work through the key points. This is found in the [/code/Chapter4/ListView/B_SimpleListView_TappedEvent](/code/Chapter4/ListView/B_SimpleListView_TappedEvent) folder.

> * Build and run the code
> * Click on a row
> * Familiarize yourself with the code, especially `MainPage.xaml`, `MainPage.xaml.cs` and `MainPageViewModel.cs`

This example adds a tap event handler to the `ListView`. The selected string is then displayed in an alert box.

Key questions to consider are:

* How do we know which row was tapped?
* How do we find the data for a given row?
* How does this work with MVVM

There are no changes in the XAML file, so we will jump straight to the code-behind.

## MainPage.xaml.cs
First consider the constructor. Two lines have been added:

```C#
    ...
    PlanetListView.SelectionMode = ListViewSelectionMode.None;        

    PlanetListView.ItemTapped += PlanetListView_ItemTapped;     
    ...
```

We do not strictly need the first statement, except to emphasize that there is a difference between _tapped_ and _selected_.

The second statement is similar to that of any event handler, such as the `Clicked` event on a button. The event handler is shown below:

```C#
    private async void PlanetListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        string itemString = (string)e.Item;
        int selectedRow = e.ItemIndex;
        await vm.UserTappedListAsync(row: selectedRow, planetString: itemString);
    }
```

Here we see the answer to 2 of the 3 questions posted above. 

* The row number is simply `e.ItemIndex`
* We could pass the row to the view model and allow it to look up the data, but conveniently the data element for the row is also made available as `e.Item`

**Note:** The data is know to be type `string`, so a direct cast has been performed on `e.Item`. There are safer ways to perform such operations.

