using EntityModel.Connection;
using EntityModel.Repository.Abstract;
using Infrastructure.FileExport;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel.Repository
{
    public class SCPartRepository : QueryBuilder
    {
        SqlDbConnection _sqlDbConnection;
        string _connectionString;

        public string _orderBy = "ORDER BY [Part_Last_Update] desc";

        public SCPartRepository(IQueryBuilder queryBuilder, string connectionString = null)
        {
            if (queryBuilder != null)
            {
                base.DateRange = queryBuilder.DateRange;
                base.EndDate = queryBuilder.EndDate;
                base.EndDateInclusive = queryBuilder.EndDateInclusive;
                base.EndDateString = queryBuilder.EndDateString;
                base.LastQueryString = queryBuilder.LastQueryString;
                base.OrderBy = queryBuilder.OrderBy;
                base.PageCount = queryBuilder.PageCount;
                base.SkipCount = queryBuilder.SkipCount;
                base.StartDate = queryBuilder.StartDate;
                base.StartDateString = queryBuilder.StartDateString;
                base.TimeRange = queryBuilder.TimeRange;
                base.WhereExpression = queryBuilder.WhereExpression;
            }
            _sqlDbConnection = new SqlDbConnection();

        }

        public SCPartRepository(string connectionString = null)
        {
            _sqlDbConnection = new SqlDbConnection();
        }

        public string QueryBuilder(string whereExpression = null)
        {
            var returnQueryString = "";

            QueryString = "SELECT " + (PageCount > 0 ? "TOP " + PageCount.ToString() + " " : "") +
                "NULLIF(LTRIM(RTRIM([Part_Num])), '') AS [PART_NO], " +
                "NULLIF(LTRIM(RTRIM([Part_Desc])), '') AS [PART_DESC], " +
                "NULLIF(LTRIM(RTRIM([Part_Alt_Part_Num])), '') AS [PART_DESC_ALT], " +
                "NULLIF(LTRIM(RTRIM([Part_Type])), '') AS [PART_TYPE], " +
                "NULLIF(LTRIM(RTRIM([Part_Last_Update])), '') AS [UPDATE_DATE] " +
                "FROM [SCPart] (NOLOCK) ";
            //QueryStringJoin = "LEFT JOIN [SCPart] ON audit_part_num = [Part_Num]";
            //QueryString = QueryString + QueryStringJoin;

            //if (string.IsNullOrEmpty(WhereExpression))
            if (StartDate > DateTime.MinValue)
            {
                if (TimeRange > 0)
                    _whereExpression = "WHERE [Part_Last_Update] >= CONVERT(DATETIME, '" + StartDate + "', 103) AND [Part_Last_Update] <= CONVERT(DATETIME, '" + StartDate.AddHours(TimeRange) + "', 103) ";
                else if (DateRange > 0)
                    _whereExpression = "WHERE [Part_Last_Update] >= CONVERT(DATETIME, '" + StartDate + "', 103) AND [Part_Last_Update] <= CONVERT(DATETIME, '" + StartDate.AddDays(DateRange) + "', 103) ";
                else if (EndDate > DateTime.MinValue)
                    _whereExpression = "WHERE [Part_Last_Update] >= CONVERT(DATETIME, '" + StartDate + "', 103) AND [Part_Last_Update] <= CONVERT(DATETIME, '" + EndDate + "', 103) ";
                else
                    _whereExpression = "WHERE [Part_Last_Update] >= CONVERT(DATETIME, '" + StartDate + "', 103) AND [Part_Last_Update] <= CONVERT(DATETIME, '" + StartDate.AddDays(1) + "', 103) ";
            }
            else _whereExpression = "";
            WhereExpression = string.IsNullOrEmpty(WhereExpression) ? _whereExpression :
                 string.IsNullOrEmpty(_whereExpression) ? WhereExpression : _whereExpression + WhereExpression.Replace("WHERE", "AND")  ;
            //WhereExpression = !string.IsNullOrEmpty(WhereExpression) ? WhereExpression + "AND [Part_Type] != 'D' " : "WHERE [Part_Type] != 'D'";

            OrderBy = !string.IsNullOrEmpty(OrderBy) ? OrderBy : _orderBy;

            LastQueryString = returnQueryString = QueryString + WhereExpression + OrderBy;
            // Need to reset the Where and Order by, otherwise they remain in memory for next request
            Reset();

            return returnQueryString;
        }


        public List<SCPart> GetAll()
        {
            var model = new SCPartModel();

            using (var connection = _sqlDbConnection.CreateConnection(_connectionString))
            {
                var command = new SqlCommand(UseQueryBuilder ? QueryBuilder() : QueryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        model.SCPartList.Add(new SCPart()
                        {
                            Asset_Part_Num = reader["PART_NO"].ToString(),
                            Asset_Desc = reader["PART_DESC"].ToString(),
                            Asset_Desc_Code = reader["PART_DESC_ALT"].ToString(),
                            Asset_PartType = reader["PART_TYPE"].ToString(),
                            Asset_Last_Update = DateTime.Parse(reader["UPDATE_DATE"].ToString())
                        });
                    }
                    Reset();
                    reader.Close();
                }
                catch (Exception exp)
                {
                    JSON_FileExport.WriteFile("EntityModel" + "_ScheduleRepeater_Exception_04_" + DateTime.Now.ToString("yyMMddhhmm"), exp, 0, "Exception");
                    Console.WriteLine(exp.Message);
                }
            }

            return model.SCPartList;
        }
    }
}
