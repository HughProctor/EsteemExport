using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;

namespace LayrCake.WCFServiceHost
{
    public static class AttachDebugger
    {
        const string debugConfigurationSettingsKey = "appSettings";

        public static void Attach()
        {
            if (!Debugger.IsAttached)
                Debugger.Launch();
            else
                Debugger.Break();
        }

        public static void Debug()
        {
            //AppSettingsSection appSection = ConfigurationManager.AppSettings..GetSection(debugConfigurationSettingsKey) as AppSettingsSection;

            if (ConfigurationManager.AppSettings["Debug"] != null && ConfigurationManager.AppSettings["Debug"] == "true")
                Attach();
        }
    }
}
