# A second exploration into Xamarin.Forms
In this section, we build another single paged application, only this time we will add a little more complexity. For this, there are some suggested prerequisites including:

- **Static methods** There is one instance of a static method in this example. Worth revising if you're unsure.
- **await and async** If you've not met these two before, you will now! They deserve a much more thorough treatment than is given here. If you want the long treatment, [try this article](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/). Alternatively, maybe give it a try here first.
- **`out` parameters** Useful for in-place modification, but bordering on obscure. If unsure, [this is a good time to revise this](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-parameter-modifier).

For this section, we will specifically look at the following:

- **StackLayout** - especially the start, centre, end, fill and expand combinations
- **Nested StackLayouts** - including horizontal layout
- **Text `Entry` boxes** - including associated events 

**A word about MVVM**
It should be stressed this point that a conscious effort is being made to 'keep it simple'. For sure there are benefits from employing patterns, such as Model-View-ViewModel (MVVM) which incorporares 'binding' between components. However, there is already much to take in, so this will be deferred to a later discussion. Afterall, how is anyone supposed to appreciate the solution to a problem before they've encountered the problem? The intent here is to first become familiar and even comfortable with Xamarin Forms, maybe encounter some spaghetti code mess on route, before recognising the need and desire to employ patterns to bring things back under control. Motivation is key in education, maybe it is _the_ key. Without first establishing a need, clever and elegant solutions risk being passed by as just more 'stuff' to know. 

**A word about unit testing**
In the next section we will encounter Unit testing.This is something that is easily grasped and can be liberating (I know this sounds a little hippy with bear with me). It is also a great vehicle to practise our C# and to help us reflect on the code being written. Most of all, it allows us to slow down, test what we have crafted, and _build confidence_ so we can progress. It is sometimes said that to go faster, you must slow down. I recognise that in myself whether writing software, HDL or designing some electronics. Unit testing can help here.

Although considered good practise, I do not consider unit testing an advanced concept. I remmeber the first time I encountered unit testing. 30 minutes in and I was wondering, _why had I not known about this before?_ If you've not done unit testing before, maybe you will experience something similar. Visual studio also makes it very easy to do.

In part, it's become a necessity due to the complexity of modern software. In the early 1980's, life was simpler - you wrote BASIC (Beginners All-purpose Symbolic Instruction Code) into an interpreter, and saw instant results. You would often write short little programs to try ideas, and back then, a small red cube moving across the screen scored 'cool points' with your friends. It did not matter so much that your code structure got out of control and turned to spaghetti.

Now the bar is higher, and we are (even mentally) trapped inside large IDEs and complex frameworks, not always feeling in total control. The simple hello world gets entangled with user interface components, classes etc. Yes, you can write console applications and that is fine, but unit testing let's you write just functions to test your own project code. No mess, no UI, just code all kept tidily together. Best of all, as you update your project code, you can keep applying those tests. If you find a bug, you return to the tests. It's similar to testing an electronic circuit where you pull out a component (from a bigger system) and test it in isolation. Trying to do this while still connected to everything else adds orders of magnitude levels of complexity. Longer term, we aim to write our code more like an electronic circuit - component based and inheretly testable.

It is also brought in early as it underpins some of the benefits (that MVVM thing again) that will become apparent later in the course. As a heads-up, with certain architectural decisions, it becomes possible to test the code that manages your UI state. If you've ever written an activity (Android) or view contoller (iOS) you may recognise this problem. If not, do't worry, you're going to meet it in Xamarin Forms as well. Testing UI _logic_ is a pretty big thing - you've met those bugs, the button that was not reenabled, the visual element that was not updated, the switch that was ignored etc. 

As a closing point, some perspective. The problem of testing will not go away. Any stateful system that depends on a sequence of events is always going to be trouble. Testing needs to be performed at all levels - user, UI, unit, and manual code walkthroughs. For sequential logic, full coverage soon becomes impossible to achieve. It's why safety critical systems often use older established technology. Anyway, we want to write code and not waffle on about testing, so with that in mind, let's build another app.

# StackLayout
Let's start with a simple example of using stack layout, and explore some of the layout options.
Watch the following video / tutorial.

<p align="center">
<a href="http://www.youtube.com/watch?feature=player_embedded&v=nLUlfb7Ia0g" target="_blank"><img src="http://img.youtube.com/vi/nLUlfb7Ia0g/0.jpg" alt="IMAGE ALT TEXT HERE" width="480" height="360" border="10" /></a>
</p>

