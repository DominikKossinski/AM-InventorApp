using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware;
using Xamarin.Forms;

namespace App.Droid
{
    [Activity(Label = "App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ISensorEventListener
    {
        private static readonly object _syncLock = new object();
        private SensorManager _sensorManager;
        private TextView myTextView;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            throw new NotImplementedException();
        }

        public void OnSensorChanged(SensorEvent e)
        {
            lock (_syncLock)
            {
                myTextView.Text = string.Format("x={0:f}, y={1:f}, z={2:f}", e.Values[0], e.Values[1], e.Values[2]);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            /*TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
*/            
            base.OnCreate(savedInstanceState);
            /*global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());*/

            SetContentView(Resource.Layout.Main);
            _sensorManager = (SensorManager)GetSystemService(SensorService);
            myTextView = FindViewById<TextView>(Resource.Id.myTextView);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _sensorManager.RegisterListener(this,
                _sensorManager.GetDefaultSensor(SensorType.Accelerometer),
                SensorDelay.Ui);
        }

        protected override void OnPause()
        {
            base.OnPause();
            _sensorManager.UnregisterListener(this);
        }
    }
}