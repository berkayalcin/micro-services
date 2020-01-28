using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Common.Commands;
using MicroServices.Common.Mongo;
using MicroServices.Common.RabbitMq;
using MicroServices.Services.Identity.Domain.Repositories;
using MicroServices.Services.Identity.Domain.Services;
using MicroServices.Services.Identity.Handlers;
using MicroServices.Services.Identity.Repositories;
using MicroServices.Services.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MicroServices.Services.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRabbitMq(Configuration);
            services.AddMongoDb(Configuration);
            services.AddSingleton<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserRepository, UserRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
