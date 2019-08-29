using System;
using Android.App;
using Depcy.Droid;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(Robot))]
namespace Depcy.Droid
{
    public class Robot : IRobot
    {
        public Robot()
        {
        }

        public int WalkForward(int b)
        {
            var ac = CrossCurrentActivity.Current.Activity;
            AlertDialog.Builder dialog = new AlertDialog.Builder(ac);
            AlertDialog alert = dialog.Create();
            alert.SetTitle($"Moving forward {b} pulses");
            alert.SetMessage("Complex Alert");
            alert.SetButton("OK", (c, ev) => { Console.WriteLine("OK Clicked"); });
            alert.SetButton2("CANCEL", (c, ev) => { Console.WriteLine("Cancel Clicked"); });
            alert.Show();
            return b + 1;
        }
    }
}
