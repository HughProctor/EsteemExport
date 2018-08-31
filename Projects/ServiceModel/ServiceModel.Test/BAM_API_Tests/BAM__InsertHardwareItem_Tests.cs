using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ServiceModel.Models;
using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;
using ServiceModel.Services;
using ServiceModel.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Test.BAM_API_Tests
{
    [TestClass]
    public class BAM__InsertHardwareItem_Tests : BaseTestClient
    {
        private string _typePrefix = "BAM_API_";

        [TestMethod]
        public async Task B00_InsertNewAsset_OLD()
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
                HWAssetStatus = EST_HWAssetStatus.NewItem
            };

            // Get User
            IBAM_UserService userService = new BAM_UserService();
            var user = userService.GetUser(asset.RequestUser);
            Assert.IsNotNull(user, "User item is null");
            Assert.IsTrue(user.Name == asset.RequestUser, "user record is not the same");

            // Get AssetStatus
            IBAM_AssetStatusService assetStatusService = new BAM_AssetStatusService();
            var assetStatus = assetStatusService.GetAssetStatusTemplate(asset.HWAssetStatus);
            Assert.IsNotNull(assetStatus, "assetStatus item is null");
            Assert.IsTrue(assetStatus.Name == asset.HWAssetStatus.ToDescriptionString(), "assetStatus record is not the same");

            // Get Projection Template
            var hardwareTemplate = new HardwareTemplate_Full()
            {
                LastModified = new DateTime(),
                ClassTypeId = hardwareAssetTemplateId,
                BaseId = "e728d3d3-3104-47e3-b760-9b9863ebbd9a",
                ClassName = "Cireson.AssetManagement.HardwareAsset",
                FullClassName = "Hardware Asset",
                Manufacturer = asset.Manufacturer,
                Model = asset.Model,
                SerialNumber = asset.SerialNumber,
                AssetTag = asset.AssetName,
                DisplayName = asset.DisplayName,
                HardwareAssetStatus = assetStatusService.GetAssetStatusTemplate(asset.HWAssetStatus),
                HardwareAssetType = new HardwareAssetType()
                {
                    Id = "b4a14ffd-52c8-064f-c936-67616c245b35",
                    Name = "Computer"
                },
                Description = "Hugh Testing",
                Target_HardwareAssetHasCostCenter = new TargetHardwareAssetHasCostCenter()
                {
                    Id = "128bdb2d-f5bd-f8b6-440e-e3f7d8ab4858",
                    DisplayName = "BBN.014A"
                },
                Target_HardwareAssetHasLocation = new TargetHardwareAssetHasLocation()
                {
                    Id = "b1ae24b1-f520-4960-55a2-62029b1ea3f0",
                    DisplayName = "Esteem"
                },
                
                
            };

            var template = new Models.HardwareTemplate_Json()
            {
                formJson = new FormJson()
                {
                   // Original = hardwareTemplate,
                    Current = hardwareTemplate // new BAM_HardwareTemplate()
                }
            };
            //var queryFilter = string.Format("?id={0}&createdById={1}", projectionId, user.Id);

            //var queryResult_Get = _client.GetAsync("Projection/CreateProjectionByTemplate" + queryFilter).Result;
            //if (!queryResult_Get.IsSuccessStatusCode)
            //{
            //    string responseContent = await queryResult_Get.Content.ReadAsStringAsync();
            //    ExceptionResponse exceptionResponse = JsonConvert.DeserializeObject<ExceptionResponse>(responseContent);
            //    Assert.IsNull(exceptionResponse, exceptionResponse.Exception);
            //}

            //Assert.IsTrue(queryResult_Get.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult_Get.StatusCode.ToString());
            //Assert.IsNotNull(queryResult_Get, "queryResult is empty");

            //var resultSring_Get = queryResult_Get.Content.ReadAsStringAsync().Result;
            //Assert.IsFalse(string.IsNullOrEmpty(resultSring_Get), "Query resultSring is null");

            //var result_Get = JsonConvert.DeserializeObject<BAM_HardwareTemplate_Full[]>(resultSring_Get);
            //Assert.IsNotNull(result_Get, "queryResult is null");
            //Assert.IsTrue(result_Get.Any(), "queryResult is empty");

            //////////////-------------POST---------------////////////
            //var content = new StringContent(JsonConvert.SerializeObject(hardwareTemplate), Encoding.UTF8, "application/json");
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var json = JsonConvert.SerializeObject(template, jsonSettings);
            var content = new StringContent(JsonConvert.SerializeObject(template), Encoding.UTF8, "application/json");

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

            var result = JsonConvert.DeserializeObject<HardwareTemplate[]>(resultSring);

            Assert.IsNotNull(result, "Query result is null");
            ////Assert.IsNotNull(result.TemplateClasses, "Articles list is null");
            ////Assert.IsTrue(result.TemplateClasses.Any(), "Articles doesn't contain any items");

        }

        [TestMethod]
        public async Task B01_InsertNewAsset()
        {
            var originalModifiedDate = new DateTime();
            var updatedModifiedDate = new DateTime();

            var asset = new BAM_Asset()
            {
                Manufacturer = "Hewlet-Packard",
                Model = "HP ELITEBOOK 840G3",
                SerialNumber = "5CG749386E",
                AssetName = "BAM-L-005289",
                DisplayName = "HP ELITEBOOK 840G3 - Test",
                RequestUser = "Riley, Roger",
                HWAssetStatus = EST_HWAssetStatus.NewItem
            };

            // Get User
            IBAM_UserService userService = new BAM_UserService();
            var user = userService.GetUser(asset.RequestUser);
            Assert.IsNotNull(user, "User item is null");
            Assert.IsTrue(user.Name == asset.RequestUser, "user record is not the same");

            // Get AssetStatus
            IBAM_AssetStatusService assetStatusService = new BAM_AssetStatusService();
            var assetStatus = assetStatusService.GetAssetStatusTemplate(asset.HWAssetStatus);
            Assert.IsNotNull(assetStatus, "assetStatus item is null");
            Assert.IsTrue(assetStatus.Name == asset.HWAssetStatus.ToDescriptionString(), "assetStatus record is not the same");

            // Get Projection Template
            var hardwareTemplate = new Models.BAM.HardwareTemplate()
            {
                LastModified = new DateTime(0001, 01, 01, 00, 00, 00),
                LastModifiedBy = "7431e155-3d9e-4724-895e-c03ba951a352",
                ClassTypeId = "20d06950-4c1a-1afa-41a6-f46f4f863550",
                BaseId = "e728d3d3-3104-47e3-b760-9b9863ebbd9a",
                ClassName = "Cireson.AssetManagement.HardwareAsset",
                FullClassName = "Hardware Asset",
                Manufacturer = asset.Manufacturer,
                Model = asset.Model,
                SerialNumber = asset.SerialNumber,
                AssetTag = asset.AssetName,
                DisplayName = asset.DisplayName,
                HardwareAssetStatus = assetStatusService.GetAssetStatusTemplate(asset.HWAssetStatus),
                Description = "Hugh Testing",
                HardwareAssetType = new HardwareAssetType()
                {
                    Id = "b4a14ffd-52c8-064f-c936-67616c245b35",
                    Name = "Computer"
                },
                //Target_HardwareAssetHasCostCenter = new TargetHardwareAssetHasCostCenter()
                //{
                //    Id = "128bdb2d-f5bd-f8b6-440e-e3f7d8ab4858",
                //    DisplayName = "BBN.014A"
                //},
                //Target_HardwareAssetHasLocation = new TargetHardwareAssetHasLocation()
                //{
                //    Id = "b1ae24b1-f520-4960-55a2-62029b1ea3f0",
                //    DisplayName = "Esteem"
                //},
                //Target_HardwareAssetHasPrimaryUser = new TargetHardwareAssetHasPrimaryUser()
                //{
                //    ClassTypeId = user.Id,
                //    FullName = user.Name,
                //}

            };
            var hardwareAssetService = new BAM_HardwareAssetServices();

            var returnItems = hardwareAssetService.InsertTemplate(hardwareTemplate).FirstOrDefault();
            Assert.IsNotNull(returnItems, "Updated Asset is null");
            Assert.IsTrue(returnItems.SerialNumber == hardwareTemplate.SerialNumber, "SerialNumbers don't match");

            var updatedHardwareAsset = hardwareAssetService.GetHardwareAsset(hardwareTemplate.SerialNumber).FirstOrDefault();

            Assert.IsNotNull(updatedHardwareAsset, "Updated Asset is null");
            Assert.IsTrue(updatedHardwareAsset.SerialNumber == hardwareTemplate.SerialNumber, "SerialNumbers don't match");

            updatedModifiedDate = (DateTime)updatedHardwareAsset.LastModified;

            Assert.IsTrue(updatedModifiedDate != originalModifiedDate, "Original and Updated LastModified Date are the same");
            Assert.IsTrue(updatedModifiedDate > originalModifiedDate, "Updated LastModified Date is not greater that the Original");

        }

    }
}
