[Contents](/docs/README.md)

----

# Model-View-ViewModel (MVVM)
In the previous example, a Model-View-Controller architecture was used. For simple applications, this works well with the following caveats:

- It does not tends to scale well as controllers get complicated
- The controller is tightly coupled with the view, making it difficult to unit test

Despite this, many excellent applications have been written with MVC. 

> The Model-View-ViewModel (MVVM) architecture is similar to MVC, only it is not tightly coupled to the view making it easier to test. XAML was designed with MVVM in mind.

[From the Microsoft Documentation](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/xaml/xaml-basics/data-bindings-to-mvvm) (accessed 24/07/2019)

> The Model-View-ViewModel (MVVM) architectural pattern was invented with XAML in mind. The pattern enforces a separation between three software layers — the XAML user interface, called the View; the underlying data, called the Model; and an intermediary between the View and the Model, called the ViewModel. The View and the ViewModel are often connected through data bindings defined in the XAML file. The BindingContext for the View is usually an instance of the ViewModel.

_From: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/xaml/xaml-basics/data-bindings-to-mvvm (accessed 24/07/2019)_

The key point here is the connection between the View Model (which probably sounds like a Controller at this point) and the View: _they are not tightly coupled_.

- The View knows something of the ViewModel, but the ViewModel does not know any specifics about the View
- The View Model knows something of the Model, but the Model knows nothing about the ViewModel

![MVVM Layers](img/mvvm-visibility.png)

This is achieved through the use of a binding layer between the view model and the view.

![MVVM](img/mvvm.png)

The example that is developed in this section is also very simple. It starts with a MVC architecture and is evolved incrementally to a MVVM. Although the MVVM mode is longer and possibly overkill for such a simple application, it is 

----
[Contents](/docs/README.md)
