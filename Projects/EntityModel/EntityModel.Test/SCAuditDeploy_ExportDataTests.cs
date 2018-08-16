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
    public class SCAuditDeploy_ExportDataTests
    {
        SCDeployService _sCDeployService;
        private string _startDateTimeString = "";
        private string _endDateTimeString = "";
        private string _typePrefix = "SCAuditDeploy_";

        [TestInitialize]
        public void Startup()
        {
            _sCDeployService = new SCDeployService();
        }

        private List<SCAuditDeploy> GetAll_BaseQuery()
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

        [TestMethod]
        public void ExportToJson()
        {
            _sCDeployService.WhereExpression = "WHERE ISNULL(Fsrl_Ret_ID, '') <> '' ";

            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            JSON_FileExport.WriteFile(_typePrefix + "00_NEWITEM_ALL", returnList, returnList.Count);
        }

        [TestMethod]
        public void ExportToJson_TypeR()
        {
            _startDateTimeString = "01/01/2017";
            _endDateTimeString = "30/12/2019";
            _sCDeployService.WhereExpression = "WHERE ISNULL(Fsrl_Ret_ID, '') <> '' AND [Part_Type] = 'R' AND [PART_DESC] NOT LIKE '%**%' ";

            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            JSON_FileExport.WriteFile(_typePrefix + "00_NEWITEM_ALL_TypeR", returnList, returnList.Count);
        }

        [TestMethod]
        public void LambdaQuery_04_RETURNED_TO_BNL()
        {
            _startDateTimeString = "01/01/2017";
            _endDateTimeString = "30/12/2019";
   //         _sCDeployService.WhereExpression = "WHERE ISNULL(Fsrl_Ret_ID, '') <> '' AND [Part_Type] = 'R' AND [PART_DESC] NOT LIKE '%**%' ";

            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            var newItemList = returnList
                .Where
                (
                    item => item.Audit_Ser_Num.StartsWith("BAM")
                        //&& item.Audit_Dest_Site_Num.ToUpper().StartsWith("BNLTEST")
                       // && (item.Audit_Ser_Num.StartsWith("BAM-")
                      //  && item.Audit_Ser_Num.Contains("/"))
                ).ToList();

            var count = 0;
            Assert.IsTrue(newItemList.Any(), "Query didn't return any results");

            foreach (var item in newItemList)
            {
                count++;
                Assert.IsTrue(item.Audit_Ser_Num.StartsWith("BAM"),
                    "Item No: " + count.ToString() + " doesn't StartWith either BAM :" + item.Audit_Ser_Num);
            }

            JSON_FileExport.WriteFile(_typePrefix + "06_STAGE_E_ANDOR_LTX", newItemList, newItemList.Count);
        }

    }
}
