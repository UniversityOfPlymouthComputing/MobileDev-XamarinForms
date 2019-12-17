[Contents](README.md)

----

# Tabbed Pages
In the section on hierarchial navigation, an application was split into separate pages. The usual pattern is to drill down into more fine-grained levels of detail.

Sometimes we have distinct sections in our application that are not related hierarchically. 

Consider a personal organiser, which supports sections for the following:

* Mail - send and receive emails
* Calendar - add, read and delete calendar events
* Contacts - add, edit and lookup contact details
* Search - search for keyword in any of the above

Ultimately, all three may be quite distinct views within the same application.

* They may render different data from the same database, or a different database.
* Some may operate completely independently of each other
* There may be occasions where information from one is communicated to another. For example: 
   * In the contacts view, there may be a link to send an email
   * Search results may link to any other the others

One of the standard ways to structure such an application is to use a [TabbedPage](https://docs.microsoft.com/xamarin/xamarin-forms/app-fundamentals/navigation/tabbed-page)

> Scan through this documentation page, and familiarise yourself with the scope of content. We will not and cannot go into every detail.

In this section, a simple application is presented. This application is also a nice excuse to demonstrate [Xamarin Essentials](https://docs.microsoft.com/xamarin/essentials/) which is now included and configured as standard in Xamarin.Forms

> I strongly suggest you take some time to look at the documentation of Xamarin.Essentials. Some great work has been done to abstract some of the most useful device features.

With this in mind, let's now look at an example of an application with Tabbed Pages.

----

[Next - Tabbed Pages](tabbedpage.md)