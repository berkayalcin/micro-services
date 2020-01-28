using System;
using MicroServices.Common.Exceptions;
using MicroServices.Services.Identity.Domain.Services;

namespace MicroServices.Services.Identity.Domain.Models
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string UserName { get; protected set; }
        public string Salt { get;protected set; }
        public DateTime CreatedAt { get;protected set; }

        protected User()
        {

        }

        public User(string email,string name)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new MicroServicesException("empty_email",$"Email field cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new MicroServicesException("empty_username", $"Username field cannot be empty.");
            }
            Id =Guid.NewGuid();
            Email = email.ToLowerInvariant();
            UserName = name;
            CreatedAt=DateTime.UtcNow;

        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new MicroServicesException("empty_password", $"Password cannot be empty.");
            }

            Salt = encrypter.GetSalt();
            Password = encrypter.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password,IEncrypter encrypter)
        {
            return Password.Equals(encrypter.GetHash(password, Salt));
        }
    }
}