using System;
using System.Collections.Generic;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class MovieListController : UITableViewController
    {
        private List<Film> _movieList;

        public MovieListController(List<Film> movieList)
        {
            this._movieList = movieList;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "Movie List";

            this.TableView.Source = new MovieListDataSource(this._movieList, OnSelectedMovie);
        }

        protected void OnSelectedMovie(int row)
        {
            this.NavigationController.PushViewController(new MovieDetailsController(this._movieList[row]), true);
        }
    }


}
