using Newtonsoft.Json;
using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;
using ServiceModel.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceModel.Services
{
    public class BAM_CostCenterService : IBAM_CostCenterService //BAM_ApiClient, 
    {
        public BAM_ApiClient _bamclient;
        public List<TargetHardwareAssetHasCostCenter> CostCenterList { get; set; }

        public BAM_CostCenterService() : this(null)
        {
        }

        public BAM_CostCenterService(BAM_ApiClient bamclient)
        {
            _bamclient = bamclient;
            if (_bamclient == null)
            {
                _bamclient = new BAM_ApiClient();
                Task.Run(() => _bamclient.Setup()).Wait();
            }
            CostCenterList = GetCostCenterList();
        }

        //https://SCSM-Lab/api/V3/Config/GetConfigItemsByClass?userId=520e001e-b7e1-604a-4849-511ab1bc0cb3&isUserScoped=false&searchFilter=%&objectClassId=128bdb2d-f5bd-f8b6-440e-e3f7d8ab4858 
        public List<TargetHardwareAssetHasCostCenter> GetCostCenterList()
        {
            var queryFilter = "?userId=520e001e-b7e1-604a-4849-511ab1bc0cb3&isUserScoped=false&searchFilter=%&objectClassId=128bdb2d-f5bd-f8b6-440e-e3f7d8ab4858"; 
            var queryResult = _bamclient._client.GetAsync("api/V3/Config/GetConfigItemsByClass" + queryFilter).Result;

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<TargetHardwareAssetHasCostCenter>>(resultSring);
            return result;
        }
    }
}
