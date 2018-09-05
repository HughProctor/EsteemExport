using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Reflection;
using System.Configuration;
using System.ServiceModel.Description;
using System.Diagnostics;
using ServiceModelEx;

namespace LayrCake.WCFServiceHost
{
    /// <summary>
    /// A class that starts all hosts configured in the application configuration file.
    /// </summary>
    internal class WcfServiceHostContainer
    {
        #region Fields

        private List<ServiceHost> _hosts = new List<ServiceHost>();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Starts all services in the application configuration file.
        /// </summary>
        internal void StartServices()
        {
            // Get the list of services to run
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var configFilePath = Path.Combine(path, "LayrCake.WCFServiceHost.exe");
            var config = ConfigurationManager.OpenExeConfiguration(configFilePath);

            var servicesSection = config.GetSection("serviceSettings") as ServiceConfigurationSection;

            if (servicesSection == null)
            {
                EventLogging.WriteError("Failed to load 'serviceSettings' configuration section.");
                throw new ConfigurationErrorsException("Failed to load 'serviceSettings' configuration section.");
            }

            foreach (ServiceConfigurationElement serviceElement in servicesSection.Services)
            {
                Type serviceType = null;
                // T
                if (!String.IsNullOrEmpty(serviceElement.ServicePath))
                {
                    EventLogging.WriteEvent("Trying to load assembly : " + serviceElement.Name + " from " + serviceElement.ServicePath);

                    // Try to load the assembly from the remote file path
                    var serviceAssembly = AssemblyLoader.LoadAssembly(serviceElement.ServicePath, serviceElement.Name);

                    if (serviceAssembly == null)
                    {
                        EventLogging.WriteEvent("Failed to load assembly : " + serviceElement.Name + " from " + serviceElement.ServicePath);
                        continue;
                    }
                    else
                    {
                        foreach (var modules in serviceAssembly.GetLoadedModules())
                            EventLogging.WriteEvent("Loaded modules " + modules.Name);

                        serviceType = serviceAssembly.GetType(serviceElement.ServiceType.Split(',')[0], false, true);
                        EventLogging.WriteEvent("Trying to load assembly : " + serviceElement.Name + " from " + serviceElement.ServicePath);
                    }
                }
                else
                {
                    EventLogging.WriteEvent("Trying to load referenced assembly : " + serviceElement.Name);

                    // Get the service host type
                    serviceType = Type.GetType(serviceElement.ServiceType, false, true);
                }

                if (serviceType == null)
                {
                    EventLogging.WriteError("Failed to get service type '" + serviceElement.ServiceType + "'.");
                    throw new TypeLoadException("Failed to get service type '" + serviceElement.ServiceType + "'.");
                }

                // Open the host
                EventLogging.WriteEvent("Opening Host");
                OpenHost(serviceType);
            }
        }

        // Stops all configured services.
        internal void StopServices()
        {
            foreach (ServiceHost host in _hosts)
            {
                CloseHost(host);
            }
        }

        /// <summary>
        /// Gets the hosted service names/
        /// </summary>
        /// <returns>string[] of the service names</returns>
        public string[] GetHostedServiceNames()
        {
            List<string> names = new List<string>();
            foreach (ServiceHost host in _hosts)
            {
                StringBuilder svcInfo = new StringBuilder();
                svcInfo.AppendFormat(host.Description.ConfigurationName);

                foreach (ServiceEndpoint endPoint in host.Description.Endpoints)
                {
                    svcInfo.AppendLine(endPoint.ListenUri.ToString());
                }

                names.Add(svcInfo.ToString());
            }
            return names.ToArray();
        }

        #endregion Methods

        #region HelperMethods

        /// <summary>
        /// Opens the host using the specified type
        /// </summary>
        /// <param name="hostType">The host type.</param>
        private void OpenHost(Type hostType)
        {
            EventLogging.WriteEvent("Trying to load service host : " + hostType.Name);

            // Create the host
            ServiceHost host = null;
            try
            {
                host = new ServiceHost(hostType);
            }
            catch (Exception e)
            {
                EventLogging.WriteError("Failed to load service host : " + hostType.Name);
                EventLogging.WriteError("Error Message : " + e.Message);
                throw new ServiceActivationException("Failed to load service host : " + hostType.Name, e);
            }

            // Open the host
            OpenHost(host);
        }

        /// <summary>
        /// Opens the specified host
        /// </summary>
        /// <param name="host">The host.</param>
        private void OpenHost(ServiceHost host)
        {
            try
            {
                // Open the host
                //host.AddGenericResolver();
                host.Open();

                // Add the host to the list of the opened hosts
                _hosts.Add(host);
            }
            catch (Exception e)
            {
                EventLogging.WriteError("Failed to open service host : " + host.Description.Name);
                EventLogging.WriteError("Error Message : " + e.Message);
                throw new ServiceActivationException("Failed to load service host : " + host.Description.Name, e);
            }
        }

        /// <summary>
        /// Closes the specified host.
        /// </summary>
        /// <param name="host">The host.</param>
        private void CloseHost(ServiceHost host)
        {
            try
            {
                if (host != null)
                {
                    EventLogging.WriteEvent("Trying to close service host : " + host.Description.Name);
                    host.Close();
                }
            }
            catch (Exception e)
            {
                EventLogging.WriteError("Failed to close service host : " + host.Description.Name);
                EventLogging.WriteError("Error Message : " + e.Message);
            }
        }

        #endregion Helper Methods
    }
}
