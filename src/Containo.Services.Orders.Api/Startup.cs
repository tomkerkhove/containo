using Containo.Services.Orders.Api.Extensions;
using Containo.Services.Orders.Storage.Caching;
using Containo.Services.Orders.Storage.Caching.Interfaces;
using Containo.Services.Orders.Storage.Repositories;
using Containo.Services.Orders.Storage.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Containo.Services.Orders.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseOpenApiUi();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICache, RedisCache>();
            services.AddSingleton<ICachedReadOrdersRepository, CachedOrdersRepository>();
            services.AddSingleton<IReadOrdersRepository, OrdersRepository>();

            services.AddMvc();
            services.UseOpenApiSpecifications(apiVersion: 1);
        }
    }
}