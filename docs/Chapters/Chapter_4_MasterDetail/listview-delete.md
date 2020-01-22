[back](listview-templates.md)

---

# ListView - Cells and Data Templates

The example for this section is found in the [/code/Chapter4/ListView/E_SimpleListView_Datatemplate](/code/Chapter4/ListView/E_SimpleListView_Datatemplate) folder.

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

### Binding the Command















