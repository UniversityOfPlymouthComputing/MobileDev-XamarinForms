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

This is summarised in the following figure.

![MVVM Layers](img/mvvm-visibility.png)

Inside the view model, you won't expect to see any types that are specific to Xamarin.Forms. You should instead see methods which use C#.net types. This makes it simpler to unit test.

This is achieved through the use of a _binding layer_ between the view model and the view. Xamarin.Forms comes with a baked-in binding mechanism, but others are available. This is illustrated in the next figure:

![MVVM](img/mvvm.png)

Some key conceptual points to note:

- Through "bindings", properties of user interface objects will be bound to properties in the view model. This means when one is changed, the other is automatically updated
   - Often there is a one-to-one mapping, such as the `Text` propery of a `Label` (type `string`) to a `string` property in the view model.
   - Bindings can be uni-directional or bi-directional
 - Content pages and UI Components have a property called the `BindingContext` - this is typically the ViewModel
 - It is also possible to bind UI elements to other UI elements (not view model required)

## Part 1 - The Wise Sayings Application

The example that is developed in this section is also very simple. It starts with a MVC architecture and is evolved incrementally to a MVVM. Although the MVVM mode is longer and possibly overkill for such a simple application, it is 

----
[Contents](/docs/README.md)
