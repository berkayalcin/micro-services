using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Common.Commands;
using MicroServices.Services.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroServices.Services.Identity.Controllers
{
    [Route("")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser command)
        {
            return Ok(await _userService.LoginAsync(command.Email, command.Password));
        }


    }
}