using System;
using System.Linq;
using Depcy.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Robot))]
namespace Depcy.iOS
{
    public class Robot : IRobot
    {
        public Robot()
        {
        }

        public int WalkForward(int b)
        {
            UIAlertController contr = UIAlertController.Create("Moving Forward", $"Sending {b} pulses", UIAlertControllerStyle.Alert);
            contr.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (action) => Console.WriteLine("OK Pressed")));
            contr.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Default, (action) => Console.WriteLine("Cancel Pressed")));

            //Find ViewController context
            UIViewController vc = UIApplication.SharedApplication.KeyWindow.RootViewController;

            //The following is needed for cases where the visible view controller is presented modally.
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }

            //I cannot prove the following two lines are strictly necessary
            if (vc is UINavigationController navController)
                vc = navController.ViewControllers.Last();

            //Finally - present the AlertController
            vc.PresentViewController(contr, true, null);
            return b - 1;
        }

    }
}
