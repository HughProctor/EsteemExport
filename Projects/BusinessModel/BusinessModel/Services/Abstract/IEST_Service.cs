using BusinessModel.Models;
using EntityModel.Repository.Abstract;

namespace BusinessModel.Services.Abstract
{
    public interface IEST_Service
    {
        EST_DataExportModel GetExportData(IQueryBuilder queryBuilder);
    }
}