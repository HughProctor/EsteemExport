using System;

namespace ESTReporting.EntityModel.Models
{
    public class BAM_Reporting : BaseObjectProperties
    {
        public EST_SCAudit SCAudit_Item { get; set; }
        public EST_SCAuditDeploy SCAuditDeploy_Item { get; set; }
        public BAM_HardwareTemplate_Full BAM_HardwareTemplate_Exception { get; set; }
    }
}
