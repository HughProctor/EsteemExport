using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceModel.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Test.BAM_API_Tests
{
    [TestClass]
    public class BAM__GetManufacturerItem_Tests : BaseTestClient
    {
        private string _typePrefix = "BAM_API_";

        [TestMethod]
        public async Task B00_GetManufacturerItems_Test()
        {
            var queryFilter = "?classId=98fbecc7-f76a-dcd6-7b17-62cee34e38de&columnNames=Name%2CModelString%2CManufacturerString%2C";

            var queryResult_Get = _bamClient._client.GetAsync("Search/GetSearchObjectsWithEnumObjectByClassId" + queryFilter).Result;
            Assert.IsTrue(queryResult_Get.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult_Get.StatusCode.ToString());
            Assert.IsNotNull(queryResult_Get, "queryResult is empty");

            var resultSring_Get = queryResult_Get.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring_Get), "Query resultSring is null");
        }
    }
}
