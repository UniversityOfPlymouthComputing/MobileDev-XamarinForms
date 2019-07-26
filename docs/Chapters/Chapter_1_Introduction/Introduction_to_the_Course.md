[Table of Contents](README.md)

# Introduction to the Course

This is a course in cross platform mobile software development using Xamarin.Forms and Microsoft Visual Studio 2019. The focus is on cross platform software development for mobile phones (as opposed to tablet or Desktop computers). However, with further study, other targets can easily be accomodated.

> "Xamarin.Forms is a cross-platform UI toolkit that allows developers to efficiently create native user interface layouts that can be shared across iOS, Android, and Universal Windows Platform apps." 
https://docs.microsoft.com/xamarin/get-started/

A key feature of Xamarin is that the code you produce is _native_ as opposed to interpreted (such as JavaScript). Apps based on native code typically provide better performance and can be written in a number of ways, including:

Developer Tool | Language(s) | Targets
---------------|-------------|--------
Android Studio | Java / Kotlin | Android Only
Apple Xcode | ObjectiveC / Swift | iOS Only
Xamarin.iOS | C#.NET | iOS Only (see below)
Xamarin.Android | C#.NET | Android Only (see below)
Xamarin.Forms | C#.NET | Android and iOS
Google Flutter | Dart | Android and iOS
React Native | JavaScript | Android and iOS

A few points to note:

- It is possible to write Xamarin.iOS and Xamarin.Android and share code. With the right architectural decisions, this can be mostly limited to UI code but in the same language (typically C#). Each variant provides C# wrappers around the native platform (iOS or Android) APIs, therefore you still need knowledge of the APIs for both platforms. An excellent treatment of this can be found in the book [Xamarin In Action](https://www.manning.com/books/xamarin-in-action) by Jim Bennett.

- Xamarin.Forms is actually three projects rolled into one:
   - A Xamarin.iOS Project
   - A Xamrin.Android Project
   - A Shared Project (Forms)
   
   The advantage is that much of the code for both Android and iOS, including most of the UI, is contained in the Shared Project. In some cases 99% code can be shared between both platforms, with a small amount of platform-specific boiler-plate code. This is made possible through clever abstraction of platform specific features. You also retain the possibility to write native code where the abstractions do not exist or meet requirements.
   
   Xamarin.Forms also allows you to specify your user interface layout using a declarative XML based language known as XAML. Components employ _renderers_ to draw each Xamarin Forms component with an equivalent platform native control (where one exists). For example, a Xamarin Button placed in a view heirarchy will end up as a UIButton for iOS or an Android Button for Android. This means all the standard accessibility and localisation features continue to operate as normal. Arguably, it's easier for the Xamarin team to keep up with evolving platforms.
   
   Google Flutter is relatively new, and takes a slightly different approach. Like Xamarin.Forms, UI is specificed declaritively and not with a UI designer. However, the controls are drawn to _look like_ native controls on each respective platform. In essence, it mimicks the native UI of each platform.
   
   Xamarin.Forms, Google Flutter and React Native all have their merits, so to choose a "best" approach is somewhat artificial and risks being more preference than science. As this course builds on previous courses in C# and design patterns (at the University of Plymouth) and leverages [Microsoft Azure](https://azure.microsoft.com/), it made sense to opt for Xamarin.Forms and C# (front to back). From an educational perspective, Xamarin.Forms is also a good platform to expose learners to abstraction techniques using Object Orientated patterns and the mature C# language and .NET frameworks. At the time of writing Xamarin Forms is on it's 4th version, and is fairly mature and widely adopted.
   
## Approach
A taught module is shorter than many might first think. Over a 13 week period, it typically consumes 100Hrs including lectures, practicals, self-study, assignments and assessment. 
It is interesting to contrast this with a full time job. The nearest equivalent is to start a new job, and spend approximately 3 weeks full-time learning a new platform _and_ delivering a working demonstration at the end. Oh, and these are only second year students, not graduates. This helps set some idea of expectation.

There are some guiding principles in this course:

- Teach the minimum API, Terminology and Concepts so the learner can teach themselves from the official documentation
- Assume only minimal C# and .NET experience (Java would probably be ok) and don't be afraid to repeat topics.
- There is no such thing as an explaination that is too simple. Equally there are no silly questions (only silly answers ;)

> Anticipate the difficult by managing the easy. 
> Lao Tzu    

It is sometimes good to see examples of what it possible, but only to set aspiration. In the context of this course, special effort is made to provide examples that focus right in on a particular concept (or related concepts). Throwing many new concepts into the ring at once can be overwhelming and demotivating. Better to study one simple concept well so learners can  apply it to more complex examples (often their own) when the time comes. This is not the time and place for fancy and showy examples. This can even mean forgoing elegance for simplicity.

Loading the short term memory can also result in _cognitive overload_ . Where possible, effort is made to keep all relevent information in the near field of view. Where not possible, use a diagram to show the bigger picture (e.g. relationship between objects).




