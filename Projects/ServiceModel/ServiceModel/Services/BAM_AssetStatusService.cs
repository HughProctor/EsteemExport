using Newtonsoft.Json;
using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;
using ServiceModel.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceModel.Services
{
    public class BAM_AssetStatusService : BAM_ApiClient, IBAM_AssetStatusService
    {
        public BAM_AssetStatusService()
        {
            Task.Run(() => this.Setup()).Wait(); 
        }

        public AssetStatus GetAssetStatusTemplate2(EST_HWAssetStatus assetStatus)
        {
            var id = "6b7304c4-1b09-bffc-3fe3-1cfd3eb630cb";
            var itemFiler = EST_HWAssetStatus.NewItem.ToDescriptionString(); 
            var flatten = true;

            var queryFilter = string.Format("?id={0}&itemFilter={1}&Flatten={2}",
                id, itemFiler, flatten);
            var queryResult = _client.GetAsync("Enum/GetList" + queryFilter).Result;

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;

            var resultTemp = JsonConvert.DeserializeObject<List<AssetStatus>>(resultSring);
            var result = new BAM_AssetStatusList()
            {
                AssetStatuses = resultTemp.OrderBy(x => x.Name).ToList()
            };
            var newItem = result.AssetStatuses.Where(x => x.Name == assetStatus.ToDescriptionString()).FirstOrDefault();
            return newItem;
        }

        public HardwareAssetStatus GetAssetStatusTemplate(EST_HWAssetStatus assetStatus)
        {
            var id = "6b7304c4-1b09-bffc-3fe3-1cfd3eb630cb";
            var itemFiler = EST_HWAssetStatus.NewItem.ToDescriptionString();
            var flatten = true;

            var queryFilter = string.Format("?id={0}&itemFilter={1}&Flatten={2}",
                id, itemFiler, flatten);
            var queryResult = _client.GetAsync("Enum/GetList" + queryFilter).Result;

            var resultSring = queryResult.Content.ReadAsStringAsync().Result;

            var resultTemp = JsonConvert.DeserializeObject<List<HardwareAssetStatus>>(resultSring);
            var result = new BAM_AssetStatusList()
            {
                HardwareAssetStatuses = resultTemp.OrderBy(x => x.Name).ToList()
            };
            var newItem = result.HardwareAssetStatuses.Where(x => x.Name == assetStatus.ToDescriptionString()).FirstOrDefault();
            return newItem;
        }
    }
}
