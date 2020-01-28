﻿using FluentAssertions;
using MicroServices.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MicroServices.Api.Tests.Unit.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void home_controller_get_should_return_string_content()
        {
            var controller = new HomeController();
            var result = controller.Get();
            var contentResult = result as ContentResult;
            contentResult.Should().NotBeNull();
            contentResult.Content.Should().BeEquivalentTo("Hello from Action Api");
        }
    }
}