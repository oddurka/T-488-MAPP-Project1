using Android.App;
using Android.Widget;
using Android.OS;
using DM.MovieApi;
using System;
using Android.Views.InputMethods;
using System.Collections.Generic;

namespace MovieSearch.Droid
{
    [Activity(Label = "MovieSearch", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var movies = new FilmCollection();
            MovieDbFactory.RegisterSettings(movies);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var movieInputField = this.FindViewById<EditText>(Resource.Id.movieInputEditText);
            var searchBtn = this.FindViewById<Button>(Resource.Id.getMovieButton);
            var outputViewText = this.FindViewById<TextView>(Resource.Id.outputViewText);

            // Get our button from the layout resource,
            // and attach an event to it
            searchBtn.Click += async (object sender, EventArgs e) =>
            {
                List<Film> movie = await FilmAPISearches.PopulateMovieListAsync(FilmAPISearches.movieApi, await FilmAPISearches.movieApi.SearchByTitleAsync(movieInputField.Text));
                var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
                manager.HideSoftInputFromWindow(movieInputField.WindowToken, 0);

                outputViewText.Text = movie[0].ToString(); 
            };
        }
    }
}

