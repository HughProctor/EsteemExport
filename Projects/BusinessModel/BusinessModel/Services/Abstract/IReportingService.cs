using System.Collections.Generic;
using BusinessModel.Models;

namespace BusinessModel.Services.Abstract
{
    public interface IReportingService
    {
        void ProcessExceptions(List<BAM_ReportingBsm> reporting);
    }
}