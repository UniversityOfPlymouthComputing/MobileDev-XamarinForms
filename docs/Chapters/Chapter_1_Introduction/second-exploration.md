# A second exploration into Xamarin.Forms
In this section, we build another single paged application, only this time we will add a little more complexity. For this, there are some suggested prerequisites include:

- **C# Classes and Constructors** You need to be able to create a C# class in visual studio
- **Static methods** There is one instance of a static method in this example. Worth revising if you're unsure.
- **await and async** If you've not met these two before, you will now! They deserve a much more thorough treatment than is given here.
- **out parameters** Useful for in-place modification. Of unsure, this is a good time to revise this.

**A word about MVVM**
It should be stressed this point that a conscious effort is being made to 'keep it simple'. For sure there are benefits from employing patterns, such as Model-View-ViewModel (MVVM) which incorporares 'binding' between components. However, there is already much to take in, so this will be deferred to a later discussions. Afterall, how is anyone supposed to apprecisate the solution to a problem before they've encountered the problem? The intent here is to first become familiar and even comfortable with Xamarin Forms, maybe encounter some spaghetti code mess on route, before recognising the need and desire to employ patterns to bring things back under control. Motivation is key in education, maybe it is _the_ key. Without first establishing a need, clever and elegant solutions risk being passed by as just more 'stuff' to know. 

**A word about unit testing**
In the next section we will encounter Unit testing.This is something that is easily grasped and can be liberating (I know this sounds a little hippy with bear with me). It is also a great vehicle to practise our C# and to help us reflect on the code being written. Most of all, it allows us to slow down, test what we have crafted, and _build confidence_. Although considered good practise, I do not consider unit testing an advance concept. I remmeber the first time I encountered unit testing. 30 minutes in and I was wondering, _why had I not known about this before?_ If you've not done unit testing before, maybe you will feel something similar.
In part, it's become a necessity due to the complexity of modern software. In the early 1980's, life was simple - you wrote basic into an interter, and saw instant results. You would often write short little programmes to try ideas, and back then, a small red cube moving across the screen scored 'cool points' with your friends. Now the bar is higher, and we are mentally trapped inside large IDEs and complex frameworks. The simple hello world gets entangled with user interface components, classes etc. Yes, you can write console applications and that is fine, but unit testing let's you write just functions to test your own code. No mess, no UI, just code. Best of all, as you update your code, you can keep applying those tests. If you find a bug, you return to the tests.

It is also brought in early as it underpins some of the benefits (that MVVM thing again) that will become apparent later in the course. As a heads-up, with certain architectural decisions, it becomes possible to test the code that manages your UI state. That's a pretty big thing - you've met those bugs, the button that was no reenabled, the visual element that was not updated etc. 

## The BMI Estimator
The application is called the 'Body mass Index' (BMI) Estimator. The task is fairly easy to decribe:

> A user enters two numeric values into two text boxes respectively. The first is the person's weight (Kg) and the second is the person's height (in meters). The application validates the input values and if valid, calculates and displays the so-called body mass index (weight / height<sup>2</sup>)

Sounds simple? Well, in relative terms it is (which is why it's used at this point), but when learning a new framework it still provides plenty to think about.

First, let's see what it does:

**INSERT VIDEO HERE**
[ YOU TUBE VIDEO ]


## Setting up the project
This is a similar process to the previous exercise, but as this is new, it does not hard to repeat. 

**INSERT VIDEO HERE**
[ YOU TUBE VIDEO ON SETTING UP]

Furthermore, there are some addition steps needed to add some images to the project (any maybe some IDE bugs to traverse). Watch the following video to learn how to add image assets to your project.

**INSERT VIDEO HERE**
[ YOU TUBE VIDEO ON ADDING IMAGES ]

To know more about image resolution for different Android screen densities, [Take a read of the section 'Provide alternative bitmaps'](https://developer.android.com/training/multiscreen/screendensities#TaskProvideAltBmp)

## Building the UI

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


## Writing Model Classes

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

### Unit Testing 

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


### Unit Testing 

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


## Hooking it all up

## Summary and Reflection
