using EntityModel.Connection;
using EntityModel.Repository.Abstract;
using Infrastructure.FileExport;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EntityModel.Repository
{
    public class SCAuditRepository : QueryBuilder, ISCAuditRepository
    {
        SqlDbConnection _sqlDbConnection;
        string _connectionString;
        public string _orderBy = "ORDER BY [Audit_Move_Date] desc";
        public IQueryBuilder QueryBuilderObject { get; set; }

        public SCAuditRepository(string connectionString = null)
        {
            _connectionString = connectionString;
            _sqlDbConnection = new SqlDbConnection();
        }

        public string QueryBuilder(string whereExpression = null)
        {
            var returnQueryString = "";

            QueryString = "SELECT " + (PageCount > 0 ? "TOP " + PageCount.ToString() + " " : "") +
                "NULLIF(LTRIM(RTRIM(SCA.[Audit_Ser_Num])), '') AS [SERIAL_NO], " +
                "NULLIF(LTRIM(RTRIM(SCA.[Audit_Part_Num])), '') AS [PART_NO], " +
                "NULLIF(LTRIM(RTRIM(SCP.[Part_Desc])), '') AS [PART_DESC], " +
                "NULLIF(LTRIM(RTRIM(SCP.[Part_Alt_Part_Num])), '') AS [PART_DESC_ALT], " +
                "NULLIF(LTRIM(RTRIM(SCA.[Audit_Source_Site_Num])), '') AS [SOURCE_SITE_NO], " +
                "NULLIF(LTRIM(RTRIM(SCA.[Audit_Dest_Site_Num])), '') AS [DESTINATION_SITE_NO], " +
                "NULLIF(LTRIM(RTRIM(SCP.[Part_Type])), '') AS [PART_TYPE], " +
                "NULLIF(LTRIM(RTRIM(SCA.[Audit_Rem])), '') AS [REMARK], " +
                "NULLIF(LTRIM(RTRIM(SCA.[Audit_User])), '') AS [USER], " +
                "SCA.[Audit_Move_Date] AS [MOVE_DATE], " +
                "SCA.[Audit_Last_Update] AS [UPDATE_DATE] " +
                "FROM [SCAudit] AS SCA (NOLOCK) ";
            QueryStringJoin = "FULL JOIN [SCPart] AS SCP ON SCA.[Audit_Part_Num] = SCP.[Part_Num] ";
            QueryString = QueryString + QueryStringJoin;

            //if (string.IsNullOrEmpty(WhereExpression))
            if (StartDate > DateTime.MinValue)
            {
                if (TimeRange > 0)
                    _whereExpression = "WHERE [Audit_Last_Update] >= CONVERT(DATETIME, '" + StartDate + "', 103) AND [Audit_Last_Update] <= CONVERT(DATETIME, '" + StartDate.AddHours(TimeRange) + "', 103) ";
                else if (DateRange > 0)
                    _whereExpression = "WHERE [Audit_Last_Update] >= CONVERT(DATETIME, '" + StartDate + "', 103) AND [Audit_Last_Update] <= CONVERT(DATETIME, '" + StartDate.AddDays(DateRange) + "', 103) ";
                else if (EndDate > DateTime.MinValue && EndDate > DateTime.MinValue.AddDays(1))
                    _whereExpression = "WHERE [Audit_Last_Update] >= CONVERT(DATETIME, '" + StartDate + "', 103) AND [Audit_Last_Update] <= CONVERT(DATETIME, '" + EndDate + "', 103) ";
                else
                    _whereExpression = "WHERE [Audit_Last_Update] >= CONVERT(DATETIME, '" + StartDate + "', 103) AND [Audit_Last_Update] <= CONVERT(DATETIME, '" + StartDate.AddDays(1) + "', 103) ";
            }
            else _whereExpression = "";
            WhereExpression = string.IsNullOrEmpty(WhereExpression) ? _whereExpression :
                 string.IsNullOrEmpty(_whereExpression) ? WhereExpression : _whereExpression + WhereExpression.Replace("WHERE", "AND");
            WhereExpression = !string.IsNullOrEmpty(WhereExpression) ? WhereExpression + "AND [Part_Type] != 'D' " : "WHERE [Part_Type] != 'D'";

            OrderBy = !string.IsNullOrEmpty(OrderBy) ? OrderBy : _orderBy;

            LastQueryString = returnQueryString = QueryString + WhereExpression + OrderBy;
            if (QueryBuilderObject != null)
                QueryBuilderObject.LastQueryString = LastQueryString;

            // Need to reset the Where and Order by, otherwise they remain in memory for next request
            Reset();

            return returnQueryString;
        }

        public List<SCAudit> GetAll()
        {
            var scAuditModel = new SCAuditModel();
            if (QueryBuilderObject != null)
            {
                base.DateRange = QueryBuilderObject.DateRange;
                base.EndDate = QueryBuilderObject.EndDate;
                base.EndDateInclusive = QueryBuilderObject.EndDateInclusive;
                base.EndDateString = QueryBuilderObject.EndDateString;
                base.LastQueryString = QueryBuilderObject.LastQueryString;
                base.OrderBy = QueryBuilderObject.OrderBy;
                base.PageCount = QueryBuilderObject.PageCount;
                base.SkipCount = QueryBuilderObject.SkipCount;
                base.StartDate = QueryBuilderObject.StartDate;
                base.StartDateString = QueryBuilderObject.StartDateString;
                base.TimeRange = QueryBuilderObject.TimeRange;
                base.WhereExpression = QueryBuilderObject.WhereExpression;
            }

            using (var connection = _sqlDbConnection.CreateConnection(_connectionString))
            {
                var command = new SqlCommand(UseQueryBuilder ? QueryBuilder() : QueryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        scAuditModel.SCAuditList.Add(new SCAudit()
                        {
                            Audit_Ser_Num = string.IsNullOrEmpty(reader["SERIAL_NO"]?.ToString()) ? null : reader["SERIAL_NO"]?.ToString().Trim(),
                            Audit_Part_Num = reader["PART_NO"]?.ToString(),
                            Audit_Prod_Desc = reader["PART_DESC"]?.ToString(),
                            Audit_Prod_Desc_Alt = reader["PART_DESC_ALT"]?.ToString(),
                            Audit_Source_Site_Num = reader["SOURCE_SITE_NO"]?.ToString(),
                            Audit_Dest_Site_Num = reader["DESTINATION_SITE_NO"]?.ToString(),
                            Audit_Part_Type = reader["PART_TYPE"]?.ToString(),
                            Audit_Rem = reader["REMARK"]?.ToString(),
                            Audit_User = reader["USER"]?.ToString(),
                            Audit_Move_Date = DateTime.Parse(reader["MOVE_DATE"].ToString()),
                            Audit_Last_Update = DateTime.Parse(reader["UPDATE_DATE"].ToString())
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

            return scAuditModel.SCAuditList;
        }
    }
}
