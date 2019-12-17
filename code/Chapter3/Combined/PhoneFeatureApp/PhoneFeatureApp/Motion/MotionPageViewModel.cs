using System;
using MVVMBase;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Windows.Input;

namespace PhoneFeatureApp.Motion
{
    public class MotionPageViewModel : ViewModelCommon
    {
        // Metrics
        private DisplayInfo MainDisplayInfo { get; set; }
        private string DeviceRotationString 
        {
            get
            {
                DisplayRotation r = MainDisplayInfo.Rotation;
                switch (r)
                {
                    case DisplayRotation.Rotation0:
                        return "0 degrees";
                    case DisplayRotation.Rotation90:
                        return "+90 degrees";
                    case DisplayRotation.Rotation180:
                        return "180 Degrees";
                    case DisplayRotation.Rotation270:
                        return "-90 Degrees";
                    case DisplayRotation.Unknown:
                        return "Unknown";
                    default:
                        return "Error";
                }
            }
        }
        private string accelerometerStatus = "Accelerometer: OFF";
        private string gyroStatus = "Gyro: OFF";
        private bool accelerometerEnabled = false;
        private bool gyroEnabled;
        private string accelerometerString;
        private string gyroString;
        private Random rnd = new Random();

        // ************************* Constructor **************************
        public MotionPageViewModel() : base(null)
        {
            // Subscribe to changes of screen metrics
            MainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
            ShakeButtonCommand = new Command(execute: () => DoShake());
        }


        // ********************** Bindable properties **********************
        // Orientation (Landscape, Portrait, Square, Unknown)
        public string Orientation => "Orientation: " + MainDisplayInfo.Orientation.ToString();

        // Bindable property Rotation (0, 90, 180, 270)
        public string Rotation => "Rotation: " + DeviceRotationString;

        // Accelerometer Status String
        public string AccelerometerStatus {
            get => accelerometerStatus;
            set
            {
                if (accelerometerStatus != value)
                {
                    accelerometerStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public string GyroStatus {
            get => gyroStatus;
            set
            {
                if (gyroStatus != value)
                {
                    gyroStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        // Accelerometer Enable / Disable
        public bool AccelerometerEnabled {
            get => accelerometerEnabled;
            set
            {
                if (accelerometerEnabled == value) return;

                accelerometerEnabled = value;
                try
                {
                    if (accelerometerEnabled)
                    {
                        Accelerometer.Start(sensorSpeed: SensorSpeed.UI);
                        // Register accelerometer reading changes, be sure to unsubscribe when finished
                        Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
                        Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
                        AccelerometerStatus = "Accelerometer ON";

                    }
                    else
                    {
                        Accelerometer.Stop();
                        Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
                        Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
                        AccelerometerStatus = "Accelerometer OFF";
                    }
                }
                catch (FeatureNotSupportedException)
                {
                    AccelerometerStatus = "Accelerometer Not Supported";
                }
                catch (Exception)
                {
                    AccelerometerStatus = "Accelerometer Error";
                }
            }
        }

        //Exposed property for the gyro-enabled switch
        public bool GyroEnabled {
            get => gyroEnabled;
            set
            {
                if (gyroEnabled == value) return;

                gyroEnabled = value;
                try
                {
                    if (gyroEnabled)
                    {
                        Gyroscope.Start(sensorSpeed: SensorSpeed.UI);
                        Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
                        GyroStatus = "Gyroscope ON";
                    }
                    else
                    {
                        Gyroscope.Stop();
                        Gyroscope.ReadingChanged -= Gyroscope_ReadingChanged;
                        GyroStatus = "Gyroscope OFF";
                    }
                }
                catch (FeatureNotSupportedException)
                {
                    GyroStatus = "Gyroscope Not Supported";
                }
                catch (Exception)
                {
                    GyroStatus = "Gyroscope Error";
                }
            }
        }

        public string AccelerometerString {
            get => accelerometerString;
            set
            {
                if (accelerometerString != value)
                {
                    accelerometerString = value;
                    OnPropertyChanged();
                }
            }
        }

        public string GyroString {
            get => gyroString;
            set
            {
                if (gyroString != value)
                {
                    gyroString = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ShakeButtonCommand { get; private set; }

        // **************************** EVENTS *****************************
        //Rotation event
        void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            // Process changes
            MainDisplayInfo = e.DisplayInfo;
            OnPropertyChanged(nameof(Orientation));
            OnPropertyChanged(nameof(Rotation));
        }


        //Accelerometer
        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            AccelerometerData data = e.Reading;
            AccelerometerString = string.Format("(X,Y,Z) = ({0,10:F4}, {1,10:F4}, {2,10:F4})",
                data.Acceleration.X, data.Acceleration.Y, data.Acceleration.Z);
        }

        //Gyro
        private void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            GyroscopeData data = e.Reading;
            // Process Angular Velocity X, Y, and Z reported in rad/s
            GyroString = string.Format("(X,Y,Z) rad/s = ({0,10:F4}, {1,10:F4}, {2,10:F4})",
                data.AngularVelocity.X, data.AngularVelocity.Y, data.AngularVelocity.Z);
        }
        //Shake
        private void DoShake()
        {
            BackgroundColor = Color.FromRgba(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255), 64);
            MessagingCenter.Send<ViewModelCommon, Color>(this, "BackgroundColorChange", BackgroundColor);
        }
        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            //cmd-m or ctrl-m to simulate a shake
            DoShake();
        }

        // *****************************************************************
    }
}
