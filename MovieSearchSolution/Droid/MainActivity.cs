using DM.MovieApi;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Fragment = Android.Support.V4.App.Fragment;


namespace MovieSearch.Droid
{
    [Activity(Label = "Movie Search", Theme = "@style/MyTheme")]
    public class MainActivity : FragmentActivity
    {
        public static FilmCollection Movies { get; set; }

        private SearchFragment _searchFragment;
        private TopRatedFragment _topRatedFragment;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            Movies = new FilmCollection();
            MovieDbFactory.RegisterSettings(Movies);

            SetContentView(Resource.Layout.Main);

            var fragments = new Fragment[]
            {
                this._searchFragment = new SearchFragment(Movies),
                this._topRatedFragment = new TopRatedFragment(Movies)
            };

            var titles = CharSequence.ArrayFromStringArray(new[] { "Search", "Top Rated" });

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);

            var tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            tabLayout.TabSelected += async (sender, args) =>
            {
                if (fragments[args.Tab.Position] == this._topRatedFragment)
                {
                    this.ActionBar.Title = "Top Rated";
                    await this._topRatedFragment.GetTopRatedAsync();
                }
            };

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "Movie Search";
        }
    }
}

