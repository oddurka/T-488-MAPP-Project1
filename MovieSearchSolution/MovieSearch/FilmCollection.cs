using System;
using System.Collections.Generic;
using DM.MovieApi;

namespace MovieSearch
{
    public class FilmCollection : IMovieDbSettings
    {
        string IMovieDbSettings.ApiKey => "da6faf6c896968f856209d2aa32ef48c";
        string IMovieDbSettings.ApiUrl => "http://api.themoviedb.org/3/";

        public List<Film> _movies;

        public FilmCollection()
        {
            this._movies = new List<Film>();
        }
    }
}
