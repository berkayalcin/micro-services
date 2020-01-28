using System.Threading.Tasks;

namespace MicroServices.Services.Identity.Domain.Services
{
    public interface IEncrypter
    {
        string GetSalt();
        string GetHash(string value, string salt);
    }
}