using BusinessModel.Models;
using EntityModel.Repository.Abstract;
using System.Threading.Tasks;

namespace BusinessModel.Services.Abstract
{
    public interface IBAM_Service
    {
        Task<EST_DataExportModel> ExportDataToBAM(IQueryBuilder queryBuilder);
    }
}