using System;
using System.Threading.Tasks;
using MicroServices.Common.Commands;
using MicroServices.Common.Events;
using RawRabbit;

namespace MicroServices.Services.Activities.Handlers
{
    public class CreateActivityHandler:ICommandHandler<CreateActivity>
    {
        private IBusClient _busClient;

        public CreateActivityHandler(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task HandleAsync(CreateActivity command)
        {
           Console.WriteLine($"Creating Activity : {command.Name}");
           ActivityCreated created=new ActivityCreated(command.Id,command.UserId,command.Category,command.Name,command.Description,command.CreatedAt);
           await _busClient.PublishAsync(created);
        }
    }
}