using System.Collections.Generic;

namespace EntityModel.Repository.Abstract
{
    public interface ISCAuditRepository : IQueryBuilder
    {
        IQueryBuilder QueryBuilderObject { get; set; }
        List<SCAudit> GetAll();
        string QueryBuilder(string whereExpression = null);
    }
}