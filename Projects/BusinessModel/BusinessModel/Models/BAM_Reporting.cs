using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;

namespace BusinessModel.Models
{
    public class BAM_ReportingBsm
    {
        public string SerialNumber { get; set; }
        public EST_HWAssetStatus AssetStatus { get; set; }
        public SCAuditBsm SCAudit_Item { get; set; }
        public SCAuditDeployBsm SCAuditDeploy_Item { get; set; }
        public HardwareTemplate_Full BAM_HardwareTemplate_Exception { get; set; }
        public int ServiceProgressReportId { get; set; }
        public ServiceProgressReportBsm ServiceProgressReport { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
