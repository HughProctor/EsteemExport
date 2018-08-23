using AutoMapper;
using BusinessModel.Mappers;
using BusinessModel.Services;
using BusinessModel.Services.Abstract;
using EntityModel;
using EntityModel.Repository;
using Infrastructure.FileExport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Test
{
    /// <summary>
    /// Unit test for the final Outputs to BAM Nuttall
    /// </summary>
    [TestClass]
    public class BAM_Report_QueryDataSet_Tests
    {
        SCAuditRepository _sCAuditService;
        SCDeployRepository _sCDeployService;
        IEST_DataCleanseService _DataCleanseService;

        private string _typePrefix = "BAMReport_";
        private string _startDateTimeString = "";
        private string _endDateTimeString = "";
        DateTime _startDateTime;
        DateTime _endDateTime;

        [TestInitialize]
        public void Startup()
        {
            Map.Init();
            _sCAuditService = new SCAuditRepository();
            _sCDeployService = new SCDeployRepository();
            _DataCleanseService = new EST_DataCleanseService();
        }

        private List<SCAudit> GetAll_Audit_BaseQuery()
        {
            var returnList = new List<SCAudit>();
            _startDateTimeString = string.IsNullOrEmpty(_startDateTimeString) ? "01/11/2017" : _startDateTimeString;
            _endDateTimeString = string.IsNullOrEmpty(_endDateTimeString) ? "30/11/2017" : _endDateTimeString;

            DateTime.TryParse(_startDateTimeString, out var startDateTime);
            DateTime.TryParse(_endDateTimeString, out var endDateTime);
            _sCAuditService.StartDate = startDateTime;
            _sCAuditService.EndDate = endDateTime;
            _sCAuditService.PageCount = 10000000;

            returnList = _sCAuditService.GetAll();
            return returnList;
        }

        private List<SCAuditDeploy> GetAll_Deploy_BaseQuery()
        {
            var returnList = new List<SCAuditDeploy>();
            _startDateTimeString = string.IsNullOrEmpty(_startDateTimeString) ? "01/11/2017" : _startDateTimeString;
            _endDateTimeString = string.IsNullOrEmpty(_endDateTimeString) ? "30/11/2017" : _endDateTimeString;

            DateTime.TryParse(_startDateTimeString, out var startDateTime);
            DateTime.TryParse(_endDateTimeString, out var endDateTime);
            _sCDeployService.StartDate = startDateTime;
            _sCDeployService.EndDate = endDateTime;
            _sCDeployService.PageCount = 10000000;

            returnList = _sCDeployService.GetAll();
            return returnList;
        }


        /// <summary>
        /// Esteem functions effectively like a Storage Warehouse for BAM Nuttall
        /// Weekly / Monthly Stock Control is performed on various items regularly purchased by BAM
        /// If Stock levels fall below a certain number (40), then Esteem contacts BAM to make them aware
        /// BAM purchases more stock (from HP for example)
        /// Product arrives at Esteem
        /// This is recorded by Esteem as a New Item in Esteem
        /// a.k.a WHERE [Audit_Rem] like '%Added PO%'
        /// 
        /// Data is stored in the Audit table and links to Parts table
        /// 
        /// At this Point the Item should not exist in BAM system
        /// </summary>
        [TestMethod]
        public void S01_New_Item_In_Esteem()
        {
            _startDateTimeString = "01/01/2017";
            _endDateTimeString = "30/12/2019";
            _sCAuditService.WhereExpression = "WHERE [Part_Type] = 'R' AND [PART_DESC] NOT LIKE '%**%' ";

            var returnList = GetAll_Audit_BaseQuery();

            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var newItemList = _DataCleanseService.Process_SCAuditList(
                returnList
                .Where
                (
                    item => item.Audit_Part_Num.ToUpper().StartsWith("BNL")
                        && item.Audit_Rem.StartsWith("Added PO")
                        && (item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-") || item.Audit_Dest_Site_Num.ToUpper() == "LTX")
                ).ToList());

            Assert.IsTrue(newItemList.Any(), "Filter Query didn't return any results");

            var count = 0;
            foreach (var item in newItemList)
            {
                count++;
                Assert.IsTrue(item.Audit_Dest_Site_Num.ToUpper().StartsWith("LTX") || item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-"),
                    "Item No: " + count.ToString() + " doesn't StartWith either LTX or E- :" + item.Audit_Dest_Site_Num);
            }

            JSON_FileExport.WriteFile(_typePrefix + "S01_New_Item_In_Esteem", newItemList, newItemList.Count, "BAMReport");
        }

        /// <summary>
        /// The Item now exists in both the Esteem and BAM systems
        /// Whether the item was a new item or has been returned from 'the field'
        /// 
        /// The item might be: [Audit_Rem] 
        ///     Returned - like 'Returned to stock'
        ///     Repaired - like 'Stock Made Good'
        ///     Scrapped - like 'Stock Made Bad'
        ///     In Transit
        ///     Shipping
        ///     
        /// Data is stored in the Audit table and links to Parts table
        /// </summary>
        [TestMethod]
        public void S02_Esteem_Location_Change()
        {
            _startDateTimeString = "01/01/2017";
            _endDateTimeString = "30/12/2019";
            _sCAuditService.WhereExpression = "WHERE [Part_Type] = 'R' AND [PART_DESC] NOT LIKE '%**%' ";

            var returnList = GetAll_Audit_BaseQuery();

            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var newItemList = _DataCleanseService.Process_SCAuditList(returnList
                .Where
                (
                    item => item.Audit_Part_Num.ToUpper().StartsWith("BNL")
                        && (item.Audit_Dest_Site_Num.ToUpper() == "LTX"
                         || item.Audit_Dest_Site_Num.ToUpper() == "LTXR"
                         || item.Audit_Dest_Site_Num.ToUpper() == "LTX BAD"
                         || item.Audit_Dest_Site_Num.ToUpper() == "BNLSCRAP"
                        )
                ).ToList());

            Assert.IsTrue(newItemList.Any(), "Filter Query didn't return any results");

            var count = 0;
            foreach (var item in newItemList)
            {
                count++;
                Assert.IsTrue((item.Audit_Dest_Site_Num.ToUpper() == "LTX"
                         || item.Audit_Dest_Site_Num.ToUpper() == "LTXR"
                         || item.Audit_Dest_Site_Num.ToUpper() == "LTX BAD"
                         || item.Audit_Dest_Site_Num.ToUpper() == "BNLSCRAP"
                        ),
                    "Item No: " + count.ToString() + " doesn't StartWith either LTX or E- :" + item.Audit_Dest_Site_Num);
            }

            JSON_FileExport.WriteFile(_typePrefix + "S02_Esteem_Location_Change", newItemList, newItemList.Count, "BAMReport");
        }

        /// <summary>
        /// The Item is back or still at Esteem
        /// The item might be: [Audit_Rem] 
        ///     Tagged - Asset Tag Changed a.k.a 'ID Changed from' / 'ID Changed to' 
        ///     Changed - Product Number Change (They add suffix '-New' or prefix 'BNL') 'PN Changed to' / 'PN Changed from'
        ///     
        /// We need to Record for BAM that the Asset Name has changed
        /// </summary>
        [TestMethod]
        public void S03_Esteem_Asset_Tag_Change()
        {
            _startDateTimeString = "01/01/2017";
            _endDateTimeString = "30/12/2019";
            _sCAuditService.WhereExpression = "WHERE [Part_Type] = 'R' AND [PART_DESC] NOT LIKE '%**%' ";

            var returnList = GetAll_Audit_BaseQuery();

            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var newItemList = _DataCleanseService.Process_SCAuditList(returnList
                .Where
                (
                    item => (item.Audit_Part_Num.ToUpper().StartsWith("BNL")
                        && item.Audit_Rem.ToUpper().StartsWith("ID CHANGED FROM")
                        && (item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-") || item.Audit_Dest_Site_Num.ToUpper() == "LTX")
                        && (item.Audit_Ser_Num.StartsWith("BAM-") && item.Audit_Ser_Num.Contains("/"))
                )).ToList());

            Assert.IsTrue(newItemList.Any(), "Filter Query didn't return any results");

            var count = 0;
            foreach (var item in newItemList)
            {
                count++;
                Assert.IsTrue((item.Audit_Part_Num.ToUpper().StartsWith("BNL")
                        && item.Audit_Rem.ToUpper().StartsWith("ID CHANGED FROM")
                        && (item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-") || item.Audit_Dest_Site_Num.ToUpper() == "LTX")
                        && (item.Audit_Ser_Num.StartsWith("BAM-") && item.Audit_Ser_Num.Contains("/"))
                ),
                    "Item No: " + count.ToString() + " doesn't StartWith either LTX or E- :" + item.Audit_Dest_Site_Num);
            }

            JSON_FileExport.WriteFile(_typePrefix + "S03_Esteem_Asset_Tag_Change", newItemList, newItemList.Count, "BAMReport");
        }

        /// <summary>
        /// A Service Call (SCCall) is logged by someone in BAM requesting for an item in Esteem Stock to be sent and deployed 
        /// to someone in BAM 
        /// 
        /// The requester is someone who is already registered as a User in BAM, but
        /// the end User may be / and may never be a BAM user, it might be a temporary worker, or someone is has yet to start
        /// their employment with BAM (the request is made in preparation of their arrival)
        /// 
        /// The item gets requested,
        ///     then Deployed
        ///     then it's In Transit
        ///     
        /// To get the Asset details - 
        ///     Get the Serial Deployed Number from FSRL_ID_NUM
        ///     Split out the value after backslash /
        ///     Look up from SCAuditService the Item details
        /// </summary>
        [TestMethod]
        public void S04_Deployed_to_BAM_User()
        {
            _startDateTimeString = "01/01/2017";
            _endDateTimeString = "30/12/2019";
            _sCDeployService.WhereExpression = "WHERE [Part_Type] = 'R' AND [PART_DESC] NOT LIKE '%**%' ";

            var returnList = GetAll_Deploy_BaseQuery();

            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var newItemList = _DataCleanseService.Process_SCAuditDeployList(returnList
                .Where
                (
                    item => item.Audit_Ser_Num.StartsWith("BAM")
                ).ToList());

            Assert.IsTrue(newItemList.Any(), "Filter Query didn't return any results");

            var count = 0;
            foreach (var item in newItemList)
            {
                count++;
                Assert.IsTrue(item.Audit_Ser_Num.StartsWith("BAM"),
                    "Item No: " + count.ToString() + " doesn't StartWith either BAM :" + item.Audit_Ser_Num);
            }

            JSON_FileExport.WriteFile(_typePrefix + "S04_Deployed_to_BAM_User", newItemList, newItemList.Count, "BAMReport");
        }

        /// <summary>
        /// A Service Call (SCCall) is logged by someone in BAM requesting for an item to be picked up and 
        /// returned to Esteem Stock
        /// 
        /// The item maybe broken in need of repair, or the user has finished using the item
        /// 
        /// The item gets requested,
        ///     then it's In Transit
        ///     then it's Serial Returned value is set
        ///     
        /// Somtimes the Part returned Serial Number doesn't match
        ///     in this case the Part Serial Number Returned value - 
        ///     Split out the value after backslash
        ///     Look up from SCAuditService
        /// </summary>
        [TestMethod]
        public void S05_Returned_from_BAM_User()
        {
            _startDateTimeString = "01/01/2017";
            _endDateTimeString = "30/12/2019";
            _sCDeployService.WhereExpression = "WHERE [Part_Type] = 'R' AND [PART_DESC] NOT LIKE '%**%' ";

            var returnList = GetAll_Deploy_BaseQuery();

            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var newItemList = _DataCleanseService.Process_SCAuditDeployList(returnList
                .Where
                (
                    item => !string.IsNullOrEmpty(item.Audit_Ser_Num_Returned)
                        && item.Audit_Ser_Num.StartsWith("BAM")
                ).ToList());

            Assert.IsTrue(newItemList.Any(), "Filter Query didn't return any results");

            var count = 0;
            foreach (var item in newItemList)
            {
                count++;
                Assert.IsTrue(item.Audit_Ser_Num.StartsWith("BAM"),
                    "Item No: " + count.ToString() + " doesn't StartWith either BAM :" + item.Audit_Ser_Num);
            }

            JSON_FileExport.WriteFile(_typePrefix + "S05_Returned_from_BAM_User", newItemList, newItemList.Count, "BAMReport");
        }
    }
}
