﻿using EntityModel;
using System.Collections.Generic;

namespace BusinessModel.Models
{
    public class EST_DataExportModel
    {
        public List<SCAuditBsm> NewItemList { get; set; }
        public List<SCAuditBsm> LocationChangeList { get; set; }
        public List<SCAuditBsm> ReturnedFromBAMList { get; set; }
        public List<SCAuditBsm> RetiredAssetList { get; set; }
        public List<SCAuditBsm> DisplosedAssetList { get; set; }
        public List<SCAuditBsm> AssetTagChangeList { get; set; }
        public List<SCAuditDeployBsm> DeployedToBAMUserList { get; set; }
        public List<SCAuditDeployBsm> ReturnedFromBAMUserList { get; set; }
        public List<SCAuditDeployBsm> SwappedAssetList { get; set; }
    }
}
