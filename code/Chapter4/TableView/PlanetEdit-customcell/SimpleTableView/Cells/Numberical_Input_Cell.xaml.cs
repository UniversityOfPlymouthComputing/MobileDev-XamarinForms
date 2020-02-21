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
    public partial class Numberical_Input_Cell : ViewCell
    {
        // ************ COMMON ************
        public static readonly BindableProperty MinValueProperty =
            BindableProperty.Create(propertyName: "MinValue",
                            returnType: typeof(double),
                            declaringType: typeof(Numberical_Input_Cell),
                            defaultValue: 0.0);

        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public static readonly BindableProperty MaxValueProperty =
            BindableProperty.Create(propertyName: "MaxValue",
                            returnType: typeof(double),
                            declaringType: typeof(Numberical_Input_Cell),
                            defaultValue: 100.0);

        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        // ************ SLIDER ************
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(propertyName: "Value",
                            returnType: typeof(double),
                            declaringType: typeof(Numberical_Input_Cell),
                            defaultValue: 0.0);

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set
            {
                if (value == Value) return;
                SetValue(ValueProperty, value);
                SetValue(EntryStringProperty, String.Format("{0:F1}", value));
            }
        }

        void Button_Reduce_Clicked(System.Object sender, System.EventArgs e) => Value -= (Value >= 100.0) ? 100.0 : 0.0;
        void Button_Increase_Clicked(System.Object sender, System.EventArgs e) => Value += (Value <= 900.0) ? 100.0 : 0.0;

        // ************ ENTRY *************
        public static readonly BindableProperty EntryStringProperty =
            BindableProperty.Create(propertyName: "EntryString",
                            returnType: typeof(string),
                            declaringType: typeof(Numberical_Input_Cell),
                            defaultValue: "");

        public string EntryString
        {
            get => (string)GetValue(EntryStringProperty);
            set {
                double newValue;
                try
                {
                    //Convert to double and set if in range
                    newValue = Double.Parse(value);
                    if ((newValue >= MinValue) && (newValue <= MaxValue))
                    {
                        SetValue(EntryStringProperty, value);
                        SetValue(ValueProperty, newValue);
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        // ************ GESTURE *************
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


        // ************ CONSTRUCTOR *************
        public Numberical_Input_Cell()
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
