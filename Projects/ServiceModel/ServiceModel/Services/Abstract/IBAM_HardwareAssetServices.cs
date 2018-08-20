using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;
using System.Collections.Generic;
using System.Net.Http;

namespace ServiceModel.Services.Abstract
{
    public interface IBAM_HardwareAssetServices
    {
        BAM_HardwareTemplate CreateNewTemplate();
        string CreateProjectionFilter(string serialNumber, bool useFullProjection = false);
        StringContent CreateProjectionFilter_StringContent(string serialNumber, bool useFullProjection = false);
        List<BAM_HardwareTemplate> GetHardwareAsset(string serialNumber);
        List<BAM_HardwareTemplate_Full> GetHardwareAsset_Full(string serialNumber);
        List<BAM_HardwareTemplate> InsertTemplate(BAM_HardwareTemplate newTemplate);
        BAM_HardwareTemplate_Full SetHardwareAssetPrimaryUser(BAM_HardwareTemplate_Full template, BAM_User user);
        BAM_HardwareTemplate SetHardwareAssetStatus(BAM_HardwareTemplate template, EST_HWAssetStatus hWAssetStatus);
        BAM_HardwareTemplate_Full SetHardwareAssetStatus(BAM_HardwareTemplate_Full template, EST_HWAssetStatus hWAssetStatus);
        List<BAM_HardwareTemplate> UpdateTemplate(BAM_HardwareTemplate newTemplate, BAM_HardwareTemplate originalTemplate);
        List<BAM_HardwareTemplate_Full> UpdateTemplate(BAM_HardwareTemplate_Full newTemplate, BAM_HardwareTemplate_Full originalTemplate);
        BAM_HardwareTemplate_Full SetLocation(BAM_HardwareTemplate_Full template, string audit_Dest_Site_Num);
        BAM_HardwareTemplate SetAssetTag(BAM_HardwareTemplate template, string assetTag);
    }
}