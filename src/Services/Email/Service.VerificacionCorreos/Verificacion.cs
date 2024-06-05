using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Service.VerificacionCorreos
{
    public partial class Verificacion : ServiceBase
    {
        private IScheduler scheduler;
        public Verificacion()
        {
            InitializeComponent();
        }

        protected override async void OnStart(string[] args)
        {
            scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            //IJobDetail job = JobBuilder.Create<HelloJob>()
            //    .WithIdentity("myJob", "group1")
            //    .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(6)
                    .RepeatForever())
                .Build();

            //await scheduler.ScheduleJob(job, trigger);
        }

        protected override async void OnStop()
        {
            await scheduler.Shutdown();
        }
    }
}
