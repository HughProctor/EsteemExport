using System;

namespace ESTReporting.EntityModel.Models
{
    public class BAM_Deployments : BaseObjectProperties
    {
        public string SerialNumber { get; set; }
        public EST_HWAssetStatus AssetStatus { get; set; }
        public EST_SCAudit SCAudit_Item { get; set; }
        public EST_SCAuditDeploy SCAuditDeploy_Item { get; set; }
        public BAM_HardwareTemplate_Full BAM_HardwareTemplate_Exception { get; set; }
    }
}
