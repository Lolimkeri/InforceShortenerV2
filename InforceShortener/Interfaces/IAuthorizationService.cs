using InforceShortener.Data.Models;
using System.Security.Claims;

namespace InforceShortener.Interfaces
{
    public interface IAuthorizationService
    {
        IQueryable<User> GetUsers();

        void AddUser(User user);

        string GetUsernameByHttpContext(HttpContext httpContext);

        string GetRoleByHttpContext(HttpContext httpContext);

        string GetToken(string username, string password);

        ClaimsIdentity GetIdentity(string username, string password);
    }
}
