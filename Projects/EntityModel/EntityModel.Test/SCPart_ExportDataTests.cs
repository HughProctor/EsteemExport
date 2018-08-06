using EntityModel.BusinessLogic;
using EntityModel.Service;
using Infrastructure.FileExport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityModel.Test
{
    [TestClass]
    public class SCPart_ExportDataTests
    {
        SCPartService _sCPartService;
        private string _startDateTimeString = "";
        private string _endDateTimeString = "";
        private string _typePrefix = "SCPart_";

        [TestInitialize]
        public void Startup()
        {
            _sCPartService = new SCPartService();
        }

        private List<SCPart> GetAll_BaseQuery()
        {
            var returnList = new List<SCPart>();
            _startDateTimeString = string.IsNullOrEmpty(_startDateTimeString) ? "01/11/2017" : _startDateTimeString;
            _endDateTimeString = string.IsNullOrEmpty(_endDateTimeString) ? "30/11/2017" : _endDateTimeString;

            DateTime.TryParse(_startDateTimeString, out var startDateTime);
            DateTime.TryParse(_endDateTimeString, out var endDateTime);
            _sCPartService.StartDate = startDateTime;
            _sCPartService.EndDate = endDateTime;

            returnList = _sCPartService.GetAll();
            return returnList;
        }

        [TestMethod]
        public void ExportToJson()
        {
            _sCPartService.PageCount = 10000;

            var returnList = GetAll_BaseQuery();
            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            JSON_FileExport.WriteFile(_typePrefix + "00_NEWITEM_ALL", returnList, returnList.Count);
        }

        [TestMethod]
        public void ExportToJson_All()
        {
            _startDateTimeString = "01/01/2010";
            _endDateTimeString = "30/12/2019";
            _sCPartService.PageCount = 10000000;
            var returnList = GetAll_BaseQuery();

            returnList = returnList
                .Where(x => !x.Asset_Desc.Contains("**"))
                .OrderBy(x => x.Asset_Desc).ToList();

            Assert.IsTrue(returnList.Any(), "Query didn't return any results");

            JSON_FileExport.WriteFile(_typePrefix + "00_NEWITEM_ALL", returnList, returnList.Count);
        }

        [TestMethod]
        public void ExportToJson_Split_HP()
        {
            _startDateTimeString = "01/01/2010";
            _endDateTimeString = "30/12/2019";
            _sCPartService.PageCount = 10000000;

            _sCPartService.WhereExpression = "WHERE [Part_Type] = 'R' AND [PART_DESC] NOT LIKE '%**%' ";

            var returnList = GetAll_BaseQuery();

            returnList = returnList
                .Where(x => !x.Asset_Desc.Contains("**"))
      //          .Where(x => x.Asset_Desc_Code_Split.Contains("HP"))
              //  .Where(x => x.Asset_Desc_Code_Suf.Contains("HEW") || x.Asset_Desc_Code_Suf.Contains("HPX"))
                .OrderBy(x => x.Asset_Desc).ToList();

            Assert.IsTrue(returnList.Any(), "Query didn't return any results");
            JSON_FileExport.WriteFile(_typePrefix + "00_NEWITEM_ALL", returnList, returnList.Count);

            var returnList_Manu = returnList.Where(x => !string.IsNullOrEmpty(x.Manufacturer)).ToList();
            JSON_FileExport.WriteFile(_typePrefix + "00_NEWITEM_ALL_MANU", returnList_Manu, returnList_Manu.Count);

            var returnList_NoManu = returnList.Where(x => string.IsNullOrEmpty(x.Manufacturer)).ToList();
            JSON_FileExport.WriteFile(_typePrefix + "00_NEWITEM_ALL_NO_MANU", returnList_NoManu, returnList_NoManu.Count);
        }

        [TestMethod]
        public void Split_Sort_Description_01()
        {
            var list = new List<string>()
            {
                "PROBOOK",
                "455",
                "G1",
                "HP",
            };

            list = list.OrderBy(x => x, new ManufacturerComparer()).ThenByDescending(x => x, new SemiNumericComparer()).ToList();

            Assert.IsTrue(list[0] == "HP", string.Join(" ", list.ToArray()));
            Assert.IsTrue(list[1] == "PROBOOK", string.Join(" ", list.ToArray()));
            Assert.IsTrue(list[2] == "G1", string.Join(" ", list.ToArray()));
            Assert.IsTrue(list[3] == "455", string.Join(" ", list.ToArray()));
        }

        [TestMethod]
        public void Split_Sort_Description_02()
        {
            var list = new List<string>()
            {
                "LATITUDE",
                "LAPTOP",
                "E7480",
                "DELL"
            };

            //list = list.OrderBy(x => x, new SemiNumericComparer()).ThenBy(x => x, new ManufacturerComparer()).ToList();
            list = list.OrderBy(x => x, new ManufacturerComparer()).ThenByDescending(x => x, new SemiNumericComparer()).ToList();

            Assert.IsTrue(list[0] == "DELL", string.Join(" ", list.ToArray()));
            Assert.IsTrue(list[1] == "LATITUDE", string.Join(" ", list.ToArray()));
            Assert.IsTrue(list[2] == "LAPTOP", string.Join(" ", list.ToArray()));
            Assert.IsTrue(list[3] == "E7480", string.Join(" ", list.ToArray()));
        }

        [TestMethod]
        public void ExportToJson_Split_Sort_Description()
        {
            _startDateTimeString = "01/01/2017";
            _endDateTimeString = "30/12/2019";
            _sCPartService.PageCount = 10000;

            _sCPartService.WhereExpression = "WHERE [Part_Type] = 'R' AND [PART_DESC] NOT LIKE '%**%' ";


            var returnList = GetAll_BaseQuery();

            JSON_FileExport.WriteFile(_typePrefix + "00_Ordered_DescCode_Original", returnList, returnList.Count);

            returnList.ForEach(item => item.Asset_Desc_Code_Split = item.Asset_Desc.ToUpper().Split(' ').ToList()
                    .OrderBy(x => x, new ManufacturerComparer()).ToList());

            //returnList.ForEach(item =>  {
            //    if (TestData.GetManufacturers().Contains()
            //        item.Asset_Desc_Code_New =
            //});

            //.ThenByDescending(x => x, new SemiNumericComparer())
            JSON_FileExport.WriteFile(_typePrefix + "00_Ordered_DescCode", returnList, returnList.Count);

            var returnList_Manu = returnList.Where(x => !string.IsNullOrEmpty(x.Manufacturer)).ToList();
            JSON_FileExport.WriteFile(_typePrefix + "00_Ordered_DescCode_Manufacturer", returnList_Manu, returnList_Manu.Count);

            var returnList_NoManu = returnList.Where(x => string.IsNullOrEmpty(x.Manufacturer)).OrderBy(x => x.Asset_Desc).ToList();
            JSON_FileExport.WriteFile(_typePrefix + "00_Ordered_DescCode_No_Manufacturer", returnList_NoManu, returnList_NoManu.Count);
        }
    }
}
