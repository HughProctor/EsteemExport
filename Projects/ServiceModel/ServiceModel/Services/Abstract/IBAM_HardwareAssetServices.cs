using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;
using System.Collections.Generic;
using System.Net.Http;

namespace ServiceModel.Services.Abstract
{
    public interface IBAM_HardwareAssetServices
    {
        HardwareTemplate CreateNewTemplate();

        string CreateProjectionFilter(string serialNumber, bool useFullProjection = false);
        StringContent CreateProjectionFilter_StringContent(string serialNumber, bool useFullProjection = false);

        List<HardwareTemplate> GetHardwareAsset(string serialNumber);
        List<HardwareTemplate_Full> GetHardwareAsset_Full(string serialNumber);

        List<HardwareTemplate> InsertTemplate(HardwareTemplate newTemplate);
        HardwareTemplate_Full SetHardwareAssetPrimaryUser(HardwareTemplate_Full template, BAM_User user);
        List<HardwareTemplate> UpdateTemplate(HardwareTemplate newTemplate, HardwareTemplate originalTemplate);
        List<HardwareTemplate_Full> UpdateTemplate(HardwareTemplate_Full newTemplate, HardwareTemplate_Full originalTemplate);

        HardwareTemplate SetHardwareAssetStatus(HardwareTemplate template, EST_HWAssetStatus hWAssetStatus);
        HardwareTemplate_Full SetHardwareAssetStatus(HardwareTemplate_Full template, EST_HWAssetStatus hWAssetStatus);
        HardwareTemplate_Full SetLocation(HardwareTemplate_Full template, string audit_Dest_Site_Num);
        HardwareTemplate SetAssetTag(HardwareTemplate template, string assetTag);
    }
}