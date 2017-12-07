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

        public TopRatedFragment(FilmCollection movies)
        {
            this._movieCollection = movies;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.TopRated, container, false);

            return rootView;
        }

        public async System.Threading.Tasks.Task GetTopRatedAsync()
        {

            this._movieCollection._movies = await FilmAPISearches.PopulateMovieListAsync(FilmAPISearches.movieApi, await FilmAPISearches.movieApi.GetTopRatedAsync());
            var intent = new Intent(this.Context, typeof(MovieListActivity));
            intent.PutExtra("movieList", JsonConvert.SerializeObject(this._movieCollection._movies));

            this.StartActivity(intent);

        }
    }
}