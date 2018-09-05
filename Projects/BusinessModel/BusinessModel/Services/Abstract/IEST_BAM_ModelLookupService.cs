using EntityModel.Repository.Abstract;
using ServiceModel.Models.BAM;

namespace BusinessModel.Services.Abstract
{
    public interface IEST_BAM_ModelLookupService
    {
        void GetBAM_Manufacturers();
        void GetEST_Manufacturers(IQueryBuilder queryBuilder);
        void SetModelData(HardwareTemplate_Full bamTemplate);
    }
}