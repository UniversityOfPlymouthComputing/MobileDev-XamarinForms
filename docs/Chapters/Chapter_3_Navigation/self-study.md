[Table of Contents](README.md)

----


# Self Study Task 1

## Task 1.1 - Basic View Based Navigation
Starting with a blank Xamarin.Forms template, write a small application with two pages, page01 and page02. 

* Use a `NavigationPage` to display page01.
* Add a button to page01 that presents page02 using the `NavigationPage`


## Task 1.2 - Passing Data Forward
Modify Task 1.1 to include some data entry. 

* Add a label to page01 and bind it to a string property `Page0Title` in the code-behind (or a view model if you prefer)
* Add a label to page02 and bind it to a string property `Page1Title` in the code-behind (or a view model if you prefer)
* When the button is tapped on page01, pass the `PageTitle` string as a parameter so that it is displayed in page02
* Add an `Entry` control to page02 to allow you to edit the string. Use an event handler to the `Entry` control so that the string only updates when the return key is pressed

## Task 1.3 - Passing data forwards and backwards
Modify task 1.2 so that edited data is passed back to page01.

* It is suggested you use `MessagingCenter`.


# Self Study Task 2
Starting with a blank Xamarin.Forms application, create two pages page01 and page02, and display them in a `TabbedPage`. 

Add a slider, label and button to each page

* The slider value should be bound to a property `val` (type double) in the code behind (or a viewmodel if you prefer)
* The label text on each page should be bound to the value property of the slider on the same page (hint: use the stringformat in the binding). Alternatively you could bind to the `val` property.

The button is used to set the value on the other page. For example, if you move the slider on page01 to the value 1.0, and click the button, the slider on page02 is set to 1.0

* Create an event handler for the button. Use `MessagingCenter` to communicate between pages





----


[Table of Contents](README.md)