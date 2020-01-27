using System;

namespace MicroServices.Common.Commands
{
    //Marker Command
    public interface IAuthenticatedCommand:ICommand
    {
        Guid UserId { get; set; }
    }
}