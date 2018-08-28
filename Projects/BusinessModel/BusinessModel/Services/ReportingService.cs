using BusinessModel.Mappers;
using BusinessModel.Models;
using BusinessModel.Services.Abstract;
using ESTReporting.EntityModel.Context;
using System.Collections.Generic;
using System.Linq;

namespace BusinessModel.Services
{
    public class ReportingService : IReportingService
    {
        private BAMEsteemExportContext dbContext;
        public ReportingService()
        {
            dbContext = new BAMEsteemExportContext();
        }

        public void ProcessExceptions(List<BAM_ReportingBsm> reporting)
        {
            if (reporting == null || !reporting.Any()) return;

            dbContext.BAM_Reporting.AddRange(Map.Map_Results(reporting));
            dbContext.SaveChanges();
        }
    }
}
