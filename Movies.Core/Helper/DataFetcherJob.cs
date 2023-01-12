using Microsoft.Extensions.Configuration;
using Movies.Core.DTOs;
using Newtonsoft.Json;
using NLayerApp.Core.Services;
using Quartz;
using Quartz.Impl;

namespace Movies.Core.Helper
{
    public class DataFetcherJob : IJob
    {
        private readonly IMoviesService _moviesService;

        public DataFetcherJob(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            string apiKey = "2ffe153e25788cfac01580dae1018af4";
            string baseUrl = "https://api.themoviedb.org/3/";
            string apiUrl = baseUrl + "discover/movie?api_key=" + apiKey;
            var response = WebHelper.Get(apiUrl);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response);

           var result = _moviesService.InsertMovies(myDeserializedClass);

            //LOG
        }
    }

    public class Scheduler
    {
        public static async Task Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<DataFetcherJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                  )
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
