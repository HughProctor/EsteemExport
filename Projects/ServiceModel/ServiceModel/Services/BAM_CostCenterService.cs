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
        }

        /// Set Cost Center
        /// 
        // Target_HardwareAssetHasCostCenter

    }
}
