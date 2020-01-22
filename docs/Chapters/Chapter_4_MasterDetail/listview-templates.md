[back](listview-add.md)

---

# ListView - Cells and Data Templates

The example for this section is found in the [/code/Chapter4/ListView/E_SimpleListView_Datatemplate](/code/Chapter4/ListView/E_SimpleListView_Datatemplate) folder.

> * Build and run the code
> * Click on a row to add an additional row
> * Familiarize yourself with the code

Note the individual rows now have two text properties. Setting this up requires a bit of extra work:

* Updating the model data - each data entry is now type `SolPlanet`
* Setting up data template to generate a cell at run time

## MainPageViewModel.cs
The ViewModel (which also encapsulates the model data) has one important change, and that is the model data.

Each data item is of type `SolPlanet`

```C#
    public class SolPlanet
    {
        public string Name { get; set; }
        public double Distance { get; set; }
        public SolPlanet(string name, double dist) => (Name, Distance) = (name, dist);
    }
```

Note the names of the two public properties: `Name` and `Distance`.

The ViewModel instantiates the `Planets` property as follows:

```C#
    ...
    Planets = new ObservableCollection<SolPlanet>()
    {
        new SolPlanet("Earth", 147.1),
        new SolPlanet("Mercury", 69.543),
        new SolPlanet("Venus", 108.62),
        new SolPlanet("Jupiter", 782.32),
        new SolPlanet("Mars", 238.92),
        new SolPlanet("Saturn", 1498.3),
        new SolPlanet("Pluto", 5906.4)
    };
    ...
```            

Both the `Name` and `Distance` (from the Sun) are displayed in the `ListView`. To do this, we need a different type of "cell" that can display two strings.

## Predefined and Custom Cell Types
Scrolling tables of data are an aspect of mobile operating systems that seem to be very performance sensitive. Scrolling tables of data seem to be one of the benchmarks for evaluating a mobile device / platform performance. The smooth and rapid scrolling of tables has even been known to feature in product demonstrations.

Each row of `ListView` has an associated _cell_ (typically a subclass of the `Cell` type). Cells are not actually views, but describe how a view should appear. The appearance and actual rendering of cells is very platform specific (and performance sensitive).

Up to now, we've only used the default cell type which has a single `string` property. A single string is very limiting of course, but is useful for getting started. 

> So far, we didn't have to write any code to instantiate a cell. If we want anything other than the default single-string cell, we will need to provide a cell type.

There are some pre-defined cell types we can use that cover many use-cases.

* `TextCell`: A cell that has a main text label and a details text label
* `EntryCell`: A cell that has a label and an editable entry
* `SwitchCell`: A cell that has a label and a switch
* `ImageCell`: A cell that has an image on the left and two labels

All of these are optimised for performance and should usually be considered as a first-choice cell type.

[See the documentation for details](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/controls/cells)

If one of the pre-defined cell types does not meet your requirements, you can create a custom cell using `ViewCell`, albeit with some performance costs:

* `ViewCell`: A cell with custom layout

