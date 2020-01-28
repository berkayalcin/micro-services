using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServices.Services.Activities.Domain.Models;

namespace MicroServices.Api.Repositories
{
    public interface IActivityRepository
    {
        Task AddAsync(Activity model);
        Task<Activity> GetAsync(Guid id);
        Task<IEnumerable<Activity>> BrowseAsync(Guid userId);
    }
}