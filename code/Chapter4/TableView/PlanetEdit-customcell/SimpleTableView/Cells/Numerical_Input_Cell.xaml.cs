using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleTableView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Numerical_Input_Cell : ViewCell
    {
        // ****************************** SLIDER *******************************
        public static readonly BindableProperty MinValueProperty =
            BindableProperty.Create(propertyName: "MinValue",
                            returnType: typeof(double),
                            declaringType: typeof(Numerical_Input_Cell),
                            defaultValue: 0.0);

        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public static readonly BindableProperty MaxValueProperty =
            BindableProperty.Create(propertyName: "MaxValue",
                            returnType: typeof(double),
                            declaringType: typeof(Numerical_Input_Cell),
                            defaultValue: 100.0);

        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(propertyName: "Value",
                            returnType: typeof(double),
                            declaringType: typeof(Numerical_Input_Cell),
                            defaultValue: 0.0);

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        // ************************** BUTTON EVENTS ****************************
        void Button_Reduce_Clicked(System.Object sender, System.EventArgs e) => Value -= (Value >= 100.0) ? 100.0 : 0.0;
        void Button_Increase_Clicked(System.Object sender, System.EventArgs e) => Value += (Value <= 900.0) ? 100.0 : 0.0;

        // ********************** ENTRY STRING EVENTS **************************
        void Entry_Completed(System.Object sender, System.EventArgs e)
        {
            //Validate
            double proposedValue;
            string strValue = ValueEntry.Text;
            bool parsed = double.TryParse(strValue, out proposedValue);
            if (parsed == false) return;
            if ((proposedValue >= MinValue) && (proposedValue <= MaxValue))
            {
                Value = Math.Round(proposedValue,1);
            }
        }

        // ****************************** GESURE ******************************
        public static readonly BindableProperty DoubleTapCommandProperty =
            BindableProperty.Create(propertyName: "DoubleTapCommand",
                                    returnType: typeof(ICommand),
                                    declaringType: typeof(Numerical_Input_Cell),
                                    defaultValue: null);
        public ICommand DoubleTapCommand
        {
            get => (ICommand)GetValue(DoubleTapCommandProperty);
            set => SetValue(DoubleTapCommandProperty, value);
        }


        // **************************** CONSTRUCTOR ***************************
        public Numerical_Input_Cell()
        {
            InitializeComponent();

            // ** Create Gesture Recogniser **
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.NumberOfTapsRequired = 2;
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                if ((DoubleTapCommand != null) && DoubleTapCommand.CanExecute(null))
                {
                    DoubleTapCommand.Execute(null);
                }
            };
            //Attach gesture recogniser to the view
            View.GestureRecognizers.Add(tapGestureRecognizer);
        }

    }
}
