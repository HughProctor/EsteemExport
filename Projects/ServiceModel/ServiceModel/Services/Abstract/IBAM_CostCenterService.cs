using ServiceModel.Models.BAM;
using System.Collections.Generic;

namespace ServiceModel.Services.Abstract
{
    public interface IBAM_CostCenterService
    {
        List<TargetHardwareAssetHasCostCenter> GetCostCenterList();
        List<TargetHardwareAssetHasCostCenter> CostCenterList { get; set; }
    }
}