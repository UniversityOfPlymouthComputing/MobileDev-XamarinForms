using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using uoplib;
using System.Diagnostics;

namespace SimpleTableView
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new PlanetDetailsViewModel(new SolPlanet("Earth", 147.0, explored:true));
            //Banner.Source = ImageSource.FromResource("SimpleTableView.img.planet_header.png");
        }
    }
}
