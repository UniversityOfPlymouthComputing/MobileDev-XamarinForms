[..](listview.md)

[Prev](listview-viewcell-sub.md))

---

# ListView - Adding Bindable Properties to Custom Cells

The example for this section is found in the [/code/Chapter4/ListView/J_SimpleListView_ViewCell_BindableProp](/code/Chapter4/ListView/J_SimpleListView_ViewCell_BindableProp) folder.

> * Build and run the code
> * Familiarize yourself with the code, noting the changes in the MainPage.xaml, PlanetViewCell.xaml and its code-behind

In this section, we add bindable properties to the `ViewCell` sub-class, `PlanetViewCell`. When the cell is bound to a data item, this will allow us to use any property name we need.

On this occasion, we will work top-down.

## MainPage.xaml
The purpose of this section is captured in `MainPage.xaml`

```XML
    ...
    <ListView.ItemTemplate>
        <DataTemplate>
            <local:PlanetViewCell PlanetName="{Binding Name}" DistanceFromSun="{Binding Distance}">
                <local:PlanetViewCell.ContextActions>
                    <MenuItem   Command="{Binding Source={x:Reference MainContentPage}, Path=BindingContext.DeleteCommand}"
                                CommandParameter="{Binding .}"
                                Text="Delete"
                                IsDestructive="True"/>
                </local:PlanetViewCell.ContextActions>
            </local:PlanetViewCell>
        </DataTemplate>
    </ListView.ItemTemplate>
    ...
```

Note how when specifying the cell type, two custom properties `PlanetName` and `DistanceFromSun` are now explicitly bound to the data item:

```XML
    ...
    <local:PlanetViewCell PlanetName="{Binding Name}" DistanceFromSun="{Binding Distance}">
    ...
```

whereas previously it simply read:

```XML
    ...
    <local:PlanetViewCell">
    ...
```

Previously the property names of the data item _had_ to be specifically "Name" and "Distance". If this does not match the property names of the data items, it will not work. Furthermore, this information is buried and hard-coded inside `PlanetViewCell`.

These new properties are set up in the code behind.

## PlanetViewCell.xaml.cs
The properties being added are `PlanetName` (type `string`) and `DistanceFromSun` (type `string`). For each of these, a corresponding `BindableProperty` is added (this is one of the parameter types of the `SetBinding` method).

```C#
    public partial class PlanetViewCell : ViewCell
    {
        public static readonly BindableProperty PlanetNameProperty =
            BindableProperty.Create(propertyName:"PlanetName",
                                    returnType:typeof(string),
                                    declaringType:typeof(PlanetViewCell),
                                    defaultValue: "Uncharted");

        public string PlanetName
        {
            get => (string)GetValue(PlanetNameProperty);
            set => SetValue(PlanetNameProperty, value);
        }


        public static readonly BindableProperty DistanceFromSunProperty =
            BindableProperty.Create("DistanceFromSun", typeof(string), typeof(PlanetViewCell), "?");

        public string DistanceFromSun
        {
            get => (string)GetValue(DistanceFromSunProperty);
            set => SetValue(DistanceFromSunProperty, value);
        }

        public PlanetViewCell()
        {
            InitializeComponent();
        }
    }
```

For the first example, I've used explicit parameter labels:

```C#
    public static readonly BindableProperty PlanetNameProperty = BindableProperty.Create(
            propertyName:"PlanetName",
            returnType:typeof(string),
            declaringType:typeof(PlanetViewCell),
            defaultValue: "Uncharted");
```  

The properties themselves make reference to these as follows:

```C#
    public string PlanetName
    {
        get => (string)GetValue(PlanetNameProperty);
        set => SetValue(PlanetNameProperty, value);
    }
```        

Having done this, the cell needs to bind to these properties, and NOT the data item as before (remember the binding context of this cell will still be the data item).

## PlanetViewCell.xaml
The XAML for the cell has been slightly modified. The relevant sections are shown below:

```XML
<?xml version="1.0" encoding="UTF-8"?>
<ViewCell xmlns="http://xamarin.com/schemas/2014/forms"
          xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
          x:Name="ThisCell"
          x:Class="SimpleListView.PlanetViewCell">
    <ViewCell.View>
        <Grid RowSpacing="0" Padding="10">

            ...

            <Label Grid.Row="0" Grid.Column="0" Text="Planet:" />
            <Label Grid.Row="0" Grid.Column="1" Text="{Binding PlanetName, Source={x:Reference ThisCell}}" />
            <Label Grid.Row="1" Grid.Column="0" Text="Distance from the Sun:" />
            <Label Grid.Row="1" Grid.Column="1" Text="{Binding DistanceFromSun, Source={x:Reference ThisCell}}" />
        </Grid>
    </ViewCell.View>
</ViewCell>
```

Notes:

* Note that the cell now has a name `x:Name="ThisCell"`
* The `Text` property bindings of the two non-static labels have been changed

We are no longer relying on the binding context, but instead explicitly binding to properties declared in the code-behind. Note the new property names `PlanetName` and `DistanceFromSun` and `Source={x:Reference ThisCell}}"` 

## How many binding layers?
It would seem we are performing binding twice. 

* The `Text` property of the two labels in `PlanetViewCell` are bound to the bindable properties `PlanetName` and `DistanceFromSun`
* The data template is binding `PlanetName` and `DistanceFromSun` to the data item properties `Name` and `Distance`

It's not clear what the performance impact of this is, and it does raise the question whether this is always the best approach.
Should we always go to the effort to create bindable properties or just leave the bindings hard coded? I suppose it depends in part on whether you expect to reuse the cell.

---

[Next - Organizing and Listing Data in Groups](listview-groups.md)