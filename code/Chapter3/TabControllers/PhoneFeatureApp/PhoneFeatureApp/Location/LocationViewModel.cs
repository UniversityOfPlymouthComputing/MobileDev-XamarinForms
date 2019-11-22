using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Windows.Input;

// see https://docs.microsoft.com/en-us/xamarin/essentials/geolocation?context=xamarin%2Fxamarin-forms&tabs=android

namespace PhoneFeatureApp.Location
{
    public class LocationViewModel : ViewModelCommon
    {

        private string locationString;
        private ICommand buttonCommand;
        private ICommand mapCommand;
        private bool isBusy = false;
        private Xamarin.Essentials.Location loc = null;

        // ********************** Non-Bindable properties **********************
        public Xamarin.Essentials.Location Loc {
            get => loc;
            private set
            {
                if (loc != value)
                {
                    loc = value;
                    (MapCommand as Command).ChangeCanExecute();
                }
            }
        }

        // ********************** Bindable properties **********************
        public string LocationString {
            get => locationString;
            set
            {
                if (locationString != value)
                {
                    locationString = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ButtonCommand { get => buttonCommand; private set => buttonCommand = value; }
        public ICommand MapCommand { get => mapCommand; set => mapCommand = value; }

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged();
                    (ButtonCommand as Command).ChangeCanExecute();
                    (MapCommand as Command).ChangeCanExecute();
                }
            }
        }

        // ************************* Constructor **************************
        public LocationViewModel()
        {
            //Update background colour when a shake is detected
            subscribeToBackgroundColChange();

            //Command for updating location
            ButtonCommand = new Command(
                execute: async () =>
                {
                    try
                    {
                        IsBusy = true;
                        GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best);
                        Loc = await Geolocation.GetLocationAsync(request);
                        IsBusy = false;
                        if (Loc != null)
                        {
                            LocationString = string.Format("Lat: {0:F3}, Long: {1:F3}, Alt: {2:F3}",
                                Loc.Latitude, Loc.Longitude, Loc.Altitude);
                        }
                    }
                    catch (FeatureNotSupportedException)
                    {
                        // Handle not supported on device exception
                        LocationString = "Feature not supported";
                    }
                    catch (FeatureNotEnabledException)
                    {
                        // Handle not enabled on device exception
                        LocationString = "Location not enabled";
                    }
                    catch (PermissionException)
                    {
                        // Handle permission exception
                        LocationString = "Permission Denied";
                    }
                    catch (Exception)
                    {
                        // Unable to get location
                        LocationString = "Error - cannot get location";
                    }
                },
                canExecute: () => { return !IsBusy;  }
                );

            //Command for launching a map application with the same location
            MapCommand = new Command(
                execute: async () =>
                {
                    var options = new MapLaunchOptions { Name = "Current Location" };
                    await Map.OpenAsync(Loc, options);
                },
                canExecute: () => 
                {
                    return (Loc != null) && (IsBusy == false);
                } );

        }

        // **************************** EVENTS *****************************


    }
}
