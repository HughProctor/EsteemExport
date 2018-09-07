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
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace Esteem.ServiceHost.Jobs
{
    public class RepeatJob_05_PurgeData : Job
    {
        BAMEsteemExportContext _dbContext;

        public RepeatJob_05_PurgeData()
        {
            _dbContext = new BAMEsteemExportContext();
        }
        private void ServiceSetup()
        {
        }

        private void RunProcess()
        {
            var itemList_1 = _dbContext.ServiceProgressReport.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList().ToList();
            var itemList_2 = _dbContext.BAM_TargetHardwareAssetHasCostCenter.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList();
            var itemList_3  = _dbContext.BAM_TargetHardwareAssetHasLocation.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList();
            var itemList_4  = _dbContext.BAM_TargetHardwareAssetHasPrimaryUser.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList();
            var itemList_5  = _dbContext.BAM_ManufacturerEnum.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList();
            var itemList_6  = _dbContext.BAM_ModelEnum.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList();
            var itemList_7  = _dbContext.SCAudit.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList();
            var itemList_8  = _dbContext.SCAuditDeploy.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList();
            var itemList_9  = _dbContext.BAM_HardwareAssetStatus.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList();
            var itemList_10  = _dbContext.BAM_HardwareAssetType.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList();
            var itemList_11  = _dbContext.BAM_HardwareTemplate_Full.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList();
            var itemList_12  = _dbContext.BAM_AssetStatus.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList();
            var itemList_13  = _dbContext.BAM_Deployments.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList();
            var itemList_14  = _dbContext.BAM_Reporting.Where(x => x.CreatedDate <= DbFunctions.AddDays(DateTime.Now, -60)).ToList();

            itemList_1.ForEach(item => _dbContext.ServiceProgressReport.Remove(item));
            itemList_2.ForEach(item => _dbContext.BAM_TargetHardwareAssetHasCostCenter.Remove(item));
            itemList_3.ForEach(item => _dbContext.BAM_TargetHardwareAssetHasLocation.Remove(item));
            itemList_4.ForEach(item => _dbContext.BAM_TargetHardwareAssetHasPrimaryUser.Remove(item));
            itemList_5.ForEach(item => _dbContext.BAM_ManufacturerEnum.Remove(item));
            itemList_6.ForEach(item => _dbContext.BAM_ModelEnum.Remove(item));
            itemList_7.ForEach(item => _dbContext.SCAudit.Remove(item));
            itemList_8.ForEach(item => _dbContext.SCAuditDeploy.Remove(item));
            itemList_9.ForEach(item => _dbContext.BAM_HardwareAssetStatus.Remove(item));
            itemList_10.ForEach(item => _dbContext.BAM_HardwareAssetType.Remove(item));
            itemList_11.ForEach(item => _dbContext.BAM_HardwareTemplate_Full.Remove(item));
            itemList_12.ForEach(item => _dbContext.BAM_AssetStatus.Remove(item));
            itemList_13.ForEach(item => _dbContext.BAM_Deployments.Remove(item));
            itemList_14.ForEach(item => _dbContext.BAM_Reporting.Remove(item));
            _dbContext.SaveChanges();
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
            SleepTimer(0, 24, 0);
            ServiceSetup();
            RunProcess();
            counter++;
        }

        /// <summary>
        /// Determines this job is repeatable.
        /// </summary>
        /// <returns>Returns true because this job is repeatable.</returns>
        public override bool IsRepeatable()
        {
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
