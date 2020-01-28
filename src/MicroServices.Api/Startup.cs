using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Api.Handlers;
using MicroServices.Api.Repositories;
using MicroServices.Common.Auth;
using MicroServices.Common.Events;
using MicroServices.Common.Mongo;
using MicroServices.Common.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MicroServices.Api
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
            services.AddJwt(Configuration);
            services.AddMongoDb(Configuration);

            services.AddRabbitMq(Configuration);
            services.AddSingleton<IEventHandler<ActivityCreated>,ActivityCreatedHandler>();
            services.AddSingleton<IEventHandler<CreateActivityRejected>, CreateActivityRejectedHandler>();
            services.AddSingleton<IActivityRepository,ActivityRepository>();
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
