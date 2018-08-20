using System.Collections.Generic;

namespace EntityModel.Repository.Abstract
{
    public interface ISCDeployRepository : IQueryBuilder
    {
        IQueryBuilder QueryBuilderObject { get; set; }
        List<SCAuditDeploy> GetAll();
        string QueryBuilder(string whereExpression = null);
    }
}