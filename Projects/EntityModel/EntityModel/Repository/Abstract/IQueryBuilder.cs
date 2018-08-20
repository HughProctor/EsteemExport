using System;

namespace EntityModel.Repository.Abstract
{
    public interface IQueryBuilder
    {
        int DateRange { get; set; }
        DateTime EndDate { get; set; }
        bool EndDateInclusive { get; set; }
        string EndDateString { get; set; }
        string LastQueryString { get; set; }
        string OrderBy { get; set; }
        int PageCount { get; set; }
        string QueryString { get; set; }
        string QueryStringJoin { get; set; }
        int SkipCount { get; set; }
        DateTime StartDate { get; set; }
        string StartDateString { get; set; }
        int TimeRange { get; set; }
        bool UseQueryBuilder { get; set; }
        string WhereExpression { get; set; }

        void Reset();
    }
}