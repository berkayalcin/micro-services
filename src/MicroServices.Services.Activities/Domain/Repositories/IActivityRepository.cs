using System;
using System.Threading.Tasks;
using MicroServices.Services.Activities.Domain.Models;

namespace MicroServices.Services.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetAsync(Guid id);
        Task AddAsync(Activity activity);
    }
}