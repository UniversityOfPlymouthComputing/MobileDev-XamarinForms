using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleTableView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyReuseableCell : ViewCell
    {
        public static readonly BindableProperty DistanceFromSunProperty =
            BindableProperty.Create(propertyName: "DistanceFromSun",
                                    returnType: typeof(double),
                                    declaringType: typeof(MyReuseableCell),
                                    defaultValue: 500.0);

        public double DistanceFromSun
        {
            get => (double)GetValue(DistanceFromSunProperty);
            set => SetValue(DistanceFromSunProperty, value);
        }

        public static readonly BindableProperty DoubleTapCommandProperty =
            BindableProperty.Create(propertyName: "DoubleTapCommand",
                                    returnType: typeof(ICommand),
                                    declaringType: typeof(MyReuseableCell),
                                    defaultValue: null);
        public ICommand DoubleTapCommand
        {
            get => (ICommand)GetValue(DoubleTapCommandProperty);
            set => SetValue(DoubleTapCommandProperty, value);
        }

        public MyReuseableCell()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 2
            };
            tapGestureRecognizer.Tapped += (s, e) => {
                // handle the tap
                if ((DoubleTapCommand != null) && DoubleTapCommand.CanExecute(null))
                {
                    DoubleTapCommand.Execute(null);
                }
            };
            this.View.GestureRecognizers.Add(tapGestureRecognizer);
        }

        void Button_Reduce_Clicked(System.Object sender, System.EventArgs e)
        {
            if (DistanceFromSun >= 100.0)
            {
                DistanceFromSun -= 100.0;
            }
        }

        void Button_Increase_Clicked(System.Object sender, System.EventArgs e)
        {
            if (DistanceFromSun <= 900.0)
            {
                DistanceFromSun += 100.0;
            }
        }
    }
}
