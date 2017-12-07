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
using DM.MovieApi;
using Newtonsoft.Json;
using Android.Views.InputMethods;
using Fragment = Android.Support.V4.App.Fragment;

namespace MovieSearch.Droid
{
    public class SearchFragment : Fragment
    {
        private readonly FilmCollection _movieCollection;

        public SearchFragment(FilmCollection movieList)
        {
            this._movieCollection = movieList;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.SearchInput, container, false);

            var movieInputField = rootView.FindViewById<EditText>(Resource.Id.movieInputEditText);
            var searchBtn = rootView.FindViewById<Button>(Resource.Id.getMovieButton);
            var progressBar = rootView.FindViewById<ProgressBar>(Resource.Id.progressBar_cyclic);
            progressBar.Visibility = Android.Views.ViewStates.Invisible;

            // Get our button from the layout resource,
            // and attach an event to it
            searchBtn.Click += async (object sender, EventArgs e) =>
            {
                var manager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                manager.HideSoftInputFromWindow(movieInputField.WindowToken, 0);

                progressBar.Visibility = Android.Views.ViewStates.Visible;
                searchBtn.Enabled = false;

                if (movieInputField.Text != "")
                    this._movieCollection._movies = await FilmAPISearches.PopulateMovieListAsync(FilmAPISearches.movieApi, await FilmAPISearches.movieApi.SearchByTitleAsync(movieInputField.Text));

                var intent = new Intent(this.Context, typeof(MovieListActivity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(this._movieCollection._movies));

                this.StartActivity(intent);
                progressBar.Visibility = Android.Views.ViewStates.Invisible;
                searchBtn.Enabled = true;
            };

            return rootView;
        }
    }
}