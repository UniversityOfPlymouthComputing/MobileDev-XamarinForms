[back](listview-add.md)

---

# ListView - Adding Data

The example for this section is found in the [/code/Chapter4/ListView/E_SimpleListView_Datatemplate_Del](/code/Chapter4/ListView/E_SimpleListView_Datatemplate_Del) folder.

> * Build and run the code
> * Click on a row to add an additional row
> * Swipe a row and choose delete
> * Familiarize yourself with the code

The delete function varies across platforms. Setting this up requires a bit of extra work:

* Setting up data template to generate a cell at run time
* Adding a context action (a `MenuItem` in this case) to the generated cell

## Predefined and Custom Cell Types
Scrolling tables of data are an aspect of mobile operating systems that seem to be very performance sensitive. Scrolling tables of data seem to be one of the benchmarks for evaluating a mobile device / platform performance. The smooth and rapid scrolling of tables has even been known to feature in product demonstrations.

Each row of `ListView` has an associated *data item* known as a cell (typically a subclass of the `Cell` type). Cells are not actually views, but describe how a view should appear. The appearance and actual rendering of cells is very platform specific and performance sensitive.

Up to now, we've only used the default cell type which has a single `string` property. A single string is very limiting of course, but is useful for getting started. 

> So far, we didn't have to write any code to instantiate a cell. If we want anything other than the default single-string cell, we will need to do more work.

 There are some pre-defined cell types we can use that cover many use-cases.

* `TextCell`: A cell that has a main text label and a details text label
* `EntryCell`: A cell that has a label and an editable entry
* `SwitchCell`: A cell that has a label and a switch
* `ImageCell`: A cell that has an image on the left and two labels

All of these are optimised for performance.

[See the documentation for details](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/controls/cells)

If one of the pre-defined cell types does not meet your requirements, you can create a custom cell using `ViewCell`, with some performance costs:

* `ViewCell`: A row with custom layout 

[See the documentation for details](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/customizing-cell-appearance#custom-cells)

We will initially use `TextCell` and look at `ViewCell` later.

## Data Templates
Both the iOS and Android platforms support scrolling tables, but perform this is different ways.

Xamarin.Forms provides cross-platform abstractions so you can write code once to produce a native application for two platforms, iOS and Android. At the same time, it works hard to maintain performance.

One of the (clever) abstractions provided in Xamarin.Forms are data templates. These are objects that are called upon at run-time to generate cells. To get some insight into what a data-template is, it's easiest to see it in C# code. As we will see, the syntax of XAML is a lot more concise, but it tends to obscure the underlying mechanism and can be confusing. For now I've purposely left the XAML unchanged for this reason.

### The term "Item"
Throughout this discussion you will encounter the term "item". This is referring to an item from the collection of data for a particular row. An "item" is data-centric.

> Where I refer to **data item**, it refers to a specific data object extracted from a `ListView<>` or `ObservableCollection<>` for a given row. Our collection is a list of data items.

For example:

* We have already met one - `SelectedItem`. This was the data item for the row selected by the user. It's nothing to do with a `TableView`, it's the data itself.

* `ItemTemplate` - is a property of `ListView`. It is assigned to a data template object, who's job it is to create a cell at run time (for each row) and bind it's visible properties to properties of a particular _data item_.

Hopefully, this will be more apparent when we see the code:

## MainPage.xaml.cs
Our attention is in the constructor, where we will work top down.

```C#
    PlanetListView.ItemTemplate = dataTemplate;
```










