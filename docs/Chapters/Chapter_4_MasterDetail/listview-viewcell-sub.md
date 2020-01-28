[..](listview.md)

[back](listview-viewcell.md) 

---

# ListView - Sub-classing `ViewCell`

The example for this section is found in the [/code/Chapter4/ListView/I_SimpleListView_ViewCell_Subclass](/code/Chapter4/ListView/I_SimpleListView_ViewCell_Subclass) folder.

> * Build and run the code
> * Familiarize yourself with the code, noting the changes in the XAML and the new file `PlanetViewCell.xaml`

This section is relatively short as the only changes are in the XAML.

## MainPage.xaml
The XAML has the same structure but been shortened and simplified. The `ItemTemplate` is now reduced as follows:
```XML
    <ListView.ItemTemplate>
        <DataTemplate>
            <local:PlanetViewCell>
                <local:PlanetViewCell.ContextActions>
                    <MenuItem   Command="{Binding Source={x:Reference MainContentPage}, Path=BindingContext.DeleteCommand}"
                                CommandParameter="{Binding .}"
                                Text="Delete"
                                IsDestructive="True"/>
                </local:PlanetViewCell.ContextActions>
            </local:PlanetViewCell>
        </DataTemplate>
    </ListView.ItemTemplate>
``` 

The only property being set (for now) is the property element `<local:PlanetViewCell.ContextActions>`. Note that the `MenuItem` needs a reference to the ViewModel (for commanding), so has remained in the page XAML.

## The Custom Cell `PlanetViewCell`
Note the new element `<local:PlanetViewCell>`, where `local` is the namespace for any class in the namespace `SimpleListView`. The `PlanetViewCell` class is a subclass of `ViewCell` and is created in Visual Studio as follows:

* Right click the `SharedListView` project and add a new class
* From the Forms category, select "Forms ViewCell XAML" 

Let's look at the code behind:

```C#
    public partial class PlanetViewCell : ViewCell
    {
        public PlanetViewCell()
        {
            InitializeComponent();
        }
    }
```    

This is similar to a `ContentPage` in that the constructor invokes `InitializeComponent()` to load and parse the XAML. The XAML is also concise:

```XML
<?xml version="1.0" encoding="UTF-8"?>
<ViewCell xmlns="http://xamarin.com/schemas/2014/forms"
          xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
          x:Class="SimpleListView.PlanetViewCell1">
    <ViewCell.View>
        <StackLayout>
            <Label Text="Hello Xamarin.Forms!" />
        </StackLayout>
    </ViewCell.View>
</ViewCell>
```

We now replace the `ViewCell.View` properties with those in `MainPage.xaml`, including the top level layout. The `ViewCell` layout (type `Grid`) is cut from the `MainPage.xaml` and inserted in place of the `StackLayout` in the XAML for the custom cell. This is the final result:

```XML
<?xml version="1.0" encoding="UTF-8"?>
<ViewCell xmlns="http://xamarin.com/schemas/2014/forms"
          xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
          x:Class="SimpleListView.PlanetViewCell">
    <ViewCell.View>
        <Grid RowSpacing="0" Padding="10">
            <Grid.Resources>
                <ResourceDictionary>
                    <!-- Implicit style : applies to all Labels in the grid -->
                    <Style TargetType="Label">
                        <Setter Property="HorizontalOptions" Value="FillAndExpand" />
                        <Setter Property="VerticalOptions" Value="FillAndExpand" />
                        <Setter Property="HorizontalTextAlignment" Value="Start"/>
                        <Setter Property="VerticalTextAlignment" Value="Center"/>
                        <Setter Property="BackgroundColor" Value="Transparent" />
                    </Style>
                </ResourceDictionary>            
            </Grid.Resources>

            <Grid.RowDefinitions>
                <RowDefinition Height="*"/>
                <RowDefinition Height="*"/>
            </Grid.RowDefinitions>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="2*"/>
                <ColumnDefinition Width="*"/>
            </Grid.ColumnDefinitions>
            <Label Grid.Row="0" Grid.Column="0" Text="Planet:" />
            <Label Grid.Row="0" Grid.Column="1" Text="{Binding Name}" />
            <Label Grid.Row="1" Grid.Column="0" Text="Distance from the Sun:" />
            <Label Grid.Row="1" Grid.Column="1" Text="{Binding Distance}" />
        </Grid>
    </ViewCell.View>
</ViewCell>
```

Build and run, and it just works!

**Notes**
* The label bindings are currently hard-coded to literal strings (`Name` and `Distance`) in this cell. 
    * For this custom cell to be reusable and allow other property names to be bound to the visual elements, we would need to do more work in the code-behind to allow this to happen.
* Remember that the _data template is responsible for instantiating a cell and setting the binding context_.
    * The cell has no knowledge or reference to the `DataTemplate`, `ListView`, `Page` or ViewModel.
    * The binding context is usually a data item
    * The `MenuItem` is not included as it needs a reference to the ViewModel to support commanding.

I refer you to the section p610 in [Petzold C., Creating Mobile Apps with Xamarin.Forms, Microsoft Press](https://docs.microsoft.com/xamarin/xamarin-forms/creating-mobile-apps-xamarin-forms/) for more.

If we wish to reuse this cell, maybe with a different data set, then we can add bindable properties to the cell.

---

[Next - Adding Bindable Properties to Custom Cells](listview-bindableprops.md)

