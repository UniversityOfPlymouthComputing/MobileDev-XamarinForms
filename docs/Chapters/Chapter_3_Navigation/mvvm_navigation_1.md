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

Run the code, and experiment editing the data using both the `Slider` control (for the year) and the `Entry` control (for the name).

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















---

