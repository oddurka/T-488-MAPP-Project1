using System;
using System.Collections.Generic;
using DM.MovieApi;

namespace MovieSearch
{
    public class Film
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Runtime { get; set; }
        public List<string> Genre { get; set; }
        public List<string> Actors { get; set; }
        public string Description { get; set; }
        public string PosterPath { get; set; }
    }
}
