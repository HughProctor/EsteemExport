using Newtonsoft.Json;
using ServiceModel.Models.BAM;
using ServiceModel.Services.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceModel.Services
{
    public class BAM_UserService : BAM_ApiClient, IBAM_UserService
    {
        public BAM_UserService()
        {
            Task.Run(() => this.Setup()).Wait(); 
        }

        public BAM_User GetUser(string userName)
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

    }
}
