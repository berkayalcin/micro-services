using System;
using System.Threading.Tasks;
using MicroServices.Common.Commands;
using MicroServices.Common.Events;
using MicroServices.Common.Exceptions;
using MicroServices.Services.Activities.Services;
using RawRabbit;

namespace MicroServices.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private IBusClient _busClient;
        private IActivityService _activityService;

        public CreateActivityHandler(IBusClient busClient, IActivityService activityService)
        {
            _busClient = busClient;
            _activityService = activityService;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            Console.WriteLine($"Creating Activity : {command.Name}");
            try
            {
                await _activityService.AddAsync(command.Id, command.UserId, command.Category, command.Name,
                    command.Description, command.CreatedAt);
                ActivityCreated created = new ActivityCreated(command.Id, command.UserId, command.Category, command.Name, command.Description, command.CreatedAt);
                await _busClient.PublishAsync(created);
            }
            catch (MicroServicesException e)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id.ToString(), e.Message, e.Code));
            }
            catch (Exception e)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id.ToString(), e.Message, "error"));
            }

        }
    }
}