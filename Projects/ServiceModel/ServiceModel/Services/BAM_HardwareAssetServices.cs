using Infrastructure.FileExport;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ServiceModel.Extensions;
using ServiceModel.Models;
using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;
using ServiceModel.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Services
{
    public class BAM_HardwareAssetServices : IBAM_HardwareAssetServices //BAM_ApiClient, 
    {
        IBAM_AssetStatusService _assetStatusService;
        public BAM_ApiClient _bamclient;
        public List<NameRelationship> _bamApiRelationships;

        #region Constructors
        public BAM_HardwareAssetServices() : this(null, null)
        {
        }
        public BAM_HardwareAssetServices(BAM_ApiClient bamclient)
        {
            _bamclient = bamclient;
            if (_bamclient == null)
            {
                _bamclient = new BAM_ApiClient();
                Task.Run(() => _bamclient.Setup()).Wait();
            }
            _assetStatusService = new BAM_AssetStatusService(_bamclient);

            var jsonNameRelationships = JSON_FileExport.ReadFile("NameRelationships.json", "JsonStatics");
            _bamApiRelationships = JsonConvert.DeserializeObject<List<NameRelationship>>(jsonNameRelationships);
        }

        public BAM_HardwareAssetServices(IBAM_AssetStatusService assetStatusService, BAM_ApiClient bamclient)
        {
            _bamclient = bamclient;
            if (_bamclient == null)
            {
                _bamclient = new BAM_ApiClient();
                Task.Run(() => _bamclient.Setup()).Wait();
            }
            _assetStatusService = assetStatusService ?? new BAM_AssetStatusService(_bamclient);
        }

        //public BAM_HardwareAssetServices(IBAM_AssetStatusService assetStatusService) 
        //{
        //    _assetStatusService = assetStatusService ?? new BAM_AssetStatusService();
        //    Task.Run(() => this.Setup()).Wait();
        //}
        #endregion

        #region CRUD
        public List<HardwareTemplate> GetHardwareAsset(string serialNumber)
        {
            var returnValue = new List<Models.BAM.HardwareTemplate>();

            var content = CreateProjectionFilter_StringContent(serialNumber);
            var queryResult_Get = _bamclient._client.PostAsync("api/V3/Projection/GetProjectionByCriteria", content).Result;

            var resultSring_Get = queryResult_Get.Content.ReadAsStringAsync().Result;

            returnValue = JsonConvert.DeserializeObject<List<Models.BAM.HardwareTemplate>>(resultSring_Get);

            return returnValue;
        }

        public List<HardwareTemplate_Full> GetHardwareAsset_Full(string serialNumber)
        {
            var returnValue = new List<HardwareTemplate_Full>();

            var content = CreateProjectionFilter_StringContent(serialNumber, true);
            var queryResult_Get = _bamclient._client.PostAsync("api/V3/Projection/GetProjectionByCriteria", content).Result;

            var resultSring_Get = queryResult_Get.Content.ReadAsStringAsync().Result;

            returnValue = JsonConvert.DeserializeObject<List<HardwareTemplate_Full>>(resultSring_Get);

            return returnValue;
        }

        public List<HardwareTemplate> UpdateTemplate(HardwareTemplate newTemplate, HardwareTemplate originalTemplate)
        {
            var returnValue = new List<HardwareTemplate>();
            if (newTemplate == null)
                throw new Exception("Template must not be null");

            // Set API NameRelationship default values
            newTemplate.NameRelationship = _bamApiRelationships;

            var template = new HardwareTemplate_Json()
            {
                formJson = new FormJson()
                {
                    Original = originalTemplate,
                    Current = newTemplate
                }
            };
            return BAM_ApiPost(newTemplate, originalTemplate, returnValue, template);
        }

        public List<HardwareTemplate_Full> UpdateTemplate(HardwareTemplate_Full newTemplate, HardwareTemplate_Full originalTemplate)
        {
            var returnValue = new List<HardwareTemplate_Full>();
            if (newTemplate == null)
                throw new Exception("Template must not be null");

            // Set API NameRelationship default values
            newTemplate.NameRelationship = _bamApiRelationships;

            var template = new HardwareTemplate_Json()
            {
                formJson = new FormJson()
                {
                    Original = originalTemplate,
                    Current = newTemplate
                }
            };
            return BAM_ApiPost(newTemplate, originalTemplate, returnValue, template);
        }

        public List<HardwareTemplate> InsertTemplate(HardwareTemplate newTemplate)
        {
            var returnValue = new List<HardwareTemplate>();
            if (newTemplate == null)
                throw new Exception("Template must not be null");

            // Set API NameRelationship default values
            newTemplate.NameRelationship = _bamApiRelationships;

            var template = new HardwareTemplate_Json()
            {
                formJson = new FormJson()
                {
                    Original = null,
                    Current = newTemplate
                }
            };
            return BAM_ApiPost(newTemplate, null, returnValue, template);
        }

        public List<HardwareTemplate_Full> InsertTemplate(HardwareTemplate_Full newTemplate)
        {
            var returnValue = new List<HardwareTemplate_Full>();
            if (newTemplate == null)
                throw new Exception("Template must not be null");

            // Set API NameRelationship default values
            newTemplate.NameRelationship = _bamApiRelationships;

            var template = new HardwareTemplate_Json()
            {
                formJson = new FormJson()
                {
                    Original = null,
                    Current = newTemplate
                }
            };
            return BAM_ApiPost(newTemplate, null, returnValue, template);
        }

        #endregion

        #region Private Post
        private List<HardwareTemplate> BAM_ApiPost(HardwareTemplate newTemplate, HardwareTemplate originalTemplate, List<HardwareTemplate> returnValue, Models.HardwareTemplate_Json template)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var json = JsonConvert.SerializeObject(template);//, jsonSettings
            var content = new StringContent(JsonConvert.SerializeObject(template), Encoding.UTF8, "application/json");

            var queryResult_Set = _bamclient._client.PostAsync("api/V3/Projection/Commit", content).Result;
            if (!queryResult_Set.IsSuccessStatusCode)
            {
                string responseContent = queryResult_Set.Content.ReadAsStringAsync().Result;
                ExceptionResponse exceptionResponse = JsonConvert.DeserializeObject<ExceptionResponse>(responseContent);
                //throw new Exception(exceptionResponse.Exception);
            }

            var resultSring = queryResult_Set.Content.ReadAsStringAsync().Result;

            //var result = JsonConvert.DeserializeObject<List<BAM_HardwareTemplate>>(resultSring);
            /////-------------This could be moved out and converted into an async Task ---- we can handle response outside///
            var result = JsonConvert.DeserializeObject<BAM_Api_SuccessResponse>(resultSring);
            //if (result.BaseId != newTemplate.BaseId)
            //    throw new Exception("Updated BaseId's didn't match");

            returnValue.Add(newTemplate);
            returnValue.Add(originalTemplate);
            return returnValue;
        }

        private List<HardwareTemplate_Full> BAM_ApiPost(HardwareTemplate_Full newTemplate, HardwareTemplate_Full originalTemplate, List<HardwareTemplate_Full> returnValue, Models.HardwareTemplate_Json template)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var json = JsonConvert.SerializeObject(template);//, jsonSettings
            var content = new StringContent(JsonConvert.SerializeObject(template), Encoding.UTF8, "application/json");

            var queryResult_Set = _bamclient._client.PostAsync("api/V3/Projection/Commit", content).Result;
            if (!queryResult_Set.IsSuccessStatusCode)
            {
                string responseContent = queryResult_Set.Content.ReadAsStringAsync().Result;
                ExceptionResponse exceptionResponse = JsonConvert.DeserializeObject<ExceptionResponse>(responseContent);
                //throw new Exception(exceptionResponse.Exception);
            }

            var resultSring = queryResult_Set.Content.ReadAsStringAsync().Result;

            //var result = JsonConvert.DeserializeObject<List<BAM_HardwareTemplate>>(resultSring);
            /////-------------This could be moved out and converted into an async Task ---- we can handle response outside///
            var result = JsonConvert.DeserializeObject<BAM_Api_SuccessResponse>(resultSring);
            //if (result.BaseId != newTemplate.BaseId)
            //    throw new Exception("Updated BaseId's didn't match");

            returnValue.Add(newTemplate);
            returnValue.Add(originalTemplate);
            return returnValue;
        }
        #endregion

        #region Filters
        public string CreateProjectionFilter(string serialNumber, bool useFullProjection = false)
        {
            var returnValue = "";
            var projectionId = "6fd42dd3-81b4-ec8d-14d6-08af1e83f63a";
            var projectionIdFullUser = "7dd5144c-bd5d-af27-e3af-debcb5a53546";
            //serialNumber = serialNumberFull;
            if (useFullProjection) projectionId = projectionIdFullUser;

            returnValue = "{\"Id\": \"" + projectionId + "\",\"Criteria\": {\"Base\": {" +
                    "\"Expression\": { \"And\": { \"Expression\": [{" +
                    "\"SimpleExpression\": { \"ValueExpressionLeft\": { " +
                        "\"Property\": \"$Context/Property[Type='62f0be9f-ecea-e73c-f00d-3dd78a7422fc']/ObjectStatus$\"}," +
                        "\"Operator\": \"NotEqual\",\"ValueExpressionRight\": {" +
                        "\"Value\": \"47101e64-237f-12c8-e3f5-ec5a665412fb\" }}}, " +
                    "{\"SimpleExpression\": { \"ValueExpressionLeft\": { " +
                        "\"Property\": \"$Context/Property[Type='c0c58e7f-7865-55cc-4600-753305b9be64']/SerialNumber$\"}," +
                        "\"Operator\": \"Like\",\"ValueExpressionRight\": {" +
                        "\"Value\": \"%" + serialNumber + "%\"}}}]}}}}}";

            return returnValue;
        }

        public StringContent CreateProjectionFilter_StringContent(string serialNumber, bool useFullProjection = false)
        {
            return new StringContent(CreateProjectionFilter(serialNumber, useFullProjection), Encoding.UTF8, "application/json");
        }
        #endregion

        #region Set Values
        public HardwareTemplate_Full CreateNewTemplate()
        {
            Guid.TryParse("c0c58e7f-7865-55cc-4600-753305b9be64", out var classTypeId);
            Guid.TryParse("e728d3d3-3104-47e3-b760-9b9863ebbd9a", out var baseId);
            Guid.TryParse("b4a14ffd-52c8-064f-c936-67616c245b35", out var hardwareAssetTypeId);
            Guid.TryParse("acdcedb7-100c-8c91-d664-4629a218bd94", out var objectStatusId);
            var hardwareAssetId = Guid.NewGuid();

            var returnValue = new HardwareTemplate_Full() {
                ClassName = null,
                FullClassName = null,
                BaseId = null,
                LastModified = new DateTime(0001, 01, 01, 00, 00, 00),
                TimeAdded = new DateTime(0001, 01, 01, 00, 00, 00),
                ClassTypeId = classTypeId.ToString(),
                HardwareAssetID = hardwareAssetId.ToString(),
                ObjectStatus = new ObjectStatus()
                {
                    Id = objectStatusId.ToString()
                },
            };

            return returnValue;
        }

        public HardwareTemplate SetHardwareAssetStatus(HardwareTemplate template, EST_HWAssetStatus hWAssetStatus)
        {
            if (template == null)
                throw new Exception("Template must not be null");

            // Clone the object so we can check the changes
            var newHardwareAsset = CloneObject.Clone(template);
            newHardwareAsset.HardwareAssetStatus = _assetStatusService.GetAssetStatusTemplate(hWAssetStatus);
            return newHardwareAsset;
        }

        public HardwareTemplate_Full SetHardwareAssetStatus(HardwareTemplate_Full template, EST_HWAssetStatus hWAssetStatus)
        {
            if (template == null)
                throw new Exception("Template must not be null");

            // Clone the object so we can check the changes
            var newHardwareAsset = CloneObject.Clone(template);
            newHardwareAsset.HardwareAssetStatus = _assetStatusService.GetAssetStatusTemplate(hWAssetStatus);
            return newHardwareAsset;
        }

        public HardwareTemplate_Full SetCostCode(HardwareTemplate_Full template, string costCode)
        {
            if (template == null)
                throw new Exception("Template must not be null");

            // Clone the object so we can check the changes
            var newHardwareAsset = CloneObject.Clone(template);
            newHardwareAsset.HardwareAssetStatus = _assetStatusService.GetAssetStatusTemplate(hWAssetStatus);
            return newHardwareAsset;
        }

        public HardwareTemplate_Full SetHardwareAssetPrimaryUser(HardwareTemplate_Full template, BAM_User user)
        {
            if (template == null)
                throw new Exception("Template must not be null");

            // Clone the object so we can check the changes
            var newHardwareAsset = CloneObject.Clone(template);
            newHardwareAsset.OwnedBy = new OwnedBy
            {
                BaseId = user.Id,
            }; 
            return newHardwareAsset;
        }

        public HardwareTemplate_Full SetLocation(HardwareTemplate_Full template, string siteLocation)
        {
            if (template == null)
                throw new Exception("Template must not be null");

            // Clone the object so we can check the changes
            var newHardwareAsset = CloneObject.Clone(template);
            //if (siteLocation == "Esteem" || siteLocation == "LTX")
            //{
                newHardwareAsset.Target_HardwareAssetHasLocation = new TargetHardwareAssetHasLocation
                {
                    //ClassTypeId = "b1ae24b1-f520-4960-55a2-62029b1ea3f0",
                    BaseId = "ae7423eb-0952-d69c-4d7d-77f1699bfe92",
                    //DisplayName = "Esteem",
                    //ClassName = "Cireson.AssetManagement.Location",
                    //FullClassName = "Location",
                    //LastModified = DateTime.Now,
                };
            //}
            return newHardwareAsset;
        }

        public HardwareTemplate SetAssetTag(HardwareTemplate template, string assetTag)
        {
            if (template == null)
                throw new Exception("Template must not be null");

            // Clone the object so we can check the changes
            var newHardwareAsset = CloneObject.Clone(template);
            newHardwareAsset.AssetTag = assetTag;
            return newHardwareAsset;
        }
        #endregion
    }
}
