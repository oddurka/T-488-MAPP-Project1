using System;
using System.Collections.Generic;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using System.Threading.Tasks;
using MovieDownload;

namespace MovieSearch
{
    public static class FilmAPISearch
    {
        public static IApiMovieRequest movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

        public static async Task<List<Film>> PopulateMovieListAsync(IApiMovieRequest movieApi, ApiSearchResponse<MovieInfo> apiResponse)
        {
            List<Film> movies = new List<Film>();

            StorageClient storageClient = new StorageClient();
            ImageDownloader imageDownloader = new ImageDownloader(storageClient);

            foreach (MovieInfo info in apiResponse.Results)
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

                if (castResponse.Item.CastMembers.Count != 0)
                {
                    for (int i = 0; i < castResponse.Item.CastMembers.Count && i < 3; i++)
                    {
                        movie.Actors.Add(castResponse.Item.CastMembers[i].Name);
                    }
                }
                movies.Add(movie);

                if (movie.PosterPath != null)
                    await imageDownloader.GetImage(movies);
            }
            return movies;
        }
    }
}
