[back](listview-datatemplate-xaml.md)

---

# ListView - Custom cell layout with `ViewCell`

The example for this section is found in the [/code/Chapter4/ListView/H_SimpleListView_ViewCell](/code/Chapter4/ListView/H_SimpleListView_ViewCell) folder.

> * Build and run the code
> * Familiarize yourself with the code, noting the changes in the XAML

This section is relatively short as the only changes are in the XAML.

If one of the pre-defined cell types does not meet your requirements, then you can create your own custom cell. One way to achieve this is with the `ViewCell`. Although not as performant, this provides freedom to specify which visual elements to include and the layout.

An example is given here, where a grid is used to layout four labels (note that I am not expecting any design awards for this).

Note that two properties if `ViewCell` are being set:

* `View` - This is the content property, so in practice, it can be omitted. 
* `ContextActions` - as in the previous section

Note the use of a local resource dictionary. This sets the default attributes of `Label` to keep the remaining XAML concise.

```XML
    ...
    <ViewCell>
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
            
        <ViewCell.ContextActions>
            <MenuItem   Command="{Binding Source={x:Reference MainContentPage}, Path=BindingContext.DeleteCommand}"
                        CommandParameter="{Binding .}"
                        Text="Delete"
                        IsDestructive="True"/>
        </ViewCell.ContextActions>

    </ViewCell>
    ...
```

Getting the layout right can be 'challenging'. Some tips:

* Use a style as shown above and set the background colour will reveal the space occupied with with Label
    * `<Setter Property="BackgroundColor" Value="Transparent" />`
    * Change "Transparent" to another value, such as "Aqua"
* For the `ListView`, try setting the `RowHeight` to larger values to create space  
    * Alternatively, try setting `HasUnevenRows` to "True". This will try and calculate the height for you, but there is a performance cost to this.
* If using `Grid` layout, try adjusting the `RowSpacing` and `Padding`
    * `<Grid RowSpacing="0" Padding="10">`


I refer you to the section on "Custom Cells" in [Petzold C., Creating Mobile Apps with Xamarin.Forms, Microsoft Press](https://docs.microsoft.com/xamarin/xamarin-forms/creating-mobile-apps-xamarin-forms/) (p561)

You might find the `MainPage.xaml` is getting quite long. Equally, if you've investing time is creating a cell layout you might want to reuse it. In the next section we will create a new (reusable) subclass ov `ViewCell` but still use XAML for layout. This also helps us split the XAML into more than one document. 

---

[Next - Sub-classing `ViewCell`](listview-viewcell-sub.md)