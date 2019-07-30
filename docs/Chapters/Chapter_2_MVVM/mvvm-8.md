[Contents](README.md)

----

[Prev](mvvm-7.md)

## Part 8 - Improving the User Experience

[Part 8 is here](/code/Chapter2/Bindings/HelloBindings-08). Inspect and familiarise yourself with the code fully before proceeding. 

In this last section, we look at how to better communicate with the user. This include:

- Handling network error conditions gracefully. 
- Giving feedback that during a network transaction, something is happening

A demo of the final application is here

<p align="center">
<a href="http://www.youtube.com/watch?feature=player_embedded&v=tDCPw1KSoUc" target="_blank"><img src="http://img.youtube.com/vi/tDCPw1KSoUc/0.jpg" alt="IMAGE ALT TEXT HERE" width="480" height="360" border="10" /></a>
</p>


### Updating the View
Let's start with what we want the application to look like:

[Updated user interface](img/hello-bindings-final.png)

The XAML for this interface is slightly modified:

```XAML
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:local="clr-namespace:HelloBindings;assembly=HelloBindings"
             mc:Ignorable="d"
             x:Class="HelloBindings.MainPage">
    <!--        
    <ContentPage.BindingContext>
        <local:MainPageViewModel/>
    </ContentPage.BindingContext>
    -->
    
    <ContentPage.Resources>
        <ResourceDictionary>
            <local:ColorConverter x:Key="ColorConv"/>
        </ResourceDictionary>
    </ContentPage.Resources>
    
    <StackLayout Padding="0,40,0,0">
        
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
                Command="{Binding Path=FetchNextSayingCommand}"
                HorizontalOptions="Center" 
                VerticalOptions="CenterAndExpand"
                />
        
        <Label Text="Tap the button to fetch a saying"
               VerticalOptions="Start"
               HorizontalOptions="Center"
               IsVisible="{Binding HasNoData}"
               />

        <ActivityIndicator x:Name="Activity"
                           HorizontalOptions="Center"
                           VerticalOptions="Start"
                           IsRunning="{Binding Path=IsRequestingFromNetwork}"
            />
            
        <Switch x:Name="ToggleSwitch"  
                HorizontalOptions="Center"
                VerticalOptions="End"
                IsToggled="{Binding Path=UIVisible, Mode=TwoWay}"
                />

        <Button Text="About" 
                Clicked="DoAboutButtonClicked"
                VerticalOptions="End"
                HorizontalOptions="End"
                />
        
    </StackLayout>

</ContentPage>

```

- The ViewModel is no longer instantiated via XAML. Instead this is performed in the code-behind.
- A label saying "Tap the button to fetch a saying" is displayed. 
    - It's `IsVisible` property is bound to the (new) ViewModel property `HasNoData`
    - `HasNoData` is a property on the view model which is derived from the `HasData` property on the Model
- An activity indicator (hidden by default) is added. It appears when it's `IsRunning` property is set to true.
    - `IsRunning` is bound to the ViewModel property `IsRequestingFromNetwork`
    - You may recall from the previous section this is set true before a network trasnaction, and false once completed or failed. 
    - This property is simply _passed through_ via the ViewModel as it needs no modification or conversion.
- New Button has been added
    - The Clicked event handler is set to `DoAboutButtonClicked`. Shock and horror I hear you cry...this is not using a binding!
    - The view that presents itself modally (the About information) is not in _this_ XAML file, but another as we shall see.
    - Pragmatism rules here ;)
- When a network error occurs, a message window appears. This does not feature in the XAML, so where is it?
    - It is view related, so _belongs_ in the view code. All will be revealed!

Not let's look at the code behind. As you can see, the notion of everything in XAML with no-code-behind was quickly dropped. 

