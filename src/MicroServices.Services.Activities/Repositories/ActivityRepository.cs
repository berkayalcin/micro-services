using System;
using System.Threading.Tasks;
using MicroServices.Services.Activities.Domain.Models;
using MicroServices.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MicroServices.Services.Activities.Repositories
{
    // TODO Flatten Object Patterns
    public class ActivityRepository:IActivityRepository
    {
        private IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Activity> GetAsync(Guid id)
        {
          return  await Collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Activity activity)
        {
            await Collection.InsertOneAsync(activity);
        }
        private IMongoCollection<Activity> Collection => _database.GetCollection<Activity>("Activities");

    }
}