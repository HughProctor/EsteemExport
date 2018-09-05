using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace LayrCake.WCFServiceHost
{
    public partial class WcfServiceHost : ServiceBase
    {
        #region Fields

        private WcfServiceHostContainer _container = new WcfServiceHostContainer();

        #endregion
        /// <summary>
        /// Initialises a new instance of the <see cref="WcfServiceHost"/> class
        /// </summary>
        public WcfServiceHost()
        {
            InitializeComponent();
            // Set default service name
            this.ServiceName = "_LayrCakeService";
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service
        /// by the Service Control Manager (SCM) or when the operating system starts 
        /// (for a service that starts automatically)
        /// </summary>
        /// <param name="args">Data passed by the start command</param>
        protected override void OnStart(string[] args)
        {
            // Start all services listed in the application configuration file.
            _container.StartServices();
        }

        protected override void OnStop()
        {
            // Stop all services listed in the application configuration file.
            _container.StopServices();
        }
    }
}
