using BusinessModel.Mappers;
using BusinessModel.Models;
using BusinessModel.Services.Abstract;
using ESTReporting.EntityModel.Context;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BusinessModel.Services
{
    public class ReportingService : IReportingService
    {
        private BAMEsteemExportContext dbContext;
        public ReportingService()
        {
            dbContext = new BAMEsteemExportContext();
        }

        public Task<int> ProcessExceptions(List<BAM_ReportingBsm> reporting)
        {
            if (reporting == null || !reporting.Any()) return Task.FromResult(1); 
            var mapResults = Map.Map_Results(reporting);
            mapResults.ForEach(item =>
            {
                dbContext.BAM_Reporting.Add(item);
                //dbContext.Entry(item.ServiceProgressReport).State = EntityState.Unchanged;

                //dbContext.ServiceProgressReport.AddOrUpdate(item.ServiceProgressReport);
                //dbContext.Entry(item.ServiceProgressReport).State = System.Data.Entity.EntityState.Unchanged;
                //dbContext.BAM_Reporting.AddOrUpdate(dbItem => new { dbItem.SerialNumber }, item);
            });
            return dbContext.SaveChangesAsync();
        }

        public Task<int> ProcessBillables(List<BAM_ReportingBsm> billables)
        {
            if (billables == null || !billables.Any()) return Task.FromResult(1);
            var mapResults = Map.Map_Results(billables, true);
            mapResults.ForEach(item =>
            {
                dbContext.BAM_Deployments.Add(item);

                //dbContext.Entry(item.ServiceProgressReport).State = System.Data.Entity.EntityState.Unchanged;
                //dbContext.BAM_Deployments.AddOrUpdate(dbItem => new { dbItem.SerialNumber }, item);
            });
            return dbContext.SaveChangesAsync();
        }

        public ServiceProgressReportBsm ServiceProgressReporting(ServiceProgressReportBsm serviceProgressReport)
        {
            var mapResults = Map.Map_Results(serviceProgressReport);
            dbContext.ServiceProgressReport.AddOrUpdate(dbItem => new { dbItem.StartDateTime, dbItem.ServiceJobType }, mapResults);
            dbContext.SaveChanges();
            var returnResults = Map.Map_Results(mapResults);
            return returnResults; // serviceProgressReport;
        }
    }
}
