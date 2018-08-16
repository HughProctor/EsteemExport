using Infrastructure.FileExport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;
using ServiceModel.Services;
//using ServiceModel.Models.BAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Test.BAM_API_Tests
{
    [TestClass]
    public class BAM__GetHardwareItem_Tests : BaseTestClient
    {
        private string _typePrefix = "BAM_API_";

        [TestMethod]
        public async Task B00_GetWorkItem_Test()
        {
            var expression = "{\"Id\": \"2d460edd-d5db-bc8c-5be7-45b050cba652\",\"Criteria\": {" +
                "\"Base\": { \"Expression\": { \"SimpleExpression\": { \"ValueExpressionLeft\": { " +
                "\"Property\": \"$Context/Property[Type='a604b942-4c7b-2fb2-28dc-61dc6f465c68']/28b1c58f-aefa-a449-7496-4805186bd94f$\"" +
                "}, \"Operator\": \"Equal\", \"ValueExpressionRight\": { \"Value\": \"IR693\" } } } } } }";
            var content = new StringContent(expression, Encoding.UTF8, "application/json");

            //var queryFilter = string.Format("?id={0}&createdById={1}", hardwareAssetTemplateId, user.Id);

            var queryResult_Get = _client.PostAsync("Projection/GetProjectionByCriteria", content).Result;
            Assert.IsTrue(queryResult_Get.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult_Get.StatusCode.ToString());
            Assert.IsNotNull(queryResult_Get, "queryResult is empty");

            var resultSring_Get = queryResult_Get.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring_Get), "Query resultSring is null");
        }

        [TestMethod]
        public async Task B01_GetAssetItem_Test_Original()
        {
            var hardwareAssetTemplateId = "c0c58e7f-7865-55cc-4600-753305b9be64";
            var serialNumber = "CNU0183F33"; // "BAM -L-00"; //0852
            var serialNumberfULL = "CND7506PT8"; // "BAM -L-00"; //0852
            var projectionId = "6fd42dd3-81b4-ec8d-14d6-08af1e83f63a";
            var projectionIdFullwUser = "7dd5144c-bd5d-af27-e3af-debcb5a53546";
            serialNumber = serialNumberfULL;
            projectionId = projectionIdFullwUser;

            //var expression = "{\"Id\": \"" + projectionId + "\",\"Criteria\": {" +
            //    "\"Base\": { \"Expression\": { \"SimpleExpression\": { \"ValueExpressionLeft\": { " +
            //    "\"Property\": \"$Context/Property[Type='62f0be9f-ecea-e73c-f00d-3dd78a7422fc']/ObjectStatus$\"" +
            //    "}, \"Operator\": \"Equal\", \"ValueExpressionRight\": { \"Value\": \"BAM-L-001394\" } } } } } }";
            //var projection = "2dd3-81b4-ec8d-14d6-08af1e83f63a";
            var jsonCriteria = "{\"Id\": \""+ projectionId + "\",\"Criteria\": {\"Base\": {" +
                    "\"Expression\": { \"And\": { \"Expression\": [{" +
                    "\"SimpleExpression\": { \"ValueExpressionLeft\": { " +
                        "\"Property\": \"$Context/Property[Type='62f0be9f-ecea-e73c-f00d-3dd78a7422fc']/ObjectStatus$\"}," +
                        "\"Operator\": \"NotEqual\",\"ValueExpressionRight\": {" +
                        "\"Value\": \"47101e64-237f-12c8-e3f5-ec5a665412fb\" }}}, " +
                    "{\"SimpleExpression\": { \"ValueExpressionLeft\": { " +
                        "\"Property\": \"$Context/Property[Type='c0c58e7f-7865-55cc-4600-753305b9be64']/SerialNumber$\"}," +
                        "\"Operator\": \"Like\",\"ValueExpressionRight\": {" +
                        "\"Value\": \"%" + serialNumber + "%\"}}}]}}}}}";
            //var exp = new Expression<Projection>(x => x.Id != "47101e64-237f-12c8-e3f5-ec5a665412fb");
            //Expression< Projection > = x => x.SerialNumber == serialNumber;
            //Func<Projection> o = o => o.SerialNumber == serialNumber;
            //Func<int, int> func1 = x => x + 1;
            //Expression<Func<ServiceModel.Projection, bool>> func2 = x => x.SerialNumber.Equals(serialNumber);
            //var express = new BAM_Expression()
            //{
            //    Id = projection,
            //    Criteria = func2
            //};
            //var criteria = JsonConvert.SerializeObject(express);
            var content = new StringContent(jsonCriteria, Encoding.UTF8, "application/json");

            //var queryFilter = string.Format("?id={0}&createdById={1}", hardwareAssetTemplateId, user.Id);

            var queryResult_Get = _client.PostAsync("Projection/GetProjectionByCriteria", content).Result;
            Assert.IsTrue(queryResult_Get.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult_Get.StatusCode.ToString());
            Assert.IsNotNull(queryResult_Get, "queryResult is empty");

            var resultSring_Get = queryResult_Get.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring_Get), "Query resultSring is null");
        }

        [TestMethod]
        public async Task B02_GetAssetItem_Full_Test()
        {
            var hardwareAssetTemplateId = "c0c58e7f-7865-55cc-4600-753305b9be64";
            var serialNumber = "CNU0183F33"; // "BAM -L-00"; //0852
            var serialNumberFull = "CND7506PT8"; // "BAM -L-00"; //0852
            var projectionId = "6fd42dd3-81b4-ec8d-14d6-08af1e83f63a";
            var projectionIdFullwUser = "7dd5144c-bd5d-af27-e3af-debcb5a53546";
            serialNumber = serialNumberFull;
            projectionId = projectionIdFullwUser;

            var jsonCriteria = "{\"Id\": \"" + projectionId + "\",\"Criteria\": {\"Base\": {" +
                    "\"Expression\": { \"And\": { \"Expression\": [{" +
                    "\"SimpleExpression\": { \"ValueExpressionLeft\": { " +
                        "\"Property\": \"$Context/Property[Type='62f0be9f-ecea-e73c-f00d-3dd78a7422fc']/ObjectStatus$\"}," +
                        "\"Operator\": \"NotEqual\",\"ValueExpressionRight\": {" +
                        "\"Value\": \"47101e64-237f-12c8-e3f5-ec5a665412fb\" }}}, " +
                    "{\"SimpleExpression\": { \"ValueExpressionLeft\": { " +
                        "\"Property\": \"$Context/Property[Type='c0c58e7f-7865-55cc-4600-753305b9be64']/SerialNumber$\"}," +
                        "\"Operator\": \"Like\",\"ValueExpressionRight\": {" +
                        "\"Value\": \"%" + serialNumber + "%\"}}}]}}}}}";

            var content = new StringContent(jsonCriteria, Encoding.UTF8, "application/json");

            var queryResult_Get = _client.PostAsync("Projection/GetProjectionByCriteria", content).Result;
            Assert.IsTrue(queryResult_Get.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult_Get.StatusCode.ToString());
            Assert.IsNotNull(queryResult_Get, "queryResult is empty");

            var resultSring_Get = queryResult_Get.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring_Get), "Query resultSring is null");

            var result = new HardwareTemplate_FullList()
            {
                BAM_HardwareTemplate_FullList = JsonConvert.DeserializeObject<BAM_HardwareTemplate_Full[]>(resultSring_Get).ToList()
            };

            Assert.IsNotNull(result, "Result didn't deserialize to BAM_HardwareTemplate_Full");
            Assert.IsTrue(result.BAM_HardwareTemplate_FullList.Any(), "Result didn't any results");
            Assert.IsTrue(result.BAM_HardwareTemplate_FullList.First().SerialNumber == serialNumber, "SerialNumbers don't match");

            JSON_FileExport.WriteFile(_typePrefix + "B02_GetAssetItem_Full_Test", result.BAM_HardwareTemplate_FullList, result.BAM_HardwareTemplate_FullList.Count);
        }

        [TestMethod]
        public async Task B03_GetAssetItem_Test()
        {
            var hardwareAssetTemplateId = "c0c58e7f-7865-55cc-4600-753305b9be64";
            var serialNumber = "CNU0183F33"; // "BAM -L-00"; //0852
            var serialNumberFull = "CND7506PT8"; // "BAM -L-00"; //0852
            var projectionId = "6fd42dd3-81b4-ec8d-14d6-08af1e83f63a";
            var projectionIdFullwUser = "7dd5144c-bd5d-af27-e3af-debcb5a53546";
            //serialNumber = serialNumberFull;
            //projectionId = projectionIdFullwUser;

            var jsonCriteria = "{\"Id\": \"" + projectionId + "\",\"Criteria\": {\"Base\": {" +
                    "\"Expression\": { \"And\": { \"Expression\": [{" +
                    "\"SimpleExpression\": { \"ValueExpressionLeft\": { " +
                        "\"Property\": \"$Context/Property[Type='62f0be9f-ecea-e73c-f00d-3dd78a7422fc']/ObjectStatus$\"}," +
                        "\"Operator\": \"NotEqual\",\"ValueExpressionRight\": {" +
                        "\"Value\": \"47101e64-237f-12c8-e3f5-ec5a665412fb\" }}}, " +
                    "{\"SimpleExpression\": { \"ValueExpressionLeft\": { " +
                        "\"Property\": \"$Context/Property[Type='c0c58e7f-7865-55cc-4600-753305b9be64']/SerialNumber$\"}," +
                        "\"Operator\": \"Like\",\"ValueExpressionRight\": {" +
                        "\"Value\": \"%" + serialNumber + "%\"}}}]}}}}}";

            var content = new StringContent(jsonCriteria, Encoding.UTF8, "application/json");

            var queryResult_Get = _client.PostAsync("Projection/GetProjectionByCriteria", content).Result;
            Assert.IsTrue(queryResult_Get.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult_Get.StatusCode.ToString());
            Assert.IsNotNull(queryResult_Get, "queryResult is empty");

            var resultSring_Get = queryResult_Get.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring_Get), "Query resultSring is null");
            var result = JsonConvert.DeserializeObject<BAM_HardwareTemplate[]>(resultSring_Get);
            Assert.IsNotNull(result, "Result didn't deserialize to BAM_HardwareTemplate_Full");
            Assert.IsTrue(result.Any(), "Result didn't any results");
            Assert.IsTrue(result.First().SerialNumber == serialNumber, "SerialNumbers don't match");
        }

        [TestMethod]
        public async Task B04_GetAssetItem_Test()
        {
            var hardwareAssetTemplateId = "c0c58e7f-7865-55cc-4600-753305b9be64";
            var serialNumber = "CNU0183F33"; // "BAM -L-00"; //0852
            var serialNumberFull = "CND7506PT8"; // "BAM -L-00"; //0852

            var hardwareAssetService = new BAM_HardwareAssetServices();
            var result = hardwareAssetService.GetHardwareAsset(serialNumber);

            Assert.IsNotNull(result, "Result didn't deserialize to BAM_HardwareTemplate_Full");
            Assert.IsTrue(result.Any(), "Result didn't any results");
            Assert.IsTrue(result.First().SerialNumber == serialNumber, "SerialNumbers don't match");
        }

        [TestMethod]
        public async Task B05_GetAssetItem_SetHardwareStatus_Test()
        {
            var hardwareAssetTemplateId = "c0c58e7f-7865-55cc-4600-753305b9be64";
            var serialNumber = "CNU0183F33"; // "BAM -L-00"; //0852
            var serialNumberFull = "CND7506PT8"; // "BAM -L-00"; //0852

            var hardwareAssetService = new BAM_HardwareAssetServices();
            var result = hardwareAssetService.GetHardwareAsset(serialNumber);

            Assert.IsNotNull(result, "Result didn't deserialize to BAM_HardwareTemplate_Full");
            Assert.IsTrue(result.Any(), "Result didn't any results");
            Assert.IsTrue(result.First().SerialNumber == serialNumber, "SerialNumbers don't match");

            var hardwareAsset = result.First();
            var newHardwareAsset = hardwareAssetService.SetHardwareAssetStatus(hardwareAsset, EST_HWAssetStatus.Deployed);

            Assert.IsNotNull(newHardwareAsset, "Updated Hardware Asset returned null");
            Assert.IsFalse(newHardwareAsset.Equals(hardwareAsset));
            Assert.IsTrue(newHardwareAsset.HardwareAssetStatus.Name != hardwareAsset.HardwareAssetStatus.Name);
            Assert.IsTrue(newHardwareAsset.HardwareAssetStatus.Name == "Deployed");
        }

    }

    public class BAM_Asset
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string AssetName { get; set; }
        public string DisplayName { get; set; }
        public string RequestUser { get; set; }
        public EST_HWAssetStatus HWAssetStatus { get; set; }
    }
}
