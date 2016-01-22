using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using POCProject.Services;
using Microsoft.Data.Entity;
using POCProject.Model;
using POCProject.Models;
using Microsoft.Extensions.Logging;

namespace POCProject
{
    public class Startup
    {
        public static IConfigurationRoot configuration;
        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath("")
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddLogging();

            services.AddEntityFramework()
                     .AddSqlServer()
                     .AddDbContext<WorldContext>();

            services.AddTransient<WorldContextSeedData>();

#if DEBUG
            services.AddTransient<IMailService, DebugMailService>();
#else
            services.AddScope<IMailService, RealMailService>();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, WorldContextSeedData seeder, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug(LogLevel.Information);
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                        );
            });
            seeder.EnsureSeedData();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
