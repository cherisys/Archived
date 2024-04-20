using Api.Entities;
using Api.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User() {Id=1,Username="Delhi", Password="Del", Firstname="Mohd", Lastname="Arshad"},
            new User() {Id=2, Username="Dehradun", Password="Ddn", Firstname="Mohd", Lastname="Asjad"}
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public User Authenticate(string username, string password)
        {
            var _user = _users.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (_user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,_user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                                            new SymmetricSecurityKey(key),
                                            SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            _user.Token = tokenHandler.WriteToken(token);
            _user.Password = null;
            return _user;
        }


        public IEnumerable<User> GetAll()
        {
            return _users.Select(x => { x.Password = null; return x; });
        }
    }
}
