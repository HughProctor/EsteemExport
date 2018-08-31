using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Models
{
    public class ServiceProgressReportBsm
    {
        public DateTime? StartDateTime { get; set; }
        public DateTime? EsteemExtractDateTime { get; set; }
        public DateTime? BAMExportDateTime { get; set; }
        public bool ProcessSuccessFlag { get; set; }
        public int? NewItemCount { get; set; }
        public int? LocationChangeCount { get; set; }
        public int? AssetTagChangeCount { get; set; }
        public int? DeployedCount { get; set; }
        public int? ReturnedCount { get; set; }
    }
}
