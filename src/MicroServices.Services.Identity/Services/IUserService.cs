using System.Threading.Tasks;
using MicroServices.Common.Auth;
using MicroServices.Services.Identity.Domain.Models;

namespace MicroServices.Services.Identity.Services
{
    public interface IUserService
    {
        Task Register(string email, string password, string name);
        Task<JsonWebToken> LoginAsync(string email,string password);
    }
}