[Prev - Basic Navigation Part 4](basic_navigation_4.md)

---

## Basic Navigation 5 - Implementing undo with a Singleton Model
Open the solution in the folder [BasicNavigation-5-Binding](/code/Chapter3/NavigationControllers/1-View_Based/BasicNavigation-5-Nondestructuve)

Run the code, and try editing the data using both the `Slider` control (for the year) and the `Entry` control (for the name). 

> On the `NameEditPage`, compare the behaviour of back button to the save button.

The first tqo pages are the same as the previous project. What has changed is the `NameEditPage`, which has it's own binable `Name` property.

> Rather than bind to the singleton model, the UI is once again bound to properties associated with the page. This way, we can control when the singleton model data is updated (or left unchanged).

We can see this in the code-behind. First, note the `Name` property is restored.

```C#
    private string _name = "Anon";
    public string Name
    {
        get => _name;
        set
        ...
    }
```

The constructor reads data from the singleton and populates the local property.

```C#
    public NameEditPage()
    {
        InitializeComponent();
        this.Name = SingletonModel.SharedInstance.Name;
        SaveButton.Clicked += SaveButton_Clicked;
    }
```

When the save button is touched, the singleton model is updated from the local.

```C#
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        SingletonModel.SharedInstance.Name = this.Name;
        await Navigation.PopToRootAsync();
    }
```

What seems appealing here is that the page is bound to it's own properties, independent of any other page. We retain full control over whether the singleton data is updated or not. We will benefit from bindings of course.

--- 

[Next - Navigation with MVVM Part 1](mvvm_navigation_1.md)