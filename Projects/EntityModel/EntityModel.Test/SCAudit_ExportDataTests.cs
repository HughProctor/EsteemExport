using EntityModel.Service;
using EntityModel.Test.Specifications;
using Infrastructure.FileExport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityModel.Test
{
    [TestClass]
    public class SCAudit_ExportDataTests
    {
        SCAuditService _sCAuditService;
        private string _startDateTimeString = "";
        private string _endDateTimeString = "";
        private string _typePrefix = "SCAudit_";

        [TestInitialize]
        public void Startup()
        {
            _sCAuditService = new SCAuditService();
        }

        private List<SCAudit> GetAll_BaseQuery()
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

        [TestMethod]
        public void ExportToJson()
        {
            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            JSON_FileExport.WriteFile(_typePrefix + "00_NEWITEM_ALL", returnList, returnList.Count);
        }

        [TestMethod]
        public void ExportToJson_TypeR()
        {
            _startDateTimeString = "01/01/2017";
            _endDateTimeString = "30/12/2019";
            _sCAuditService.WhereExpression = "WHERE [Part_Type] = 'R' AND [PART_DESC] NOT LIKE '%**%' ";

            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            JSON_FileExport.WriteFile(_typePrefix + "00_NEWITEM_ALL_TypeR", returnList, returnList.Count);
        }

        [TestMethod]
        public void ExportToJson_TypeR_BNL()
        {
            _startDateTimeString = "01/01/2017";
            _endDateTimeString = "30/12/2019";
            _sCAuditService.WhereExpression = "WHERE [Part_Type] = 'R' AND [PART_DESC] NOT LIKE '%**%' AND [Part_Num] LIKE '%BNL%' ";

            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            JSON_FileExport.WriteFile(_typePrefix + "00_NEWITEM_ALL_TypeR_BNL", returnList, returnList.Count);
        }


        [TestMethod]
        public void Specification_01_BNL_and_AddedPO()
        {
            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var SPEC_NEWITEMESTEEM = (BNL_SPEC.SPEC_PART_IS_BNL.AND(BNL_SPEC.SPEC_NEW_PO));

            var newItemList = returnList.FindAll(x => SPEC_NEWITEMESTEEM.IsSatisfiedBy(x));

            var count = 0;
            foreach(var item in newItemList)
            {
                count++;
                Assert.IsTrue(item.Audit_Part_Num.StartsWith("BNL"), "Item No: " + count.ToString() + " doesn't StartWith BNL :" + item.Audit_Part_Num);
                Assert.IsTrue(item.Audit_Rem.StartsWith("Added PO"), "Item No: " + count.ToString() + " doesn't StartWith Added PO :" + item.Audit_Rem);
            }

            JSON_FileExport.WriteFile(_typePrefix + "01_BNL_AND_ADDEDPO", newItemList, newItemList.Count);
        }

        [TestMethod]
        public void Specification_02_STAGE_E_andor_LTX()
        {
            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var SPEC_NEWITEMESTEEM = (BNL_SPEC.SPEC_PART_IS_BNL.AND(BNL_SPEC.SPEC_NEW_PO));
            var SPEC_STAGE_E_OR_LTX = (BNL_SPEC.SPEC_STAGE_E.OR(BNL_SPEC.SPEC_DESTINATION_LTX));

            var newItemList = returnList.FindAll(x => SPEC_STAGE_E_OR_LTX.IsSatisfiedBy(x));

            var count = 0;
            foreach (var item in newItemList)
            {
                count++;
                Assert.IsTrue(item.Audit_Dest_Site_Num.ToUpper().StartsWith("LTX") || item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-"), 
                    "Item No: " + count.ToString() + " doesn't StartWith either LTX or E- :" + item.Audit_Dest_Site_Num);
            }

            JSON_FileExport.WriteFile(_typePrefix + "02_STAGE_E_ANDOR_LTX", newItemList, newItemList.Count);
        }

        [TestMethod]
        public void Specification_03_STAGE_E_andor_LTX_2()
        {
            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var SPEC = BNL_SPEC.SPEC_STAGE_E_OR_LTX;
            Assert.IsNotNull(SPEC, "SPEC is null");
            var newItemList = returnList.FindAll(x => SPEC.IsSatisfiedBy(x));

            var count = 0;
            foreach (var item in newItemList)
            {
                count++;
                Assert.IsTrue(item.Audit_Dest_Site_Num.ToUpper().StartsWith("LTX") || item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-"),
                    "Item No: " + count.ToString() + " doesn't StartWith either LTX or E- :" + item.Audit_Dest_Site_Num);
            }

            JSON_FileExport.WriteFile(_typePrefix + "03_STAGE_E_ANDOR_LTX_2", newItemList, newItemList.Count);
        }

        [TestMethod]
        public void Specification_04_BNL_AddedPO_STAGE_E_andor_LTX()
        {
            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var SPEC = BNL_SPEC.SPEC_NEWITEM;

            var newItemList = returnList.FindAll(x => SPEC.IsSatisfiedBy(x));

            var count = 0;
            foreach (var item in newItemList)
            {
                count++;
                Assert.IsTrue(item.Audit_Part_Num.StartsWith("BNL"), "Item No: " + count.ToString() + " doesn't StartWith BNL :" + item.Audit_Part_Num);
                Assert.IsTrue(item.Audit_Rem.StartsWith("Added PO"), "Item No: " + count.ToString() + " doesn't StartWith Added PO :" + item.Audit_Rem);
                Assert.IsTrue(item.Audit_Dest_Site_Num.ToUpper().StartsWith("LTX") || item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-"),
                    "Item No: " + count.ToString() + " doesn't StartWith either LTX or E- :" + item.Audit_Dest_Site_Num);
            }

            JSON_FileExport.WriteFile(_typePrefix + "04_BNL_ADDEDPO_STAGE_E_ANDOR_LTX", newItemList, newItemList.Count);
        }

        [TestMethod]
        public void LinqQuery_05_STAGE_E_andor_LTX_3()
        {
            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var newItemList = (from item in returnList
                               where item.Audit_Part_Num.ToUpper().StartsWith("BNL")
                                 && item.Audit_Rem.StartsWith("Added PO")
                                 && (item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-") || item.Audit_Dest_Site_Num.ToUpper() == "LTX")
                               select item).ToList();


            var count = 0;
            foreach (var item in newItemList)
            {
                count++;
                Assert.IsTrue(item.Audit_Dest_Site_Num.ToUpper().StartsWith("LTX") || item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-"),
                    "Item No: " + count.ToString() + " doesn't StartWith either LTX or E- :" + item.Audit_Dest_Site_Num);
            }

            JSON_FileExport.WriteFile(_typePrefix + "05_STAGE_E_ANDOR_LTX", newItemList, newItemList.Count);
        }

        [TestMethod]
        public void LambdaQuery_06_STAGE_E_andor_LTX_4()
        {
            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var newItemList = returnList
                .Where
                (
                    item => item.Audit_Part_Num.ToUpper().StartsWith("BNL")
                        && item.Audit_Rem.StartsWith("Added PO")
                        && (item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-") || item.Audit_Dest_Site_Num.ToUpper() == "LTX")
                ).ToList();


            var count = 0;
            foreach (var item in newItemList)
            {
                count++;
                Assert.IsTrue(item.Audit_Dest_Site_Num.ToUpper().StartsWith("LTX") || item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-"),
                    "Item No: " + count.ToString() + " doesn't StartWith either LTX or E- :" + item.Audit_Dest_Site_Num);
            }

            JSON_FileExport.WriteFile(_typePrefix + "06_STAGE_E_ANDOR_LTX", newItemList, newItemList.Count);
        }

        [TestMethod]
        public void LinqQuery_07_PartDescriptionList_Only()
        {
            _startDateTimeString = "01/01/2016";
            _endDateTimeString = "30/12/2019";

            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var newItemList = (from item in returnList
                               where item.Audit_Part_Num.ToUpper().StartsWith("BNL")
                                 && item.Audit_Rem.StartsWith("Added PO")
                                 && (item.Audit_Dest_Site_Num.ToUpper().StartsWith("E-") || item.Audit_Dest_Site_Num.ToUpper() == "LTX")
                               orderby item.Audit_Prod_Desc ascending
                               select new { item.Audit_Prod_Desc }).ToList();

            JSON_FileExport.WriteFile(_typePrefix + "07_PROD_DESCRIPTION_ONLY", newItemList, newItemList.Count);
        }

        [TestMethod]
        public void LinqQuery_08_PartDescriptionList_Only_ALL()
        {
            _startDateTimeString = "01/01/2016";
            _endDateTimeString = "30/12/2019";

            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var newItemList = returnList.Where(x => !x.Audit_Prod_Desc.StartsWith("***"))
                .Select(x => new { x.Audit_Prod_Desc }).OrderBy(x => x.Audit_Prod_Desc)
                    .Distinct()
                    .ToList();

            JSON_FileExport.WriteFile(_typePrefix + "08_PROD_DESCRIPTION_ONLY_ALL", newItemList, newItemList.Count);
        }

    }
}
