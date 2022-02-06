using AnalyzeApp.API.Impl;
using AnalyzeApp.API.Interface;
using AnalyzeApp.API.Job;
using Quartz;

namespace AnalyzeApp.API
{
    public class Startup
    {
        public Startup(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAnalyzeRepo, AnalyzeRepo>(sp => {
                return new AnalyzeRepo(Configuration.GetConnectionString("SqliteConnection"));
            });
            services.AddScoped<IAnalyzeService, AnalyzeService>();
            // Add the required Quartz.NET services
            services.AddQuartz(q =>
            {
                // Use a Scoped container to create jobs. I'll touch on this later
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                q.UseDefaultThreadPool(tp => { tp.MaxConcurrency = 5; });

                // Register the job, loading the schedule from configuration
                q.AddJobAndTrigger<ScheduleJobTelegram>(Configuration);
            });

            // Add the Quartz.NET hosted service

            services.AddQuartzHostedService(
                q => q.WaitForJobsToComplete = true);
            // other config
        }

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            // ...
        }
    }
}
