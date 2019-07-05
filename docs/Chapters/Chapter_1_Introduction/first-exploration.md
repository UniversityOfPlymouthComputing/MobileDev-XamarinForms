[Table of Contents](README.md)

# First Exploration in Xamarin Forms
This section is intended to familiarise you with the tools and to demonstrate some of the key concepts.
The first task is to create a blank forms app.

- For the Mac, [follow this link](create-project-mac.md)
- For Windows, [follow this link](create-project-pc.md)

For more detailed information on setting up the Android Emulator, [see the  Microsoft page](https://docs.microsoft.com/en-us/xamarin/android/get-started/installation/android-emulator/)

To setup a real device for development, [see the guide from Microsoft](https://docs.microsoft.com/en-us/xamarin/android/get-started/installation/set-up-device-for-development)

Before you try and run anything, take some time to explore these projects. There are some important points to note:

- A Solution is a collection of projects that may have dependencies between each other.
- A Forms Solution is actually two apps (one for iOS and the other for Android) and one shared library (the Forms project)
- The entry point of each is a Native _Platform Specific_ App - each instantiates Forms code to create the UI etc.
- Each constituent project within the solution has it's own settings, so the Android project has distinct settings to iOS.

![Relationship between a Xamarin Native Project and the Xamarin.Forms Library](img/Xamarin-solution-relationships.png)

Let's now take an initial look at a single page Xamarin.Forms application.

##  (i) Hello World
It is customary to start every course with a Hello World. So much can be captured with such a simple example that is is worth doing. Sometimes it is easier to show rather than explain, so we begin with a short video walkthrough.

### Mac OS
<p align="center">
<a href="http://www.youtube.com/watch?feature=player_embedded&v=zwc2u7w6d3Y" target="_blank"><img src="http://img.youtube.com/vi/zwc2u7w6d3Y/0.jpg" alt="IMAGE ALT TEXT HERE" width="240" height="180" border="10" /></a>
</p>

### Microsoft Windows
*TBD*

*TASK*
Recreate what you saw in the video. This is an exercise in familiarisation more than anything, so don't worry if it all seems a bit strange.
   
## (ii) Unpicking what just happened
Ok, let's step back and look at what is happening here. It is suggested that having a high-level view helps to visualise the relationships between all the components, without which, it can all feel strange and out of control. We can then dive into each aspect and try to demystefy what is going on.

<img src="img/startup objects.png">

The `MainPage` class is what we are most interested in here. This is related to a single page of content within which is placed a label and button. It also contains all the code to manage the UI and react to user events. It is what is referred to as a _ContentPage_. As you build up your application, and add in nagivation, you will probably have multiple Content Pages (typically one per screen of infomration).

Before the first content page, we see classes that are responsible for managing the application life cycle. Let's take a quick look at each in turn. Our focus will be on the cross platform code, which is where the developer is expecting to spend most of the time.

### The App Class
This is the first (cross-platform) Xamarin.Forms class that is instantiated. Before that are classes that are platform specific. We wish to focus on cross-platform for now and do our best to avoid touching any platform-specific code.

A single instance of the `App` class is instantiated by the platform-specific code. This App class is built from two documents:

- App.xaml.cs
- App.xaml

If you've come from other platforms, you might find this surprising. It certainly was to me! Let's take a brief look at each in turn.

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

The parent class is `Application`, a Xamarin.Forms class that provides important hooks into the application lifecycle. 

> If you are interested, details can be found in the [Microsoft Documentation](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.application?view=xamarin-forms)

iOS and Android have their own equivalent of course (e.g. the AppDelegate on iOS), but you can avoid writing platform specific code by subclassing the `Application` class and overriding methods such as `OnStart()`, as is shown above.

- This is a `partial` class declatation. This must mean that _somewhere_, there is at least one other [C# partial class](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods)  declaration. More on that in the next section.

This class is also declared within a _namespace_
```C#
namespace HelloXamarinForms
```
[Namespaces](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/namespaces/) are a way to avoid name collisions. You can think of them as _prefixes_ on Class, Structure and Interface names - so really we are declaring `HelloXamarinForms.App`. 

> Namespaces are helpful as there may be other classes called `App` maybe in your own code or a third-party library. Whenever we wish to refer to this particular class, we _could_ type `HelloXamarinForms.App`. This can produce verbose code, so often you see the `using` keyword at the top of a source file. This allows for implicit namespace qualifiers which helps keep code concise and readable. Of course, if there are names which conflict within the set of namespaces you use, then you would still need to give the explicit name. Again, more on this later.

- Note also that the constructor calls `InitializeComponent()` - again, we will look at this more closely in the next section, but as we will see, it is related to this being a partial class.

- Finally, the `MainPage` property of `Application` is set to an insance of our `MainPage` class. This is something we may change once we start adding navigation features. This very line is often the lowest entry point for an application.

So quite a bit to think about here. Why is this a partial class? Where is the rest of the class? How come we have a XAML file alongside? If you are confused, don't be put off (yet!), all should become clear.

#### App.xaml
Alongside the C# file is a XAML file. In fact, the the solution explorer, these two files seem to be implicitly linked in some way.

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
Ok, so this is something to do with the `Application` parent class we met in the C# file above. There are also quite a few [XML namespaces](https://www.w3schools.com/xml/xml_namespaces.asp) being defined. We will talk more about these later. They look scarier than they really are.

```XAML
x:Class="HelloXamarinForms.App"
```
This suggests this is something to do with the `App` class. 

### The MainPage Class
Let's recap on where we are in the greater scheme of things:

<img src="img/startup objects.png">

The constructor for the `App` class obtains an instance of `MainPage` and sets it to a property that happens to have the same name. This way, the instance of `MainPage` becomes the root screen of information that the user sees. 

Let's now look at `MainPage` a little more closely. 

Once again, the `MainScreen` class is constructed using two files, `MainScreen.xaml.cs` and `MainScreen.xaml`.

#### MainScreen.xaml.cs
Examine the source code

```C#
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HelloXamarinForms
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            MessageLabel.Text = "Hello World";
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            var aboutPage = new About();
            Navigation.PushAsync(aboutPage);
        }
    }
}
```

Firstly there is the namespace, `namespace HelloXamarinForms`. This effectively makes `HelloXamarinForms` a prefix on anything declared within it.

Once again, we see a partial class declaration

```C#
public partial class MainPage : ContentPage
```

Again, we are expecting there to be at least one other C# file with a partial declaration of `MainPage`, _somewhere_.

We see it subclasses [`ContentPage`](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.contentpage?view=xamarin-forms).

Once again, the constructor of the C# class calls the initialisation method `InitializeComponent()`. This is realted to the companion XAML file (as we will see).

```C#
   public MainPage()
   {
      InitializeComponent();
   }
```


### Diving Down Deeper and the Mystery of the Label!

We've had a look at the two classes, `App` and `MainPage`. Both are constructed from a C# and a XAML file.

<p align="center">
<a href="http://www.youtube.com/watch?feature=player_embedded&v=jlwr3PLytAw" target="_blank"><img src="http://img.youtube.com/vi/jlwr3PLytAw/0.jpg" alt="IMAGE ALT TEXT HERE" width="240" height="180" border="10" /></a>
</p>


#### Task
Try the following:

- Put a breakpoint in the App class as shown below:
<img src="img/driling-into-the-app-class.png">
- Run the code and when it breaks, step in to the function.
- Look at the name of the source file you enter
- What is the classname?

# Summary
There is quite a lot of 'detail stuff' in a simple hello world application. We already met the following:

- partial classes
- XML and XAML
- name spaces
- event handlers
- references to UI elements



