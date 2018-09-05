using System;
using System.Collections.Generic;
using System.ServiceProcess;

namespace LayrCake.WCFServiceHost
{
    /// <summary>
    /// A console host.
    /// </summary>
    public class WcfConsoleHost
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The args.</param>
        static void Main(string[] args)
        {
#if DEBUG
            AttachDebugger.Debug();
            args = new List<string> {"-D"}.ToArray();
#endif
            // Puts it into Debug allowed (Development mode)
            if (args.Length > 0 && args[0].Trim().ToUpperInvariant() == "-D")
            {
                // Attaches a VS debugger depending upon the appSettings compilation debug value
                Console.WriteLine("Wcf Service Host");

                // Start all services
                var container = new WcfServiceHostContainer();
                container.StartServices();

                Console.WriteLine("Hosting the following services:");
                string[] names = container.GetHostedServiceNames();

                foreach (string name in names)
                {
                    Console.WriteLine("Service: " + name);
                }

                Console.WriteLine("Press <Enter> to close.");
                Console.ReadLine();

                // Stop all services
                container.StopServices();
                container = null;
            }
            else
            {
                // Run the service
                var wcfServiceHost = new ServiceBase[] 
                { 
                    new WcfServiceHost() 
                };
                ServiceBase.Run(wcfServiceHost);
            }
        }
    }
}
