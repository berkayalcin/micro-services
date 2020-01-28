using System;
using System.Threading.Tasks;
using MicroServices.Api.Repositories;
using MicroServices.Common.Events;
using MicroServices.Services.Activities.Domain.Models;

namespace MicroServices.Api.Handlers
{
    public class ActivityCreatedHandler:IEventHandler<ActivityCreated>
    {
        private IActivityRepository _activityRepository;

        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {
            await _activityRepository.AddAsync(new Activity(@event.Id,@event.Category,@event.UserId,@event.Name,@event.Description,@event.CreatedAt));
            Console.WriteLine($"Activity created: {@event.Name}");

        }
    }
}