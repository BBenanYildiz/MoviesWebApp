using AutoMapper;
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
    public class UsersService : GenericService<User>, IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public UsersService(IGenericRepository<User> repository,
            IUnitOfWork unitOfWork, IUsersRepository usersRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await _usersRepository.GetUser(email, password);
        }
    }
}
