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

        private void FetchButton_Clicked(object sender, EventArgs e)
        {
            Spinner.IsRunning = true;
            FetchButton.IsEnabled = false;
            DownloadImageAsync("https://pbs.twimg.com/profile_images/471641515756769282/RDXWoY7W_400x400.png", (Image img)=>{
                img.VerticalOptions = LayoutOptions.CenterAndExpand;
                img.HorizontalOptions = LayoutOptions.CenterAndExpand;
                img.Aspect = Aspect.AspectFit;
                MainStackLayout.Children.Add(img);
                Spinner.IsRunning = false;
                FetchButton.IsEnabled = true;
            });
        }

        void DownloadImageAsync(string fromUrl, Action<Image> Completed)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadDataCompleted += (object sender, DownloadDataCompletedEventArgs e)=>
                {
                    Image Img = new Image();
                    Img.Source = ImageSource.FromStream(() => new MemoryStream(e.Result));
                    Completed(Img); //Call back
                };
                var url = new Uri(fromUrl);
                webClient.DownloadDataAsync(url);
            }
        }

    }
}
