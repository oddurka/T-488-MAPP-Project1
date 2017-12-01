using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieDownload;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class TopRatedMovieController : UITableViewController
    {
        private List<Film> _movieList;
        private UIActivityIndicatorView activityIndicator;

        public TopRatedMovieController(List<Film> movieList)
        {
            this._movieList = movieList;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 1);

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "Top Rated";

            this.activityIndicator = ActivityIndicator();

            this.View.AddSubview(this.activityIndicator);
        }

        protected void OnSelectedMovie(int row)
        {
            this.NavigationController.PushViewController(new MovieDetailsController(this._movieList[row]), true);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(true);

            this.TabBarController.ViewControllerSelected += async (sender, args) =>
            {
                
                this.activityIndicator.StartAnimating();
                this.TabBarController.TabBar.UserInteractionEnabled = false;
                this._movieList = await FilmAPISearch.PopulateMovieListAsync(FilmAPISearch.movieApi, await FilmAPISearch.movieApi.GetTopRatedAsync());
                this.TableView.Source = new MovieListDataSource(this._movieList, OnSelectedMovie);
                this.TableView.ReloadData();
                this.activityIndicator.StopAnimating();
                this.TabBarController.TabBar.UserInteractionEnabled = true;
            };

        }

        public UIActivityIndicatorView ActivityIndicator()
        {
            var activityIndicator = new UIActivityIndicatorView();
            activityIndicator.Color = UIColor.Gray;
            activityIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;
            activityIndicator.Frame = new CGRect(20, 80, this.View.Bounds.Width - 2 * 20, 50);

            return activityIndicator;
        }

    }
}