Let's recap.

- `StackLayout` is itself a view, that manages child views. It is given space by its parent. It distributes and allocates the available space between the children.
- A StackLayout can be vertical or horizontal
   - For a vertical StackLayout, it will by default only allocate the _required_ vertical space for each component. It will allocate all the horizontal space it has
   - For a horizontal StackLayout, it will it will by default only allocate the _required_ horizontal space for each component. It will allocate all the vertical space it has.
- Where child view requests to expand along an axis, the stack view will try to allocate the maximuim space available. Where more than one component ask to expand, so the space will be distributed evenly
- Where a child requests to fill along a given dimension, it will attempt to resize to fill the available space.

## The BMI Estimator
The application is called the 'Body mass Index' (BMI) Estimator. The task is fairly easy to decribe:

> A user enters two numeric values into two text boxes respectively. The first is the person's weight (Kg) and the second is the person's height (in meters). The application validates the input values and if valid, calculates and displays the so-called body mass index (weight / height<sup>2</sup>)

Sounds simple? Well, in relative terms it is (which is why it's used at this point), but when learning a new framework it still provides plenty to think about.

First, let's see what it does:

<p align="center">
<a href="http://www.youtube.com/watch?feature=player_embedded&v=LzxN8CvPero" target="_blank"><img src="http://img.youtube.com/vi/LzxN8CvPero/0.jpg" alt="IMAGE ALT TEXT HERE" width="480" height="360" border="10" /></a>
</p>

### Setting up the project
This is a similar process to the previous exercise, but as this is (presumably) all new to you, it does not hurt to repeat. 

<p align="center">
<a href="http://www.youtube.com/watch?feature=player_embedded&v=boWrMFmcwcQ" target="_blank"><img src="http://img.youtube.com/vi/boWrMFmcwcQ/0.jpg" alt="IMAGE ALT TEXT HERE" width="480" height="360" border="10" /></a>
</p>

Furthermore, there are some addition steps needed to add some images to the project (any maybe some IDE bugs to traverse). Watch the following video to learn how to add image assets to your project.

<p align="center">
<a href="http://www.youtube.com/watch?feature=player_embedded&v=BJ3V_U9dqGY" target="_blank"><img src="http://img.youtube.com/vi/BJ3V_U9dqGY/0.jpg" alt="IMAGE ALT TEXT HERE" width="480" height="360" border="10" /></a>
</p>

To know more about image resolution for different Android screen densities, [Take a read of the section 'Provide alternative bitmaps'](https://developer.android.com/training/multiscreen/screendensities#TaskProvideAltBmp). 

### Building the User Interface (UI)
The user interface is built using one top-level vertical `StackLayout` and three horizontal child `StackLayout` elements.

A summary of the UI is shown below:

![User Interface](img/BMI-Est-Layout.png)

Yes, a grid layout might be a better choice. However, we are learning about `StackLayout` so let's go with it. 

### The Architecture
The Model-View-Controller (MVC) 'pattern' is commonly used in native Android and iOS projects, so I've chosen to stick to something that resembles this simple paradigm. The Xamarin community do tend to prefer another known as MVVM, but let's hold back on that for now.

Consider the figure below:

![MVC Architecture](img/mvc_bmi_est.png)

Let's look at each aspect of MVC in turn.

- **Model** The Model is pure C#.NET code. It encalsulates the data and all the operations performed on it. It also exposes an API to update and read back the values. Becuase it is pure C# code, this lends it to being more easily tested.
- **View** The View is written in XAML, and only contains View objects, including objects of type `Label`, `Image` and `Entry`. The view tends to be computer generated (to some extent). There are UI Test Frameworks, but these are harder to use than code testing tools.
- **Controller** The coordinates information flow between the Model and View (so they never have to meet!). It handles events generated by the view objects and updates the Model. It also updates view objects with newly calculated values and UI state changes (such as hiding/showing different UI elements). If the Model class makes a spontaneous change (e.g. via a network), then these changes would be observed by the controller and updates to the view would be made, although the example here does not do this.

> A key with this version of MVC is that the Model and View are not directly connected. The controller is has _strong references_ to both the model object(s) and the view objects. We say they are _tightly coupled_. In fact, it's often the case that the controller instantiates both model and view objects (whereby it may be said to _own_ the model and view objects) . A look under the hood and this will be apparent. 

A downside with the MVC architecture is that the controller can quickly bloat and become overly complex. Furthermore, _they are also hard to test_ especially as they are so tightly coupled to the UI (and it's hard to simulate UI based events). 

### Building the Model Code
There are only two central data parameters, _weight_ and _height_. However, both of these each have their own minimum and maximum values. They also have individual units and names which may be needed for display purposes. 

- An early decision was made to encapsulate all this relevent information into the `BodyParameter` class and test.

Looking at the UI, it is observed that the numerical values entered are not numbers, but strings. Somewhere these strings are going to need to be _parsed_ to double values. These values need to be validated.

- Values may be out of range - this needs to be checked and handled
- String parsing is an operation that may potentially fail - such a failure needs to be handled gracefully

- It was also decided (rightly or wrongly) to include the parsing and validation in the model objects. The only difference is the range of valid values.

Finally, both parameters are encapsulated inside a `BmiModel` class. This will be responsible for calculating the Body Mass Index ( weight / height<sup>2</sup>

One criteria for all the above is that there shall be no reference / dependency on the UI code in the model classes. It should be possible to use them in a stand-alone command line application for example. More relevent to this case, they should be useable inside a _Unit Test_ project.

> If we can get the model right, hopefully the rest of the application will fall into place.

**Note** I don't want to create the illusion that the app was written in such a linear and logical order as is presented here. My brain at least does not work quite like that! The truth is there were a few iterations until a model design was settled upon. Yes there are more formal ways to model data and OO applications - for that, there are no doubt many excellent courses and books (none of which I've taken or read ;). Moving on...

#### `BodyParameter` class
The weight and height are encapsulated in the `BodyParameter` class. This class is repsonsible for hold, describing and validating each parameter.

Creation of the `BodyParameter` class is shown in the next video:

<p align="center">
<a href="http://www.youtube.com/watch?feature=player_embedded&v=IEMC40W75dA" target="_blank"><img src="http://img.youtube.com/vi/IEMC40W75dA/0.jpg" alt="IMAGE ALT TEXT HERE" width="480" height="360" border="10" /></a>
</p>

Visual Studio (Enterprise) has a useful feature that allows you to visualise your objects as a "Code Map". The Code Map for the `BodyParameter` class is shown below:

<table>
   <tr>
      <td>
         <img src="./img/BodyParameter.png" alt="BodyParameter code map">
      </td>   
      <td>
         <img src="./img/legend.png" alt="Legend">
      </td>
   </tr>
   <tr>
      <td colspan = "2">
         Code Map for the `BodyParameter` class. Left: Write operations, Right: Read operations. Calls are shown with purple arrows. Field write operations are shown as solid blue arrow. The field read operations are shown as dashed blue arrows.*
      </td>
   </tr>
</Table>

[The `BodyParameter` class code is shown here](/code/Chapter1/bmi_estimate/final/bmi_estimate/BodyParameter.cs). Before running ahead and embedded the `BodyParameter` class into the project, we first pause to create a **Unit Test** project, and to test our new class.

#### Unit testing

Setting up a unit test project in Visual Studio is mostly straightforward. Watch the following video:

<p align="center">
<a href="http://www.youtube.com/watch?feature=player_embedded&v=NC4DJVDy7j8" target="_blank"><img src="http://img.youtube.com/vi/NC4DJVDy7j8/0.jpg" alt="IMAGE ALT TEXT HERE" width="480" height="360" border="10" /></a>
</p>

Some check points to consider:

- Add a new MSTest (C#) unit test project to your soluton
- Make sure your class being tested is public
- In the Unit Testing project, add a reference to the project containing the model code (Right click Dependencies->Add Reference)
- Note the namespace of the unit testing project is probably different to the code under test. Use the `using <namespace>` where possible.

Building the unit tests builds confidence in the code. All tests passing does not guarantee everything is perfect of course, and it's a fairly common experience that more tests may be added later as more obscure bugs are found, but every effort should be made to cover all eventualities. Don't be surprised if you write more test code than there is code being tested!

> For hardware chip designers, it would be normal to spend more than 2/3 project development writing `testbenches`. Even hardware gets shipped with bugs

[The unit tests used for demonstration are shown here](/code/Chapter1/bmi_estimate/final/ModelTest/UnitTest1.cs)

Rather than encalsulate this in the controller code, I've instead chosen to add one more layer of abstraction.

### The `BmiModel` class

The final `BmiModel` class is created. This encapsulates both a Height and Weight (both of type `BodyParameter`) and performs the calculation for the BMI value itself. It also presents an interface to mirror the UI (it exchanges string values between the model and UI code).

Watch the following video to see the BmiModel class being created

<p align="center">
<a href="http://www.youtube.com/watch?feature=player_embedded&v=eZar2Tr9N7A" target="_blank"><img src="http://img.youtube.com/vi/eZar2Tr9N7A/0.jpg" alt="IMAGE ALT TEXT HERE" width="480" height="360" border="10" /></a>
</p>

To recap, the `BmiModel` class was created and unit tests were added. The Code Map is shown below.

 <img src="./img/DataModel.png" alt="BodyParameter code map">
 
Note the type of `Weight` and `Height` are `BodyParameter`. If we've written all the tests to cover as many eventualities as possible, and all tests pass, then it's time to write UI logic to synchronise the UI elements to the model. This will be done in a way that tries to be more familiar than ideal.

### UI Logic - hooking it all up



 
## Final Code
I've included a copy of the final code

### MainPage.xaml
The complete XAML file is shown below

```XAML
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d"
             x:Class="bmi_estimate.MainPage">

    <StackLayout>
        <!-- Place new controls here -->
        <Label 
            Text="BMI Estimate" 
            HorizontalOptions="Center"
            VerticalOptions="Start"
            FontSize="Large"
            Margin="0, 0, 0, 20"
            />

        <Image 
            Source="heart"
            HeightRequest="100"
            Aspect="AspectFit"
            HorizontalOptions="Center"
            VerticalOptions="Start"
            />

        <StackLayout Orientation="Horizontal" HorizontalOptions="FillAndExpand" VerticalOptions="Start" Padding="40,0,40,0">
            <Label
                Text="Height (m)"
                HorizontalOptions="Start"
                VerticalOptions="Center" />

            <Entry 
                Placeholder="Height in Meters" 
                HorizontalOptions="FillAndExpand"
                HorizontalTextAlignment="End"
                VerticalOptions="Center"
                Keyboard="Numeric" 
                TextChanged="Handle_HeightChanged"
                />

            <Label 
                Text="*"
                VerticalOptions="Center"
                HorizontalOptions="End"
                x:Name="HeightErrorLabel"
                />
        </StackLayout>

        <StackLayout Orientation="Horizontal" HorizontalOptions="FillAndExpand" VerticalOptions="Start" Padding="40,0,40,0">
            <Label
                Text="Weight (Kg)"
                HorizontalOptions="Start"
                VerticalOptions="Center" />

            <Entry 
                Placeholder="Weight in Kg" 
                HorizontalOptions="FillAndExpand"
                HorizontalTextAlignment="End"
                Keyboard="Numeric"
                VerticalOptions="Center" 
                TextChanged="Handle_WeightChanged"
                />

            <Label 
                Text="*"
                VerticalOptions="Center"
                HorizontalOptions="End"
                x:Name="WeightErrorLabel"
                />

        </StackLayout>

        <Label 
            Text="Please enter a numerical value"
            HorizontalOptions="Center"
            Margin="0,20,0,0"
            x:Name="ErrorLabel"
            />

        <StackLayout Orientation="Horizontal" HorizontalOptions="Center" VerticalOptions="CenterAndExpand">
            <Label 
                VerticalOptions="Center"
                Text="BMI:"
                FontSize="Large"
                x:Name="BmiLabel"
            />
            <Label 
                VerticalOptions="Center"
                Text="..."
                FontSize="Large"
                x:Name="OutputLabel"
            />
        </StackLayout>

    </StackLayout>

</ContentPage>

```

#### Some points of interest. 
Note the properties of the `Entry` types:

```XAML
      <Entry 
          Placeholder="Height in Meters" 
          HorizontalOptions="FillAndExpand"
          HorizontalTextAlignment="End"
          VerticalOptions="Center"
          Keyboard="Numeric" 
          TextChanged="Handle_HeightChanged"
          />
```

The keyboard type is set to "Numeric". 

- **Experiment** Try different keyboard types. If you can contrast Android and iOS. Note the software keyboard is native to each platform.

The property `TextChanged` is an event handler. Look in the code below and locate the event handler 

### MainPage.xaml.cs
The complete "code behind" is shown below. 

```C#
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

/// <summary>
// BMI Estimation demo
/// </summary>
namespace bmi_estimate
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        enum EntrySource
        {
            Weight,
            Height
        }

        private BmiModel Model = new BmiModel();

        public MainPage()
        {
            InitializeComponent();
            BmiLabel.IsVisible = false;
            OutputLabel.IsVisible = false;
        }

        private async Task SyncViewAndModelAsync(EntrySource src, string newValueAsString)
        {
            bool success;
            string ErrorString;

            //Choose which parameter we are using
            if (src == EntrySource.Height)
            {
                success = Model.SetHeightAsString(newValueAsString, out ErrorString);
                HeightErrorLabel.IsVisible = !success;
            }
            else
            {
                success = Model.SetWeightAsString(newValueAsString, out ErrorString);
                WeightErrorLabel.IsVisible = !success;
            }

            if (Model.BmiValue != null)
            {
                BmiLabel.IsVisible = true;
                OutputLabel.IsVisible = true;
                OutputLabel.Text = string.Format("{0:f1}", Model.BmiValue);
            }
            else
            {
                BmiLabel.IsVisible = false;
                OutputLabel.IsVisible = false;
            }

            //Animate message to user
            if (!success)
            {
                await GiveFeedback(ErrorString);
            }

        }

        private async Task GiveFeedback(string MessageString)
        {
            ErrorLabel.Text = MessageString;
            await ErrorLabel.FadeTo(1.0, 500);
            await Task.Delay(2000);
            await ErrorLabel.FadeTo(0.0, 500);
        }

        private async void Handle_HeightChanged(object sender, TextChangedEventArgs e)
        {
            await SyncViewAndModelAsync(EntrySource.Height, e.NewTextValue);
        }
        private async void Handle_WeightChanged(object sender, TextChangedEventArgs e)
        {
            await SyncViewAndModelAsync(EntrySource.Weight, e.NewTextValue);
        }

    }
}
```

### BodyParameter Class

```C#
using System;
using System.Collections.Generic;
using System.Text;

namespace bmi_estimate
{
    public class BodyParameter
    {
        private double? _value;
        private double _min;
        private double _max;
        private string _nameString;
        private string _unitString;
        public BodyParameter(double min, double max, string ParameterName, string Units)
        {
            _min = min;
            _max = max;
            _nameString = ParameterName;
            _unitString = Units;
        }

        public double? Value {

            get
            {
                return _value;
            }
                
            set
            {
                if ((value >= _min) && (value <= _max))
                {
                    _value = value;
                } else
                {
                    _value = null;
                }
            }
        }

        public static implicit operator double?(BodyParameter d)
        {
            return d.Value;
        }

        public bool SetValueFromString(string StringValue, out string ErrorString)
        {
            if (double.TryParse(StringValue, out double NewValue))
            {
                Value = NewValue;
                if (Value == null)
                {
                    ErrorString = string.Format(_nameString + " must be between {0:f1} and {1:f1}", _min, _max) + _unitString;
                    return false;
                }
                else
                {
                    ErrorString = "";
                    return true;
                }
            }
            else
            {
                _value = null;
                ErrorString = "Please enter a numerical value";
                return false;
            }
        }
    }
}
```

#### Unit Testing BodyParameter 

```C#
    [TestClass]
    public class BodyParameterTests
    {
        [TestMethod]
        public void TestLowerEdge()
        {
            var p1 = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");

            string errStr;
            bool res1 = p1.SetValueFromString("20.0", out errStr);

            Assert.IsTrue(res1, "SetValueFromString failed lower edge case");
            Assert.IsTrue(p1 == 20.0, "SetValueFromString had wrong value for lower edge case");
            Assert.IsTrue(errStr.Equals(""), "Error string incorrect for lower edge");
        }

        [TestMethod]
        public void TestUpperEdge()
        {
            var p1 = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");

            string errStr;
            bool res1 = p1.SetValueFromString("200.0", out errStr);

            Assert.IsTrue(res1, "SetValueFromString failed upper edge case");
            Assert.IsTrue(p1 == 200.0, "SetValueFromString had wrong value for lower edge case");
            Assert.IsTrue(errStr.Equals(""), "Error string incorrect for upper edge");

        }

        [TestMethod]
        public void TestBelowLowerEdge()
        {
            var p1 = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");

            string errStr;
            bool res1 = p1.SetValueFromString("19.99", out errStr);
            double? v = p1;
            Assert.IsNull(v, "Out of range value did not return null");
            Assert.IsFalse(res1, "SetValueFromString failed for value below lower edge");
            Assert.IsTrue(errStr.Equals("Weight must be between 20.0 and 200.0Kg"), "Error string incorrect for below lower edge");

        }
        [TestMethod]
        public void TestAboveUpperEdge()
        {
            var p1 = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");

            string errStr;
            bool res1 = p1.SetValueFromString("200.001", out errStr);
            double? v = p1;
            Assert.IsNull(v, "Out of range value did not return null");
            Assert.IsFalse(res1, "SetValueFromString failed for value above upper edge");
            Assert.IsTrue(errStr.Equals("Weight must be between 20.0 and 200.0Kg"), "Error string incorrect for above upper edge");

        }
        [TestMethod]
        public void TestNullString()
        {
            var p1 = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");
            string errStr;
            bool res1 = p1.SetValueFromString("", out errStr);
            double? v = p1;
            Assert.IsNull(v, "Empty string value did not return null");
            Assert.IsFalse(res1, "Failed to detect empty string");
            Assert.IsTrue(errStr.Equals("Please enter a numerical value"), "Error string \"" + errStr + "\" is incorrect for null string:");
        }

        [TestMethod]
        public void TestInvalidString()
        {
            var p1 = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");
            string errStr;
            bool res1 = p1.SetValueFromString("12a", out errStr);
            double? v = p1;
            Assert.IsNull(v, "Invalid string did not return null");
            Assert.IsFalse(res1, "Failed to detect invalid string");
            Assert.IsTrue(errStr.Equals("Please enter a numerical value"), "Error string incorrect for invalid string input");
        }
    }
```


### The BmiModel Class


```C#
using System;
using System.Collections.Generic;
using System.Text;

namespace bmi_estimate
{
    public class BmiModel
    {
        private BodyParameter Weight = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");
        private BodyParameter Height = new BodyParameter(min: 0.5, max: 3.0, "Height", "m");

        public double? BmiValue
        {
            get
            {
                if ((Weight != null) && (Height != null))
                {
                    return Weight / (Height * Height);
                }
                else
                {
                    return null;
                }
            }
        }

        public static implicit operator double?(BmiModel m)
        {
            return m.BmiValue;
        }

        public bool SetWeightAsString(string strWeight, out string ErrorString)
        {
            return Weight.SetValueFromString(strWeight, out ErrorString);
        }
        public bool SetHeightAsString(string strHeight, out string ErrorString)
        {
            return Height.SetValueFromString(strHeight, out ErrorString);
        }

    }
}

```


#### Unit Testing BmiModel 

```C#
   [TestClass]
    public class Modeltests
    {
        [TestMethod]
        public void TestBuildUp()
        {
            var m = new BmiModel();
            string errString;

            bool valid1 = m.SetHeightAsString("2.0", out errString);
            Assert.IsTrue(valid1);
            double? v = m;
            Assert.IsTrue(v == null, "BMIs should be null at this point");
            Assert.IsTrue(errString.Equals(""));

            bool valid2 = m.SetWeightAsString("100.0", out errString);
            Assert.IsTrue(valid2);
            Assert.IsTrue(m == 25.0, "BMI should be 25");
            Assert.IsTrue(errString.Equals(""));

        }

        [TestMethod]
        public void TestInvalidate()
        {
            var m = new BmiModel();
            string errString;

            m.SetHeightAsString("2.0", out errString);
            m.SetWeightAsString("100.0", out errString);
            bool valid = m.SetHeightAsString("0.1", out errString);
            Assert.IsFalse(valid);
            double? v = m;
            Assert.IsTrue(v == null, "BMIs should be null at this point");
            Assert.IsTrue(errString.Equals("Height must be between 0.5 and 3.0m"));
        }


        [TestMethod]
        public void TestInvalidString()
        {
            var m = new BmiModel();
            string errString;

            m.SetHeightAsString("2.0", out errString);
            m.SetWeightAsString("100.0", out errString);
            bool valid = m.SetHeightAsString("0.5a", out errString);
            Assert.IsFalse(valid);
            double? v = m;
            Assert.IsTrue(v == null, "BMIs should be null at this point");
            Assert.IsTrue(errString.Equals("Please enter a numerical value"));
        }
    }

```

## Summary and Reflection
