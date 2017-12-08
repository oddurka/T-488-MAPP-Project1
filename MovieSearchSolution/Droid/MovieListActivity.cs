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
    [Activity(Label = "Movie List", Theme = "@style/MyTheme")]
    public class MovieListActivity : Activity
    {
        private List<Film> _movieList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MovieList);


            var jsonStr = this.Intent.GetStringExtra("movieList");
            this._movieList = JsonConvert.DeserializeObject<List<Film>>(jsonStr);


            this.FindViewById<ListView>(Resource.Id.movieListView).ItemClick += (sender, args) =>
            {
                var intent = new Intent(this, typeof(MovieDetailsActivity));
                intent.PutExtra("movieDetails", JsonConvert.SerializeObject(this._movieList[args.Position]));
                this.StartActivity(intent);
            };

            this.FindViewById<ListView>(Resource.Id.movieListView).Adapter = new MovieListAdapter(this, this._movieList);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "Movie List";
        }
    }
}