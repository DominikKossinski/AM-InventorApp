using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using Android.Hardware;
using Android.Graphics;


namespace App.Droid
{
    [Activity(Label = "App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ISensorEventListener
    {


        public delegate void TickEvent(long millisUntilFinished);
        public delegate void FinishEvent();

        
        private static readonly object _syncLock = new object();
        private SensorManager _sensorManager;
        private TextView myTextView;
        private ImageView canvasImageView;
        private Bitmap bmp;
        private Canvas canvas;
        private CountDown timer;
        private bool counting = false;
        int up = 0;
        float radius = 5;
        private float x = 250;
        private float y = 250;
        class CountDown : CountDownTimer
        {
            public event TickEvent Tick;
            public event FinishEvent Finish;
            public MainActivity activity;
     

            public CountDown(MainActivity activity, long totaltime, long interval)
                : base(totaltime, interval)
            {
                this.activity = activity;
            }

            public override void OnTick(long millisUntilFinished)
            {
                if (Tick != null)
                    Tick(millisUntilFinished);
            }

            public override void OnFinish()
            {
                activity.setRadius(activity.getRadius() + activity.getUp());
                activity.setCounting(false);
                activity.setUp(0);
                if (Finish != null)
                    Finish();
            }
        }

        public void setUp(int up)
        {
            this.up = up;
        }
        public void setRadius(float radius)
        {
            this.radius = radius;
        }

        public void setCounting(bool counting)
        {
            this.counting = counting;
        }

        public float getRadius()
        {
            return this.radius;
        }

        public int getUp()
        {
            return this.up;
        }
        
        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            throw new NotImplementedException();
        }

        public void OnSensorChanged(SensorEvent e)
        {
            lock (_syncLock)
            {
                myTextView.Text = string.Format("x={0:f}, y={1:f}, z={2:f}", e.Values[0], e.Values[1], e.Values[2]);
                x -= e.Values[0];
                y += e.Values[1];
                if(x > 500)
                {
                    x = 500;
                }
                if( x < 0)
                {
                    x = 0;
                }
                if (y > 500)
                {
                    y = 500;
                }
                if (y < 0)
                {
                    y = 0;
                }
                if (e.Values[2] - 9.8 < -3)
                {
                    if (counting)
                    {
                        if (up == 0)
                        {
                            up = 5;
                        }
                    } else
                    {
                        up = 5;
                        counting = true;
                        timer = new CountDown(this, 300, 100);
                        timer.Start();                 
                    }
                    
                }
               if (e.Values[2] - 9.8 > 3.5)
                {
                    if (counting)
                    {
                        up = -5;
                    }
                    else
                    {
                        up = -5;
                        counting = true;
                        timer = new CountDown(this, 300, 50);
                        timer.Start();
                    }
                }
               if (radius > 30)
                {
                    radius = 30;
                }
               if (radius < 2)
                {
                    radius = 2;
                }
       
                canvas.DrawCircle(x, y, radius, new Paint());
                canvasImageView.SetImageBitmap(bmp);
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
            canvasImageView = FindViewById<ImageView>(Resource.Id.canvasImageView);
            bmp = Bitmap.CreateBitmap(500, 500, Bitmap.Config.Argb8888);
            canvas = new Canvas(bmp);
            

            
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