using BusinessModel.Models;
using BusinessModel.Services.Abstract;
using EntityModel;
using BusinessModel.Mappers;
using EntityModel.Repository;
using EntityModel.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessModel.Services
{
    public class EST_Service : IEST_Service
    {
        #region Fields
        ISCAuditRepository _sCAuditService;
        ISCDeployRepository _sCDeployService;
        IEST_DataCleanseService _dataCleanseService;
        private string _startDateTimeString = "";
        private string _endDateTimeString = "";
        DateTime _startDateTime;
        DateTime _endDateTime;
        public IQueryBuilder _queryBuilder { get; set; }
        #endregion Fields

        public EST_Service() : this(new SCAuditRepository(), new SCDeployRepository(), new EST_DataCleanseService())
        {
        }

        public EST_Service(ISCAuditRepository auditService, ISCDeployRepository deployService, IEST_DataCleanseService dataCleanseService)
        {
            Map.Init();
            _sCAuditService = auditService;
            _sCDeployService = deployService;
            _dataCleanseService = dataCleanseService;
        }

        public EST_DataExportModel GetExportData(IQueryBuilder queryBuilder)
        {
            var returnValue = new EST_DataExportModel();

            _queryBuilder = SetDefaultValues(queryBuilder);

            string whereExpression = "WHERE [Part_Type] = 'R' AND [PART_DESC] NOT LIKE '%**%' ";
            _queryBuilder.WhereExpression = whereExpression;

            // Build the Query
            _sCAuditService.QueryBuilderObject = _queryBuilder;
            _sCDeployService.QueryBuilderObject = _queryBuilder;

            // Get the data
            var returnAuditList = GetData_Audit_BaseQuery();
            var returnDeployList = GetData_Deploy_BaseQuery();

            // Cleanse, Reduce and subset data
            returnValue.NewItemList = Get_New_Item(returnAuditList);
            returnValue.LocationChangeList = Get_Location_Change(returnAuditList);
            returnValue.AssetTagChangeList = Get_Asset_Tag_Change(returnAuditList);
            returnValue.DeployedToBAMUserList = Get_Deployed_to_BAM_User(returnDeployList);
            returnValue.ReturnedFromBAMUserList = Get_Returned_from_BAM_User(returnDeployList);


            returnValue.ReturnedFromBAMList = Get_ReturnedFromBAM(returnAuditList);
            returnValue.RetiredAssetList = Get_RetiredAsset(returnAuditList);
            returnValue.DisplosedAssetList = Get_DisplosedAsset(returnAuditList);
            returnValue.SwappedAssetList = Get_SwappedAsset(returnDeployList);

            return returnValue;
        }

        #region private Methods
        private IQueryBuilder SetDefaultValues(IQueryBuilder queryBuilder)
        {
            if (queryBuilder != null) return queryBuilder;
            var _queryBuilder = queryBuilder ?? new QueryBuilder();
            if (_queryBuilder.StartDate.Equals(new DateTime())) _queryBuilder.StartDateString = "01/01/2017";
            if (_queryBuilder.EndDate.Equals(new DateTime())) _queryBuilder.EndDateString = "30/12/2019";
            _queryBuilder.PageCount = 10000000;
            return _queryBuilder;
        }

        internal List<SCAudit> GetData_Audit_BaseQuery()
        {
            var returnList = new List<SCAudit>();
            returnList = _sCAuditService.GetAll();
            return returnList;
        }

        internal List<SCAuditDeploy> GetData_Deploy_BaseQuery()
        {
            var returnList = new List<SCAuditDeploy>();
            returnList = _sCDeployService.GetAll();
            return returnList;
        }

        internal List<SCAuditBsm> Get_New_Item(List<SCAudit> returnList)
        {
            var newItemList = _dataCleanseService.Process_SCAuditList(returnList
                .Where
                (
                    item => item.Audit_Part_Num.ToUpper().StartsWith("BNL")
                        && item.Audit_Rem.StartsWith("Added PO")
                        && (item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-") || item.Audit_Dest_Site_Num.ToUpper() == "LTX")
                ).ToList().Distinct().ToList());
            return newItemList;
        }

        //internal List<SCAuditBsm> Get_New_Item_Configured(List<SCAudit> returnList)
        //{
        //    var newItemList = _dataCleanseService.Process_SCAuditList(returnList
        //        .Where
        //        (
        //            item => item.Audit_Part_Num.ToUpper().StartsWith("BNL")
        //                && item.Audit_Rem.StartsWith("Added PO")
        //                && item.
        //                && (item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-") || item.Audit_Dest_Site_Num.ToUpper() == "LTX")
        //        ).ToList().Distinct().ToList());
        //    return newItemList;
        //}

        internal List<SCAuditBsm> Get_Location_Change(List<SCAudit> returnList)
        {
            var newItemList = _dataCleanseService.Process_SCAuditList(returnList
                .Where
                (
                    item => item.Audit_Part_Num.ToUpper().StartsWith("BNL")
                        && (item.Audit_Dest_Site_Num.ToUpper() == "LTX"
                         || item.Audit_Dest_Site_Num.ToUpper() == "LTXR"
                         || item.Audit_Dest_Site_Num.ToUpper() == "LTX BAD"
                         || item.Audit_Dest_Site_Num.ToUpper() == "BNLSCRAP"
                        )
                ).ToList().Distinct().ToList());
            return newItemList;
        }

        internal List<SCAuditBsm> Get_Asset_Tag_Change(List<SCAudit> returnList)
        {
            var newItemList = _dataCleanseService.Process_SCAuditList(returnList
                .Where
                (
                    item => item.Audit_Part_Num.ToUpper().StartsWith("BNL")
                        && item.Audit_Rem.ToUpper().StartsWith("ID CHANGED FROM")
                        && (item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-") || item.Audit_Dest_Site_Num.ToUpper() == "LTX")
                        && (item.Audit_Ser_Num.StartsWith("BAM-") && item.Audit_Ser_Num.Contains("/"))
                ).ToList().Distinct().ToList());
            return newItemList;
        }

        internal List<SCAuditDeployBsm> Get_Deployed_to_BAM_User(List<SCAuditDeploy> returnList)
        {
            var newItemList = _dataCleanseService.Process_SCAuditDeployList(returnList
                .Where
                (
                    item => item.Audit_Part_Num.StartsWith("BNL")
                        && string.IsNullOrEmpty(item.Audit_Part_Num_Returned) == true
                ).ToList().Distinct().ToList());
            return newItemList;
        }

        /// <summary>
        /// Depreciated
        /// </summary>
        /// <param name="returnList"></param>
        /// <returns></returns>
        internal List<SCAuditDeployBsm> Get_Returned_from_BAM_User(List<SCAuditDeploy> returnList)
        {
            var newItemList = _dataCleanseService.Process_SCAuditDeployList(returnList
                .Where
                (
                    item => !string.IsNullOrEmpty(item.Audit_Ser_Num_Returned)
                        && (item.Audit_Dest_Site_Num.StartsWith("BNLTEST") || item.Audit_Dest_Site_Num.StartsWith("LTXR"))
                        && item.Audit_Ser_Num.StartsWith("BAM")
                ).ToList().Distinct().ToList());
            return newItemList;
        }



        /*---------------------*/
        private List<SCAuditBsm> Get_ReturnedFromBAM(List<SCAudit> returnList)
        {
            var newItemList = _dataCleanseService.Process_SCAuditList(returnList
                .Where
                (
                    item => item.Audit_Part_Num.StartsWith("BNL") && 
                        (item.Audit_Source_Site_Num.StartsWith("BNLTEST") ||
                            item.Audit_Source_Site_Num.StartsWith("LTXR"))
                ).ToList().Distinct().ToList());
            return newItemList;
        }

        private List<SCAuditBsm> Get_RetiredAsset(List<SCAudit> returnList)
        {
            var newItemList = _dataCleanseService.Process_SCAuditList(returnList
                .Where
                (
                    item => item.Audit_Part_Num.StartsWith("BNL") &&
                        item.Audit_Dest_Site_Num.StartsWith("BNLSCRAP")
                ).ToList().Distinct().ToList());
            return newItemList;
        }

        private List<SCAuditBsm> Get_DisplosedAsset(List<SCAudit> returnList)
        {
            var newItemList = _dataCleanseService.Process_SCAuditList(returnList
                .Where
                (
                    item => item.Audit_Part_Num.StartsWith("BNL") &&
                        item.Audit_Source_Site_Num.StartsWith("BNLSCRAP") &&
                        string.IsNullOrEmpty(item.Audit_Dest_Site_Num) == true
                ).ToList().Distinct().ToList());
            return newItemList;
        }

        private List<SCAuditDeployBsm> Get_SwappedAsset(List<SCAuditDeploy> returnList)
        {
            var newItemList = _dataCleanseService.Process_SCAuditDeployList(returnList
                .Where
                (
                    item => item.Audit_Part_Num.StartsWith("BNL")
                        && string.IsNullOrEmpty(item.Audit_Part_Num_Returned) == false
                ).ToList().Distinct().ToList());
            return newItemList;
        }

        #endregion
    }
}
