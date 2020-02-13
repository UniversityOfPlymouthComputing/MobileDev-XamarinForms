[Contents](README.md)

----


[Prev](mvvm-5.md)

## Part 6 - Bindings in XAML
[Part 6 is here](/code/Chapter2/Bindings/HelloBindings-06). Build and run this to see what it does. Inspect and familiarize yourself with the code fully before proceeding. 

First look at the Code-Behind. Rather than remove code, I have commented out code so you can still see the APIs. This is helpful for following the changes in the XAML.

The new XAML file is shown below:

```XML
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:local="clr-namespace:HelloBindings;assembly=HelloBindings"
             mc:Ignorable="d"
             x:Class="HelloBindings.MainPage">
            
    <ContentPage.BindingContext>
        <local:MainPageViewModel/>
    </ContentPage.BindingContext>
    
    <ContentPage.Resources>
        <ResourceDictionary>
            <local:ColorConverter x:Key="ColorConv"/>
        </ResourceDictionary>
    </ContentPage.Resources>
    
    <StackLayout Padding="0,40,0,40">
        
        <Label x:Name="MessageLabel" 
               FontSize="Large"
               Text="{Binding Path=CurrentSaying}" 
               IsVisible="{Binding Path=UIVisible}"
               TextColor="{Binding Path=SayingNumber, Converter={StaticResource ColorConv}}"
               HorizontalOptions="Center"
               VerticalOptions="Start" 
               />

        <Button x:Name="MessageButton"
                Text='{Binding Path=SayingNumber, StringFormat="Saying {0:d}"}'
                Command="{Binding Path=ButtonCommand}"
                HorizontalOptions="Center" 
                VerticalOptions="CenterAndExpand"
                />

        <Switch x:Name="ToggleSwitch"  
                HorizontalOptions="Center"
                VerticalOptions="End"
                IsToggled="{Binding Path=UIVisible, Mode=TwoWay}"
                />

    </StackLayout>

</ContentPage>

```

### Instantiating the ViewModel in XAML
The first task is to instantiate a view model to bind to. We can do this using an element property

```XML
    <ContentPage.BindingContext>
        <local:MainPageViewModel/>
    </ContentPage.BindingContext>
```

Remember - All elements have a namespace prefix. Where one is not specified, it default to the namespace for Xamarin.Forms. Therefore class names such as `Button` and `Label` can be kept concise.

Our view model class `MainPageViewModel` is not part of Forms, XAML or any other framework. It is out own hand-rolled class that is part of the cross platform project with namespace HelloBindings, and in the _assembly_ HelloBindings (you can confirm the namespace from the properties of the cross platform project).

You can create a namespace specially for this purpose as follows:
```XML
    xmlns:local="clr-namespace:HelloBindings;assembly=HelloBindings"
```
It is called `local` by convention only. You could name it anything you like. 

```XML
    <ContentPage.BindingContext>
        <local:MainPageViewModel/>
    </ContentPage.BindingContext>
```
is equivalent to writing the following in the `MainPage` class (a subclass of `ContentPage`) 
```C#
   BindingContext = new MainPageViewModel();
```

There is nothing (that I am aware of) that says you should instantiate a ViewModel in this way. In fact, often we don't want to use the parameterless constructor. 

> If you do want to pass a parameter, you can use `x:Arguments`.
>
> [See the Microsoft Documentation for more details](https://docs.microsoft.com/xamarin/xamarin-forms/xaml/passing-arguments).
>
> An alternative is to use public properties instead and bind them in the XAML.

### Instantiating the ColorConverter class
A similar technique is used to instantiate the `ColorConverter` class needed to convert an `int` to `Color`. We could have done this in the code behind, but luckily we have a tidier way: the `ResourceDictionary`

```XML
    <ContentPage.Resources>
        <ResourceDictionary>
            <local:ColorConverter x:Key="ColorConv"/>
        </ResourceDictionary>
    </ContentPage.Resources>
```

A `ContentPage` has dictionary of key-value pairs for holding many types of objects referenced in the XAML code. Here the key is "ColorConv" and the associated value is an instance of `ColorConv` (from the cross platform project). We can now obtain a reference to this object using the key (as we will see below).

### Setting the Bindings
First the label used to display the current saying:

```XML
     <Label x:Name="MessageLabel" 
            FontSize="Large"
            Text="{Binding Path=CurrentSaying}" 
            IsVisible="{Binding Path=UIVisible}"
            TextColor="{Binding Path=SayingNumber, Converter={StaticResource ColorConv}}"
            HorizontalOptions="Center"
            VerticalOptions="Start" 
            />
```
The Text property is bound to the `CurrentSaying` property of the source using the special {Binding..} syntax. The `IsVisible` property is similar.

```XML
    Text="{Binding Path=CurrentSaying}"
    IsVisible="{Binding Path=UIVisible}"
```
**TASK** At this point, I suggest you refer back to the code API equivalent - look at the property names for `SetBinding` and contrast.

The text colour is a little more involved:
```XML
    TextColor="{Binding Path=SayingNumber, Converter={StaticResource ColorConv}}"
```
The `Converter` property (from the `SetBinding` API) must refer to an instance of `ColorConverter`. We use the special syntax `{StaticResource ..}` and a key name to refer to objects in the resource dictionary. 

_Do not forget the comma when providing multiple parameters_ (this one had be going for some time!)

> **Note** - do not confuse `{Static ...}` and `{StaticResource ...}`. They look similar, but they are distinctly different.

Next the Button. Again, beginning with the code API, note again how for the `Text` property, two parameters are passed. To accommodate the double-quotes, the outer string uses single-quotes:

```XML
    <Button x:Name="MessageButton"
            Text='{Binding Path=SayingNumber, StringFormat="Saying {0:d}"}'
            Command="{Binding Path=ButtonCommand}"
            HorizontalOptions="Center" 
            VerticalOptions="CenterAndExpand"
            />
```                

Finally, the Switch

```XML

    <Switch x:Name="ToggleSwitch"  
            HorizontalOptions="Center"
            VerticalOptions="End"
            IsToggled="{Binding Path=UIVisible, Mode=TwoWay}"
            />             
```            

I've specified two-way so the switch is initialized in the correct position. 

**TASK**
Take some time to study the XAML in this project, and compare it to the code equivalent from the previous version. Trying experimenting to see how intellisense helps you complete XAML bindings.

## Self-Study Task - Binding in XAML
Create yourself a new project. Add a Slider and a label to the page. Again, set the maximum value of the slider to 100. 
Use bindings to display the slider value using the label, only this time, set up all the bindings using XAML

Again, create an integer property as a go-between.

Remember (as before):

* Create a Model class that has a public integer property and implements the interface `INotifyPropertyChanged`
* Instantiate the model class but do not hook up the bindings from the code behind.

Make reference to the notes up to this point to assist.

 [Next](mvvm-7.md)

----

[Contents](README.md)
