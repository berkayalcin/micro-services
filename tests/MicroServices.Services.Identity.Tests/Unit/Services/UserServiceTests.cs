using System;
using System.Threading.Tasks;
using FluentAssertions;
using MicroServices.Common.Auth;
using MicroServices.Services.Identity.Domain.Models;
using MicroServices.Services.Identity.Domain.Repositories;
using MicroServices.Services.Identity.Domain.Services;
using MicroServices.Services.Identity.Services;
using Moq;
using Xunit;

namespace MicroServices.Services.Identity.Tests.Unit.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task user_service_login_should_return_jwt()
        {
            var email = "test@test.com";
            var password = "secret";
            var name = "test";
            var salt = "salt";
            var hash = "hash";
            var token = "token";
            var userRepositoryMock = new Mock<IUserRepository>();
            var encyrpterMock = new Mock<IEncrypter>();
            var jwtHandlerMock = new Mock<IJwtHandler>();

            encyrpterMock.Setup(x => x.GetSalt()).Returns(salt);
            encyrpterMock.Setup(x => x.GetHash(password, salt))
                .Returns(hash);
            jwtHandlerMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(
                new JsonWebToken()
                {
                    Token = token
                });
            var user=new User(email,name);
            user.SetPassword(password,encyrpterMock.Object);
            userRepositoryMock.Setup(x => x.GetAsync(email))
                .ReturnsAsync(user);

            var userService=new UserService(userRepositoryMock.Object,encyrpterMock.Object,jwtHandlerMock.Object);
            var jwt = await userService.LoginAsync(email, password);
            userRepositoryMock.Verify(x=>x.GetAsync(email),Times.Once);
            jwtHandlerMock.Verify(x=>x.Create(It.IsAny<Guid>()),Times.Once);
            jwt.Should().NotBeNull();
            jwt.Token.Should().BeEquivalentTo(token);
        }

    }
}