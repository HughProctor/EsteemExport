using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Win32;

namespace Esteem.ServiceHost
{
    public static class EventLogging
    {
        public const string EVENT_LOG_NAME = "Esteem.ServiceHost";
        public const string EVENT_LOG_SOURCE = "EsteemExportToBAM";
        public static EventLog ServiceHostEventLog = new EventLog(EVENT_LOG_NAME, Environment.MachineName, EVENT_LOG_SOURCE);
        private const string REG_KEY_NAME = @"SYSTEM\CurrentControlSet\Services\EventLog\" + EVENT_LOG_NAME + @"\" + EVENT_LOG_SOURCE;

        public static void CreateEventLog()
        {
            CreateLogger();
            //CheckRegKeyExists();
            //if (!EventLog.SourceExists(EVENTLOG_SOURCE))
            //{
            //    EventLog.CreateEventSource(EVENTLOG_SOURCE, EVENTLOG_LOG);

            //    DateTime dateT = DateTime.Now;
            //    EventLog.WriteEntry(EVENTLOG_SOURCE, EVENTLOG_SOURCE + "Event Log Created - " + dateT.ToString(),
            //        EventLogEntryType.Information);
            //}
        }

        public static void WriteEvent(string eventStr)
        {
            //CreateEventLog();
            ServiceHostEventLog.WriteEntry(eventStr, EventLogEntryType.Information);
        }

        public static void WriteError(string eventStr)
        {
            //CreateEventLog();
            ServiceHostEventLog.WriteEntry(eventStr, EventLogEntryType.Error);
        }

        public static void WriteError(string eventStr, int errorCode)
        {
            //CreateEventLog();
            ServiceHostEventLog.WriteEntry(eventStr, EventLogEntryType.Error, errorCode);
        }

        private static RegistryKey SetRegKeyValue(string regKeyName, string parameters = "")
        {
            RegistryKey rkEventSource = Registry.LocalMachine.OpenSubKey(regKeyName);

            if (rkEventSource == null)
            {
                // Key doesn't exist. Create key which represents source
                var proc = new Process();
                var procStartInfo = new ProcessStartInfo("Reg.exe");
                procStartInfo.Arguments = @"add HKLM\" + regKeyName + parameters;
                procStartInfo.UseShellExecute = true;
                procStartInfo.Verb = "runas";
                proc.StartInfo = procStartInfo;
                proc.Start();

                rkEventSource = Registry.LocalMachine.OpenSubKey(regKeyName);
            }
            return rkEventSource;
        }

        private static void CreateLogger()
        {
            //// set default event source (to be same as event log name) if not passed in
            //if ((EVENT_SOURCE_NAME == null) || (EVENT_SOURCE_NAME.Trim().Length == 0))
            //{
            //    EVENT_SOURCE_NAME = EVENT_LOG_NAME;
            //}

            ServiceHostEventLog.Log = EVENT_LOG_NAME;
            ServiceHostEventLog.Source = EVENT_LOG_SOURCE;

            // Check whether the Event Source exists. It is possible that this may
            // raise a security exception if the current process account doesn't
            // have permissions for all sub-keys under 
            // HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\EventLog

            // Check whether registry key for source exists
            var rkEventSource = Registry.LocalMachine.OpenSubKey(REG_KEY_NAME);

            // Check whether key exists
            if (rkEventSource == null)
            {
                // Key does not exist. Create key which represents source
#if WindowsXP                
                //Registry.LocalMachine.CreateSubKey(REG_KEY_NAME);
#endif
                rkEventSource = SetRegKeyValue(REG_KEY_NAME);
            }

            // Now validate that the .NET Event Message File, EventMessageFile.dll (which correctly
            // formats the content in a Log Message) is set for the event source
            object eventMessageFile = null;
            try
            { eventMessageFile = rkEventSource.GetValue("EventMessageFile"); }
            catch { }

            // If the event Source Message File is not set, then set the Event Source message file.
            if (eventMessageFile == null)
            {
                // Source Event File Doesn't exist - determine .NET framework location,
                // for Event Messages file.
                RegistryKey dotNetFrameworkSettings = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NetFramework\");

                if (dotNetFrameworkSettings != null)
                {
                    object dotNetInstallRoot = null;
                    try
                    { dotNetInstallRoot = dotNetFrameworkSettings.GetValue("InstallRoot", null, RegistryValueOptions.None); }
                    catch { }

                    if (dotNetInstallRoot != null)
                    {
                        string eventMessageFileLocation =
                            dotNetInstallRoot.ToString() + "v" +
                            System.Environment.Version.Major.ToString() + "." +
                            System.Environment.Version.Minor.ToString() + "." +
                            System.Environment.Version.Build.ToString() +
                            @"\EventLogMessages.dll";

                        // Validate File exists
                        if (System.IO.File.Exists(eventMessageFileLocation))
                        {
                            // The Event Message File exists in the anticipated location on the
                            // machine. Set this value for the new Event Source
#if WindowsXP
                            // Re-open the key as writable
                            rkEventSource = Registry.LocalMachine.OpenSubKey(REG_KEY_NAME, true);

                            // Set the "EventMessageFile" property
                            rkEventSource.SetValue("EventMessageFile", eventMessageFileLocation, RegistryValueKind.String);
#endif
                            // Set the "EventMessageFile" property
                            string eventMessageValue = @" /v EventMessageFile /t REG_SZ /d " + eventMessageFileLocation;
                            rkEventSource = SetRegKeyValue(REG_KEY_NAME, eventMessageValue);
                        }
                    }
                }

                dotNetFrameworkSettings.Close();
            }

            rkEventSource.Close();
        }

        private static void LogEvent(string logMessage, EventLogEntryType type, int eventId)
        {
            // Extra Raw event data can be added (later) if needed
            byte[] rawEventData = Encoding.ASCII.GetBytes("");


            // Log the message
            ServiceHostEventLog.WriteEntry(logMessage, type, eventId, 0, rawEventData);
        }

    }
}
