using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Movies.Core.Helper;
using Movies.Core.Model;
using Movies.Core.Model.ResponseModel;
using Movies.Core.Repositories;
using Movies.Core.Services;
using Movies.Core.UnitOfWorks;
using NLayerApp.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Service.Services
{
    public class UsersService : GenericService<User>, IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public IConfiguration _configuration;
        public UsersService(IGenericRepository<User> repository,
            IUnitOfWork unitOfWork, IUsersRepository usersRepository, IMapper mapper, IConfiguration configuration) : base(repository, unitOfWork)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        /// <summary>
        /// Mail ve Passworduna göre kullanıcı detayını getirir.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<User> GetByUserMailAndPass(string email, string password)
        {
            var userDetail = _usersRepository.GetUser(email, password);

            if (userDetail is null)
                return null;

            return userDetail;
        }

        /// <summary>
        /// Seçilen Filme yorum ve puan ekler.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mailAdress"></param>
        /// <returns></returns>
        public async Task<User> InsertUser(User model)
        {
                User entity = new User();

                entity.Name = model.Name;
                entity.Username = model.Username;
                entity.Surname = model.Surname;
                entity.Mail = model.Mail;
                entity.Password = model.Password;

                var resultInsert = await this.AddAsync(entity);

            if (resultInsert is null)
                return null;

            return resultInsert;
        }

        /// <summary>
        /// JWT token oluşturur
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetToken(User userData)
        {
            try
            {
                if (userData != null && userData.Mail != null && userData.Password != null)
                {

                    var validationEmailResult = CustomValidation.IsValidEmail(userData.Mail);
                    if (!validationEmailResult.IsValid)
                        return ApiResponse.CreateResponse(HttpStatusCode.NoContent, ApiResponse.ErrorMessage);

                    var validationPassowrdResult = CustomValidation.IsValidPassword(userData.Password);
                    if (!validationPassowrdResult.IsValid)
                        return ApiResponse.CreateResponse(HttpStatusCode.NoContent, ApiResponse.ErrorMessage);

                    var user = await GetByUserMailAndPass(userData.Mail, userData.Password);

                    if (user is null)
                        user = await InsertUser(userData);

                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Key"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Name", user.Name),
                        new Claim("Username", user.Username),
                        new Claim("Mail", user.Mail)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return ApiResponse.CreateResponse(HttpStatusCode.OK, ApiResponse.SuccessMessage, new JwtSecurityTokenHandler().WriteToken(token));

                }
                else
                    return ApiResponse.CreateResponse(HttpStatusCode.NoContent, ApiResponse.ErrorMessage);

            }
            catch (Exception ex)
            {
                MailHelper.SystemInformationMail(ex.ToString(), "GetToken / İşlem Sırasında Hata Meydana Geldi.");
                return ApiResponse.CreateResponse(HttpStatusCode.InternalServerError, ApiResponse.ErrorMessage);
            }
        }
        
    }
}