```C#
    public partial class MainPage : ContentPage, IMainPageViewHelper
    {
        private MainPageViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();

            //Create ViewModel
            ViewModel = new MainPageViewModel(new MockedRemoteModel(), this);
            BindingContext = ViewModel;
        }

        public ICommand CreateConcreteCommand(Action execute, Func<bool> canExecute)
        {
            return new Command(execute, canExecute);
        }

        public void ChangeCanExecute(ICommand cmd)
        {
            Command c = cmd as Command;
            c.ChangeCanExecute();
        }

        //Show error message dialog
        public async Task ShowErrorMessageAsync(string ErrorMessage)
        {
            await DisplayAlert("Error", ErrorMessage, "OK");
        }

        //Show modal AboutPage (could be called from the ViewModel)
        public async Task ShowModalAboutPageAsync()
        {
            var about = new AboutPage();
            await Navigation.PushModalAsync(about);
        }

        //View-specific event handler
        private async void DoAboutButtonClicked(object sender, EventArgs e)
        {
            await ShowModalAboutPageAsync();
        }
    }
```

First the declaration

```c#
  public partial class MainPage : ContentPage, IMainPageViewHelper
```

The View now implements an interface `IMainPageViewHelper`. The consolidated list of methods implemented by anything of type `IMainPageViewHelper` is as follows:

```C#
  Task ShowModalAboutPageAsync();
  Task ShowErrorMessageAsync(string ErrorMessage);
  ICommand CreateConcreteCommand(Action execute, Func<bool> canExecute);
  void ChangeCanExecute(ICommand obj);    
```

You should see that all these methods are indeed implemented in the View code behind. Why is another matter! Consider what each of these methods does:

#### Task ShowModalAboutPageAsync()
This pushes another view (see `AboutPage.xaml` and `AboutPage.xaml.cs`) onto the **Modal View Stack**. 
```C#
    public async Task ShowModalAboutPageAsync()
    {
        var about = new AboutPage();
        await Navigation.PushModalAsync(about);
    }
```
The `Navigation` property is a property of a Page, and **not** a view model. This **has** to be performed from within a View class. However, we _might_ want to invoke it from our ViewModel.

#### Task ShowErrorMessageAsync(string)
This displays an alert box (as shown in the video). 
```C#
    public async Task ShowErrorMessageAsync(string ErrorMessage)
    {
        await DisplayAlert("Error", ErrorMessage, "OK");
    }
```
Again, the `DisplayAlert` API is only available as a method on a View object, but does not exist in a ViewModel. We might however wish to invoke this from a ViewModel.

#### Command APIs
The last two are less obvious, but both relate to the concrete class `Command`

```C#
  ICommand CreateConcreteCommand(Action execute, Func<bool> canExecute);
  void ChangeCanExecute(ICommand obj);   
```

Recall that the button command property is bound to a ViewModel property of type `ICommand`. Now, `ICommand` is an _interface_, so this needs to be assigned to an instance of a concrete derivative. In our case, this the concrete _class_ is type `Command` which is part of Xamarin Forms, and is strictly a view object. 

> Take a sneaky peek at the ViewModel and you will see the following line is no longer present at the top of the source file: `using Xamarin.Forms;`  Any activities related to the concrete `Command` class have now been delegated to the View.

I appreciate this is pedantic, but it was done to simply float the idea that we have a _separation of concerns_. ViewModels can of course instantiate View objects, but that makes them harder to test.

### Handling Network Errors
The discussion begins back in the model base class with the following method:

```C#
  //Wrapper around the specific implmentation for fetching a saying
  protected async Task<(bool success, string status)> FetchSayingAsync(int WithIndex = 0)
  {
      try
      {
          PayLoad p = await FetchPayloadAsync(WithIndex);
          if (p != null)
          {
              Count = p.From;
              CurrentSaying = p.Saying;
              SayingNumber = WithIndex;
              HasData = true;
              return (success: HasData, status: "OK");
          }
          else
          {
              HasData = false;
              return (success: HasData, status: "Invalid Response");
          }
      }
      catch
      {
          HasData = false;
          return (success: HasData, status: "Permission Denied");
      }
  }
```

Note the following:

- The return type of this asynchronous method is a tuple `(bool, string)`. 
- The (bindable) `HasData` property is also set should that be needed.
- In the event of an error, all other properties are unchanged
- This method is invoked via the public model API  `Task<(bool success, string status)> FetchSayingAsync(int)` from the ViewModel code




[TO BE DONE]




----

[Contents](README.md)
