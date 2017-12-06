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
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
    [Activity(Label = "MovieDetailsActivity", Theme = "@style/MyTheme")]
    public class MovieDetailsActivity : Activity
    {
        private Film _movie;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MovieDetails);

            var jsonStr = this.Intent.GetStringExtra("movieDetails");
            this._movie = JsonConvert.DeserializeObject<Film>(jsonStr);

            var moviePoster = this.FindViewById<ImageView>(Resource.Id.posterPath);
            var movieTitle = this.FindViewById<TextView>(Resource.Id.title);
            var movieRuntimeAndGenre = this.FindViewById<TextView>(Resource.Id.runtimeAndGenre);
            var movieDescription = this.FindViewById<TextView>(Resource.Id.description);

            movieTitle.Text = this._movie.Title + "(" + this._movie.ReleaseYear + ")";
            movieRuntimeAndGenre.Text = this._movie.Runtime.ToString() + " min | " + String.Join(",", this._movie.Genre);
            movieDescription.Text = this._movie.Description;
        }
    }
}