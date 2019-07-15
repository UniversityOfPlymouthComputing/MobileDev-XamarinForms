using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

/// <summary>
// BMI Estimation demo
/// </summary>
namespace bmi_estimate
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        enum EntrySource
        {
            Weight,
            Height
        }

        private BmiModel Model = new BmiModel();

        public MainPage()
        {
            InitializeComponent();
            BmiLabel.IsVisible = false;
            OutputLabel.IsVisible = false;
        }

        private async Task SyncViewAndModelAsync(EntrySource src, string newValueAsString)
        {
            bool success;
            string ErrorString;

            //Choose which parameter we are using
            if (src == EntrySource.Height)
            {
                success = Model.SetHeightAsString(newValueAsString, out ErrorString);
                HeightErrorLabel.IsVisible = !success;
            }
            else
            {
                success = Model.SetWeightAsString(newValueAsString, out ErrorString);
                WeightErrorLabel.IsVisible = !success;
            }

            if (Model.BmiValue != null)
            {
                BmiLabel.IsVisible = true;
                OutputLabel.IsVisible = true;
                OutputLabel.Text = string.Format("{0:f1}", Model.BmiValue);
            }
            else
            {
                BmiLabel.IsVisible = false;
                OutputLabel.IsVisible = false;
            }

            //Animate message to user
            if (!success)
            {
                await GiveFeedback(ErrorString);
            }

        }

        private async Task GiveFeedback(string MessageString)
        {
            ErrorLabel.Text = MessageString;
            await ErrorLabel.FadeTo(1.0, 500);
            await Task.Delay(2000);
            await ErrorLabel.FadeTo(0.0, 500);
        }

        private async void Handle_HeightChanged(object sender, TextChangedEventArgs e)
        {
            await SyncViewAndModelAsync(EntrySource.Height, e.NewTextValue);
        }
        private async void Handle_WeightChanged(object sender, TextChangedEventArgs e)
        {
            await SyncViewAndModelAsync(EntrySource.Weight, e.NewTextValue);
        }

    }
}
