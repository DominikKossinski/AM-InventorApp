using Android.Hardware;
using Android.OS;
using Android.Runtime;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App
{
    public partial class App : Application
    {

        public IntPtr Handle => throw new NotImplementedException();


        
       

        private Label myLabel;

        public App()
        {
            InitializeComponent();
           
            MainPage mainPage = new MainPage();
            myLabel = mainPage.FindByName<Label>("myLabel");
            myLabel.Text = "dziala";
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
