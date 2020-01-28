using System.Threading.Tasks;
using MicroServices.Services.Identity.Domain.Models;

namespace MicroServices.Services.Identity.Services
{
    public interface IUserService
    {
        Task Register(string email, string password, string name);
        Task LoginAsync(string email,string password);
    }
}