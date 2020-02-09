[..](listview.md)

[Back](listview-groups.md)

---

# ListView - Using Different Cell Types with Template Selectors

The example for this section is found in the [/code/Chapter4/ListView/L_SimpleListView_TemplateSel](/code/Chapter4/ListView/L_SimpleListView_TemplateSel) folder.

> * Build and run the code
> * Click on a planet to select it
> * Swipe and move a planet (not Earth)
> * Try to swipe and move planet Earth
> * Familiarize yourself with the code, noting the changes in XAML, the ViewModel and the new class PlanetGroup

You will notice that the cell type for planet Earth is different both in terms of visuals and behavior. To achieve this, we have a new custom cell `HomePlanetViewCell`, and we introduce another object, the `DataTemplateSelector` which chooses a data template at run time. This is simple in concept and in implementation.


## MainPage.xaml
Starting at the top level, there have been some small changes to the main page. First, in the resource dictionary, there is an instance of a new object type `PlanetTemplateSelector`:

```XML
    <ResourceDictionary>
        <local:BoolToSelectionModeConverter x:Key="bool2mode"/>
        <local:PlanetTemplateSelector x:Key="TemplateSelector" PageRef="{x:Reference MainContentPage}" />
    </ResourceDictionary>
```        

We give this the key name "TemplateSelector" to we can reference it elsewhere in the XAML. Remember that such XAML instantiates an instance of `PlanetTemplateSelector`.

If we now look at the `ListView`, we can see how this is used:

```C#
    ...
    <ListView ItemsSource="{Binding PlanetGroups}"
                x:Name="PlanetListView"
                ...
                SelectionMode="{Binding SelectionModeOn, Converter={StaticResource bool2mode}, Mode=TwoWay }"
                SelectedItem="{Binding SelectedPlanet}"
                ItemTemplate="{d:StaticResource TemplateSelector}" >            
    </ListView>
    ...
```        

We could equally have set this up in the code-behind constructor as follows:

```C#
    PlanetListView.ItemTemplate = new PlanetTemplateSelector()
    {
        PageRef = this
    };
```            

Which approach you use is your decision of course. With both, we are setting the public property `PageRef` to the page.
Let's now look at this template selector:

## `PlanetTemplateSelector.cs`
This class has the task of interrogating each data item and deciding which data template should be used. First, two properties of type `DataTemplate` are instantiated. Note that you only need to create one instance of each (they are reused).

```C#
public class PlanetTemplateSelector : DataTemplateSelector
{
    public ContentPage PageRef { get; set; }
    private DataTemplate _earth = null;
    public DataTemplate Earth
    {
        get
        {
            if (_earth == null)
            {
                _earth = new DataTemplate(typeof(HomePlanetViewCell));
            }
            return _earth;
        }
    }
    private DataTemplate _other = null;
    public DataTemplate Other
    {
        get
        {
            if (_other == null)
            {
                _other = new DataTemplate(() =>
                {
                    MenuItem m1 = new MenuItem
                    {
                        Text = "Delete",
                        IsDestructive = true,
                    };
                    m1.SetBinding(MenuItem.CommandProperty, new Binding("DeleteCommand", source: PageRef.BindingContext));
                    m1.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

                    MenuItem m2 = new MenuItem
                    {
                        Text = "Swap",
                        IsDestructive = false,
                    };
                    m2.SetBinding(MenuItem.CommandProperty, new Binding("SwapCommand", source: PageRef.BindingContext));
                    m2.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

                    PlanetViewCell cell = new PlanetViewCell();
                    cell.ContextActions.Add(m1);
                    cell.ContextActions.Add(m2);
                    return cell;
                });
            }
            return _other;
        }
    }

    ...
}
```    

The focus is mostly the following extract, which interrogates the data and decides which `DataTemplate` to use:

```C#
    ...
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        ListView list = (ListView)container;

        if (item is SolPlanet p)
        {
            if (p.Name == "Earth")
            {
                return Earth;
            }
            else
            {
                return Other;
            }
        }
        else
        {
            return new DataTemplate(typeof(TextCell));
        }
    }
```               

The menu items are not included for our home planet Earth, after all, it would seem inappropriate to delete or move it.

Notes:

* This is a subclass of `DataTemplateSelector`, which is an abstract base class. 
    * You are required to implement the `OnSelectTemplate(...)`
* The public property `PageRef` is a reference back to the containing page. This is needed to support MVVM.
* The parameter `item` is a data item (type `SolPlanet`)
* The parameter `container` is a reference to the `ListView`
* The menu items still use commanding. The reference to the ViewModel is accessed via `PageRef.BindingContext`

# Summary
That was a lot. Dynamic tables of data are central to so many applications that it was felt necessary to give them a fairly comprehensive treatment. I hope this provided sufficient coverage to enable you to begin developing your own applications. There is almost always more to know, but hopefully, you should be able to use the documentation to discover how to use most features of `ListView`. Some of the things not covered included search, pull to refresh and customizing headers and footers.

Some of the difficulty with `ListView` enters when behaviour is hidden, and in particular, the setting of the binding context. For me personally, this is more confusing when using XAML. However, we should probably be grateful that we've been able to create functionally equivalent behaviour on two platforms _with no platform specific code_ whilst seemingly maintaining good performance. That is something of an achievement for all involved.

----

[Back to top of ListView](listview.md)

[Back to the main menu](readme.md)