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


[TO BE DONE]




----

[Contents](README.md)
