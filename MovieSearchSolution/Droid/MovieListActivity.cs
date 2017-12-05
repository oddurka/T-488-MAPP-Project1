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
    [Activity(Label = "MovieListActivity", Theme = "@style/MyTheme")]
    public class MovieListActivity : ListActivity
    {
        private List<Film> _movieList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            var jsonStr = this.Intent.GetStringExtra("movieList");
            this._movieList = JsonConvert.DeserializeObject<List<Film>>(jsonStr);

            this.ListView.ItemClick += (sender, args) =>
            {
                var intent = new Intent(this, typeof(MovieDetailsActivity));
                intent.PutExtra("movieDetails", JsonConvert.SerializeObject(this._movieList[args.Position]));
                this.StartActivity(intent);
            };

            this.ListAdapter = new MovieListAdapter(this, this._movieList);
        }
    }
}