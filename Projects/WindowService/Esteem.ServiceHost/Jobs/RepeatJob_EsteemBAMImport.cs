using AutoMapper;
using BusinessModel.Models;
using BusinessModel.Services;
using BusinessModel.Services.Abstract;
using EntityModel;
using EntityModel.Mappers;
using EntityModel.Repository;
using EntityModel.Repository.Abstract;
using Infrastructure.FileExport;
using SchedulerManager.Mechanism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteemBAMConsoleApp.Jobs
{
    public class RepeatJob_EsteemBAMImport : Job
    {
        IEST_Service _eST_Service;
        IQueryBuilder _queryBuilder;
        private string _startDateTimeString = "";
        private string _endDateTimeString = "";
        private string _typePrefix = "BAMImport_";
        private DateTime _startDateTime;
        private DateTime _currentTime;

        public RepeatJob_EsteemBAMImport()
        {
            _eST_Service = new EST_Service();
            _queryBuilder = new QueryBuilder();

            _queryBuilder.StartDateString = _startDateTimeString = "01/01/2017";
            _queryBuilder.EndDateString = "30/11/2017";
            _currentTime = _queryBuilder.StartDate;

        }
        private void ServiceSetup()
        {
            // Set StartDateTime to be the last recorded Start time
            _startDateTime =_currentTime;

            _queryBuilder.StartDate = _startDateTime;
            //_sCAuditService.EndDate = endDateTime;
            _queryBuilder.PageCount = 10000;
            //_sCAuditService.TimeRange = counter == 0 ? 1 : counter * 24;

            _currentTime = _startDateTime.AddHours(24);
        }

        private List<SCAudit> GetAll_BaseQuery()
        {
            var returnValue = new EST_DataExportModel(); 

            returnValue = _eST_Service.GetExportData(_queryBuilder);
            if (returnValue.Any())
            {
                returnValue = returnValue
                    .Where
                    (
                        item => item.Audit_Part_Num.ToUpper().StartsWith("BNL")
                            && item.Audit_Rem.StartsWith("Added PO")
                            && (item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-") || item.Audit_Dest_Site_Num.ToUpper() == "LTX")
                    )
                    .ToList();
            }

            return returnValue;
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
