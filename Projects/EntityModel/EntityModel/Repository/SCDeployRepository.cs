using EntityModel.Connection;
using EntityModel.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EntityModel.Repository
{
    public class SCDeployRepository : QueryBuilder, ISCDeployRepository
    {
        SqlDbConnection _sqlDbConnection;
        string _connectionString;
        public IQueryBuilder QueryBuilderObject { get; set; }

        public string _orderBy = "ORDER BY [UPDATE_DATE] desc";

        public SCDeployRepository(string connectionString = null)
        {
            _connectionString = connectionString;
            _sqlDbConnection = new SqlDbConnection();
        }

        public string QueryBuilder(string whereExpression = null)
        {
            var returnQueryString = "";
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

            QueryString = "SELECT " + (PageCount > 0 ? "TOP " + PageCount.ToString() + " " : "") +
                "NULLIF(LTRIM(RTRIM(SL.[Fsrl_Id_Num])), '') AS [SERIAL_NO], " +
                "NULLIF(LTRIM(RTRIM(SL.[Fsrl_Ret_Id])), '') AS [SERIAL_NO_RETURNED], " +
                "NULLIF(LTRIM(RTRIM(SL.[Fsrl_Part_Num])), '') AS [PART_NO], " +
                "NULLIF(NULLIF(LTRIM(RTRIM(SL.[Fsrl_Ret_Part_Num])), ''), 'NONE') AS [PART_NO_RETURNED], " +
                "NULLIF(LTRIM(RTRIM(SL.[Fsrl_Part_Desc])), '') AS [PART_DESC], " +
                "NULLIF(NULLIF(LTRIM(RTRIM(SCP.[Part_Desc])), ''), 'PART USED NONE RETURNED') AS [PART_DESC_RETURNED], " +
                //"NULLIF(LTRIM(RTRIM(SCA.[Audit_Source_Site_Num])), '') AS [SOURCE_SITE_NO], " +
                //"NULLIF(LTRIM(RTRIM(SCA.[Audit_Dest_Site_Num])), '') AS [DESTINATION_SITE_NO], " +
                "NULLIF(LTRIM(RTRIM(SCP.[Part_Type])), '') AS [Part_Type], " +
                "NULLIF(LTRIM(RTRIM(SCP.[Part_Alt_Part_Num])), '') AS [Part_Code], " +
                "NULLIF(LTRIM(RTRIM(SL.[Fsrl_Call_Num])), '') AS [Call_Num], " +
                "NULLIF(LTRIM(RTRIM(SC.[Call_Contact])), '') AS [Call_Contact], " +
                "NULLIF(LTRIM(RTRIM(SC.[Call_Num])), '') AS [Call_Num], " +
                "NULLIF(LTRIM(RTRIM(SC.[Call_Ref])), '') AS [Call_Ref], " +
                "CASE WHEN LEFT([Call_Phone], 2) LIKE '0%' THEN NULL ELSE LTRIM(RTRIM([Call_Phone])) END AS [Cost_Code], " +
                "SL.[Fsrl_Last_Update] AS [UPDATE_DATE] " +
                "FROM [SCFsrl] AS SL (NOLOCK) ";
            QueryStringJoin = "FULL JOIN [SCPart] AS SCP ON SL.[Fsrl_Ret_Part_Num] = SCP.[Part_Num] ";
            QueryStringJoin += "FULL JOIN [SCCall] AS SC ON SL.[Fsrl_Call_Num] = SC.[Call_Num] ";
            QueryStringJoin += "FULL JOIN [SCCalt] AS SCT ON SC.[Call_Calt_Code] = SCT.[Calt_Code] ";
            QueryString = QueryString + QueryStringJoin;

            //if (string.IsNullOrEmpty(WhereExpression))
            if (StartDate > DateTime.MinValue)
            {
                if (TimeRange > 0)
                    _whereExpression = "WHERE [Fsrl_Last_Update]  >= CONVERT(DATETIME, '" + StartDate + "', 103) AND [Fsrl_Last_Update] <= CONVERT(DATETIME, '" + StartDate.AddHours(TimeRange) + "', 103) ";
                else if (DateRange > 0)
                    _whereExpression = "WHERE [Fsrl_Last_Update] >= CONVERT(DATETIME, '" + StartDate + "', 103) AND [Fsrl_Last_Update] <= CONVERT(DATETIME, '" + StartDate.AddDays(DateRange) + "', 103) ";
                else if (EndDate > DateTime.MinValue)
                    _whereExpression = "WHERE [Fsrl_Last_Update] >= CONVERT(DATETIME, '" + StartDate + "', 103) AND [Fsrl_Last_Update] <= CONVERT(DATETIME, '" + EndDate + "', 103) ";
                else
                    _whereExpression = "WHERE [Fsrl_Last_Update] >= CONVERT(DATETIME, '" + StartDate + "', 103) AND [Fsrl_Last_Update] <= CONVERT(DATETIME, '" + StartDate.AddDays(1) + "', 103) ";
            }
            else _whereExpression = "";
            WhereExpression = string.IsNullOrEmpty(WhereExpression) ? _whereExpression :
                 string.IsNullOrEmpty(_whereExpression) ? WhereExpression : _whereExpression + WhereExpression.Replace("WHERE", "AND");
            //WhereExpression = !string.IsNullOrEmpty(WhereExpression) ? WhereExpression + "AND [Part_Type] != 'D' " : "WHERE [Part_Type] != 'D'";


            OrderBy = !string.IsNullOrEmpty(OrderBy) ? OrderBy : _orderBy;

            LastQueryString = returnQueryString = QueryString + WhereExpression + OrderBy;
            // Need to reset the Where and Order by, otherwise they remain in memory for next request
            Reset();

            return returnQueryString;
        }


        public List<SCAuditDeploy> GetAll()
        {
            var scAuditModel = new SCAuditDeployModel();

            using (var connection = _sqlDbConnection.CreateConnection(_connectionString))
            {
                var command = new SqlCommand(UseQueryBuilder ? QueryBuilder() : QueryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        scAuditModel.SCAuditDeployList.Add(new SCAuditDeploy()
                        {
                            Audit_Ser_Num = reader["SERIAL_NO"].ToString(),
                            Audit_Ser_Num_Returned = reader["SERIAL_NO_RETURNED"].ToString(),
                            Audit_Part_Num = reader["PART_NO"].ToString(),
                            Audit_Part_Num_Returned = reader["PART_NO_RETURNED"].ToString(),
                            Audit_Prod_Desc = reader["PART_DESC"].ToString(),
                            Audit_Prod_Desc_Returned = reader["PART_DESC_RETURNED"].ToString(),
                            //Audit_Prod_Desc_Alt = reader["PART_DESC_ALT"].ToString(),
                            //Audit_Source_Site_Num = reader["SOURCE_SITE_NO"].ToString(),
                            //Audit_Dest_Site_Num = reader["DESTINATION_SITE_NO"].ToString(),
                            Audit_Part_Type = reader["Part_Type"].ToString(),
                            Audit_Part_Code = reader["Part_Code"].ToString(),
                            Audit_Cost_Code = reader["Cost_Code"].ToString(),
                            //Audit_Rem = reader["REMARK"].ToString(),
                            Audit_User = reader["Call_Contact"].ToString(),
                            Audit_Call_Num = reader["Call_Num"].ToString(),
                            Audit_Call_Ref = reader["Call_Ref"].ToString(),
                            //Audit_Move_Date = DateTime.Parse(reader["MOVE_DATE"].ToString()),
                            Audit_Last_Update = DateTime.Parse(reader["UPDATE_DATE"].ToString())
                        });
                    }
                    Reset();
                    reader.Close();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return scAuditModel.SCAuditDeployList;
        }
    }
}

/*
 SELECT TOP 10000000
 NULLIF(LTRIM(RTRIM(SL.[Fsrl_Id_Num])), '') AS [SERIAL_NO],
  NULLIF(LTRIM(RTRIM(SL.[Fsrl_Ret_Id])), '') AS [SERIAL_NO_RETURNED],
   NULLIF(LTRIM(RTRIM(SL.[Fsrl_Part_Num])), '') AS [PART_NO],
    NULLIF(NULLIF(LTRIM(RTRIM(SL.[Fsrl_Ret_Part_Num])), ''), 'NONE') AS [PART_NO_RETURNED],
	 NULLIF(LTRIM(RTRIM(SL.[Fsrl_Part_Desc])), '') AS [PART_DESC],
	  NULLIF(NULLIF(LTRIM(RTRIM(SCP.[Part_Desc])), ''), 'PART USED NONE RETURNED') AS [PART_DESC_RETURNED],
	   NULLIF(LTRIM(RTRIM(SCP.[Part_Type])), '') AS [Part_Type], 
	   NULLIF(LTRIM(RTRIM(SCP.[Part_Alt_Part_Num])), '') AS [Part_Code], 
	   NULLIF(LTRIM(RTRIM(SL.[Fsrl_Call_Num])), '') AS [Call_Num], 
	   NULLIF(LTRIM(RTRIM(SC.[Call_Contact])), '') AS [Call_Contact], 
	   SL.[Fsrl_Last_Update] AS [UPDATE_DATE] 
	   FROM [SCFsrl] AS SL (NOLOCK) 
	   LEFT JOIN [SCPart] AS SCP ON SL.[Fsrl_Ret_Part_Num] = SCP.[Part_Num] 
	   LEFT JOIN [SCCall] AS SC ON SL.[Fsrl_Call_Num] = SC.[Call_Num] 
	   LEFT JOIN [SCCalt] AS SCT ON SC.[Call_Calt_Code] = SCT.[Calt_Code] 
	   WHERE [Fsrl_Last_Update] >= CONVERT(DATETIME, '01/01/2017 00:00:00', 103) 
	   AND [Fsrl_Last_Update] <= CONVERT(DATETIME, '31/12/2019 00:00:00', 103) 
	--   AND ISNULL(Fsrl_Ret_ID, '') <> '' AND [Part_Type] = 'R' AND [PART_DESC] NOT LIKE '%**%'
--AND [Fsrl_Part_Num] LIKE 'BNL%'
AND [Fsrl_Id_Num] LIKE 'BAM%'
 ORDER BY [UPDATE_DATE] desc
 */
