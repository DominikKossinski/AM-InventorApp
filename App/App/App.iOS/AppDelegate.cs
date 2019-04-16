using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using CoreMotion;
using Foundation;
using UIKit;

namespace App.iOS
{




    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        UIWindow window;
        UILabel label;
        CMMotionManager manager;
        UIImageView imageView;
        UIView v;
        UIColor color;
        UIButton colorButton;
        UIButton clearButton;
        NSArray<UIView> views;

        bool counting = false;
        int up = 0;
        float radius = 10;
        float x = 250;
        float y = 250;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds);
            window.RootViewController = new UIViewController();
            window.MakeKeyAndVisible();

            v = new UIView(window.Bounds);
            v.BackgroundColor = UIColor.White;

            label = new UILabel(new CGRect(50, 50, 200, 21));
            label.Text = "Abc";
            label.TextColor = UIColor.Black;

            colorButton = new UIButton(new CGRect(50, 75, 100, 25));
            colorButton.SetTitle("Zmień Kolor", UIControlState.Normal);
            colorButton.AddTarget(buttonEventHandler, UIControlEvent.TouchUpInside);
            v.AddSubview(colorButton);

            clearButton = new UIButton(new CGRect(175, 100, 100, 25));
            clearButton.SetTitle("Wyczyść", UIControlState.Normal);
            clearButton.AddTarget(buttonEventHandler, UIControlEvent.TouchUpInside);
            v.AddSubview(clearButton);

            color = new UIColor(0.7f, 0.1f, 0.2f, 1.0f);
            views = new NSArray<UIView>();

            manager = new CMMotionManager();
            if (manager.AccelerometerAvailable)
            {
                label.Text = "Available";
                if (manager.AccelerometerActive)
                {
                    label.Text = "Active";
                }
                manager.AccelerometerUpdateInterval = 0.01;
                manager.StartAccelerometerUpdates(NSOperationQueue.CurrentQueue,
                    new CMAccelerometerHandler(calculate));
            }
            else
            {
                UIView circle = new UIView(new CGRect(250, 250, 2 * radius, 2 * radius));
                circle.Layer.BorderWidth = 1;
                circle.BackgroundColor = color;
                views.Append(circle);
                v.AddSubview(circle);
                label.Text = "No accelerometer";
                
            }
            v.AddSubview(label);

            imageView = new UIImageView(new CGRect(0, 50, 500, 500));


            window.AddSubview(v);



            return true;
        }

        public void buttonEventHandler(object sender, EventArgs e)
        {
            if (sender == colorButton)
            {
               Random r = new Random();
                color = new UIColor((r.Next() % 256) / 255, (r.Next() % 256) / 255, (r.Next() % 256) / 255, 1);
            } else if(sender == clearButton)
            {
                for(int i = 0; i < (int) views.Count; i++)
                {
                    views[i].RemoveFromSuperview();
                }
                views = new NSArray<UIView>();
            }
        }

        private void calculate(CMAccelerometerData data, NSError error)
        {
            ///label.Text = string.Format("x={0:f}, y={1:f}, z={2:f}", data.Acceleration.X,
               //data.Acceleration.Y, data.Acceleration.Z);
            x -= (float)data.Acceleration.X;
            y += (float)data.Acceleration.Y;
            if (x > 500)
            {
                x = 500;
            }
            if (x < 0)
            {
                x = 0;
            }
            if (y > 700)
            {
                y = 700;
            }
            if (y < 200)
            {
                y = 200;
            }
            if (data.Acceleration.Z - 9.8 < -3)
            {
                if (counting)
                {
                    if (up == 0)
                    {
                        up = 5;
                    }
                }
                else
                {
                    up = 5;

                }

            }
            if (data.Acceleration.Z - 9.8 > 3.5)
            {
                if (counting)
                {
                    up = -5;
                }
                else
                {
                    up = -5;

                }
            }
            if (radius > 30)
            {
                radius = 30;
            }
            if (radius < 5)
            {
                radius = 5;
            }
            UIView circle = new UIView(new CGRect(x, y, 2 * radius, 2 * radius));
            circle.Layer.BorderWidth = 1;
            circle.BackgroundColor = color;
            views.Append(circle);
            v.AddSubview(circle);
        }

    }
}
