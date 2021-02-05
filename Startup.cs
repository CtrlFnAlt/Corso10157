using Corso10157.Models.Options;
using Corso10157.Models.Services.ADO.NET.Application;
using Corso10157.Models.Services.ADO.NET.Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Corso10157
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            /*PLACEHOLDER*/
            // services.AddTransient<ICourseService, CourseService>();
            /*PLACEHOLDER*/
            /*ADONET*/
            services.AddTransient<ICourseServiceAsync, AdoNetCourseService>();
            services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();
            /*ADONET*/
            /*STRINGA DI CONNESSIONE AL DB*/
            // string connectionStrin = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
            // string connectionStrin = Configuration.GetConnectionString("Default");
            services.Configure<ConnectionStringsOptions>(Configuration.GetSection("ConnectionStrings"));
            /*STRINGA DI CONNESSIONE AL DB*/
            /*PAGINAZIONE ED ORDINE*/
            services.Configure<CoursesOptions>(Configuration.GetSection("Courses"));
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
            app.UseRouting();
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
