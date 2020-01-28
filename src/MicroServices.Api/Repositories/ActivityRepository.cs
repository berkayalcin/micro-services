using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServices.Services.Activities.Domain.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MicroServices.Api.Repositories
{
    // Data Transfer Objectlerin tutulduğu yerdir.
    public class ActivityRepository : IActivityRepository
    {
        private IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Activity> GetAsync(Guid id)
        {
            return await Collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Activity>> BrowseAsync(Guid userId)
        {
            return await Collection.AsQueryable().Where(x => x.UserId.Equals(userId)).ToListAsync();
        }

        public async Task AddAsync(Activity activity)
        {
            await Collection.InsertOneAsync(activity);
        }
        private IMongoCollection<Activity> Collection => _database.GetCollection<Activity>("Activities");


    }
}