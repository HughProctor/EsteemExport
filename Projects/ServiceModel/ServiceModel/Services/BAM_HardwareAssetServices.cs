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
    public class BAM_HardwareAssetServices : BAM_ApiClient, IBAM_HardwareAssetServices
    {
        IBAM_AssetStatusService _assetStatusService;

        public BAM_HardwareAssetServices() : this(new BAM_AssetStatusService())
        {
        }

        public BAM_HardwareAssetServices(IBAM_AssetStatusService assetStatusService) 
        {
            _assetStatusService = assetStatusService;
            Task.Run(() => this.Setup()).Wait();
        }

        public List<BAM_HardwareTemplate> GetHardwareAsset(string serialNumber)
        {
            var returnValue = new List<BAM_HardwareTemplate>();

            var content = CreateProjectionFilter_StringContent(serialNumber);
            var queryResult_Get = _client.PostAsync("Projection/GetProjectionByCriteria", content).Result;

            var resultSring_Get = queryResult_Get.Content.ReadAsStringAsync().Result;

            returnValue = JsonConvert.DeserializeObject<List<BAM_HardwareTemplate>>(resultSring_Get);

            return returnValue;
        }

        public BAM_HardwareTemplate SetHardwareAssetStatus(BAM_HardwareTemplate template, EST_HWAssetStatus hWAssetStatus)
        {
            if (template == null)
                throw new Exception("Template must not be null");

            // Clone the object so we can check the changes
            var newHardwareAsset = CloneObject.Clone(template);
            newHardwareAsset.HardwareAssetStatus = _assetStatusService.GetAssetStatusTemplate(hWAssetStatus);
            return newHardwareAsset;
        }

        public List<BAM_HardwareTemplate> UpdateTemplate(BAM_HardwareTemplate newTemplate, BAM_HardwareTemplate originalTemplate)
        {
            if (newTemplate == null || originalTemplate == null)
                throw new Exception("Template must not be null");

            var template = new HardwareTemplate()
            {
                formJson = new FormJson()
                {
                    Original = originalTemplate,
                    Current = newTemplate
                }
            };
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var json = JsonConvert.SerializeObject(template);//, jsonSettings
            var content = new StringContent(JsonConvert.SerializeObject(template), Encoding.UTF8, "application/json");

            var queryResult_Set = _client.PostAsync("Projection/Commit", content).Result;
            if (!queryResult_Set.IsSuccessStatusCode)
            {
                string responseContent = queryResult_Set.Content.ReadAsStringAsync().Result;
                ExceptionResponse exceptionResponse = JsonConvert.DeserializeObject<ExceptionResponse>(responseContent);
                throw new Exception(exceptionResponse.Exception);
            }

            var resultSring = queryResult_Set.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<BAM_HardwareTemplate>>(resultSring);
            result.Add(originalTemplate);
            return result;
        }

        public string CreateProjectionFilter(string serialNumber)
        {
            var returnValue = "";
            var projectionId = "6fd42dd3-81b4-ec8d-14d6-08af1e83f63a";
            //var projectionIdFullUser = "7dd5144c-bd5d-af27-e3af-debcb5a53546";
            //serialNumber = serialNumberFull;
            //projectionId = projectionIdFullUser;

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

        public StringContent CreateProjectionFilter_StringContent(string serialNumber)
        {
            return new StringContent(CreateProjectionFilter(serialNumber), Encoding.UTF8, "application/json");
        }
    }
}
