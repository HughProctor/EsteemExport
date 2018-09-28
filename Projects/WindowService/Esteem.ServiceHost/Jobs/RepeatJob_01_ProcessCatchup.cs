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
using ESTReporting.EntityModel.Context;
using ESTReporting.EntityModel.Models;
using System.Data.Entity;
using Infrastructure;

namespace Esteem.ServiceHost.Jobs
{
    public class RepeatJob_01_ProcessCatchup : Job
    {
        //IBAM_Service _bamService;
        IQueryBuilder _queryBuilder;
        BAMEsteemExportContext _dbContext;

        private string _startDateTimeString = "";
        private string _endDateTimeString = "";
        private string _typePrefix = "SCAudit_";
        private DateTime _startDateTime;
        private DateTime _currentTime;

        public RepeatJob_01_ProcessCatchup()
        {
            _queryBuilder = new QueryBuilder();
            _dbContext = new BAMEsteemExportContext();

            _startDateTime = DateTime.Now;
            _queryBuilder.StartDate = _startDateTime;
            _queryBuilder.TimeRange = 1;

            _queryBuilder.PageCount = 100000000;

            _currentTime = _startDateTime;

        }
        private void ServiceSetup()
        {

            // Set StartDateTime to be the last recorded Start time
            _startDateTime =_currentTime;

            _queryBuilder.StartDate = _startDateTime;
            _queryBuilder.PageCount = 10000;

            _currentTime = _startDateTime.AddHours(1);
        }

        private void RunProcess()
        {
            try
            {
                var _bamService = new BAM_Service();
                var lastProcessedDateTime = new DateTime();

                //var lastProcessedReport = _dbContext.ServiceProgressReport.OrderByDescending(x => x.StartDateTime).LastOrDefault();
                // In the occasion that the Current Running Process over writes the Last Entry, then we need to calculate the Time Gap
                var lastProcessedReports = _dbContext.ServiceProgressReport.Where(x => x.ServiceJobType == 3)
                    .OrderByDescending(x => x.StartDateTime).Take(2).ToList();
                if (lastProcessedReports == null && !lastProcessedReports.Any()) return;

                // Incase there is only one record
                if (lastProcessedReports.Count > 1)
                {
                    // Compare the difference in times
                    var timeSpanGap = (DateTime)lastProcessedReports[0].StartDateTime - (DateTime)lastProcessedReports[1].StartDateTime;

                    // This has just been run
                    if (((DateTime)lastProcessedReports[0].StartDateTime).AddHours(1) >= DateTime.Now)
                    {
                        if (timeSpanGap.Hours > 1)
                            lastProcessedDateTime = (DateTime)lastProcessedReports[1].StartDateTime;
                        else
                            lastProcessedDateTime = (DateTime)lastProcessedReports[0].StartDateTime;
                    }
                    else
                        lastProcessedDateTime = (DateTime)lastProcessedReports[0].StartDateTime;
                }

                if (lastProcessedDateTime == null || lastProcessedDateTime == new DateTime()) return;

                if (lastProcessedDateTime.AddHours(1) >= DateTime.Now) return;

                var failedTimeSpan = DateTime.Now - lastProcessedDateTime;
                var count = failedTimeSpan.Hours;

                _queryBuilder.StartDate = lastProcessedDateTime.AddHours(-1);
                _queryBuilder.TimeRange = 1;

                for (var i = 0; i < count; i++)
                {
                    var returnList = _bamService.ExportDataToBAM(_queryBuilder, 1).Result;
                    _queryBuilder.StartDate = _queryBuilder.StartDate.AddHours(i);
                }
            }
            catch (Exception exp)
            {
                Email.SendException(exp.Message);
                JSON_FileExport.WriteFile(_typePrefix + "_ScheduleRepeater_Exception_01_" + DateTime.Now.ToString("yyMMddhhmm"), exp, 0, "Exception");
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
            SleepTimer(10, 0, 0);
            ServiceSetup();

            // Don't process between 9pm and 4am - the System is overloaded with nightly reporting processes
            if (_currentTime.Hour > 21 || _currentTime.Hour < 4)
                return;

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
