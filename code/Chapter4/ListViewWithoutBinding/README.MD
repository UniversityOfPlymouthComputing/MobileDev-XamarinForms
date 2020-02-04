# Xamarin Forms ListView without Binding

This project shows how to set the items in a LostView without using Binding.

In your code behind, set the `ItemsSource` property of the list to any `IEnumerable`.

```cs
MyList.ItemsSource = new string[]
{
    "Hello",
    "World"
};
```
