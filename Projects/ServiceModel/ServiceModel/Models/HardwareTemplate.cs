using Newtonsoft.Json;
using ServiceModel.Models.BAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Models
{
    public class HardwareTemplate_Json
    {
        [JsonProperty("formJson")]
        public FormJson formJson { get; set; }
    }

    public class FormJson
    {
        [JsonProperty("isDirty")]
        public bool IsDirty { get; set; }
        [JsonProperty("current")]
        public HardwareTemplate Current { get; set; }
        [JsonProperty("original")]
        public HardwareTemplate Original { get; set; }
    }
}
