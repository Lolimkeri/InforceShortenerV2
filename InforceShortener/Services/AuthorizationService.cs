using InforceShortener.Configuration;
using InforceShortener.Data;
using InforceShortener.Data.Models;
using InforceShortener.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InforceShortener.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly Repository<User> _userRepository;

        public AuthorizationService(Repository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.KEY));
        }

        public IQueryable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public void AddUser(User user)
        {
            if(GetUsers().FirstOrDefault(u => u.Username== user.Username) == null)
            {
                throw new ArgumentException("User with this username already exists");
            }

            _userRepository.Insert(user);
            _userRepository.Save();
        }

        public string GetUsernameByHttpContext(HttpContext httpContext)
        {
            return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType).Value;
        }

        public string GetRoleByHttpContext(HttpContext httpContext)
        {
            return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
        }

        public string GetToken(string username, string password)
        {
            var identity = GetIdentity(username, password);

            if (identity == null)
            {
                return null;
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: Constants.ISSUER,
                    audience: Constants.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(Constants.LIFETIME)),
                    signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GetIdentity(string username, string password)
        {
            User user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
