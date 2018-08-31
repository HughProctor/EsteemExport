using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessModel.Models;

namespace BusinessModel.Services.Abstract
{
    public interface IReportingService
    {
        Task<int> ProcessExceptions(List<BAM_ReportingBsm> reporting);
        Task<int> ProcessBillables(List<BAM_ReportingBsm> billables);
        ServiceProgressReportBsm ServiceProgressReporting(ServiceProgressReportBsm serviceProgressReport);
    }
}