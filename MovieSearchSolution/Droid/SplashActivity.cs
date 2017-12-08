using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MovieSearch.Droid
{
    [Activity(Label = "SplashActivity", Theme = "@style/MyTheme.Splash", MainLauncher = true, Icon = "@mipmap/newicon")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            MainActivity.Movies = new FilmCollection();

            this.StartActivity(typeof(MainActivity));
            this.Finish();
        }
    }
}