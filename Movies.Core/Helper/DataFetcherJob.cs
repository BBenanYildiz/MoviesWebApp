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
            string apiUrl = "discover/movie?api_key=";
            string requestUrl = WebHelper.CreateUrlApiKey(apiUrl);
            var response = WebHelper.Get(requestUrl);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response);

           var result = await _moviesService.InsertMovies(myDeserializedClass);

            if (result == 0)
            {
                MailHelper.SystemInformationMail("Hata", "DataFetcherJob Başarısız.");
            }
            else
            {
                MailHelper.SystemInformationMail("Başarılı", "DataFetcherJob Başarılı");
            }
           
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
