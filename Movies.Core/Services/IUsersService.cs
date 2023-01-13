using Movies.Core.Model;
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
        Task<User> GetUser(string email, string password);
    }
}
