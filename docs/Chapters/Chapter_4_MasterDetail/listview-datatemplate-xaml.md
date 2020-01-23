[back](listview-delete.md)

---

# Data templates and Menu Items in XAML

The example for this section is found in the [/code/Chapter4/ListView/G_SimpleListView_Datatemplate_XAML](/code/Chapter4/ListView/G_SimpleListView_Datatemplate_XAML) folder.

> * Build and run the code
> * Click on a row to add an additional row
> * Swipe a cell left and click delete
> * Familiarize yourself with the code, noting the changes in the code-behind

In the previous section, quite a lot of code was written to specify which cell should be used, a menu item to allow data to be deleted and a command for invoking code on the ViewModel.

In this section, much of that boiler-plate code is removed and replaced with XAML.

## The Main Page - MainPage.xaml
Open the main page XAML file, and examine the `ListView`. In this version, the following XAML code has been added:

```XML
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
```            

Let's look at this in more detail:

```XML
    <ListView.ItemTemplate>
        <DataTemplate>
            <TextCell Text="{Binding Name}" Detail="{Binding Distance}">
            ...
```
The `ItemTemplate` property of `ListView` is being set as an instance of `DataTemplate` as we did in code.

Despite how the following line might appear, a `TextCell` is not being instantiated directly.

> If you recall from the code, the `DataTemplate` object is being told which **type** of cell to create (`TextCell`) at run-time for each row.

For each `TextCell` that is instantiated, its `Text` and `Detail` properties will be bound to the properties `Name` and `Distance` of the corresponding data item (type `SolPlanet`).

Now we move on to the `MenuItem`.  For each cell that will be instantiated, the `<TextCell.ContextActions>` (collection) will contain a single instance of `MenuItem`

```XML
    <MenuItem   Command="{Binding Source={x:Reference MainContentPage}, Path=BindingContext.DeleteCommand}"
                CommandParameter="{Binding .}"
                Text="Delete"
                IsDestructive="True" />
```
The tricky part is the `Command` binding. The source object needs to be set to the `DeleteCommand` property of the ViewModel (as we did in the code). How do we reference this from XAML?

One way is via a reference to the `BindingContext` property of the `ContentPage` which is indeed the ViewModel. 

(i) To reference the page it self, the page is given a name:

```XML
<ContentPage ...
             x:Name="MainContentPage"
             ...
             >
```             

(ii) The source is then set to the page and the path to the respective property as follows:

```XML
    <MenuItem   Command="{Binding Source={x:Reference MainContentPage}, Path=BindingContext.DeleteCommand}"
                CommandParameter="{Binding .}"
                ...
```

This is similar to what we did in the code in the previous section. For the `CommandParameter`, we again specify a path of "." which you may recall is the data item for a given row.

Again, I refer to the documentation [Xamarin.Forms MenuItem - Define MenuItem behavior with MVVM](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/menuitem#define-menuitem-behavior-with-mvvm)


---

[Next - Custom cell layout with `ViewCell`](viewcell.md) 