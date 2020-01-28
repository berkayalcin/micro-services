using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServices.Services.Activities.Domain.Models;
using MicroServices.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MicroServices.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IMongoDatabase _database;

        public CategoryRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Category> GetAsync(string name)
        {
            Console.WriteLine(name);
            return await Collection.AsQueryable().FirstOrDefaultAsync(x => x.Name.Equals(name.ToLowerInvariant()));
        }

        public async Task<IEnumerable<Category>> BrowseAsync()
        {
            return await Collection.AsQueryable().ToListAsync();
        }

        public async Task AddAsync(Category category)
        {
            await Collection.InsertOneAsync(category);
        }

        private IMongoCollection<Category> Collection => _database.GetCollection<Category>("Categories");
    }
}