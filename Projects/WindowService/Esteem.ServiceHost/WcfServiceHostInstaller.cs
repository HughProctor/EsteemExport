using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Globalization;
using System.Diagnostics;
using Microsoft.Win32;

namespace LayrCake.WCFServiceHost
{
    /// <summary>
    /// Installs the service host.
    /// The installer is designed to allow multiple version of the service host with different service names.
    /// </summary>
    [RunInstaller(true)]
    public partial class WcfServiceHostInstaller : Installer
    {
        #region Fields

        private readonly ServiceProcessInstaller _processInstaller = new ServiceProcessInstaller();
        private readonly ServiceInstaller _serviceInstaller = new ServiceInstaller();
        private readonly EventLogInstaller _logInstaller = new EventLogInstaller();
        public const string EVENT_LOG_NAME = "WCF_EVENTLOG_NAME";
        public const string NetworkPassword = "WCF_NETWORK_PASSWORD";

        #endregion Fields

        #region Constants

        private const string nameKey = "name";
        private const string displayNameKey = "displayname";
        private const string descriptionKey = "description";
        private const string usageKey = "usage";

        #endregion Constants

        #region Constructors

        public WcfServiceHostInstaller()
        {
            InitializeComponent();
            //Debugger.Launch();
            EventLogging.WriteEvent("WcfServiceHostInstaller Start");
            PerformInstall();
        }

        #endregion Constructors

        #region Overrides

        /// <summary>
        /// Raises the <see cref="E:System.Configuration.Install.Installer.BeforeInstall"/> event.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary"/> that contains the state of the computer before the installers in the <see cref="P:System.Configuration.Install.Installer.Installers"/> property are installed. This <see cref="T:System.Collections.IDictionary"/> object should be empty at this point.</param>
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            // Set installer parameters
            SetParameters();

            // Set the default service account
            _processInstaller.Account = ServiceAccount.NetworkService;
            _processInstaller.Password = NetworkPassword == "WCF_NETWORK_PASSWORD" ? null : NetworkPassword;

            _logInstaller.Log = EventLogging.EVENT_LOG_NAME;

            base.OnBeforeInstall(savedState);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Configuration.Install.Installer.BeforeUninstall"/> event.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary"/> that contains the state of the computer before the installers in the <see cref="P:System.Configuration.Install.Installer.Installers"/> property uninstall their installations.</param>
        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            // Set installer parameters
            SetParameters();

            base.OnBeforeUninstall(savedState);
        }

        #endregion Overrides

        #region Helper Methods

        /// <summary>
        /// Sets the installer parameters from the command line.
        /// </summary>
        private void SetParameters()
        {
            try
            {
                // Check parameters were passed correctly
                if (Context.Parameters.ContainsKey(usageKey))
                {
                    throw new ArgumentException("Install aborted: displaying usage.");
                }
                if (!Context.Parameters.ContainsKey(nameKey))
                {
                    throw new ArgumentException("Name parameter was not supplied");
                }
            }
            catch (ArgumentException ex)
            {
                ShowUsage();
                throw ex;
            }

            // Set service name
            _serviceInstaller.ServiceName = this.Context.Parameters[nameKey];
            _logInstaller.Source = this.Context.Parameters[nameKey];

            // Set the display name (if provided)
            if (Context.Parameters.ContainsKey(displayNameKey))
            {
                _serviceInstaller.DisplayName = this.Context.Parameters[displayNameKey];
            }

            // Set the description (if provided)
            if (Context.Parameters.ContainsKey(displayNameKey))
            {
                _serviceInstaller.Description = this.Context.Parameters[descriptionKey];
            }

            // Log
            Context.LogMessage(string.Format(CultureInfo.InvariantCulture, "Name: {0}", _serviceInstaller.ServiceName));
            Context.LogMessage(string.Format(CultureInfo.InvariantCulture, "Display Name: {0}", _serviceInstaller.DisplayName));
            Context.LogMessage(string.Format(CultureInfo.InvariantCulture, "Description: {0}", _serviceInstaller.Description));
        }

        /// <summary>
        /// Shows the usage.
        /// </summary>
        private void ShowUsage()
        {
            Context.LogMessage("");
            Context.LogMessage("");
            Context.LogMessage("USAGE: InstallUtil /u /usage /name=<ServiceName> /displayname=<\"Display Name\"> /description=<\"Description of Service\" <Service Exe>");
            Context.LogMessage("");
            Context.LogMessage("WHERE:  /u              means to uninstall the service contained in the EXE file");
            Context.LogMessage("                        and described by the following parameters.");
            Context.LogMessage("        /usage          will output this help screen. If used it requires no other");
            Context.LogMessage("                        parameters besides the service.exe file");
            Context.LogMessage("        <Service Name>  is less than 33 characters, contains only alpha-numeric");
            Context.LogMessage("                        characters and has no space characters.");
            Context.LogMessage("       <Display Name>   is less then 51 characters and contains only alpha-numeric");
            Context.LogMessage("                        characters. You must quote any space characters.");
            Context.LogMessage("       <Description>    is less then 256 characters, contains only alpha-numeric");
            Context.LogMessage("                        characters. You must quote any space characters.");
            Context.LogMessage("       <Service Exe>    is the full path and file name of the new service you");
            Context.LogMessage("                        want to install.");
            Context.LogMessage("");
            Context.LogMessage("NOTE: The -u option requires only the service name,");
            Context.LogMessage("      but it must be the exact case sensitive name of the service, ");
            Context.LogMessage("      as it appears in the properties page of the Control panel Services applet.");
            Context.LogMessage("      It is NOT the display name, it must be the service name.");
            Context.LogMessage("");
            Context.LogMessage("EXAMPLE:");
            Context.LogMessage("InstallUtil /name=TestService /displayname=\"Test Display Name\" /description=\"Test Service Description\" c:\\WcfSample\\WcfSample.Service.Host.exe");
            Context.LogMessage("");
            Context.LogMessage("");
        }

        /// <summary>
        /// Performs the install.
        /// </summary>
        private void PerformInstall()
        {
            // Add installers
            Installers.Add(_processInstaller);
            Installers.Add(_serviceInstaller);
            //Installers.Add(_logInstaller);
        }

        #endregion Helper Methods
    }
}
