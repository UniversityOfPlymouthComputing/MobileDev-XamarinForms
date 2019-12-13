[Prev - Basic Navigation Part 5](basic_navigation_5.md)

---

## MVVM Navigation
In the previous section, we began focusing on the mechanics of navigation, which in itself straightforward.

We then looked at examples of moving data forward and backwards through the navigation hierarchy, which hopefully illustrated some of the complexity and problems that might be encountered (and we've not even discussed testing!).

I confess, the previous section can feel like it jumps around constantly looking for workarounds to problems as they arise. This is a real danger if you start coding without a clear architecture in mind. It is also why we have something known as design-patterns.

> Design Patterns are known solutions for common problems

This is where MVVM steps in and can really help bring a consistent and flexible structure. I am not going to be purist here, but we are going to be adhering to the essence of MVVM even if it makes our code more verbose.

> Some professional engineers tell me that they _always_ use a 3rd party MVVM framework when they write apps (such as Prism or mvvmcross, which build on the foundations of Xamarin.Forms and .NET).
>
> This will get them very clean MVVM solution that lends itself to unit testing, but such frameworks also have a learning curve.
>
> Therefore, at this stage, I prefer to avoid 3rd-party dependencies and stick to the foundation frameworks that come bundled with Xamarin.Forms, even if that means compromising on MVVM purity.

## MVVM Navigation - 1
Open the solution in the folder [MVVM_Navigation-1](/code/Chapter3/NavigationControllers/2-MVVM_Based/MVVM_Navigation-1)

Run the code, and experiment. Have a look through each MVVM component to familiarise yourself with the structure.

> What is presented here is _a way_ of approaching MVVM and navigation, the _the_ way. I am confident there will be those that have suggestions why this is not perfect. However, remember that we are trying to keep this clean and simple.

If you inspect the code, a few things may be apparent:

* There is a single data model class `PersonDetailsModel` that supports binding
* Each page has an accompanying ViewModel class with the following common features:
   * Public properties that bind to the view elements
   * An instance of a data model
   * A reference to the `Navigation` stack
   * An event handler to intercept changes to the model object (with the option to perform any conversions)
   * A method to navigate to the next page, passing data forward by parameter
* Code replication across all the ViewModels

The last point may sound negative, but it also flags that each view model shares a common pattern, and that this can be factored out (as we see MVVM_Navigation-3)

### The `App` Class
This is our entry point, in which several steps have been added:

```C#
   ...
   //Instantiate the data model
   PersonDetailsModel m = new PersonDetailsModel("Anon");

   //Instantiate the viewmodel, and pass it a reference to the model
   FirstPageViewModel vm = new FirstPageViewModel(m);

   //Instantiatge the view, and pass it a reference to the viewmodel
   FirstPage firstPage = new FirstPage(vm);

   //Navigate in the first page
   MainPage = new NavigationPage(firstPage);
```

A few observations:

* The View has a typed reference to the ViewModel. The ViewModel does not need knowledge of the View that binds to it's properties (a property of the binding mechanism).
* The ViewModel similarly has a typed reference to a Model instance. The Model is standalone and does not depend on the ViewModel.
* There is quite a lot of replication

The basic notion is for each Model View and ViewModel to be able to exist as an island (as much as possible), with the long term view of supporting reuse and testing.

## Data Model - `PersonDetailsModel`
Let's begin by looking at the Model. It's fairly simple and contains two bindable properties:

* Name (string)
* BirthYear (int)

The model implements the interface `INotifyPropertyChanged` and respective `PropertyChanged` property.

There is not a lot to dicuss about this class except that it's standalone,  testable (not that there is much to test) and *it is a reference type*. This might be relevent if we wish to pass by reference later.

A single instance of the model was instantiates in the `App` class (see previous section above) and passed to the 

## FirstPage - MVVM
There are of course three components of interest: The Model, View and the View Model.

### The View
The view is split across two files, `FirstViewPage.xaml` and `FirstViewPage.xaml.cs` with very little in the code-behind except a constructor:

```C#
   public FirstPage(FirstPageViewModel vm = null)
   {
      InitializeComponent();
      BindingContext = vm ?? new FirstPageViewModel();
   }
```        

Aside from hooking up references to UI components (in `InitializeComponent()`),  the only role is to **set the binding context to the ViewModel**.

_Any exceptions to this will be explicitly overridden in the XAML_

The rest of the view is defined in the XAML. Much of what is in the XAML file we've seen before. The main change is the following attribute of the `<ContentPage>` element:

```XML
<ContentPage
...
NavigationPage.BackButtonTitle="{Binding BackButtonTitle}"
...

</ContentPage>
```

This sets the back-button title for Navigation purposes (at least for iOS).

### The ViewModel
This is where most of the code is. By convention, it has the same name as the view, with the suffix ViewModel on the end.

`FirstPage` => `FirstPageViewModel`

Read through the ViewModel code and familiarise yourself with the contents. A few key points will be highlighed:

* It has it's own reference to `Model`. This is acquired either from a previous view model (via the contructor parameter), or is instantiated locally. Here is the constructor:

```C#
   public FirstPageViewModel(PersonDetailsModel m = null)
   {
      //Instantiate the model if one is not passed by parameter
      Model = m ?? new PersonDetailsModel("Anonymous");

      //Subscribe to changes in the model
      model.PropertyChanged += OnModelPropertyChanged;

      //The command property - bound to a button in the view
      ButtonCommand = new Command(execute: NavigateToYearEditPage);
   }
```        

An important point is the line `model.PropertyChanged += OnModelPropertyChanged;`
This subscribes to any changes in the Model object (see the calls to ` OnPropertyChanged()` in the `PersonDetailsModel`). 

> Note the extra 'Model' word in `OnModelPropertyChanged`. This is in contrast to `OnPropertyChanged`

In this simple example, any changes to the Model are passed through to the View-ViewModel binding layer:

```C#
   ...

   //Bound Data Properties Exposed to the View (read only in this case)
   public string Name => Model.Name;
   public int BirthYear => Model.BirthYear;

   ...

   //Watch for events on the model object
   protected void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
   {
      if (e.PropertyName.Equals(nameof(Model.BirthYear)))
      {
            OnPropertyChanged(nameof(BirthYear));
      }
      else if (e.PropertyName.Equals(nameof(Model.Name)))
      {
            OnPropertyChanged(nameof(Name));
      }
   }
```        

Remember:

* This page only present data, it does not edit it, so the properties `Name` and `BirthYear` are read-only.
* Calling `OnPropertyChanged` will inform the View-ViewModel binding layer to update the view objects bound to the respective property.

When the edit button is tapped, commanding is used to invoke the following method:

```C#
   void NavigateToYearEditPage()
   {
      // NOTE that Model is a reference type
      YearEditPageViewModel avm = new YearEditPageViewModel(Model); //VM knows about its model (reference)

      // Instantiate the view, and provide the viewmodel
      YearEditPage about = new YearEditPage(avm); //View knows about it's VM
      Navigation.PushAsync(about);
   }
```        

We are trying to keep the navigation logic to the ViewModel. First we instantiate the next view model, and pass the Model (a reference type encapsulating model data)

```C# 
   YearEditPageViewModel avm = new YearEditPageViewModel(Model);
```   

Next we instantiate the *next* page, passing it a reference to the view model (for it to bind to).

```C#
   YearEditPage about = new YearEditPage(avm); //View knows about it's VM
```

Finally, the navigation itself is performed:

```C#
   _ = Navigation.PushAsync(about);
```

where the `Navigation` property is a globally accessible object. For convenience, a reference to this is added to the ViewModel:

```C#
   protected INavigation Navigation => Application.Current.MainPage.Navigation;
```

In addition, note that this view model has a concrete reference to the next view. 

> Strictly speaking, these both add depednencies to view based objects (the next view, not the current view). For MVVM, some dissaprove whereas others may be more indifferent.
>
> Third-party frameworks [such as Prism](https://prismlibrary.github.io/docs/) utilise somewhat advanced techniques to further decouple ViewModels from View objects while still supporting navigation and unit testing.
>
> For now, it is enough to be aware that what is presented here is limited in this respect and that such frameworks do exist.

## The `YearEdit` Page
For the view, once again the are mostly in the code-behind, which is almost empty:

```C#
   public YearEditPage(YearEditPageViewModel vm = null)
   {
      InitializeComponent();

      //Bind to the view model
      BindingContext = vm ?? new YearEditPageViewModel();
   }
```        
The main changes are in the ViewModel, with much of the code being the same or similar to before.

Once again, the model (reference type in this case) is passed by parameter and the view model observes any changes as before.

```C#
        public YearEditPageViewModel(PersonDetailsModel model = null)
        {
            Model = model ?? new PersonDetailsModel("Anon");
            model.PropertyChanged += OnModelPropertyChanged;
            ButtonCommand = new Command(execute: NavigateToNameEditPage);
        }
```        

A quick inspection and you will notice much that is in common with the previous view model. One key difference is that the `Year` property is now editable.

If we look at the properties that are bound to the view, we see one of the changes:

```C#
   public string Name => Model.Name;   //Read Only
   public int BirthYear                //Read / Write
   {
      get => Model.BirthYear;
      set
      {
            if (value != Model.BirthYear)
            {
               Model.BirthYear = value;
               OnPropertyChanged();
            }
      }
   }
```        

There is a two-way binding between the `BirthYear` property and the slider value in the view. 

* When the slider is moved, this invokes the setter of `BirthYear` which in turn updates the model `Model.BirthYear = value;`. 

* No special conversion was needed in this instance, but it would be possible had there been a mismatch between the model data type and the view.

Another difference is in the command handler for the edit button `NavigateToNameEditPage`

```C#
   void NavigateToNameEditPage()
   {
      MessagingCenter.Subscribe<NameEditPageViewModel, string>(this, "NameUpdate", (sender, arg) =>
      {
            Model.Name = arg;
      });

      // NOTE that Model.Name is an immutable reference type
      NameEditPageViewModel vm = new NameEditPageViewModel(Model.Name); //VM knows about its model (reference)

      // Instantiate the view, and provide the viewmodel
      NameEditPage aabout = new NameEditPage(vm); //View knows about it's VM
      _ = Navigation.PushAsync(aabout);
   }
```        

Before we look at the specifics, it's good to remind ourselves of what we're trying to achieve.

> The NameEdit view requires the option to cancel any edits. This is achieved by simply clicking the back button. An additional save button is added to commit any edits.



---














---
