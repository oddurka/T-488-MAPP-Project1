using System;
using System.Collections.Generic;

using Foundation;
using MovieSearch.iOS.Views;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class MovieListDataSource : UITableViewSource
    {
        private readonly List<Film> _movieList;
        private readonly Action<int> _onSelectedMovie;
        private readonly NSString MovieListCellId = new NSString("MovieListCell");

        public MovieListDataSource(List<Film> movieList, Action<int> onSelectedMovie)
        {
            this._movieList = movieList;
            this._onSelectedMovie = onSelectedMovie;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (MovieCell)tableView.DequeueReusableCell((NSString)this.MovieListCellId);

            if(cell == null)
            {
                cell = new MovieCell(this.MovieListCellId);
            }

            var movie = this._movieList[indexPath.Row];

            cell.UpdateCell(movie.Title, movie.ReleaseYear.ToString(), movie.Actors, movie.PosterPath);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this._movieList.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            this._onSelectedMovie(indexPath.Row);
        }
    }
}
