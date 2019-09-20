using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ImageFetch
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void FetchButton_Clicked(object sender, EventArgs e)
        {
            Spinner.IsRunning = true;
            FetchButton.IsEnabled = false;
            var img =  await DownloadImageAsync("https://github.com/UniversityOfPlymouthComputing/MobileDev-XamarinForms/raw/master/code/Chapter2/ImageFetch/xam.png");
            img.VerticalOptions = LayoutOptions.CenterAndExpand;
            img.HorizontalOptions = LayoutOptions.CenterAndExpand;
            img.Aspect = Aspect.AspectFit;
            img.Opacity = 0.0;

            MainStackLayout.Children.Add(img);

            _ = await img.FadeTo(1.0, 2000);    //Allow to complete
            _ = img.RotateTo(360, 4000);        //Run concurrently with the next
            _ = await img.ScaleTo(2, 2000);     
            _ = await img.ScaleTo(1, 2000);

            Spinner.IsRunning = false;
            FetchButton.IsEnabled = true;
        }

        async Task<Image> DownloadImageAsync(string fromUrl)
        {
            using (WebClient webClient = new WebClient())
            {
                var url = new Uri(fromUrl);
                var bytes = await webClient.DownloadDataTaskAsync(url);
                
                Image img = new Image();
                img.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                return img;
            }
        }

    }
}
