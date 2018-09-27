using BusinessModel.Mappers;
using Esteem.ServiceHost;
using SchedulerManager.Mechanism;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Esteem.ServiceHost
{
    public partial class Service1 : ServiceBase
    {
        JobManager jobManager = new JobManager();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Map.Init();
                EventLogging.WriteEvent("Esteem Data Export Service Executing");

                jobManager.ExecuteAllJobs();

                EventLogging.WriteEvent("Esteem Data Export Service Processing");
            }
            catch (Exception exception)
            {
                EventLogging.WriteError(exception.Message);
            }
        }

        protected override void OnStop()
        {
            jobManager = null;
        }
    }
}
