using Newtonsoft.Json;
using ServiceModel.Models.Esteem;
using System;

namespace ServiceModel
{
    public class BAM_ProjectionTemplate
    {
        public int TemplateId { get; set; }
        public Guid CreatedById { get; set; }
    }

    public class Projection
    {
        public int ObjectId { get; set; }
        public string DisplayName { get; set; }
        public string AssetName { get; set; }
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string HWAssetStatus { get; set; }
        [JsonIgnore]
        public EST_HWAssetStatus HWAssetStatus_Enum { get; set; }
        public string ObjectStatus { get; set; }
    }
}
