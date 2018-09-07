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
using EsteemBAMConsoleApp.Models;
using ESTReporting.EntityModel.Context;
using ESTReporting.EntityModel.Models;
using System.Data.Entity;

namespace EsteemBAMConsoleApp.Jobs
{
    public class RepeatJob_02_ProcessErrors : Job
    {
        //IBAM_Service _bamService;
        IQueryBuilder _queryBuilder;
        BAMEsteemExportContext _dbContext;

        private string _startDateTimeString = "";
        private string _endDateTimeString = "";
        private string _typePrefix = "SCAudit_";
        private DateTime _startDateTime;
        private DateTime _currentTime;

        public RepeatJob_02_ProcessErrors()
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

                var reports = _dbContext.ServiceProgressReport
                    .Where(x => x.ProcessSuccessFlag == false
                        && (x.EsteemExtractDateTime == null || x.BAMExportDateTime == null));
                if (reports == null) return;

                var whereStatement = "";

                reports.ToList().ForEach(item =>
                {
                    var erroringAssets = _dbContext.BAM_Reporting.Where(x => x.ServiceProgressReportId == item.Id).ToList();
                    if (erroringAssets != null && erroringAssets.Any())
                    {
                        erroringAssets.ForEach(errorItem =>
                        {
                            whereStatement += " AND [Audit_Ser_Num] LIKE '%" + errorItem.SerialNumber + "%' ";
                        });
                    }
                    if (!string.IsNullOrEmpty(whereStatement))
                    {
                        _queryBuilder.StartDate = (DateTime)item.QueryStartParameters;
                        _queryBuilder.EndDate = _queryBuilder.StartDate.AddHours(1);
                            //(DateTime)item.QueryStartParameters == null || (DateTime)item.QueryStartParameters == new DateTime() ?
                            //    _queryBuilder.StartDate.AddHours(1) : _queryBuilder.EndDate;

                        _queryBuilder.WhereExpression = whereStatement;
                        var returnList = _bamService.ExportDataToBAM(_queryBuilder, 2).Result;
                    }
                });
            }
            catch (Exception exp)
            {
                //JSON_FileExport.WriteFile(_typePrefix + "_ScheduleRepeater_02_Exception_" + DateTime.Now.ToString("yyMMddhhmm"), exp, 0, "Exception");
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
            SleepTimer(0, 1, 0);
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
