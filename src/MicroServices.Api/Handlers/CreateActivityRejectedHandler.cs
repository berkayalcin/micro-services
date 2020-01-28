using System;
using System.Threading.Tasks;
using MicroServices.Common.Events;

namespace MicroServices.Api.Handlers
{
    public class CreateActivityRejectedHandler: IEventHandler<CreateActivityRejected>
    {
        public async Task HandleAsync(CreateActivityRejected @event)
        {
            Console.WriteLine($"Activity create failed: {@event.Name} {@event.Reason} @{@event.Code}");
        }
    }
}