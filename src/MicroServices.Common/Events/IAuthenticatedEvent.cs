using System;

namespace MicroServices.Common.Events
{

    // Marker Authenticated Event Interface
    public interface IAuthenticatedEvent : IEvent
    {
        Guid UserId { get; set; }
    }
}