using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using UIKit;
using System.Net.Http;
using MovieDownload;
using System.Threading;
using System.IO;

namespace MovieSearch.iOS.Controllers
{
    public partial class ViewController : UIViewController
    {
        private const double StartX = 20;
        private const double StartY = 80;
        private const double Height = 50;

        private List<Film> _movieList;

        public ViewController(List<Film> movieList)
        {
            this._movieList = movieList;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.White;
            this.Title = "Movie Searcher 3000";

            var activityIndicator = ActivityIndicator();
            var promptLabel = PromptLabel();
            var movieField = MovieField();
            var navigationButton = NavigationButton(movieField, activityIndicator);

            this.View.AddSubviews(new UIView[] { promptLabel, movieField, navigationButton,activityIndicator });
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }

        public UIButton NavigationButton(UITextField movieField, UIActivityIndicatorView activityIndicator)
        {
            var navigationButton = UIButton.FromType(UIButtonType.RoundedRect);
            navigationButton.Frame = new CGRect(StartX, StartY + 2 * Height, this.View.Bounds.Width - 2 * StartX, Height);
            navigationButton.SetTitle("Get Movies", UIControlState.Normal);

            navigationButton.TouchUpInside += async (sender, args) =>
            {
                navigationButton.Enabled = false;
                this.TabBarController.TabBar.UserInteractionEnabled = false;
                activityIndicator.StartAnimating();
                this._movieList.Clear();
                this._movieList = await FilmAPISearch.PopulateMovieListAsync(FilmAPISearch.movieApi ,await FilmAPISearch.movieApi.SearchByTitleAsync(movieField.Text));
                movieField.ResignFirstResponder();
                this.NavigationController.PushViewController(new MovieListController(this._movieList), true);
                activityIndicator.StopAnimating();
                navigationButton.Enabled = true;
                this.TabBarController.TabBar.UserInteractionEnabled = true;

            };
            //this._movieList.Clear();
            return navigationButton;
        }

        public UIActivityIndicatorView ActivityIndicator()
        {
            var activityIndicator = new UIActivityIndicatorView();
            activityIndicator.Color = UIColor.Gray;
            activityIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;
            activityIndicator.Frame = new CGRect(StartX, StartY + 4 * Height, this.View.Bounds.Width - 2 * StartX, Height);

            return activityIndicator;
        }

        public UILabel PromptLabel()
        {
            var promptLabel = new UILabel()
            {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
                Text = "Enter Movie Title:"
            };

            return promptLabel;
        }

        public UITextField MovieField()
        {
            var movieField = new UITextField()
            {
                Frame = new CGRect(StartX, StartY + Height, this.View.Bounds.Width - 2 * StartX, Height),
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            return movieField;
        }

        /*public async Task<List<Film>> FindMovieAsync(string movieField)
        {
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            List<Film> movies = new List<Film>();

            if(movieField == "")
            {
                return movies;
            }

            StorageClient storageClient = new StorageClient();
            ImageDownloader imageDownloader = new ImageDownloader(storageClient);

            ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(movieField);

            foreach (MovieInfo info in response.Results)
            {
                ApiQueryResponse<MovieCredit> castResponse = await movieApi.GetCreditsAsync(info.Id);
                ApiQueryResponse<Movie> infoResponse = await movieApi.FindByIdAsync(info.Id);

                Film movie = new Film()
                {
                    Title = info.Title,
                    ReleaseYear = infoResponse.Item.ReleaseDate.Year,
                    Runtime = infoResponse.Item.Runtime.ToString(),
                    Genre = new List<string>(),
                    Actors = new List<string>(),
                    Description = infoResponse.Item.Overview,
                    PosterPath = infoResponse.Item.PosterPath
                };

                if (infoResponse.Item.Genres.Count != 0)
                {
                    for (int i = 0; i < infoResponse.Item.Genres.Count; i++)
                    {
                        movie.Genre.Add(infoResponse.Item.Genres[i].Name);
                    }
                }

                if(castResponse.Item.CastMembers.Count != 0)
                {
                    for (int i = 0; i < castResponse.Item.CastMembers.Count && i < 3; i++)
                    {
                        movie.Actors.Add(castResponse.Item.CastMembers[i].Name);
                    }
                }
                movies.Add(movie);

                if(movie.PosterPath != null)
                    await imageDownloader.GetImage(movies);
            }
            return movies;
        }*/
    }
}
