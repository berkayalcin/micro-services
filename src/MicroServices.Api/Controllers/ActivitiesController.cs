using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Api.Repositories;
using MicroServices.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace MicroServices.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActivitiesController : ControllerBase
    {
        private readonly IBusClient _busClient;
        private IActivityRepository _activityRepository;
        // Real life uygulamsında burada repository değil service inject edilir.

        public ActivitiesController(IBusClient busClient, IActivityRepository activityRepository)
        {
            _busClient = busClient;
            _activityRepository = activityRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var activities = await _activityRepository.BrowseAsync(Guid.Parse(User.Identity.Name));
            return Ok(activities.Select(x => new
            {
                x.Name,
                x.Category,
                x.CreatedAt
            }));

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var activity = await _activityRepository.GetAsync(id);
            if (activity == null)
                return NotFound();
            if (activity.UserId != Guid.Parse(User.Identity.Name))
                return Unauthorized();
            return Ok(activity);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;
            command.UserId = Guid.Parse(User.Identity.Name);
            await _busClient.PublishAsync(command);
            return Accepted($"activities/{command.Id}");
        }


    }
}