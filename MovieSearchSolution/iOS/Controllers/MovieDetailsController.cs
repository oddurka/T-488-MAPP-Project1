using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class MovieDetailsController : UIViewController
    {
        private const double StartX = 20;
        private const double StartY = 150;
        private const double Height = 100;
        private const double Width = 217;

        private Film _movie;

        public MovieDetailsController(Film movie)
        {
            this._movie = movie;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.White;
            this.Title = "Movie Info";

            var movieTitleLabel = MovieTitleLabel();
            var imageview = MoviePoster();
            var movieDetailLabel = MovieDetailLabel();
            var movieTimeAndGenreLabel = MovieTimeAndGenreLabel();


            this.View.AddSubviews(new UIView[] { movieTitleLabel , movieDetailLabel, movieTimeAndGenreLabel, imageview });
        }

        public UIImageView MoviePoster()
        {
            UIImageView imageView = new UIImageView()
            {
                Frame = new CGRect(15, 80, 135, 200),
                Image = UIImage.FromFile(this._movie.PosterPath)
            };

            return imageView;
        }

        public UILabel MovieTitleLabel()
        {
            var titleLabel = new UILabel()
            {
                Frame = new CGRect(this.View.Bounds.X + 153, 20, Width, 200),
                Text = this._movie.Title + " (" + this._movie.ReleaseYear + ")",
                Lines = 0,
                Font = UIFont.FromName("Baskerville", 24f),
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear
            };
            return titleLabel;
        }

        public UILabel MovieDetailLabel()
        {
            var detailLabel = new UILabel()
            {
                Frame = new CGRect(this.View.Bounds.X, 280, this.View.Bounds.Width - 2, Width),
                Text = this._movie.Description,
                Lines = 0
            };

            return detailLabel;
        }

        public UILabel MovieTimeAndGenreLabel()
        {
            var timeLabel = new UILabel()
            {
                Frame = new CGRect(this.View.Bounds.X + 153, this.View.Bounds.Y + 180, Width, Height),
                Text = this._movie.Runtime + " min | " + string.Join(", ", this._movie.Genre),
                Lines = 0
            };

            return timeLabel;
        }

    }
}
