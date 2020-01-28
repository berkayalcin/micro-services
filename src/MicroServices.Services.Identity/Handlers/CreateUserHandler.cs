using System;
using System.Threading.Tasks;
using MicroServices.Common.Commands;
using MicroServices.Common.Events;
using MicroServices.Common.Exceptions;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace MicroServices.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _busClient;
        //private readonly IUserService _userService;

        public CreateUserHandler(IBusClient busClient,
            ILogger<CreateUser> logger)
        {
            _busClient = busClient;
        }

        public async Task HandleAsync(CreateUser command)
        {
            Console.WriteLine($"Creating user: '{command.Email}' with name: '{command.UserName}'.");
            try
            {
                //await _userService.RegisterAsync(command.Email, command.Password, command.UserName);
                await _busClient.PublishAsync(new UserCreated(command.Email, command.UserName));
                Console.WriteLine($"User: '{command.Email}' was created with name: '{command.UserName}'.");

                return;
            }
            catch (MicroServicesException ex)
            {
                Console.WriteLine(ex+" " + ex.Message);
                await _busClient.PublishAsync(new CreateUserRejected(command.Email,
                    ex.Message, ex.Code));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex+" "+ ex.Message);
                await _busClient.PublishAsync(new CreateUserRejected(command.Email,
                    ex.Message, "error"));
            }
        }
    }
}