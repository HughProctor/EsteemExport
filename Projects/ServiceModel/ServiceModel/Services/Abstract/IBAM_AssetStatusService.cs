using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;

namespace ServiceModel.Services.Abstract
{
    public interface IBAM_AssetStatusService
    {
        AssetStatus GetAssetStatusTemplate2(EST_HWAssetStatus assetStatus);
        HardwareAssetStatus GetAssetStatusTemplate(EST_HWAssetStatus assetStatus);
    }
}
