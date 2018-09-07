using AutoMapper;
using EntityModel;
using BusinessModel.Mappers;
using EntityModel.Repository;
using Infrastructure.FileExport;
using SchedulerManager.Mechanism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Services.Abstract;
using EntityModel.Repository.Abstract;
using BusinessModel.Services;
using Esteem.ServiceHost.Models;

namespace Esteem.ServiceHost.Jobs
{
    public class RepeatJob_04_Backprocess : Job
    {
        //IBAM_Service _bamService;
        IQueryBuilder _queryBuilder;

        private string _startDateTimeString = "";
        private string _endDateTimeString = "";
        private string _typePrefix = "SCAudit_";
        private DateTime _startDateTime;
        private DateTime _currentTime;

        public RepeatJob_04_Backprocess()
        {
            //_bamService = new BAM_Service();
            _queryBuilder = new QueryBuilder();

            _startDateTimeString = string.IsNullOrEmpty(_startDateTimeString) ? "01/01/2017" : _startDateTimeString;
            _endDateTimeString = string.IsNullOrEmpty(_endDateTimeString) ? "01/01/2019" : _endDateTimeString;

            DateTime.TryParse(_startDateTimeString, out _startDateTime);
            DateTime.TryParse(_endDateTimeString, out var endDateTime);

            _queryBuilder.StartDateString = _startDateTimeString;
            //_queryBuilder.EndDateString = _endDateTimeString;
            _queryBuilder.PageCount = 100000000;

            _currentTime = _startDateTime;

        }
        private void ServiceSetup()
        {
            // Set StartDateTime to be the last recorded Start time
            _startDateTime =_currentTime;

            _queryBuilder.StartDate = _startDateTime;
            //_startDateTimeString = _startDateTime.ToString();
            //_sCAuditService.EndDate = endDateTime;
            _queryBuilder.PageCount = 10000;
            //_sCAuditService.TimeRange = counter == 0 ? 1 : counter * 24;

            _currentTime = _startDateTime.AddHours(24);
        }

        private void RunProcess()
        {
            try
            {
                var _bamService = new BAM_Service();
                var returnList = _bamService.ExportDataToBAM(_queryBuilder, 4).Result;
            }
            catch (Exception exp)
            {
                JSON_FileExport.WriteFile(_typePrefix + "_ScheduleRepeater_Exception_" + DateTime.Now.ToString("yyMMddhhmm"), exp, 0, "Exception");
            }
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
            SleepTimer(1, 0, 0);
            ServiceSetup();
            //Console.WriteLine(String.Format("This is the execution number \"{0}\" of the Job \"{1}\" - DateStart: {2}, CurrentTime: {3}.", counter.ToString(), this.GetName(), _startDateTime, _currentTime.ToString()));
            RunProcess();
            counter++;
        }

        /// <summary>
        /// Determines this job is repeatable.
        /// </summary>
        /// <returns>Returns true because this job is repeatable.</returns>
        public override bool IsRepeatable()
        {
            //if (_startDateTime >= _startDateTime.AddYears(1)) return false;
            //if (_startDateTime >= DateTime.Now) return false;
            return false;
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
