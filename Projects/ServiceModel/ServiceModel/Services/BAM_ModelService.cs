using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;
using ServiceModel.Services.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceModel.Services
{
    public class BAM_ModelService : IBAM_ModelService //BAM_ApiClient, 
    {
        public BAM_ApiClient _bamclient;

        public BAM_ModelService() : this(null)
        {
        }

        public BAM_ModelService(BAM_ApiClient bamclient)
        {
            _bamclient = bamclient;
            if (_bamclient == null)
            {
                _bamclient = new BAM_ApiClient();
                Task.Run(() => _bamclient.Setup()).Wait();
            }
        }

        public List<BAM_Manufacturer> GetBAM_ModelDescriptions()
        {
            var queryFilter = "?classId=98fbecc7-f76a-dcd6-7b17-62cee34e38de&columnNames=Name%2CModelString%2CManufacturerString%2CType%2C";
            var queryResult = _bamclient._client.GetAsync("Search/GetSearchObjectsWithEnumObjectByClassId" + queryFilter).Result;

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;
            var jToken = JObject.Parse(resultSring)["Data"];
            var resultTemp = jToken.ToObject<List<BAM_Manufacturer>>();
            //var resultTemp = JsonConvert.DeserializeObject<List<BAM_Manufacturer>>(resultSring);
            return resultTemp;
        }
    }

    //private class BAM_Manufacturer
    //{
    //    public object[] Data { get; set; }
    //    public string Total { get; set; }
    //}
}
