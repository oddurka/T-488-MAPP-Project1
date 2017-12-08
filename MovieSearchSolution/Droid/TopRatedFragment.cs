using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Support.Design.Widget;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
    public class TopRatedFragment : Fragment
    {
        private readonly FilmCollection _movieCollection;
        private ListView _listView;
        private ProgressBar _progressBar;

        public TopRatedFragment(FilmCollection movies)
        {
            this._movieCollection = movies;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.TopRated, container, false);

            _listView = rootView.FindViewById<ListView>(Resource.Id.topRatedListView);
            _progressBar = rootView.FindViewById<ProgressBar>(Resource.Id.progressBar_cyclic);

            _listView.ItemClick += (sender, args) =>
            {
                var intent = new Intent(rootView.Context, typeof(MovieDetailsActivity));
                intent.PutExtra("movieDetails", JsonConvert.SerializeObject(this._movieCollection._movies[args.Position]));
                this.StartActivity(intent);
            };

            return rootView;
        }

        public async System.Threading.Tasks.Task GetTopRatedAsync()
        {
            this._movieCollection._movies = await FilmAPISearches.PopulateMovieListAsync(FilmAPISearches.movieApi, await FilmAPISearches.movieApi.GetTopRatedAsync());
            _listView.Adapter = new MovieListAdapter(this.Activity, this._movieCollection._movies);
            _progressBar.Visibility = Android.Views.ViewStates.Gone;
        }
    }
}