using AutoMapper;
using Movies.Core.DTOs;
using Movies.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Movie, MovieDTOs>().ReverseMap();
            CreateMap<MovieDTOs, Movie>().ReverseMap();

            CreateMap<User, UserDTOs>().ReverseMap();
            CreateMap<UserDTOs, User>().ReverseMap();

            CreateMap<MovieReview, MovieReviewDTOs>().ReverseMap();
            CreateMap<MovieReviewDTOs, MovieReview>().ReverseMap();

            CreateMap<Movie, MovieDetailDTOs>().ReverseMap();
            CreateMap<MovieDetailDTOs, Movie>().ReverseMap();

            CreateMap<MovieReview, MovieReviewDetailsDTOs>().ReverseMap();
            CreateMap<MovieReviewDetailsDTOs, MovieReview>().ReverseMap();

        }
    }
}
