using Movies.Core.Model;
using Movies.Core.Model.ResponseModel;
using Movies.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Services
{
    public interface IUsersService : IGenericService<User>
    {
        Task<User> GetByUserMailAndPass(string email, string password);

        Task<ApiResponse> InsertUser(User model);

        Task<ApiResponse> GetToken(User userData);
    }
}
