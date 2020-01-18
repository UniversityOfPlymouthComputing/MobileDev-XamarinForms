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

**Notes:** 

The data is known to be type `string`, so a direct cast has been performed on `e.Item` in the knowledge that it will not produce a run-time crash.

The event handler is part of the View, so the data is passed across to the ViewModel `vm.UserTappedListAsync(row: selectedRow, planetString: itemString);`

This begins to answer the third question above. Before we look at the ViewModel class, note also a change in the `MainPage` declaration:

```C#
public partial class MainPage : ContentPage, IMainPageHelper
```

This page implements the custom interface `IMainPageHelper`. The only method specifically in this interface is `Task TextPopup(string title, string message)`, implemented as follows:

```C#
    public async Task TextPopup(string title, string message)
    {
        await DisplayAlert(title, message, "Dismiss");
    }
```

You may recall that `DisplayAlert` is a method of the `Page` class, so is not available in the `ViewModel`. At the same time, the `ViewModel` does not want to be tightly coupled to the `Page` type, so we use an interface to achieve loose coupling.  

## MainPageViewModel and MVVM 
The ViewModel has a couple changes to consider:

The constructor now takes a parameter of type `IMainPageHelper`

```C#
    public MainPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
    {
        _viewHelper = viewHelper;
        ...
```

Back in the constructor of the View code behind, we see a reference to the view being passed by parameter:

```C#
    ...
    vm = new MainPageViewModel(this);
    ...
```

As you may recall from the above, the tapped event handler in the View object invokes the following method on the ViewModel.

```C#
    public async Task UserTappedListAsync(int row, string planetString)
    {
        TitleString = planetString;
        await _viewHelper.TextPopup("Tapped", $"{planetString} on row {row}");
    }
```

This brings control back into the ViewModel and gives the ViewModel an opportunity to marshal any changes in the view down to the Model objects. This makes the ViewModel the central hub of activity and business logic, something we might want to test of course.

The second line invokes the `TextPopup` method back on the View. This 'round trip' might seem somewhat overkill, but there is (at least some) rational behind it: 

> Remember that the View knows the concrete type of the ViewModel, but the ViewModel does not know the actual class type of the View.
>
> _So what and why bother?_
>
> In future, we might want to unit test our ViewModel without the requirement to maintain aa tightly bound reference to a specific `Page`. In fact, we might not use a `Page` at all! 
>
> As long as it's loosely bound, this leaves open options for _mocking_ our view. In our mocked object, the only requirement here is to implement `IMainPageHelper`. In concrete terms, _viewHelper might be a plain class that simply logs strings to a console or log file.

All this _plumbing and wiring_ between objects may seem to complicate matters, and to some extent it does. It can be quite hard to maintain and mental model of how it all fits together. It would be nice if it could all be contained in one place and not spread across different source files? Maybe later.... ;)

---

[Next - Item Selection](listview-selection.md)



