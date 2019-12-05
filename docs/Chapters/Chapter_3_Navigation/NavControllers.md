[Contents](README.md)

---

The code examples for this section are to be [found in this folder](/code/Chapter3/NavigationControllers). There are two sub-folders:

1. [View Based](/code/Chapter3/NavigationControllers/1-View_Based) - this contains a series of projects that provide examples of navigation based purely on XAML and the code-behind (View Objects). This allows us to focus on navigation itself with simple data binding in some cases.
2. [MVVM Based](/code/Chapter3/NavigationControllers/2-MVVM_Based) - The adopts the same example application in 1 and adopts a simple MVVM pattern.

# Hierarchial Navigation
One of the most apparent differences between a desktop computer and a mobile device is screen size. It is therefore common to limit the information on screen. One popular approach on both iOS and Android is to organize and display information hierarchically.

The first page may display the highest level, skipping many details

A user interaction may select an item so they can drill down to a finer grained level of detail. This process might even repeat again, displaying finer and finer grained levels of detail.

> The settings application on both iOS and Android are examples of this. At the top level, there are broad categories. As you tap these items, you are taken to a deeper level of information. This process can go several layers deep until you find the information or setting of interest.

## A Navigation Stack
A common type is hierarchial navigation.

> As each page is _pushed_ on top of the other, a Navigation Controller maintains a _stack_ of **views**. As the user navigates back, the top view is popped off the stack.

As each page is displayed, so few things happen:

* A new page of information is pushed onto the navigation _stack_ and displayed
* A title bar informs the user of the page title and often provides a button to navigate back to the previous page
* Animations are run between pages to help the user visualize where they are in the page hierarchy.

Very often this design is used in conjunction with tables or types of 'collection views'. A common pattern is to tap a table row and drill down into finer detail.

We are going to focus on just the navigation aspects first and introduce collection views (including tables) later.

## Key Issues with Hierarchial Navigation
When an application is broken into a hierarchy of pages, there are some _architectural_ challenges to consider: 

* When a user interacts with an app (e.g. selects a table row, button etc.), a new page has to be selected and pushed on the stack for presentation.
    * Very often, some relevant data may need to be passed forward for display and editing purposes
    * At this point, the user may add,edit or delete information.
    * It needs to be decided whether edits are immediate or reversible
* As a user navigates back up the hierarchy, edited or new information may need to be passed back up the chain and _before_ the view is destroyed.
   * The default 'back button' typically cannot be intercepted (let's get that one in early!)
   * We need to decide early if any changes are saved or discarded
   * Additional interface elements may be needed to 'save / commit / undo' changes.
* We will also try and keep to the MVVM pattern. 
   * Strictly, it should be the ViewModel that contains the navigation logic and it should be unit-testable.
   * Ideally a ViewModel should not depend on any views. However, we at this stage, we might need to compromise.
   * Xamarin.Forms navigation is _view based_. You need a reference to the view to push it on the navigation stack

Navigation in Xamarin.Forms has a reputation of being complicated. The underlying reason for this is related to strict adherence to the MVVM pattern to support unit testing of ViewModels.

> As we will soon learn, Xamarin.Forms navigation is _view based_ making _pure_ MVVM complex to implement.
>
> There are third-party libraries that build upon Xamarin.Forms and .NET to make pure MVVM navigation much easier. Examples include [the prism library](https://prismlibrary.github.io/), [mvvm lite](http://www.mvvmlight.net/) and [mvvmcross](https://www.mvvmcross.com)

I am going to try and maintain the essence of MVVM but forgo being purist. They key message is to be aware of the issues and limitations of the simpler approach. I am also keen to minimize the number of 3rd party dependencies (from a teaching point of view, this can cause logistical headaches).

--- 

[Next - Basic Navigation Part 1](basic_navigation_1.md)

