[back](listview-templates.md)

---

# Deleting Data and Menu Items

The example for this section is found in the [/code/Chapter4/ListView/F_SimpleListView_MenuItem](/code/Chapter4/ListView/F_SimpleListView_MenuItem) folder.

> * Build and run the code
> * Click on a row to add an additional row
> * Swipe a cell left and click delete
> * Familiarize yourself with the code

The delete function varies visually across platforms but is functionally similar. Setting this up requires 

* Adding a context action (a `MenuItem` in this case) to the generated cell
* Adding an event handler to the code behind or a Command in the ViewModel

The main changes are in the code-behind (configuring the cell) and the view model (adding a bindable `Command` that deletes data)

Let's start with the ViewModel and work up.

## MainPageViewModel.cs
There are a few additions to the view model.

### Delete Method
First, a method to delete an data item from the `Planets` collection:

```C#
    public void DeleteItem(SolPlanet p) => Planets.Remove(p);
```

When we swipe a row and tap delete, ultimately this is the method that should be called. This method is easy to test should we wish to perform unit testing.

### Delete Command
You may recall that an alternative to an event handler (which requires code in the code-behind) is a `Command`.

```C#
    public ICommand DeleteCommand { get; private set; }
```

Unlike event handlers, commands can be invoked by visual elements (that support them) through the binding layer.
The command is instantiated in the constructor as follows:

```C#

    DeleteCommand = new Command<SolPlanet>(execute: (p) =>
    {
        DeleteItem(p);
    });
```            

On the previous occasion we met commands, it was bound to a button tap. This time, some data needs to be passed as a parameter. Note the generic type. This indicates that this command also takes a parameter of type `SolPlanet` (the type of each data item).

When the delete button is tapped, this command will be invoked. An instance of `SolPlanet` (corresponding to the deleted row) will be passed by parameter. How convenient!

## MainPage.xaml.cs and the `MenuItem`
We now return to the code behind where we write a closure to instantiate a `TextCell`. This cell will now have a context action of type `MenuItem` added to it.

Here is the code to create and attach the context action:

```C#
    ...
    DataTemplate dataTemplate = new DataTemplate( () =>
    {
        TextCell cell = new TextCell();
        MenuItem m1 = new MenuItem
        {                    
            Text = "Delete",
            IsDestructive = true
        };

        ...

        cell.ContextActions.Add(m1);
        return cell;
    });
    ...
```                

The `IsDestructive` property affect the appearance of the delete button on iOS.

The code above would display the delete button, but would do nothing. We need to either attach an event handler or bind a command.

### Using an event handler
Attaching an event handler is much like any the `Button` object we've used before:

```C#
    m1.Clicked += (object sender, System.EventArgs e) => {
        //Note that conditional statements evaluate left to right - && will short-circuit if the first condition is not true
        if ((sender is MenuItem mi) && (mi.CommandParameter is SolPlanet p))
        {
            Debug.WriteLine($"Vogons are destroying {p.Name}");
            vm.DeleteItem(p);
        }
    };
```                

There are a couple points of interest here:

* Type Checking
* The role of the ViewModel

**Type Checking**

The first `sender` is type `object`, so needs to be cast to the correct type (`MenuItem`). A simple cast could have been used here. Instead, _patter matching_ is used.

```C#
    if ((sender is MenuItem mi) && (mi.CommandParameter is SolPlanet p))
```

* The local variable `mi` is type `MenuItem`. 

    * It will be assigned to `sender` but _only_ if `sender` "`is`" of type `MenuItem`

    * If `object` is not type `MenuItem` the condition fails _immediately_ and the whole `if` statement is skipped. We say the conditional statement is short circuited.

* If `object` is type `MenuItem`, then `mi` is a valid instance of `MenuItem` (and not `null`)

    * The second expression `mi.CommandParameter is SolPlanet p` can be called safely and tested

    * There is no danger of a `null` exception due to the short-circuiting behavior described in the previous point

    * The local `p`, type `SolPlanet` is assigned to `mi.CommandParameter` if the types match

* If we get past all the above, we have a data item `p` - the object to be deleted.

I could have (safely) cast `sender` to type `MenuItem` and called `CommandParameter` on it. However, this assumes the code will not change in the future. What if we change the data type of each data item. Ideally, to mitigate against such future events, we should probably throw an exception and/or log an error should the type checking fail.

