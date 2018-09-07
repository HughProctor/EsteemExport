using BusinessModel.Mappers;
using SchedulerManager.Mechanism;
using System;
using System.Diagnostics;

namespace Esteem.ServiceHost
{
    /// <summary>
    /// A console host.
    /// </summary>
    public class ServiceHost
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The args.</param>
        static void Main(string[] args)
        {
            //if (!Debugger.IsAttached)
            //    Debugger.Launch();
            //Debugger.Break();

            EventLogging.CreateEventLog();
            EventLogging.WriteEvent("Esteem Data Export Service Starting");

            Console.WriteLine("Esteem Data Export Service Starting");
            try
            {
                Map.Init();
                JobManager jobManager = new JobManager();
                EventLogging.WriteEvent("Esteem Data Export Service Executing");

                jobManager.ExecuteAllJobs();
            }
            catch (Exception exception)
            {
                EventLogging.WriteError(exception.Message);
            }
        }
    }
}
