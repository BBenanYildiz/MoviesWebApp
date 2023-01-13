using Movies.Core.DTOs;
using Movies.Core.Model;
using Movies.Core.Model.RequestModel;
using Movies.Core.Model.ResponseModel;
using Movies.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Services
{
    public interface IMoviesService : IGenericService<Movie>
    {
        Task<int> InsertMovies(Root moviesList);

        Task<ApiResponse> SharedMail(int id, string mailAdress);

        Task<ApiResponse> GetDetail(int id);

        Task<ApiResponse> Post(int id, MovieCommentAndPointRequestModel model);

        Movie GetMovieDetailWithByTitle(string orjinal_title);
    }
}