**The role of the ViewModel**
Not that once we have a valid data object, it is passed across to the ViewModel.

```C#
    vm.DeleteItem(p);
```

> Remember that the ViewModel has access to model data, whereas the View objects should not.

The event handler code tends to exist in the code-behind, where we may wish to minimise the amount of code (it is harder to test). Moreover, the event handler code is very UI centric, involving `MenuItem` and it's `CommandParameter` Property.

The code behind ends up acting as a go-between, passing the extracted data over to the ViewModel. Can we not just go straight to the ViewModel?

The answer is yes, and for that, you use a `Command`.

### Binding the Command
Binding the `MenuItem` to a property of the ViewModel is something we saw in an earlier section. This time, there is the additional command parameter to consider.

The trick with binding is to keep track of the binding context / source object.

For the command itself, the `Binding` class allows us to specify the target (`MenuItem.CommandProperty`) abd source object (ViewModel) explicitly:

```C#
    m1.SetBinding(MenuItem.CommandProperty, new Binding("DeleteCommand", source: this.BindingContext));
```
 Note:

* `m1` is of type `MenuItem`
    * It was added to the cell as a context action using `cell.ContextActions.Add(m1);`
* `this.BindingContext` is a reference to the `ViewModel`
    * Remember that `this` refers to the Page
* The ViewModel has a property `DeleteCommand` of type `ICommand` (see below).

_But what about the parameter?_

This is more hidden and a little harder to understand. We are working with a `MenuItem` associated with a cell. So what is the binding context / how to we specify the source object? 

The source object is actually a data item (type `SolPlanet`). Although the code is hidden from us, we see evidence of this in the following two lines:

```C#
    dataTemplate.SetBinding(TextCell.TextProperty, "Name");
    dataTemplate.SetBinding(TextCell.DetailProperty, "Distance");
```            

> Just property names are provided here, so we infer the source object is a **data item** of type `SolPlanet`

When it comes to the parameter, we don't just want a single property, but the whole data item, so we write the following:

```C#
    m1.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
```

The "." is the path and refers to the data source object of `m1`, which is an instance `SolPlanet`.

> If we had wanted to pass just the "Name", we could have written "Name" instead.

I recommend reading the section on Collection Views in the book by Charles Petzold. The last paragraph on p559 helps to explain the underlying mechanism.

## MainPageViewModel.cs (revisited)

Remember that in the constructor, we instantiate a `Command` with an execute closure:

```C#
    DeleteCommand = new Command<SolPlanet>(execute: (p) =>
    {
        DeleteItem(p);
    });
```

As this is a command that takes a parameter, the type is `Command<SolPlanet>`. The type of `p` can then be inferred.

Note:

* This is all type-safe. No type checking code was needed.
* The code behind can be replaced with more concise XAML (as we see in the next section)

## Concluding thoughts

Whether you use an event handler (arguably easier to understand) or a Command (less code-behind) is up to you. Which ever way you prefer, I would suggest writing the ViewModel is a way that promotes testability. This often means writing an API that uses baked-in data types and model object types.

I've heard it said that "Commands" are not easy to mock, but the `DeleteItem` method can be easily called via a unit test. In practice, the command closure only has one line, `DeleteItem(p)` so this questions the need to test the command itself.

Views on this will vary between individuals and organizations. Someone writing a free "to-do" application may not be so rigorous as a company writing a banking application for example.

When writing this section, understanding how to use the right bindings for the command and the command property took a while. Drilling down, the following link explains how `MenuItem` can be used with MVVM:

[Xamarin.Forms MenuItem - Define MenuItem behavior with MVVM](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/menuitem#define-menuitem-behavior-with-mvvm)

What makes this confusing is that some of the work is being hidden, including the instantiation of cells and setting of the binding context / data source. 

This in turn may be driven by the need to maintain performance while presenting a cross-platform API. Not an easy task!

Some of the code in this section is quite verbose. In the next section we use XAML instead, which turns out to be much more concise. However, it is not obvious from the XAML syntax what is going on and why some of the attributes are set the way they are. I hope the code in this section helps to clarify this.

---

[Next - Data templates and Menu Items in XAML](listview-datatemplate-xaml.md)
















