using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;
using System.Collections.Generic;

namespace ServiceModel.Services.Abstract
{
    public interface IBAM_ModelService
    {
        List<BAM_Manufacturer> GetBAM_ModelDescriptions();
    }
}
