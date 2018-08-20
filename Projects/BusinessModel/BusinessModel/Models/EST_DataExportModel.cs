using EntityModel;
using System.Collections.Generic;

namespace BusinessModel.Models
{
    public class EST_DataExportModel
    {
        public List<SCAuditExt> NewItemList { get; set; }
        public List<SCAuditExt> LocationChangeList { get; set; }
        public List<SCAuditExt> AssetTagChangeList { get; set; }
        public List<SCAuditDeployExt> DeployedToBAMUserList { get; set; }
        public List<SCAuditDeployExt> ReturnedFromBAMUserList { get; set; }
    }
}
