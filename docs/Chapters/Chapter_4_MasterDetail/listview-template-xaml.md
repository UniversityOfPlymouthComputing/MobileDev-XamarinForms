
---

# ListView DataTemplate Selection with XAML

The example for this section is found in the [/code/Chapter4/ListView/M_SimpleListView_TemplateSelXAML](/code/Chapter4/ListView/M_SimpleListView_TemplateSelXAML) folder.

> * Build and run the code
> * Familiarize yourself with the code, noting the changes in the data template selector and MainPage.xaml

## PlanetTemplateSelector.cs
You will notice that the `PlanetTemplateSelector` class is now a lot shorter:

```C#
    public class PlanetTemplateSelector : DataTemplateSelector
    {
        //public ContentPage PageRef { get; set; }

        //Set through bindings
        public DataTemplate EarthTemplate { get; set; }
        public DataTemplate OtherplanetTemplate { get; set; }

        //Selector
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //ListView list = (ListView)container;

            if (item is SolPlanet p)
            {
                if (p.Name == "Earth")
                {
                    return EarthTemplate;
                }
                else
                {
                    return OtherplanetTemplate;
                }
            }
            else
            {
                return new DataTemplate(typeof(TextCell));
            }
        }
    }
```    

The `DataTempate` properties `EarthTemplate` and `OtherplanetTemplate` are now instantiated elsewhere and **set through bindings** (as we will see below). We can see this in action in `MainPage.xaml`

## MainPage.xaml
The focus for this file is in the `ResourceDictionary` where objects are instantiated and referenced via a string key. Firstly, the `DataTemplate` objects are instantiated.

> Remember - we only need one instance of each. They are reused by the `ListView`

```XML
<?xml version="1.0" encoding="utf-8"?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:local="clr-namespace:SimpleListView"
             mc:Ignorable="d"
             x:Name="MainContentPage"
             x:Class="SimpleListView.MainPage">

    <ResourceDictionary>

        <!-- DATA TEMPLATES -->
        <DataTemplate x:Key="EarthDataTemplate">
            <local:HomePlanetViewCell/>
        </DataTemplate>

        <DataTemplate x:Key="OtherDataTemplate">
            <local:PlanetViewCell>
                <local:PlanetViewCell.ContextActions>
                    <MenuItem Text="Delete"
                                IsDestructive="True"
                                CommandParameter="{Binding .}"
                                Command="{Binding Source={x:Reference MainContentPage}, Path='BindingContext.DeleteCommand'}"/>
                    <MenuItem Text="Swap"
                                IsDestructive="False"
                                CommandParameter="{Binding .}"
                                Command="{Binding Source={x:Reference MainContentPage}, Path='BindingContext.SwapCommand'}"/>
                </local:PlanetViewCell.ContextActions>
            </local:PlanetViewCell>
        </DataTemplate>

        ...
```
Note the following:

* The `PlanetViewCell` class is part of the current project (and not part of XAML or Xamarin.Forms), so we need the `local` prefix.
* Each object is instantiated and stored in a resource dictionary. This is instead of using `new` in the code-behind.
* Each instance has a key name which be used later in the XAML to reference these instances

Next, we can create an instance of `PlanetTemplateSelector`. Again, this is a class from the current project, so the `local` namespace is used:

```XML
    ...

        <!-- TEMPLATE SELECTOR -->
        <local:PlanetTemplateSelector x:Key="TemplateSelector"
                                        EarthTemplate="{x:StaticResource EarthDataTemplate}"
                                        OtherplanetTemplate="{x:StaticResource OtherDataTemplate}"
                                        />
    </ResourceDictionary>

    ...
```

An instance of `PlanetTemplateSelector` is created, and the properties `EarthTemplate` and `OtherplanetTemplate` are hooked up through a one-off binding. Note the use of the XAML `x:StaticResource` namespace to specify specific objects in the resource dictionary.

Which style you choose is up to you. The code-centric approach is helpful to understand the underlying mechanisms, but is more verbose. Once you are comfortable with `DataTemplate` and `DataTemplateSelector` types, you might prefer to use XAML and keep the selector as simple as possible.

# Summary
That was a lot. Dynamic tables of data are central to so many applications that it was felt necessary to give them a fairly comprehensive treatment. I hope this provided sufficient coverage to enable you to begin developing your own applications. There is almost always more to know, but hopefully, you should be able to use the documentation to discover how to use most features of `ListView`. Some of the things not covered included search, pull to refresh and customizing headers and footers.

Some of the difficulty with `ListView` enters when behavior is hidden, and in particular, the setting of the binding context. For me personally, this is more confusing (at first) when using XAML. However, we should probably be grateful that we've been able to create functionally equivalent behavior on two platforms _with no platform specific code_ whilst seemingly maintaining good performance. That is something of an achievement for all involved.

----

[Back to top of ListView](listview.md)

[Back to the main menu](readme.md)