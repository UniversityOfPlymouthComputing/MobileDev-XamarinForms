[Table of Contents](README.md)

# First Exploration in Xamarin Forms
This section is intended to familiarise you with the tools and to demonstrate some of the key concepts.
The first task is to create a blank forms app.

- For the Mac, [follow this link](create-project-mac.md)
- For Windows, [follow this link](create-project-pc.md)

For more detailed information on setting up the Android Emulator, [see the  Microsoft page](https://docs.microsoft.com/en-us/xamarin/android/get-started/installation/android-emulator/)

To setup a real device for development, [see the guide from Microsoft](https://docs.microsoft.com/en-us/xamarin/android/get-started/installation/set-up-device-for-development)

Before you try and run anything, take some time to explore these projects. There are some important points to note:

- A Solution is a collection of projects
- A Forms Solution is actually two apps (one for iOS and the other for Android) and one shared library (the Forms project)
- The entry point of each is a Native App - each instantiates Forms code to create the UI etc.
- Each constituent project within the solution has it's own settings, so the Android project has distinct settings to iOS.

![Relationship between a Xamarin Native Project and the Xamarin.Forms Library](img/Xamarin-solution-relationships.png)

Let's now take an initial look at a single page Xamarin.Forms application.

##  (i) Hello World
It is customary to start every course with a Hello World. So much can be captured with such a simple example. Sometimes it is easier to show rather than explain, so we begin with a short video walkthrough.

<p align="center">
<a href="http://www.youtube.com/watch?feature=player_embedded&v=jlwr3PLytAw" target="_blank"><img src="http://img.youtube.com/vi/jlwr3PLytAw/0.jpg" alt="IMAGE ALT TEXT HERE" width="240" height="180" border="10" /></a>
</p>

*TASK*
Recreate what you saw in the video. This is an exercise in familiarisation more than anything, so don't worry if it all seems a bit strange.
   
## (ii) Unpicking what just happened
Ok, let's step back and look at what is happening here. It is suggested that having a high-level view helps to visualise the relationships between all the components, without which, it can all feel strange and out of control.

<img src="img/startup objects.png">

The `MainPage` class is what we are most interested in here. This is related to the single page with a label and button, as well as all the code to manage the UI. It is what is referred to as a _ContentPage_. As you build up your application, and add in nagivation, you will probably have multi Content Pages (typically one per screen of infomration).

Before the first content page, we see classes that are responsible for managing the application life cycle. Let's take a quick look.

### The App Class
This is the first Xamarin.Forms class that is instantiated. Before that are classes that are platform specific. We wish to focus on cross-platform for now.

The `App` class is built from two documents:

- App.xaml.cs
- App.xaml

Let's take a brief look at each in turn.

#### App.xaml.cs
````C#
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelloXamarinForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
````
First, consider the class declaration

```C#
public partial class App : Application
```

The parent class is `Application`, a Xamarin.Forms class that provides important hooks into the application lifecycle. iOS and Android have their own equivalent (e.g. the AppDelegate on iOS), but you can avoid writing platform specific code by using the App class and overriding methods such as OnStart().

> If you are interested, details can be found in the [Microsoft Documentation](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.application?view=xamarin-forms)

- This is a `partial` class declatation. This must mean that _somewhere_, there is another C# partial class declaration. More on that in the next section.

This class is also declared within a _namespace_
```C#
namespace HelloXamarinForms
```
Namespaces are a way to avoid name collisions. You can think of them as prefixes - so really we are declaring `HelloXamarinForms.App`. 

> Namespaces are helpful as there may be other classes called `App` maybe in your own code or a thirdparty library. Whenever we wish to refer to this particular class, we could type `HelloXamarinForms.App`. This can produce verbose code, so often you see the `using` keyword at the top of a source file. This helps us keep our code concise. Of course, for all the namespaces you use, if there are names which conflict among them, then you would still need to give the explicit name. Again, more on this later.

- Note also that the constructor calls `InitializeComponent()` - again, we will look at this more closely in the next section, but as we will see, it is related to the previous point.

- Finally, the `MainPage` property of `Application` is set to an insance of `MainPage`. This is something we may change once we start adding navigation features.

So quite a bit to think about here. Why is this a partial class? Where is the rest of the class? How come we have a XAML file alongside?

#### App.xaml
Alongside the C# file is a XAML file.
```XAML
<?xml version="1.0" encoding="utf-8"?>
<Application xmlns="http://xamarin.com/schemas/2014/forms" 
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" 
             xmlns:d="http://xamarin.com/schemas/2014/forms/design" 
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             mc:Ignorable="d" 
             x:Class="HelloXamarinForms.App">
    <Application.Resources>
    </Application.Resources>
</Application>
```
There are some clues in here:

```XAML
<Application ...
```
It is something to do with the Application class. There are also quite a few [XML namespaces](https://www.w3schools.com/xml/xml_namespaces.asp) being defined. We will talk more about these later.

```XAML
x:Class="HelloXamarinForms.App"
```
This suggests this is something to do with the App class

There is quite a lot of 'detail stuff' in a simple hello world application. 

- partial classes
- XML and XAML
- name spaces
- event handlers
- references to UI elements

