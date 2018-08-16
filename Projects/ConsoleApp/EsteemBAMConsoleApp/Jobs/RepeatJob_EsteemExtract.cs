using AutoMapper;
using EntityModel;
using EntityModel.Mappers;
using EntityModel.Service;
using Infrastructure.FileExport;
using SchedulerManager.Mechanism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteemBAMConsoleApp.Jobs
{
    public class RepeatJob_EsteemExtract : Job
    {
        SCAuditService _sCAuditService;
        private string _startDateTimeString = "";
        private string _endDateTimeString = "";
        private string _typePrefix = "SCAudit_";
        private DateTime _startDateTime;
        private DateTime _currentTime;

        public RepeatJob_EsteemExtract()
        {
            _sCAuditService = new SCAuditService();

            _startDateTimeString = string.IsNullOrEmpty(_startDateTimeString) ? "01/01/2017" : _startDateTimeString;
            _endDateTimeString = string.IsNullOrEmpty(_endDateTimeString) ? "30/11/2017" : _endDateTimeString;

            DateTime.TryParse(_startDateTimeString, out _startDateTime);
            DateTime.TryParse(_endDateTimeString, out var endDateTime);
            _currentTime = _startDateTime;

        }
        private void ServiceSetup()
        {
            // Set StartDateTime to be the last recorded Start time
            _startDateTime =_currentTime;

            _sCAuditService.StartDate = _startDateTime;
            //_sCAuditService.EndDate = endDateTime;
            _sCAuditService.PageCount = 10000;
            //_sCAuditService.TimeRange = counter == 0 ? 1 : counter * 24;

            _currentTime = _startDateTime.AddHours(24);
        }

        private List<SCAudit> GetAll_BaseQuery()
        {
            var returnList = new List<SCAudit>();

            returnList = _sCAuditService.GetAll();
            if (returnList.Any())
            {
                returnList = returnList
                    .Where
                    (
                        item => item.Audit_Part_Num.ToUpper().StartsWith("BNL")
                            && item.Audit_Rem.StartsWith("Added PO")
                            && (item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-") || item.Audit_Dest_Site_Num.ToUpper() == "LTX")
                    )
                    .ToList();
            }

            return returnList;
        }


        private void ExportToJson()
        {
            var returnList = Map.Map_Results(GetAll_BaseQuery());

            JSON_FileExport.WriteFile(_typePrefix + "_ScheduleRepeater_" + (counter + 1).ToString().PadLeft(6, '0'), returnList, returnList.Count, "ScheduleRepeaterFullJoin",
                _startDateTime, _currentTime);
        }


        #region Repeat Job Code

        /// <summary>
        /// Counter used to count the number of times this job has been
        /// executed.
        /// </summary>
        private long counter = 0;
        private int _sleepTime = 1000;
        public void SleepTimer(int minutes = 1, int hours = 0, int seconds = 0)
        {
            var time = (minutes > 0 && minutes < 60) ? 1000 * 60 * minutes : 1000;
            time = (hours > 0 && hours < 24) ? 1000 * 60 * 60 * hours : time;
            time = (seconds > 0 && seconds < 60) ? 1000 * seconds : time;
            _sleepTime = time;
        }

        /// <summary>
        /// Get the Job Name, which reflects the class name.
        /// </summary>
        /// <returns>The class Name.</returns>
        public override string GetName()
        {
            return this.GetType().Name;
        }

        /// <summary>
        /// Execute the Job itself. Just print a message.
        /// </summary>
        public override void DoJob()
        {
            SleepTimer(0, 0, 1);
            ServiceSetup();
            Console.WriteLine(String.Format("This is the execution number \"{0}\" of the Job \"{1}\" - DateStart: {2}, CurrentTime: {3}.", counter.ToString(), this.GetName(), _startDateTimeString, _currentTime.ToString()));
            ExportToJson();
            counter++;
        }

        /// <summary>
        /// Determines this job is repeatable.
        /// </summary>
        /// <returns>Returns true because this job is repeatable.</returns>
        public override bool IsRepeatable()
        {
            if (_startDateTime >= _startDateTime.AddYears(1)) return false;
            if (_startDateTime >= DateTime.Now) return false;
            return true;
        }

        /// <summary>
        /// Determines that this job is to be executed again after
        /// 1 sec.
        /// </summary>
        /// <returns>1 sec, which is the interval this job is to be
        /// executed repeatadly.</returns>
        public override int GetRepetitionIntervalTime()
        {
            return _sleepTime; 
            // 5 * 60 * 1000; 
            // 5 * 60 * 1000 - 5 minutes
        }

        #endregion
    }
}
