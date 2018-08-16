using EntityModel.BusinessLogic;
using System.Collections.Generic;
using System.Linq;

namespace EntityModel
{
    public class SCAuditExt : SCAudit
    {
        public string Audit_Part_Num_New { get { return Audit_Part_Num.Replace("BNL-", "").Replace("-NEW", ""); } }

        private string _asset_Desc;
        public string Asset_Desc
        {
            get { return string.IsNullOrEmpty(_asset_Desc) ? _asset_Desc = Audit_Prod_Desc.Replace("BNL ", "") : _asset_Desc; }
            set { _asset_Desc = value.Replace("  ", " ").Trim(); }
        }
        public string Asset_Desc_Code { get { return Audit_Prod_Desc_Alt; } }
        public string Asset_Desc_Code_New { get; set; }
        public bool Asset_Desc_Code_Changed { get; set; } = false;

        public string Asset_Desc_Code_Pre { get { return string.IsNullOrEmpty(Asset_Desc_Code) ? "" : Asset_Desc_Code.Substring(0, 3); } }
        public string Asset_Desc_Code_Suf { get { return !string.IsNullOrEmpty(Asset_Desc_Code) && Asset_Desc_Code?.Length > 3 ? Asset_Desc_Code.Substring(3) : ""; } }

        private List<string> _asset_Desc_Code = new List<string>();
        public List<string> Asset_Desc_Code_Split
        {
            get
            {
                if (_asset_Desc_Code != null && _asset_Desc_Code.Any())
                    return _asset_Desc_Code;
                _asset_Desc_Code = Asset_Desc?.ToUpper()?.Split(' ').Distinct().ToList();
                return _asset_Desc_Code;
            }
            set { _asset_Desc_Code = value.Distinct().ToList(); }
        }

        private string _manufacturer;
        public string Manufacturer
        {
            get
            {
                if (string.IsNullOrEmpty(_manufacturer))
                {
                    var item = ManufacturerModel_Data.GetManufacturers().Where(t1 => Asset_Desc_Code_Split.Any(t2 => t1.Name?.ToUpper() == t2.ToUpper() ||
                        t1.Code?.ToUpper() == t2.ToUpper() || t1.CodeEsteem?.ToUpper() == t2.ToUpper()
                        || t1.CodeEsteemAlt?.ToUpper() == t2.ToUpper()))?.FirstOrDefault();
                    if (item != null)
                    {
                        _manufacturer = item.Name + " (" + item.Code + ")";
                        Asset_Desc_Code_New = Asset_Desc_Code_Pre + item.CodeEsteem;
                        Asset_Desc_Code_Changed = Asset_Desc_Code_New != Asset_Desc_Code;
                    }
                }
                return _manufacturer;
            }
        }
        public string Model { get; set; }
        public string SerialNumber { get { return (bool)Audit_Ser_Num?.Contains('/') ? Audit_Ser_Num.Substring(Audit_Ser_Num.IndexOf('/') + 1) : Audit_Ser_Num; } }
        public string AssetName { get { return Asset_Desc; } }
        public string DisplayName { get { return Asset_Desc; } }
        private string requestUser;
        public string RequestUser { get { return (bool)Audit_User?.Contains('/') ? Audit_User.Substring(0, Audit_User.IndexOf('/')).Trim() : Audit_User; } }
    }
}
