using System;
using System.Diagnostics;
using System.ServiceProcess;

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
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }

    }
}
