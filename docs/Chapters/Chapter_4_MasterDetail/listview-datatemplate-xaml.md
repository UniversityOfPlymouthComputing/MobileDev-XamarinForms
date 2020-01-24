[back](listview-delete.md)

---

# ListView - Data Templates and Menu Items in XAML

The example for this section is found in the [/code/Chapter4/ListView/G_SimpleListView_Datatemplate_XAML](/code/Chapter4/ListView/G_SimpleListView_Datatemplate_XAML) folder.

> * Build and run the code
> * Click on a row to add an additional row
> * Swipe a cell left and click delete
> * Familiarize yourself with the code, noting the changes in the code-behind and XAML

In the previous section, quite a lot of code was written to (i) specify which cell should be used, (ii) add a menu item to allow data to be deleted and (iii) bind the menu item to a command for invoking code on the ViewModel.

In this section, much of that boiler-plate code is removed and replaced with XAML.

## The Main Page - MainPage.xaml
Open the main page XAML file, and examine the `ListView`. In this version, the following XAML code has been added:

```XML
    ...
    <ListView.ItemTemplate>
        <DataTemplate>
            <TextCell Text="{Binding Name}" Detail="{Binding Distance}">
                <TextCell.ContextActions>
                    <MenuItem   Command="{Binding Source={x:Reference MainContentPage}, Path=BindingContext.DeleteCommand}"
                                CommandParameter="{Binding .}"
                                Text="Delete"
                                IsDestructive="True" />
                </TextCell.ContextActions>
            </TextCell>
        </DataTemplate>
    </ListView.ItemTemplate>
    ...
```            

Let's look at this in more detail:

```XML
    <ListView.ItemTemplate>
        <DataTemplate>
            <TextCell Text="{Binding Name}" Detail="{Binding Distance}">
            ...
```
The `ItemTemplate` property of `ListView` is being set as an instance of `DataTemplate` as we did in code.

The following line does not instantiate a `TextCell` object (as it might first appear) however.

> If you recall from the code, the `DataTemplate` object is being told which **type** of cell to create (`TextCell`) at run-time for each row. This is either by passing the data type or a closure by parameter.

For each `TextCell` that is ultimately instantiated at run time, we see from the XAML that its `Text` and `Detail` properties will be bound to the properties `Name` and `Distance` of the corresponding data item (type `SolPlanet`).

> As with the code version, we don't see the data source / binding context being set here. It is good to make a mental note that this is done for us under the hood.

Now we move on to the `MenuItem`.  For each cell that will be instantiated, the `<TextCell.ContextActions>` (a collection) will contain a single instance of `MenuItem`

```XML
    <MenuItem   Command="{Binding Source={x:Reference MainContentPage}, Path=BindingContext.DeleteCommand}"
                CommandParameter="{Binding .}"
                Text="Delete"
                IsDestructive="True" />
```
The tricky part is the `Command` binding. The source object needs to be set to the `DeleteCommand` property of the ViewModel (as we did in the code), but our binding context is currently a data item (type `SolPlanet`). How do we reference the ViewModel from XAML?

One way is via a reference to the `BindingContext` property of the `ContentPage` which is indeed the ViewModel. 

(i) To reference the page itself, the page is first given a name:

```XML
<ContentPage ...
             x:Name="MainContentPage"
             ...
             >
```             

(ii) The source object is then set to the page and the path to the respective property as follows:

```XML
    ...
    <MenuItem   Command="{Binding Source={x:Reference MainContentPage}, Path=BindingContext.DeleteCommand}"
                CommandParameter="{Binding .}"
    ...
```

This is similar to what we did in the code in the previous section.

```C#
    m1.SetBinding(MenuItem.CommandProperty, new Binding("DeleteCommand", source: this.BindingContext));
```

For the `CommandParameter`, as we did in the code, we specify a path of "." which you may recall is the data item for a given row.

```C#
    m1.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
```

As before, what might be confusing is the binding context, especially in XAML.

Again, I refer to the documentation [Xamarin.Forms MenuItem - Define MenuItem behavior with MVVM](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/menuitem#define-menuitem-behavior-with-mvvm)


---

[Next - Custom cell layout with `ViewCell`](listview-viewcell.md) 