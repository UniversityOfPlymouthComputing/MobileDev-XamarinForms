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
            var img = DownloadImageSync("https://github.com/UniversityOfPlymouthComputing/MobileDev-XamarinForms/raw/master/code/Chapter2/ImageFetch/xam.png");
            img.VerticalOptions = LayoutOptions.CenterAndExpand;
            img.HorizontalOptions = LayoutOptions.CenterAndExpand;
            img.Aspect = Aspect.AspectFit;
            MainStackLayout.Children.Add(img);
            Spinner.IsRunning = false;
            FetchButton.IsEnabled = true;
        }

        Image DownloadImageSync(string fromUrl)
        {
            using (WebClient webClient = new WebClient())
            {
                var url = new Uri(fromUrl);
                //Download SYNCHRONOUSLY (NOT GOOD)
                var bytes = webClient.DownloadData(url);
                Image img = new Image();
                img.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                return img;
            }
        }
    }
}
