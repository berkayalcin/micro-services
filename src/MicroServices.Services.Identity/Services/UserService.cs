using System.Threading.Tasks;
using MicroServices.Common.Exceptions;
using MicroServices.Services.Identity.Domain.Models;
using MicroServices.Services.Identity.Domain.Repositories;
using MicroServices.Services.Identity.Domain.Services;

namespace MicroServices.Services.Identity.Services
{
    public class UserService:IUserService
    {
        private IUserRepository _userRepository;
        private IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        public async Task Register(string email, string password, string name)
        {
            var user = await _userRepository.GetAsync(email);
            if (user!=null)
            {
                throw new MicroServicesException("email_in_use",$"Email : {email} is already in use");
            }
            user=new User(email,name);
            user.SetPassword(password,_encrypter);
            await _userRepository.AddAsync(user);
        }

        public async Task LoginAsync(string email, string password)
        {

            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new MicroServicesException("invalid_creadentials", $"Invalid Credentials");
            }

            if (!user.ValidatePassword(password,_encrypter))
            {
                throw new MicroServicesException("invalid_creadentials", $"Invalid Credentials");

            }

        }
    }
}