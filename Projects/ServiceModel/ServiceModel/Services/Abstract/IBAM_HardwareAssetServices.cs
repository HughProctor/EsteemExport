using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;
using System.Collections.Generic;
using System.Net.Http;

namespace ServiceModel.Services.Abstract
{
    public interface IBAM_HardwareAssetServices
    {
        List<BAM_HardwareTemplate> GetHardwareAsset(string serialNumber);
        string CreateProjectionFilter(string serialNumber);
        StringContent CreateProjectionFilter_StringContent(string serialNumber);
        BAM_HardwareTemplate SetHardwareAssetStatus(BAM_HardwareTemplate template, EST_HWAssetStatus hWAssetStatus);
        List<BAM_HardwareTemplate> UpdateTemplate(BAM_HardwareTemplate newTemplate, BAM_HardwareTemplate originalTemplate);
    }
}