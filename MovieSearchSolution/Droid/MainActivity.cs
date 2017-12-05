using Android.App;
using Android.Widget;
using Android.OS;
using DM.MovieApi;
using System;
using Android.Views.InputMethods;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
    [Activity(Label = "MovieSearch", Theme = "@style/MyTheme", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        public static FilmCollection Movies { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            MovieDbFactory.RegisterSettings(Movies);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var movieInputField = this.FindViewById<EditText>(Resource.Id.movieInputEditText);
            var searchBtn = this.FindViewById<Button>(Resource.Id.getMovieButton);

            // Get our button from the layout resource,
            // and attach an event to it
            searchBtn.Click += async (object sender, EventArgs e) =>
            {
                Movies._movies = await FilmAPISearches.PopulateMovieListAsync(FilmAPISearches.movieApi, await FilmAPISearches.movieApi.SearchByTitleAsync(movieInputField.Text));
                var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
                manager.HideSoftInputFromWindow(movieInputField.WindowToken, 0);

                var intent = new Intent(this, typeof(MovieListActivity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(Movies._movies));
                this.StartActivity(intent);

            };
        }
    }
}

