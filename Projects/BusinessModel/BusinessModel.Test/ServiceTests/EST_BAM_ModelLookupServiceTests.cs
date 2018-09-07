using BusinessModel.Models;
using BusinessModel.Services;
using BusinessModel.Services.Abstract;
using EntityModel.Repository;
using EntityModel.Repository.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BusinessModel.Test.ServiceTests
{
    [TestClass]
    public class EST_BAM_ModelLookupServiceTests
    {
        IEST_BAM_ModelLookupService _bAM_ModelLookupService;

        [TestMethod]
        public void InsertOrUpdate_BAM_Manufacturers()
        {
            _bAM_ModelLookupService = new EST_BAM_ModelLookupService();

            var returnList = _bAM_ModelLookupService.GetBAM_Manufacturers();
            Assert.IsNotNull(returnList, "ReturnList is null");
            Assert.IsTrue(returnList.Any(), "ReturnList doesn't contain any items");
        }


        [TestMethod]
        public void Get_EST_Manufacturers()
        {
            IQueryBuilder queryBuilder = new QueryBuilder();
            queryBuilder.StartDateString = "01/01/2017";
            queryBuilder.EndDateString = "01/01/2019";
            queryBuilder.PageCount = 100000000;

            _bAM_ModelLookupService = new EST_BAM_ModelLookupService();

            var returnList = _bAM_ModelLookupService.GetEST_Manufacturers(queryBuilder);
            Assert.IsNotNull(returnList, "ReturnList is null");
            Assert.IsTrue(returnList.Any(), "ReturnList doesn't contain any items");
        }

        [TestMethod]
        public void Get_All_Manufacturers()
        {
            IQueryBuilder queryBuilder = new QueryBuilder();
            queryBuilder.StartDateString = "01/01/2017";
            queryBuilder.EndDateString = "01/01/2019";
            queryBuilder.PageCount = 100000000;

            _bAM_ModelLookupService = new EST_BAM_ModelLookupService();

            var estManufacturers = _bAM_ModelLookupService.GetEST_Manufacturers(queryBuilder);
            Assert.IsNotNull(estManufacturers, "ReturnList is null");
            Assert.IsTrue(estManufacturers.Any(), "ReturnList doesn't contain any items");

            var bamReportingManufacturerList = _bAM_ModelLookupService.GetBAM_ManufacturerList();
            var resultList = new List<EST_BAM_ModelLookupBsm>();
            var resultListTemp = new List<TempModel>();
            var resultListNoMatch = new List<TempModel>();
            foreach (var model in estManufacturers)
            {
                EST_BAM_ModelLookupBsm modelItem = null;
                var modelName = model.ModelName;
                foreach (var item in bamReportingManufacturerList)
                {
                    if (modelItem != null) break;
                    if (item.BAM_Name == modelName) { modelItem = item; break; }
                    var itemSplit = item.BAM_Name.ToUpper().Split(' ');
                    var count = itemSplit.Length;
                    var weight = 0;
                    for (var i = 0; i < itemSplit.Length; i++)
                    {
                        if (modelName.Contains(itemSplit[i])) weight++;
                    };
                    if (weight == 0) continue;
                    if (weight == count) { modelItem = item; break; }
                    if ((weight + 1) == count) { modelItem = item; break; }
                    if ((weight + 2) == count && weight >= 3) { modelItem = item; break; }
                }
                if (modelItem != null) { resultList.Add(modelItem); resultListTemp.Add(model); }
                else resultListNoMatch.Add(model);
            }
            var errorStr = "\r\n";
            resultListNoMatch.ForEach(x => { errorStr += x.ModelName + "\r\n"; });
            Assert.IsTrue(false, "Match count: " + resultList.Count + " No Match count: " + resultListNoMatch.Count + errorStr);
        }




    }
}
