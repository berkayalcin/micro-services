using System;
using System.Threading.Tasks;
using MicroServices.Api.Repositories;
using MicroServices.Services.Activities.Domain.Models;
using MicroServices.Services.Activities.Domain.Repositories;
using MicroServices.Services.Activities.Services;
using Moq;
using Xunit;
using IActivityRepository = MicroServices.Services.Activities.Domain.Repositories.IActivityRepository;

namespace MicroServices.Services.Activities.Tests.Unit.Services
{
    public class ActivityServiceTests
    {
        [Fact]
        public async Task activity_service_add_async_should_succeed()
        {
            var category = "work";
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(x => x.GetAsync(category))
                .ReturnsAsync(new Category(category));
            var activityService=new ActivityService(activityRepositoryMock.Object,categoryRepositoryMock.Object);

            var id = Guid.NewGuid();
            await activityService.AddAsync(id, Guid.NewGuid(), category, "name", "description", DateTime.UtcNow);

            categoryRepositoryMock.Verify(x=>x.GetAsync(category),Times.Once);
            activityRepositoryMock.Verify(x=>x.AddAsync(It.IsAny<Activity>()),Times.Once);

        }
    }
}