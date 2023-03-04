using Movies.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Repositories
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> GetUser(string email, string password);
    }
}
