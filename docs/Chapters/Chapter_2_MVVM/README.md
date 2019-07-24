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

The View Model and Model objects will contain no knowledge or reference to UI types. Only the view (XAML and code-behind) has knowledge of such types. The View Model and Model objects can therefore be compiled as part of a simple unit testing or even command line project.

This is achieved through the use of a _binding layer_ between the ViewModel and the View. Xamarin.Forms comes with a baked-in binding mechanism, but others are available. This is illustrated in the next figure:

![MVVM](img/mvvm.png)

Consider two possible scenarios:

1. The user makes a change to a UI element that has a property bound to a ViewModel property. This automatically causes an update to the ViewModel property without the need to write any code. The setter of that property may (or may not) then process the value and pass it to the Model via a public API. A common task for ViewModel is data conversion and updates to the Model.
2. A waiting network connection (invoked from within the Model) returns a new value and asynchronously updates a value in the model. This is advertised as an event. The ViewModel is listening for such events and so will observe the change. It then changes one of the bound properties which in-turn, provokes an automatic update in the UI

> The ViewModel is therefore the arbitrator between events in the View and events in the Model, marshalling data between them.

**Application State**
The Models define the _application state_ - Within the Model objects are the actual data, plus any methods that operate on the data. The ViewModel should not contan any domain specific data. A good Model should be self contained and highly testable.

**UI State**
The ViewModel also has state, but it is nothing to do with the domain Model data. These are typically properties relating to UI State, such as whether a Label is visible (`bool`), the selected row of a table (`int`) etc. A good view model will also be highly testable. In fact one of the objectives is to be able to simulate UI logic (clicks, text input etc.) through calling methods via a unit testing framework.

_You need to see it to fully appreciate this_

So how does this work? Starting with the interface between the ViewModel and View, some key conceptual points to note are as follows:

- Through "bindings", _properties_ of user interface objects will be bound to properties in the view model. This means when one is changed, the other may be automatically updated without having to write any code.
   - Often there is a one-to-one mapping, such as the `Text` propery of a `Label` (type `string`) to a `string` property in the view model.
   - Where there is no one-to-one type mapping, a _Value Converter_ can be inserted between them so that the ViewModel can avoid using UI types. 
- Bindings can be uni-directional or bi-directional. 
    - For uni-directional bindings, you also have control in which direction changes are propagated.
- Content pages and UI Components have a property called the `BindingContext` - this is typically the ViewModel
- It is also possible to bind UI elements to other UI elements (no view model involved)
- There can be multiple View Models and Models for any given View
- It is often the case that the View instantiates the ViewModel.

For the interface between the ViewModel and Model, some key conceptual points to note are as follows:

- It is often the case that the ViewModel instantiates the Model
- The link between them may be limited to the ViewModel calling synchronous (public) APIs on the Model, and storing returned values.
    - If the returned values are saved in bounded properties in the ViewModel, then the UI may be updated accordingly
- The ViewModel can also invoke Asynchronous methods on the Model - the call-back is typically performed using .NET events

Ok, that's a lot of stuff and I suspect it does not yet hold much meaning until you see it in practise. For this, we need a simple example to illustrate all the key points.

## Part 1 - The Wise Sayings Application

Like the examples before it, the example that is developed in this section is also trivially simple. This is on purpose. It starts with the familiar MVC architecture, and is evolved incrementally to a testable MVVM archirecture. Although the resulting MVVM mode is longer and possibly overkill for such a simple application, it is hopefully illustrative. You can then apply it yourself to more real-world applications that scale beyond the trivial.

All the code is available on the GitHub site. It is **strongly suggested** you open and examine each example as we progress.

[Part 1 is here](/code/Chapter2/Bindings/HelloBindings-01)



----
[Contents](/docs/README.md)
