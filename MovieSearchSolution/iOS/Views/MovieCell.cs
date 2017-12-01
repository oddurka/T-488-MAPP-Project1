using System;
using Foundation;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;

namespace MovieSearch.iOS.Views
{
    public class MovieCell : UITableViewCell
    {
        private const double ImageHeight = 33;

        private UIImageView _imageView;
        private UILabel _movieTitleLabel;
        private UILabel _castMembers;

        public MovieCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.Blue;

            this._imageView = new UIImageView()
            {
                Frame = new CGRect(this.ContentView.Bounds.Width + 2, 5, 27, 40)
            };

            this._movieTitleLabel = new UILabel()
            {
                Frame = new CGRect(5, 5, this.ContentView.Bounds.Width - 15, 25),
                Font = UIFont.FromName("Didot-Bold", 22f),
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear
            };

            this._castMembers = new UILabel()
            {
                Frame = new CGRect(this.ContentView.Bounds.X + 2, 25, this.ContentView.Bounds.Width + 1, 20),
                Font = UIFont.FromName("Arial", 12f),
                TextColor = UIColor.FromRGB(0, 100, 0),
                TextAlignment = UITextAlignment.Left,
                BackgroundColor = UIColor.Clear,
                Lines = 0
            };

            this.ContentView.AddSubviews(new UIView[] { this._movieTitleLabel, this._castMembers, this._imageView });

            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        public void UpdateCell(string title, string releaseYear, List<string> castMembers, string posterPath)
        {
            if(posterPath != null)
                this._imageView.Image = UIImage.FromFile(posterPath);
            this._movieTitleLabel.Text = title + " (" + releaseYear + ")";
            this._castMembers.Text = string.Join(", ", castMembers);
        }
    }
}
