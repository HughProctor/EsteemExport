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
    public class BAM_GetUsers : BaseTestClient
    {
        [TestMethod]
        public async Task BAM_UserList_Get_WestwoodEleanor()
        {
            var userFilter = "";
            var filterByAnalyst = false;
            var groupsOnly = false;
            var maxNumberOfResults = 100;
            var fetchAll = true;
            var queryFilter = string.Format("?userFilter={0}&filterByAnalyst={1}&groupsOnly={2}&maxNumberOfResults={3}&fetchAll={4}",
                    userFilter, filterByAnalyst, groupsOnly, maxNumberOfResults, fetchAll
                );
            var queryResult = _bamClient._client.GetAsync("api/V3/User/GetUserList" + queryFilter).Result;

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

            var esteemUser = result.BAM_Users.Where(x => x.Name.Contains("Westwood"));
            var esteemUser2 = esteemUser.Where(x => x.Name.Contains("Eleanor")).FirstOrDefault();
            Assert.IsNotNull(esteemUser, "Eleanor Westwood is null");
        }

        [TestMethod]
        public async Task BAM_User_Get_WestwoodEleanor()
        {
            var userFilter = "Westwood, Eleanor";
            var filterByAnalyst = false;
            var groupsOnly = false;
            var maxNumberOfResults = 100;
            var fetchAll = false;
            var queryFilter = string.Format("?userFilter={0}&filterByAnalyst={1}&groupsOnly={2}&maxNumberOfResults={3}&fetchAll={4}",
                    userFilter, filterByAnalyst, groupsOnly, maxNumberOfResults, fetchAll
                );
            var queryResult = _bamClient._client.GetAsync("api/V3/User/GetUserList" + queryFilter).Result;

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

            var esteemUser = result.BAM_Users.Where(x => x.Name.Contains("Westwood"));
            var esteemUser2 = esteemUser.Where(x => x.Name.Contains("Eleanor")).FirstOrDefault();
            Assert.IsNotNull(esteemUser, "Eleanor Westwood is null");
        }

        [TestMethod]
        public async Task BAM_User_Get_WestwoodEleanor_Refactored()
        {
            var userFilter = "Britton, David";
            IBAM_UserService bamUserService = new BAM_UserService();
            var user = bamUserService.GetUser(userFilter);

            Assert.IsNotNull(user, "Eleanor Westwood is null");
            Assert.IsTrue(user.Name == userFilter, "Eleanor Westwood is null");
        }
    }
}
