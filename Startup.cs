using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corso10157.Models.Interfaces;
using Corso10157.Models.Services.ADO.NET.Application;
using Corso10157.Models.Services.ADO.NET.Infrastructure;
using Corso10157.Models.Services.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Corso10157
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            // services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<ICourseService, AdoNetCourseService>();
            services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseMvc(routeBuilder => {
                routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
