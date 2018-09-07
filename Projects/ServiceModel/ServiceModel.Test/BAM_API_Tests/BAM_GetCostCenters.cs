using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ServiceModel.Services;
using ServiceModel.Models.BAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceModel.Services.Abstract;

namespace ServiceModel.Test.BAM_API_Tests
{
    [TestClass]
    public class BAM_GetCostCenters : BaseTestClient
    {
        [TestMethod]
        public async Task BAM_CostCenter_Get_List()
        {
            var filterRecord = "BAA.3007";

            IBAM_CostCenterService bamService = new BAM_CostCenterService();
            var returnList = bamService.GetCostCenterList();

            Assert.IsNotNull(returnList, "BAM User list is null");
            Assert.IsTrue(returnList.Any(), "BAM User list doesn't contain any records");
            Assert.IsTrue(returnList.Any(x => x.DisplayName == filterRecord), "Britton, David is null");
        }

    }
}
