using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.FileExport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;

namespace ServiceModel.Test.BAM_API_Tests
{
    [TestClass]
    public class BAM_ArticleCategory_Tests : BaseTestClient
    {
        private string _typePrefix = "BAM_API_";

        [TestMethod]
        public async Task BAM_ArticleCategory_Get_FlatList()
        {
            var queryResult = _client.GetAsync("ArticleCategory").Result;

            Assert.IsTrue(queryResult.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult.StatusCode.ToString());
            Assert.IsNotNull(queryResult, "queryResult is empty");

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring), "Query resultSring is null");
            
            var result = JsonConvert.DeserializeObject<ArticleCategoryRoot>(resultSring);
            //var result = queryResult.Content.ReadAsAsync<ArticleCategoryRoot>().Result;

            Assert.IsNotNull(result, "Query result is null");
            Assert.IsNotNull(result.Categories, "ArticleCategory list is null");
            Assert.IsTrue(result.Categories.Any(), "ArticleCategories doesn't contain any items");
        }


        [TestMethod]
        public async Task BAM_Article_Get_List()
        {
            var queryResult = _client.GetAsync("Article").Result;

            Assert.IsTrue(queryResult.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult.StatusCode.ToString());
            Assert.IsNotNull(queryResult, "queryResult is empty");

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring), "Query resultSring is null");

            var result = JsonConvert.DeserializeObject<ArticleList>(resultSring);
            //var result = queryResult.Content.ReadAsAsync<ArticleList>().Result;

            Assert.IsNotNull(result, "Query result is null");
            Assert.IsNotNull(result.Articles, "Articles list is null");
            Assert.IsTrue(result.Articles.Any(), "Articles doesn't contain any items");
        }

        [TestMethod]
        public async Task BAM_Template_Classes_Get_List()
        {
            var queryResult = _client.GetAsync("Template/GetAllClasses").Result;

            Assert.IsTrue(queryResult.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult.StatusCode.ToString());
            Assert.IsNotNull(queryResult, "queryResult is empty");

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring), "Query resultSring is null");

            var resultTemp = JsonConvert.DeserializeObject<TemplateClass[]>(resultSring);
            Assert.IsNotNull(resultTemp, "Query resultTemp is null");
            Assert.IsTrue(resultTemp.Any(), "Query resultTemp doesn't contain any items");

            var result = new TemplateClassList()
            {
                TemplateClasses = resultTemp.ToList()
            };
            //var result = queryResult.Content.ReadAsAsync<ArticleList>().Result;

            Assert.IsNotNull(result, "Query result is null");
            Assert.IsNotNull(result.TemplateClasses, "Articles list is null");
            Assert.IsTrue(result.TemplateClasses.Any(), "Articles doesn't contain any items");
            JSON_FileExport.WriteFile(_typePrefix + "BAM_Template_Classes_Get_List", result.TemplateClasses, result.TemplateClasses.Count);

        }

        [TestMethod]
        public async Task BAM_HardwareAssetNotificationTemplate_Get_List()
        {
            var queryResult = _client.GetAsync("Template/GetHardwareAssetNotificationTemplates").Result;

            Assert.IsTrue(queryResult.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult.StatusCode.ToString());
            Assert.IsNotNull(queryResult, "queryResult is empty");

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring), "Query resultSring is null");

            var resultTemp = JsonConvert.DeserializeObject<TemplateClass[]>(resultSring);
            Assert.IsNotNull(resultTemp, "Query resultTemp is null");
            Assert.IsTrue(resultTemp.Any(), "Query resultTemp doesn't contain any items");

            var result = new TemplateClassList()
            {
                TemplateClasses = resultTemp.ToList()
            };
            //var result = queryResult.Content.ReadAsAsync<ArticleList>().Result;

            Assert.IsNotNull(result, "Query result is null");
            Assert.IsNotNull(result.TemplateClasses, "Articles list is null");
            Assert.IsTrue(result.TemplateClasses.Any(), "Articles doesn't contain any items");
        }

        [TestMethod]
        public async Task BAM_Templates_for_Class_FirstRecord_Get_List()
        {
            var templateList = GetTemplateList();

            var deviceItem = templateList.TemplateClasses.Where(x => x.Name == "Computer").FirstOrDefault();
            Assert.IsNotNull(deviceItem, "deviceItem is empty");
            Assert.IsFalse(string.IsNullOrEmpty(deviceItem.Id), "deviceItem id is empty");

            var queryResult = _client.GetAsync(string.Format("Template/GetTemplates?classId={0}", deviceItem.Id)).Result;

            Assert.IsTrue(queryResult.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult.StatusCode.ToString());
            Assert.IsNotNull(queryResult, "queryResult is empty");

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring), "Query resultSring is null");

            var resultTemp = JsonConvert.DeserializeObject<TemplateClass[]>(resultSring);
            Assert.IsNotNull(resultTemp, "Query resultTemp is null");
            Assert.IsTrue(resultTemp.Any(), "Query resultTemp doesn't contain any items");

            var result = new TemplateClassList()
            {
                TemplateClasses = resultTemp.ToList()
            };
            //var result = queryResult.Content.ReadAsAsync<ArticleList>().Result;

            Assert.IsNotNull(result, "Query result is null");
            Assert.IsNotNull(result.TemplateClasses, "Articles list is null");
            Assert.IsTrue(result.TemplateClasses.Any(), "Articles doesn't contain any items");
        }

        [TestMethod]
        public async Task BAM_UserList_Get_List()
        {
            var userFilter = "";
            var filterByAnalyst = false;
            var groupsOnly = false;
            var maxNumberOfResults = 100;
            var fetchAll = true;
            var queryFilter = string.Format("?userFilter={0}&filterByAnalyst={1}&groupsOnly={2}&maxNumberOfResults={3}&fetchAll={4}",
                    userFilter, filterByAnalyst, groupsOnly, maxNumberOfResults, fetchAll
                );
            var queryResult = _client.GetAsync("User/GetUserList" + queryFilter).Result;

            Assert.IsTrue(queryResult.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult.StatusCode.ToString());
            Assert.IsNotNull(queryResult, "queryResult is empty");

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring), "Query resultSring is null");

            var resultTemp = JsonConvert.DeserializeObject<BAM_User[]>(resultSring);
            Assert.IsNotNull(resultTemp, "Query resultTemp is null");
            Assert.IsTrue(resultTemp.Any(), "Query resultTemp doesn't contain any items");

            var result = new BAM_UserList()
            {
                BAM_Users = resultTemp.ToList()
            };
            //var result = queryResult.Content.ReadAsAsync<ArticleList>().Result;

            Assert.IsNotNull(result, "Query result is null");
            Assert.IsNotNull(result.BAM_Users, "Articles list is null");
            Assert.IsTrue(result.BAM_Users.Any(), "Articles doesn't contain any items");
            JSON_FileExport.WriteFile(_typePrefix + "BAM_UserList_Get_List", result.BAM_Users, result.BAM_Users.Count);

            var esteemUser = result.BAM_Users.Where(x => x.Name.Contains("Esteem")).FirstOrDefault();
            Assert.IsNotNull(esteemUser, "Esteem User is null");
        }

        [TestMethod]
        public async Task BAM_Config_HardwareAsset_Get_List()
        {
            var queryResult = _client.GetAsync("Config/GetPopulatedConfigClasses").Result;

            Assert.IsTrue(queryResult.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult.StatusCode.ToString());
            Assert.IsNotNull(queryResult, "queryResult is empty");

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring), "Query resultSring is null");

            var resultTemp = JsonConvert.DeserializeObject<Config[]>(resultSring);
            Assert.IsNotNull(resultTemp, "Query resultTemp is null");
            Assert.IsTrue(resultTemp.Any(), "Query resultTemp doesn't contain any items");

            var result = new ConfigList()
            {
                Configs = resultTemp.ToList()
            };
            //var result = queryResult.Content.ReadAsAsync<ArticleList>().Result;

            Assert.IsNotNull(result, "Query result is null");
            Assert.IsNotNull(result.Configs, "Articles list is null");
            Assert.IsTrue(result.Configs.Any(), "Articles doesn't contain any items");

            var hardwareAssetConfigItem = result.Configs.Where(x => x.m_Item2.Contains("Hardware Asset")).FirstOrDefault();
            Assert.IsNotNull(hardwareAssetConfigItem, "HardwareAsset Config Item is null");
        }

        [TestMethod]
        public async Task BAM_EnumFlatList_HardwareAsset_Get_List()
        {
            //var hardwareConfigItem = Get_HardwareAsset_ConfigItem();
            var id = "6b7304c4-1b09-bffc-3fe3-1cfd3eb630cb";
            var itemFiler = EST_HWAssetStatus.NewItem.ToDescriptionString(); // "";
            var flatten = true;
            //id = hardwareConfigItem.m_Item1;

            var queryFilter = string.Format("?id={0}&itemFilter={1}&Flatten={2}",
                id, itemFiler, flatten);
            var queryResult = _client.GetAsync("Enum/GetList" + queryFilter).Result;

            Assert.IsTrue(queryResult.IsSuccessStatusCode, "Query Status Code isn't Success: " + queryResult.StatusCode.ToString());
            Assert.IsNotNull(queryResult, "queryResult is empty");

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(string.IsNullOrEmpty(resultSring), "Query resultSring is null");

            var resultTemp = JsonConvert.DeserializeObject<List<AssetStatus>>(resultSring);
            Assert.IsNotNull(resultTemp, "Query resultTemp is null");
            Assert.IsTrue(resultTemp.Any(), "Query resultTemp doesn't contain any items");

            var result = new BAM_AssetStatusList()
            {
                AssetStatuses = resultTemp.OrderBy(x => x.Name).ToList()
            };
            var newItem = result.AssetStatuses.Where(x => x.Name == EST_HWAssetStatus.NewItem.ToDescriptionString()).FirstOrDefault();
            Assert.IsNotNull(newItem, "New Item record is null");
            Assert.IsTrue(newItem.Name == EST_HWAssetStatus.NewItem.ToDescriptionString(), "New Item is not correct name " + newItem.Name);
            JSON_FileExport.WriteFile(_typePrefix + "00_ENUM_HARDWAREASSET", result.AssetStatuses, result.AssetStatuses.Count);

            Assert.IsNotNull(result, "Query result is null");
            Assert.IsNotNull(result.AssetStatuses, "Articles list is null");
            Assert.IsTrue(result.AssetStatuses.Any(), "Articles doesn't contain any items");
        }

        private TemplateClassList GetTemplateList()
        {
            var queryResult = _client.GetAsync("Template/GetAllClasses").Result;
            var resultSring = queryResult.Content.ReadAsStringAsync().Result;

            var resultTemp = JsonConvert.DeserializeObject<TemplateClass[]>(resultSring);

            var result = new TemplateClassList()
            {
                TemplateClasses = resultTemp.ToList()
            };
            return result;
        }

        private Config Get_HardwareAsset_ConfigItem()
        {
            var queryResult = _client.GetAsync("Config/GetPopulatedConfigClasses").Result;

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;

            var resultTemp = JsonConvert.DeserializeObject<Config[]>(resultSring);

            var result = new ConfigList()
            {
                Configs = resultTemp.ToList()
            };
            var hardwareAssetConfigItem = result.Configs.Where(x => x.m_Item2.Contains("Hardware Asset")).FirstOrDefault();

            return hardwareAssetConfigItem;
        }
    }
}
