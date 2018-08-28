using ServiceModel.Models.BAM;

namespace BusinessModel.Models
{
    public class BAM_ReportingBsm
    {
        public SCAuditBsm SCAudit_Item { get; set; }
        public SCAuditDeployBsm SCAuditDeploy_Item { get; set; }
        public HardwareTemplate_Full BAM_HardwareTemplate_Original { get; set; }
        public HardwareTemplate_Full BAM_HardwareTemplate_Exception { get; set; }
    }
}
