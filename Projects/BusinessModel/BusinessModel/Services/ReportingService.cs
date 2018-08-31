using BusinessModel.Mappers;
using BusinessModel.Models;
using BusinessModel.Services.Abstract;
using ESTReporting.EntityModel.Context;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

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
            if (reporting == null || !reporting.Any()) return null;
            var mapResults = Map.Map_Results(reporting);
            mapResults.ForEach(item =>
            {
                dbContext.BAM_Reporting.Add(item);
                //dbContext.BAM_Reporting.AddOrUpdate(dbItem => new { dbItem.SerialNumber }, item);
            });
            return dbContext.SaveChangesAsync();
        }

        public Task<int> ProcessBillables(List<BAM_ReportingBsm> billables)
        {
            if (billables == null || !billables.Any()) return null;
            var mapResults = Map.Map_Results(billables, true);
            mapResults.ForEach(item =>
            {
                dbContext.BAM_Deployments.Add(item);
                //dbContext.BAM_Deployments.AddOrUpdate(dbItem => new { dbItem.SerialNumber }, item);
            });
            return dbContext.SaveChangesAsync();
        }

        public ServiceProgressReportBsm ServiceProgressReporting(ServiceProgressReportBsm serviceProgressReport)
        {
            var mapResults = Map.Map_Results(serviceProgressReport);
            dbContext.ServiceProgressReport.AddOrUpdate(dbItem => new { dbItem.StartDateTime }, mapResults);
            dbContext.SaveChanges();
            //var savedItem = dbContext.ServiceProgressReport.Find()
            //var returnResults = Map.Map_Results(mapResults);
            return serviceProgressReport;
        }
    }
}
