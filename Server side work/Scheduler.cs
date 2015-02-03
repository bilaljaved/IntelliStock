using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Job;
using ConsoleApplication2;
using IntelliStock_WebService;

namespace Scheduler
{
    class SchedulerClass
    {
        public SchedulerClass()
        {
            //Create the scheduler factory
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            //Ask the scheduler factory for a scheduler
            IScheduler scheduler = schedulerFactory.GetScheduler();

            //Start the scheduler so that it can start executing jobs
            scheduler.Start();

            // Create a job of Type WriteToConsoleJob
            IJobDetail job = JobBuilder.Create(typeof(Web_Scraper)).Build();
            //ITrigger trigger = TriggerBuilder.Create().WithCronSchedule("0/2 * * * * *").StartNow().WithIdentity("MyJobTrigger", "MyJobTriggerGroup").Build();

            ITrigger trigger = TriggerBuilder.Create().WithDailyTimeIntervalSchedule(s => s.WithIntervalInMinutes(1).OnMondayThroughFriday().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(9, 0))).Build();

            //ITrigger trigger = TriggerBuilder.Create().WithDailyTimeIntervalSchedule(x=>x.WithIntervalInMinutes(15).OnMondayThroughFriday().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(9,0)).Build();

            //Schedule this job to execute every second, a maximum of 10 times
            //ITrigger trigger = TriggerBuilder.Create().WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForTotalCount(10)).StartNow().WithIdentity("MyJobTrigger", "MyJobTriggerGroup").Build();
            scheduler.ScheduleJob(job, trigger);

            //Wait for a key press. If we don't wait the program exits and the scheduler gets destroyed
            //   Console.ReadKey();

            //A nice way to stop the scheduler, waiting for jobs that are running to finish
            scheduler.Shutdown(true);

        }
       
    }
}