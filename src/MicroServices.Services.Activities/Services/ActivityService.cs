using System;
using System.Threading.Tasks;
using MicroServices.Common.Exceptions;
using MicroServices.Services.Activities.Domain.Models;
using MicroServices.Services.Activities.Domain.Repositories;

namespace MicroServices.Services.Activities.Services
{
    public class ActivityService:IActivityService
    {
        private IActivityRepository _activityRepository;
        private ICategoryRepository _categoryRepository;

        public ActivityService(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            var activityCategory = await _categoryRepository.GetAsync(name);
            if (activityCategory == null)
            {
                throw new MicroServicesException("category_not_found", $"Category: {category} was not found.");
            }
            var activity=new Activity(id,activityCategory,userId,name,description,createdAt);
            await _activityRepository.AddAsync(activity);
        }
    }
}