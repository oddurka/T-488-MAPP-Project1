using System;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class TabBarController: UITabBarController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.TabBar.BackgroundColor = UIColor.LightGray;
            this.TabBar.TintColor = UIColor.Blue;

            this.SelectedIndex = 0;
        }
    }
}
