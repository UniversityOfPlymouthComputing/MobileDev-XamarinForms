[Contents](README.md)

----

# ListView
Very often we wish to display a (mutable) collection of data on a screen. One of the most common ways to achieve this is with a [ListView](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/)

For static collections, there is also [`TableView`](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/tableview). This is covered in a [later section](tables.md)

For now we are going to focus on presenting collections (lists) of data that can _change_. For example, this my be the result of a database query, web query or data directly entered by the user.

We might then want to interact by tapping on a specific row and navigating to a new detail screen. Such a screen might review more detail and/or allow data to be edited. This takes us into the topic of master-detail, and will be considered in a [later section](master-detail.md).

## Scope

To cover `ListView` in its entirety would take a long time and risks being overwhelming. The content is limited to the following selected topics, leaving the learner to discover additional details via the official documentation (as need arises).

* [Simple `ListView` Example](simple-listview.md)
* [Tapped Event](listview-tapped.md)
* [Item Selection](listview-selection.md)
* [Adding Data](listview-add.md)
* [Cells and Data Templates](listview-templates.md)
* [Deleting Data and Menu Items](listview-delete.md)
* [Data Templates and Menu Items in XAML](listview-datatemplate-xaml.md)
* [Custom Cell Layout with `ViewCell`](listview-viewcell.md) 
* [Sub-classing `ViewCell`](listview-viewcell-sub.md)
* [Adding Bindable Properties to Custom Cells](listview-bindableprops.md)
* [Organizing and Listing Data in Groups](listview-groups.md)
* [Using Different Cell Types with Template Selectors](listview-template-sel.md)

## Example Code
All the topics above have their own section and example solution. The solution files are found in sub-folders off the [/code/Chapter4/listview](/code/Chapter4/listview) folder.

## References
[Petzold C., Creating Mobile Apps with Xamarin.Forms, Microsoft Press](https://docs.microsoft.com/xamarin/xamarin-forms/creating-mobile-apps-xamarin-forms/)

[ListView](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/)

[ListView Interactivity](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/interactivity)

[ListView Class Reference](https://docs.microsoft.com/dotnet/api/xamarin.forms.listview?view=xamarin-forms)


## Let's Begin
Click the Next link below.

----

[Next - Simple `ListView` Example](simple-listview.md)