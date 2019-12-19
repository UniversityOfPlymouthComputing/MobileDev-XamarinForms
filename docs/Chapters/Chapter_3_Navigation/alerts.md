[Table of Contents](README.md)

----

# Alerts and Pop-Ups
Xamarin.Forms provides a very simple set of APIs for pop-ups and alerts

First, you should make yourself familiar with the [Microsoft Documentation](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/pop-ups)

The APIs are fairly simple and the documentation is very comprehensive, so we will not focus on that aspect. The documentation covers three basic types of popup via three methods:

* `DisplayAlert` 
* `DisplayActionSheet`
* `DisplayPromptAsync`

It is important to understand that these are all methods on the [`Page` class](https://docs.microsoft.com/dotnet/api/xamarin.forms.page?view=xamarin-forms).

> The built-in pop-ups APIs are all **View Based**

We will look at how you might invoke these from a view model. This was briefly covered before in an earlier chapter, but it is good to look at this again.

> Open the solution in the folder [Popups](/code/Chapter3/Modal/Popups). 

Build and run this code. 

To keep the example as simple as possible, there is only one page. However, a ViewModel is used.

## Interfaces

Two interfaces have been created in this project:

* `IPage` - for general purpose view based methods 
* `IMainPageHelper` - for view based methods specific to `MainPage`

### `IPage`
This is very simple for now, and simple specifies a property that will typically reference the `Navigation` propert of a given page.

```C#
    public interface IPage
    {
        INavigation NavigationProxy { get; }
    }
```

### `IFirstPageHelper`
This provides APIs from the `MainPage` code-behind. It also inherits `IPage`

```C#
    public interface IMainPageHelper : IPage
    {
        Task<bool> YesNoAlert(string title, string message);
        Task<string> AskForString(string questionTitle, string question);
    }
```

These will be `await`able so need a return-type `Task<>`.

## The Important Bit - Implementing the Interface
Given the above two interfaces, we now ensure that the `MainPage` implements the `IMainPageHelper` interface.

> This enforces the concrete implementation of all the methods in the interfaces (so we cannot forget!).

The full source is below:

```C#
    public partial class MainPage : ContentPage, IMainPageHelper
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(this);
        }

        INavigation IPage.NavigationProxy { get => Navigation; }

        public async Task<string> AskForString(string questionTitle, string question)
        {
            string result = await DisplayPromptAsync(questionTitle, question);
            return result;
        }

        public async Task<bool> YesNoAlert(string title, string message)
        {
            bool answer = await DisplayAlert(title, message, "Yes", "No");
            return answer;
        }
    }
```    

Points to note:

* This page implements the interface `IMainPageHelper`
```C#
public partial class MainPage : ContentPage, IMainPageHelper
```
* Failure to implement any of the interface methods would result in a compile time error
* This page is type `MainPage` but is also of type `IMainPageHelper`
* A refernce to itself (`this`) is passed by parameter to the ViewModel. The ViewModel expects a type `IMainPageHelper` (and NOT `MainPage`)
```C#
BindingContext = new MainPageViewModel(this);
```
* The methods `AskForString` and `YesNoAlert` invoke the page specific methods `DisplayPromptAsync` and `DisplayAlert` respectively. These are in a parent class of `ContentPage` and would not normally be available to the view model. For test purposes, we could of course swap them out for alternatives (known as mocking).

## The ViewModel `MainPageViewModel`
The interesting part of the ViewModel is in the constructor:

```C#
    public MainPageViewModel(IMainPageHelper p) : base(p.NavigationProxy)
    {
        viewHelper = p;
        ButtonCommand = new Command(execute: async () =>
        {
            string name = await viewHelper.AskForString("Enter Name", "What is your name?");
            if (name == null) return;

            bool save = await viewHelper.YesNoAlert("Confirm", $"Are you sure you want to set the name to {name}?");
            if (save)
            {
                Name = name;
            }

        });
    }
```        

Points to note:
* The constructor has a single parameter type `IMainPageHelper`. 
   * In ordinary use, this will be a reference to the page `MainPage`
   * However, this could be any concrete class that implements this interface. In principle this means it can be swapped out for a test class that replaces a page with test code (mocking).
* In the button command, note the simplicity of the code logic (thanks again to `async` and `await`).

## Reflection on Popups
The [Microsoft Documentation](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/pop-ups) shows us how to create a number of modal popups for simple prompts. With the help of `await` and `async`, these APIs are a very simple way to display modal data and/or capture user input.

However, these are all methods inherited into the `ContentPage` class and were thus unavailable to the ViewModel. 

The ViewModel needs a way to invoke these methods whilst maintaining the core principle of maintaining loose coupling between the ViewModel and the View. Interfaces can often be used for this purpose with relatively little effort.



----

[Table of Contents](README.md)