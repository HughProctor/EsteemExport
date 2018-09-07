using BusinessModel.Models;
using EntityModel.Repository.Abstract;
using ServiceModel.Models.BAM;
using System.Collections.Generic;

namespace BusinessModel.Services.Abstract
{
    public interface IEST_BAM_ModelLookupService
    {
        List<EST_BAM_ModelLookupBsm> GetBAM_Manufacturers();
        List<TempModel> GetEST_Manufacturers(IQueryBuilder queryBuilder);
        HardwareTemplate_Full SetModelData(HardwareTemplate_Full bamTemplate, string modelName, out bool success);
        List<EST_BAM_ModelLookupBsm> GetBAM_ManufacturerList();
    }
}