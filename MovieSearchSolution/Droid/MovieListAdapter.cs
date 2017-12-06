using System;
using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;

namespace MovieSearch.Droid
{
    public class MovieListAdapter : BaseAdapter<Film>
    {
        private readonly Activity _context;
        private readonly List<Film> _movieList;

        public MovieListAdapter(Activity context, List<Film> movieList)
        {
            this._context = context;
            this._movieList = movieList;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
                view = this._context.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);

            var movie = this._movieList[position];
            view.FindViewById<TextView>(Resource.Id.title).Text = movie.Title;
            view.FindViewById<TextView>(Resource.Id.actors).Text = String.Join(", ", movie.Actors);
            Glide.With(this._context).Load(movie.PosterPath).Into(view.FindViewById<ImageView>(Resource.Id.posterPath));

            return view;
        }

        public override int Count => this._movieList.Count;

        public override Film this[int position] => this._movieList[position];
    }
}