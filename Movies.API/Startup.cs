using Quartz;

namespace Movies.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                // base Quartz scheduler, job and trigger configuration
            });

            // ASP.NET Core hosting
            services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });
        }
    }
}
