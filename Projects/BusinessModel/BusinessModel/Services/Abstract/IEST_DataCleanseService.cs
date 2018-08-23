using System.Collections.Generic;
using BusinessModel.Models;
using EntityModel;

namespace BusinessModel.Services.Abstract
{
    public interface IEST_DataCleanseService
    {
        List<BAMDataModel> ProcessSCAuditList(List<SCAudit> sCAudits);
        List<SCAuditBsm> Process_SCAuditList(List<SCAudit> sCAudits);
        List<SCAuditDeployBsm> Process_SCAuditDeployList(List<SCAuditDeploy> sCAudits);
    }
}