using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleListView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanetViewCell : ViewCell
    {
        public static readonly BindableProperty PlanetNameProperty =
            BindableProperty.Create(propertyName:"PlanetName",
                                    returnType:typeof(string),
                                    declaringType:typeof(PlanetViewCell),
                                    defaultValue: "Uncharted");

        public string PlanetName
        {
            get => (string)GetValue(PlanetNameProperty);
            set => SetValue(PlanetNameProperty, value);
        }

        public static readonly BindableProperty DistanceFromSunProperty =
            BindableProperty.Create("DistanceFromSun", typeof(string), typeof(PlanetViewCell), "?");

        public string DistanceFromSun
        {
            get => (string)GetValue(DistanceFromSunProperty);
            set => SetValue(DistanceFromSunProperty, value);
        }

        public PlanetViewCell()
        {
            InitializeComponent();
        }
    }
}
