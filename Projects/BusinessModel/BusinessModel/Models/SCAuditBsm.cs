﻿using EntityModel;
using EntityModel.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessModel
{
    public class SCAuditBsm : SCAudit, ISCBaseObject
    {
        public string Audit_Part_Num_New { get; set; }
        public string Asset_Desc { get; set; }
        public string Asset_Desc_Code { get; set; }
        public string Asset_Desc_Code_New { get; set; }
        public bool Asset_Desc_Code_Changed { get; set; } = false;

        public string Asset_Desc_Code_Pre { get; set; }
        public string Asset_Desc_Code_Suf { get; set; }
        public List<string> Asset_Desc_Code_Split { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string AssetName { get; set; }
        public string DisplayName { get; set; }
        public string RequestUser { get; set; }
        public string AssetTag { get; set; }
    }
}
