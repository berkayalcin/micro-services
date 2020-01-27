using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServices.Services.Activities.Domain.Models;

namespace MicroServices.Services.Activities.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetAsync(string name);
        Task<IEnumerable<Category>> BrowseAsync();
        Task AddAsync(Category category);

    }
}