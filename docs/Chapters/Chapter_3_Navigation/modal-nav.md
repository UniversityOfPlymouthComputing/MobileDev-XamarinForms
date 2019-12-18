[Back](README.md)

----

## Modal Pages
There is another common navigation scheme often used, and that is modal (not model!) presentation.

We see modal windows on desktop computers - windows that pop up and present and/or request information. A key feature is that these windows also prevent you clicking on anything else in an application until it is dismissed.

The equivalent is a modal page as we shall demonstrate here.

> The code for this section is in the folder [ModalPresenttion](/code/Chapter3/Modal/ModalPresentation)

Open the solution `ModalPresentation.sln`, build and run. Note the following:

* The first page is contained within a `NavigationPage`, although this is not being used except to display the page title.
* When you click the "Enter Price" button, a new page is animated over the top of the whole screen, including the navigation bar.
* Note that a dedicated button was added to discuss the modal page (in code). There is no back button.

> Presenting modally neither requires a `NavigationPage` or `TabbedPage`. If we remove the `NavigationPage`, this application still works.

Familiarise yourself with the code. We will not examine some of the aspects that make this application different.

### The Main Application `App` Class
In the start up code, there is nothing especially new

```C#
    MainPage = new NavigationPage(new MainPage());
```

> Experiment = try removing the `NavigationPage` to show the application works without it

### The MainPage
The XAML has nothing particularly new except to note it's title is specified. The code behind should also be familiar

```C#
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel(this.Navigation);
    }
```        
Note again how the ViewModel is being provided with a reference to the (correct) `Navigation` reference for the page that binds to it.

### MainPageViewModel
Again, it is the ViewModel where most of the code resides. Two key aspects are as follows:

The constructor subscribes to a message to receive data back from the modal page:

```C#
    MessagingCenter.Subscribe<PriceEditPageViewModel, double>(this, "PriceUpdate", (sender, arg) => Price = arg);
```

The button command invokes the `PresentModalPage()` method:

```C#
    private void PresentModalPage()
    {
        //Hook up next page and associated view model
        var modalPage = new PriceEditPage();
        modalPage.BindingContext = new PriceEditPageViewModel(modalPage.Navigation, Price);

        //Present modally
        _ = Navigation.PushModalAsync(modalPage, true);
    }
```

Note the following:

* The property `modalPage.Navigation` is not a reference to a `NavigationPage`. It is the default property on an ordinary `ContentPage`. This is **not** using hierarchial navigation
* The method to present a page modally is `PushModalAsync` as opposed to the `PushAsync` method used in hierarchial navigation.

### PriceEditPage
This page and its ViewModel are also fairly standard. The XAML has nothing new, and no code was added to the code-behind. 

> The only point that stands out is that we need a UI element (button in this case) to dismiss this page. Again, we are not using a `NavigationPage` when presenting modally.

We will look at key aspects in ViewModel, and in particular the constructor:

```C#
    public PriceEditPageViewModel(INavigation nav, double price = 0.0) : base(nav)
    {
        //Data passed forwards
        Price = price;

        ButtonClose = new Command(execute: () =>
        {
            // Again use MessageCentre to send data back - this time let the compiler infer the types

            //MessagingCenter.Send<PriceEditPageViewModel, double>(this, "PriceUpdate", Price);
            MessagingCenter.Send(this, "PriceUpdate", Price);
            
            //Dismiss this page
            _ = Navigation?.PopModalAsync(true);
        });
    }
```        

Note the following:

* Data is passed forwards by parameter
* The button command performs two key actions:
   * Sending updated data to any subscribed object with `MessagingCenter.Send(this, "PriceUpdate", Price);`. Note the type inference used here to simplify the method API.
   * Dismissing itself if a navigation property is set with `_ = Navigation?.PopModalAsync(true);` 
* This object is very stand-alone.

## Reflection on Modal Pages
There is not a great deal to say about modal pages. They tend to be used where the attention of the user is required. It typically occupies the whole page and requires to user to dismiss the page before being able to procede.

There is still a significant amount of work involved, especially with passing data forwards and back. 

Sometimes we simply want to pop up some information on the screen, or ask a simple yes/no question. For that, we can use alerts, and that is the next topic

[Next - Alerts and Popups](alerts.md)