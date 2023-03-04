using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Tokens;
using Movies.Core.Model;
using Movies.Repository;
using NLayerApp.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly IUsersService _usersService;

        public TokenController(IConfiguration configuration,
            AppDbContext context,
            IUsersService usersService)
        {
            _configuration = configuration;
            _context = context;
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User userData)
        {
            var result = await _usersService.GetToken(userData);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}

