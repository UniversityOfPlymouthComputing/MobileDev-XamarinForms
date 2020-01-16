# Simple ListView Example


## Predefined Cell Types
For performance reasons, it is suggested you try and use on of the pre-defined cell types:

* TextCell: A row that has a main text label and a details text label
* EntryCell: A row that has a label and an editable entry
* SwitchCell: A row that has a label and a switch
* ImageCell: A row that has an image on the left and two labels
* ViewCell: A row with custom layout 

## Updating the ListView

TBD

To quote the documentation:
> Recall that by default, ListView calls `ToString()` on each item in its collection to visualize the item on the screen
>
>The default visualization also presents a problem with changes. ListView can't detect changes to the underlying data even if you're using property notification changes. ListView calls ToString only once when it first displays the row. It doesn't realize it needs to call ToString every time the underlying data changes.
>[Reference](https://docs.microsoft.com/en-us/learn/modules/display-collections-in-xamarin-forms-apps-with-listview/5-customize-listview-rows)