[See the documentation for details](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/customizing-cell-appearance#custom-cells)

We will initially use `TextCell` and look at `ViewCell` later.

Creation, reuse and destruction of cells is closely related to performance. Rather than do this ourselves for each ListView row, this tasks is delegated to something known as a data template.

## Data Templates
Both the iOS and Android platforms support scrolling tables, but perform this is different ways in order to optimise the user experience.

Xamarin.Forms is a cross-platform framework however, and provides abstractions so you can write code once to produce a native application for two platforms, iOS and Android. At the same time, a lot of effort goes into maintaining performance.

One of the (clever) abstractions provided in Xamarin.Forms are data templates. These are objects that are called upon at run-time to generate cells. To get some insight into what a data-template is, it's easiest to see it in C# code (in the code-behind). As we will see, the syntax of XAML is a lot more concise, but it tends to obscure the underlying mechanism and can be confusing. For this section, I've purposely left the XAML unchanged for this reason.

### The term "Item"
Throughout this discussion you will encounter the term "item". This is referring to an item from the collection of data (one per row). An "item" is data and not a view object.

> Where I refer to **data item**, it refers to a specific data object extracted from a `ListView<SolPlanet>` or `ObservableCollection<SolPlanet>` for a given row. Our collection is a list of data items.

For example:

* We have already met `SelectedItem`. This was the data item for the row selected by the user.

* `ItemTemplate` is a property of `ListView`. It is assigned to a data template object, who's job it is to create a cell at run time (for each row) and bind its visible properties to the bindable properties of a particular _data item_.

Hopefully, this will be more apparent when we see the code:

## MainPage.xaml.cs
Our attention begins in the constructor of the code behind, where we will work top down.

```C#
    PlanetListView.ItemTemplate = dataTemplate;
```
 By default, `ItemTemplate` is `null`. By assigning it to an object of type `DataTemplate`, we are no longer using the default single-string cell.

 ### Creating an instance of `DataTemplate`
 There are two constructors for the `DataTemplate` class:
 
The simplest takes parameter of type `System.Type`

```C#
DataTemplate DataTemplate = new DataTemplate(typeof(TextCell)); 
```

Note how the **type** of cell is passed by parameter, and **not** an instance of a cell.

The second constructor option is to pass a closure as a parameter, and this is the approach I have used here (it illustrates the underlying mechanism more explicitly).

```C#
DataTemplate dataTemplate = new DataTemplate( () => {
        TextCell cell = new TextCell();
        return cell;
    } );
```

Later we will add more code to this so we can easily customise the cell. This is the advantage of using the closure version of the constructor.

> Remember that the closure has not run at this point. It is the `ListView` and data template that manage when the closure will run, instantiate a cell and bind it to the data items.
>
> Passing closure is an effective way to customise code. It is reminiscent of a software "plug-in".

### Cell Bindings
So far, we've seen how a data template is created, and together with the `ListView`, will instantiate a cell at run-time. This is both efficient and provides cross-platform uniformity.

For view objects, the `SetBinding` method is an _instance method_. It assumes you have a concrete instance of an object (the target) on which to apply the binding.

However, it would seem we have a problem. If the cell does not yet exist, how do we specify the bindings so the cell displays the data item? Furthermore, how do we know which data item to bind to?

Once again, this task is delegated to the `ListView` and data template. Let's look at the code-behind again.

```C#
    dataTemplate.SetBinding(TextCell.TextProperty, "Name");
    dataTemplate.SetBinding(TextCell.DetailProperty, "Distance");
```            

> The `SetBinding` method for `DataTemplate` is not the same as a visual object.

These two method calls do not (yet) hook up the bindings as the cells don't exist yet. We are simply providing enough information so it can be done for us at run-time.

> Note the second parameter values, "Name" and "Distance". These are properties of the data items (type `SolPlanet`) and not the ViewModel.

What quickly becomes confusing is the binding context at various points, so this might be a good time to summarise:

* The `BindingContext` of the `ContentPage` is the ViewModel. 

* The `ItemSource` of the `ListView` is the `Planets`  property of the ViewModel. 

   * The `Planets` property is a collection of data items, type `SolPlanet`.

* The `ItemTemplate` property (note that word _item_) of `ListView` is a data template used to create a cell and bindings _for each data item_ at run-time. Bound properties relate to the data item (type `SolPlanet`), and *not* the ViewModel.

## Next Step - Deleting Data
Now we have a data template and code to create a cell, we can begin to add additional functionality. In the next section we will add a swipe-to-delete facility. For this we attach a `MenuItem` to each cell as a "Context Action". As you will see, using once API, we get two visually different behaviors across the two platforms.

---

[Next - Deleting Data and Menu Items](listview-delete.md)
 













