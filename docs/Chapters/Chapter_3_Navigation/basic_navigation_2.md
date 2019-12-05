[Prev - Basic Navigation Part 1](basic_navigation_1.md)

---

## Basic Navigation 2 - Passing Data Forward

Open the solution in the folder 

[BasicNavigation-2-Passing_Data_Forward](/code/Chapter3/NavigationControllers/1-View_Based/BasicNavigation-2-Passing_Data_Forward)

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

Note how the `Year` and `Name` properties are passed by parameter to the next page in the navigation hierarchy via the constructor.

> Only having constructors with parameters prevents the developer from forgetting to provide key information.

When doing this, we need to answer the following question:

> Are we passing a copy of the data, or a reference?

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

> When you assign one value type to another, you make an independent **copy**.

The property `Year` in `YearEditPage` is entirely independent of the `Year` property in `FirstPage`. Put another way, any edits to `Year` won't propagate back to the previous view.

In the case of `Name`, you might think this is different. The `string` in C# is equivalent to `System.String` [(see the Microsoft Docs)](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/)

Therefore, `string` is a class and any instance a **reference type**. 

> Ordinarily, when you assign one reference to another, then each reference the exact same data.

However, `string` is written to be _immutable_ (you cannot change it once created). So for example:

```C#
string s1 = "123";
string s2 = s1;
s1 += s2;
```

* When string `s2` is assigned to `s1`, both are indistinguishable. 

* When the string `s1` is modified, in this case by `s1 += s2`, a **new** string (of the right capacity) is created. `s1` now references a new string that is different to `s2` 

* When this runs, `s2` will still be equal to "123".

> In other words, `string` is written to have value type semantics.

In the constructor we see the following:

```C#
Name = name;
```

Although `Name` is a reference type, any change to `Name` will not affect the original `name`.

> Had you used your own classes and passed an instance as a parameter, unless you do some additional work, the reference would allow you to modify the original copy.

You simply need to decide which behavior you wish to implement.

### NameEditPage
The situation is similar in this class, only this time only a string property of `Name` is held (it does not need a `Year`)

Once again, any edits to the `Name` property is not reflected in the previous page for the reasons given above.

> What if we use the mutable string `StringBuilder` instead? 
>
> This is quite an interesting exercise to try and it can be made to work. 
>
> _Hint:_ your views will not update when you navigate back. Consider binding your properties to the view.

In the next section, we will look at alternatives that use binding to manage a single copy of the data. These are not perfect by any means, but can be made to work in some instances.

--- 

[Next - Basic Navigation Part 3](basic_navigation_3.md)
