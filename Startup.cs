using Corso10157.Models.Options;
using Corso10157.Models.Services.ADO.NET.Application;
using Corso10157.Models.Services.ADO.NET.Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Corso10157
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /*PER MIDDLEWARE DI CACHING*/
            services.AddResponseCaching();
            /*PER MIDDLEWARE DI CACHING*/
            /*SERVIZI MVC*/
            services.AddMvc(option => {
                option.EnableEndpointRouting = false;
                var homeProfile = new CacheProfile();
                configuration.Bind("ResponseCache:Home", homeProfile);
                option.CacheProfiles.Add("Home", homeProfile);
                });
            /*SERVIZI MVC*/
            /*PLACEHOLDER*/
            // services.AddTransient<ICourseService, CourseService>();
            /*PLACEHOLDER*/
            /*ADONET*/
            services.AddTransient<ICourseServiceAsync, AdoNetCourseService>();
            services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();
            /*ADONET*/
            /*CACHE*/
            services.AddTransient<ICachedCourseService, MemoryCacheCourseService>();
            services.Configure<MemoryCacheOptions>(configuration.GetSection("MemoryCache"));
            /*CACHE*/
            /*STRINGA DI CONNESSIONE AL DB*/
            // string connectionStrin = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
            // string connectionStrin = Configuration.GetConnectionString("Default");
            services.Configure<ConnectionStringsOptions>(configuration.GetSection("ConnectionStrings"));
            /*STRINGA DI CONNESSIONE AL DB*/
            /*PAGINAZIONE ED ORDINE*/
            services.Configure<CoursesOptions>(configuration.GetSection("Courses"));
            /*PAGINAZIONE ED ORDINE*/
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseResponseCaching();
            app.UseRouting();
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
