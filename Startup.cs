using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnginDemirog.WebApiDemo.CustomMiddlewares;
using EnginDemirog.WebApiDemo.DataAccess;
using EnginDemirog.WebApiDemo.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EnginDemirog.WebApiDemo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMvc(option => option.OutputFormatters.Add(new VcardOutputFormatter()));
            services.AddScoped<IProductDal, EfProductDal>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<AuthenticationMiddleware>();

            app.UseMvc(config =>
            {
                //config.MapRoute("DefaultRoute", "api/{controller}/{action}");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
