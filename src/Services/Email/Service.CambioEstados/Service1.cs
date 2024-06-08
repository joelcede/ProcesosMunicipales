using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using static Quartz.Logging.OperationName;

namespace Service.CambioEstados
{
    public partial class Service1 : ServiceBase
    {
        private IScheduler scheduler;

        public Service1()
        {
            InitializeComponent();
            OnStart(null);
        }

        protected override async void OnStart(string[] args)
        {
            scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            int jobIntervalHours = int.Parse(ConfigurationManager.AppSettings["JobIntervalHours"]);

            IJobDetail job = JobBuilder.Create<CambioEstadosJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(jobIntervalHours)
                    //.WithIntervalInHours(jobIntervalHours)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        protected override async void OnStop()
        {
            if (scheduler != null)
            {
                await scheduler.Shutdown();
            }
        }
    }
}
