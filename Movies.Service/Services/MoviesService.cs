using AutoMapper;
using Movies.Core.DTOs;
using Movies.Core.Model;
using Movies.Core.Repositories;
using Movies.Core.Services;
using Movies.Core.UnitOfWorks;
using NLayerApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Service.Services
{
    public class MoviesService : GenericService<Movie>, IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMapper _mapper;
        public MoviesService(IGenericRepository<Movie> repository,
            IUnitOfWork unitOfWork, IMoviesRepository moviesRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _moviesRepository = moviesRepository;
            _mapper = mapper;
        }

    }
}
