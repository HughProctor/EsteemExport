using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ServiceModel.Models.BAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Test.BAM_API_Tests
{
    [TestClass]
    public class BAM_InsertHardwareItem_Tests : BaseTestClient
    {
        private string _typePrefix = "BAM_API_";

        [TestMethod]
        public async Task B00_InsertNewAsset()
        {
            //var hardwareAssetTemplateId = "c0c58e7f-7865-55cc-4600-753305b9be64";
            var hardwareAssetTemplateId = "20d06950-4c1a-1afa-41a6-f46f4f863550";
            var projectionId = "6fd42dd3-81b4-ec8d-14d6-08af1e83f63a";

            var asset = new BAM_Asset()
            {
                Manufacturer = "Hewlet-Packard",
                Model = "HP ELITEBOOK 840G3",
                SerialNumber = "5CG749386D",
                AssetName = "BAM-L-005289",
                DisplayName = "HP ELITEBOOK 840G3 - Test",
                RequestUser = "Riley, Roger",
                HWAssetStatus = BAM_HWAssetStatus.NewItem
            };

            // Get User
            var user = GetUser(asset.RequestUser);
            Assert.IsNotNull(user, "User item is null");
            Assert.IsTrue(user.Name == asset.RequestUser, "user record is not the same");

            // Get AssetStatus
            var assetStatus = GetAssetStatusTemplate(asset.HWAssetStatus);
            Assert.IsNotNull(assetStatus, "assetStatus item is null");
            Assert.IsTrue(assetStatus.Name == asset.HWAssetStatus.ToDescriptionString(), "assetStatus record is not the same");

            // Get Projection Template
            var hardwareTemplate = new BAM_HardwareTemplate_Full()
            {
                ClassTypeId = hardwareAssetTemplateId,
                BaseId = "e728d3d3-3104-47e3-b760-9b9863ebbd9a",
                ClassName = "Cireson.AssetManagement.HardwareAsset",
                FullClassName = "Hardware Asset",
                Manufacturer = asset.Manufacturer,
                Model = asset.Model,
                SerialNumber = asset.SerialNumber,
                AssetTag = asset.AssetName,
                DisplayName = asset.DisplayName,
                AssetStatus = new AssetStatus()
                {
                    Id = assetStatus.Id,
                    Name = assetStatus.Name
                },
                HardwareAssetType = new HardwareAssetType()
                {
                    Id = "b4a14ffd-52c8-064f-c936-67616c245b35",
                    Name = "Computer"
                },
                Description = "Hugh Testing",
                Target_HardwareAssetHasCostCenter = new TargetHardwareAssetHasCostCenter()
                {
                    ClassTypeId = "128bdb2d-f5bd-f8b6-440e-e3f7d8ab4858",
                    BaseId = "c9f7b68f-ad80-b59f-8bcf-f51671ac4d55",
                    DisplayName = "BBN.014A"
                },
                Target_HardwareAssetHasLocation = new TargetHardwareAssetHasLocation()
                {
                    ClassTypeId = "b1ae24b1-f520-4960-55a2-62029b1ea3f0",
                    BaseId = "ae7423eb-0952-d69c-4d7d-77f1699bfe92",
                    DisplayName = "Esteem"
                }
            };

            ////var queryFilter = string.Format("?id={0}&createdById={1}", projectionId, user.Id);

            ////var queryResult_Get = _client.GetAsync("Projection/CreateProjectionByTemplate" + queryFilter).Result;
            ////Assert.IsTrue(queryResult_Get.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult_Get.StatusCode.ToString());
            ////Assert.IsNotNull(queryResult_Get, "queryResult is empty");

            ////var resultSring_Get = queryResult_Get.Content.ReadAsStringAsync().Result;
            ////Assert.IsFalse(string.IsNullOrEmpty(resultSring_Get), "Query resultSring is null");

            ////var result_Get = JsonConvert.DeserializeObject<BAM_HardwareTemplate_Full[]>(resultSring_Get);
            ////Assert.IsNotNull(result_Get, "queryResult is null");
            ////Assert.IsTrue(result_Get.Any(), "queryResult is empty");

            var content = new StringContent(JsonConvert.SerializeObject(hardwareTemplate), Encoding.UTF8, "application/json");

            var queryResult_Set = _client.PostAsync("Projection/Commit", content).Result;
            if (!queryResult_Set.IsSuccessStatusCode)
            {
                string responseContent = await queryResult_Set.Content.ReadAsStringAsync();
                ExceptionResponse exceptionResponse = JsonConvert.DeserializeObject<ExceptionResponse>(responseContent);
                Assert.IsNull(exceptionResponse, exceptionResponse.Exception);
            }
            Assert.IsTrue(queryResult_Set.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult_Set.StatusCode.ToString());
            Assert.IsNotNull(queryResult_Set, "queryResult is empty");

            var resultSring = queryResult_Set.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring), "Query resultSring is null");

            var result = JsonConvert.DeserializeObject<BAM_HardwareTemplate[]>(resultSring);

            Assert.IsNotNull(result, "Query result is null");
            ////Assert.IsNotNull(result.TemplateClasses, "Articles list is null");
            ////Assert.IsTrue(result.TemplateClasses.Any(), "Articles doesn't contain any items");

        }

        private BAM_User GetUser(string userName)
        {
            var userFilter = userName;
            var filterByAnalyst = false;
            var groupsOnly = false;
            var maxNumberOfResults = 100;
            var fetchAll = false;
            var queryFilter = string.Format("?userFilter={0}&filterByAnalyst={1}&groupsOnly={2}&maxNumberOfResults={3}&fetchAll={4}",
                    userFilter, filterByAnalyst, groupsOnly, maxNumberOfResults, fetchAll
                );
            var queryResult = _client.GetAsync("User/GetUserList" + queryFilter).Result;

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<BAM_User[]>(resultSring);
            return result.FirstOrDefault();
        }

        private BAM_AssetStatus GetAssetStatusTemplate(BAM_HWAssetStatus assetStatus)
        {
            var id = "6b7304c4-1b09-bffc-3fe3-1cfd3eb630cb";
            var itemFiler = BAM_HWAssetStatus.NewItem.ToDescriptionString(); // "";
            var flatten = true;
            //id = hardwareConfigItem.m_Item1;

            var queryFilter = string.Format("?id={0}&itemFilter={1}&Flatten={2}",
                id, itemFiler, flatten);
            var queryResult = _client.GetAsync("Enum/GetList" + queryFilter).Result;

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;

            var resultTemp = JsonConvert.DeserializeObject<List<BAM_AssetStatus>>(resultSring);
            var result = new BAM_AssetStatusList()
            {
                BAM_AssetStatuses = resultTemp.OrderBy(x => x.Name).ToList()
            };
            var newItem = result.BAM_AssetStatuses.Where(x => x.Name == assetStatus.ToDescriptionString()).FirstOrDefault();
            return newItem;
        }

        public class ExceptionResponse
        {
            public string Success { get; set; }
            public string Message { get; set; }
            public string Exception { get; set; }
            public string ExceptionMessage { get; set; }
            public string ExceptionType { get; set; }
            public string StackTrace { get; set; }
            public ExceptionResponse InnerException { get; set; }
        }
    }
}
