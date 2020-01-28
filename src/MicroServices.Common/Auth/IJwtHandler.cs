using System;
using System.Threading.Tasks;

namespace MicroServices.Common.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId);
    }
}