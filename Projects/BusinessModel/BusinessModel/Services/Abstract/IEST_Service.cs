using BusinessModel.Models;
using EntityModel.Repository.Abstract;

namespace BusinessModel.Services.Abstract
{
    public interface IEST_Service
    {
        IQueryBuilder _queryBuilder { get; set; }
        EST_DataExportModel GetExportData(IQueryBuilder queryBuilder);
    }
}