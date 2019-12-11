[Prev - Basic Navigation Part 1](basic_navigation_1.md)

---

## Basic Navigation 2 - Passing Data Forward

Open the solution in the folder [BasicNavigation-2-Passing_Data_Forward](/code/Chapter3/NavigationControllers/1-View_Based/BasicNavigation-2-Passing_Data_Forward)

Run the code. You will notice that not much as changed in terms of it's visual behavior. Behind the scenes there are some changes which we will look at now.

The XAML has not been updated, so we will focus on the code behind for each page.

### FirstPage
Two properties have been created.

* `Year` (type `int`)
* `Name` (type `string`)

If you inspect the property accessors, changing either of these properties results in the UI being updated. For example, for the `Name`

```C#
...
private string _name;
...
public string Name
{
    get => _name;
    set
    {
        if (_name == value) return;

        _name = value;
        NameLabel.Text = _name;
    }
}
...
```

The edit button event handler has also changed:

```C#
private async void EditButton_Clicked(object sender, EventArgs e)
{
    //Pass data forward and push on navigation stack
    var nextPage = new YearEditPage(Name, Year);
    await Navigation.PushAsync(nextPage, true);
}
```

Note how the `Year` and `Name` properties are passed as parameters to the next page in the navigation hierarchy via the constructor.

> Only having constructors with parameters prevents the developer from forgetting to provide key information.

When doing this, we need to answer the following question:

> Are we passing a copy of the data, or a reference?
> If by reference, it is a mutable type?

In this case, the integer is a value type and the string is an immutable reference type. This is fine for passing data forward, but does not solve the problem of getting any changes back. 

To explain the significance of this, let's look at the next page, the `YearEditPage`

### YearEditPage
Once again, this page have it's own properties for `Year` and `Name`.

In the constructor, these are assigned to the valued passed from the previous page:

```C#
public YearEditPage(string name = "Anon", int year = 1970)
{
    ...

    //Populate UI
    Name = name;
    Year = year;

    ...
}
```

#### Value Types vs Reference Types
In the case of `Year`, this is an integer, so is a **value type**. 

> As we saw in a previous section, When you assign one value type to another, you make an independent **copy**.

The property `Year` in `YearEditPage` is entirely independent of the `Year` property in `FirstPage`. Put another way, any edits to `Year` won't propagate back to the previous view.

In the case of `Name`, type `string` is a class and any instance a **reference type**. However, `string` is written to be _immutable_ (you cannot change it once created). 

In the constructor we see the following:

```C#
Name = name;
```

At this point, `Name` is a reference to the original string. Although `Name` is a reference type, any change to `Name` will result in a reference to a new object, leaving the original unchanged.

For both parameters, the data flow is forward only. In some instances, this might be all you require. 

If you do wish to propagate changes back to the previous page, then you need a different strategy. A couple simple options might be:

* Pass both parameters by reference (types `ref int` and `ref string`)
* Encapsulate your data in a (mutable) model class and pass the instance as a parameter.
* Which ever method you use, you will still need to find a scheme to update the views when they are re-exposed - later we will use binding to perform this for us.

### NameEditPage
The situation is similar in this class, only this time only a string property of `Name` is held (it does not need a `Year`)

Once again, any edits to the `Name` property are not reflected in the previous page for the reasons given above.

> **Exercise** - Using a method of your choice, can you get the edits in each page to update the data in the previous page(s). Consider overriding `OnAppearing()` and `OnDisappearing()` to update the views.

In the next section, we will look at alternatives that use binding to manage a single copy of the data. This technique is not perfect by any means, but can be made to work in some instances.

--- 

[Next - Basic Navigation Part 3](basic_navigation_3.md)
