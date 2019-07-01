[Previous](README.md)

# Introduction to the Course

This is a course in cross platform mobile software development using Xamarin.Forms and Microsoft Visual Studio 2019. The focus is on cross platform software development for mobile phones (as opposed to tablet or Desktop computers). However, with further study, other targets can easily be accomodated.

> "Xamarin.Forms is a cross-platform UI toolkit that allows developers to efficiently create native user interface layouts that can be shared across iOS, Android, and Universal Windows Platform apps." 
https://docs.microsoft.com/en-us/xamarin/get-started/

A key feature of Xamarin is that the code you produce is _native_ as opposed to interpreted (such as JavaScript). Apps based on native code typically provide better performance and can be written in a number of ways, including:

Developer Tool | Language(s) | Targets
---------------|-------------|--------
Android Studio | Java / Kotlin | Android Only
Apple Xcode | ObjectiveC / Swift | iOS Only
Xamarin.iOS | C#.NET | iOS Only
Xamarin.Android | C#.NET | Android Only
Xamarin.Forms | C#.NET | Android and iOS
Google Flutter | Dart | Android and iOS
React Native | JavaScript | Android and iOS

A few points to note:

- It is possible to write Xamarin.iOS and Xamarin.Android and share code. With the right architectural decisions, this can be mostly limited to UI code but in the same language (typically C#). Each variant provides C# wrappers around the native platform (iOS or Android) APIs, therefore you still need knowledge of both platforms. An excellent treatment of this can be found in the book [Xamarin In Action](https://www.manning.com/books/xamarin-in-action) by Jim Bennett.

- Xamarin.Forms is actually three projects rolled into one:
   - A Xamarin.iOS Project
   - A Xamrin.Android Project
   - A Shared Project (Forms)
   
   The advantage is that much of the code for both Android and iOS, including most of the UI, is contained in the Shared Project. In some cases 100% code can be shared between both platforms. This is made possible through clever abstraction of platform specific features. You also retain the possibility to write native code where the abstractions do not exist.
   
   Xamarin.Forms also allows you to specify your layout using XAML (an XML document). Components employ _renderers_ to draw the Xamarin Forms component with an equivalent platform native control. For example, a Xamarin Button played in a view will end up as a UIButton for iOS or an Android Button for Android. This means all the standard accessibility features continue to operate as normal.
   
   Google Flutter is relatively new, and takes a slightly different approach. Like Xamarin.Forms, UI is specificed declaritively and not with a UI designer. However, the controls are drawn to look like native controls on each respective platform.
   
   Both Xamarin.Forms, Google Flutter and React Native all have their merits, so to choose a "best" approach is somewhat artificial and risks being opinionated. As this course builds on previous courses in C# and design patterns (at the University of Plymouth) and leverages [Microsoft Azure](https://azure.microsoft.com/en-gb/), it made sense to opt for Xamarin.Forms. From an educational perspective, Xamarin.Forms is also a good platform to expose learners to abstraction techniques using Object Orientated patterns and the mature C# language and .NET frameworks.
   

