using EntityModel.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityModel
{
    public class SCPart
    {
        public string Asset_Part_Num { get; set; }
        private string _asset_Desc;
        public string Asset_Desc {
            get { return _asset_Desc; }
            set { _asset_Desc = value.Replace("  ", " ").Trim(); } }
        public string Asset_PartType { get; set; }
        public DateTime Asset_Last_Update { get; set; }
        public string Asset_Desc_Code { get; set; }
        public string Asset_Desc_Code_New { get; set; }
        public bool Asset_Desc_Code_Changed { get; set; } = false;
        public string Asset_Desc_Code_Pre { get { return string.IsNullOrEmpty(Asset_Desc_Code) ? "" : Asset_Desc_Code.Substring(0, 3); } }
        public string Asset_Desc_Code_Suf { get { return !string.IsNullOrEmpty(Asset_Desc_Code) && Asset_Desc_Code?.Length > 3 ? Asset_Desc_Code.Substring(3) : ""; } }
        private List<string> _asset_Desc_Code;
        public List<string> Asset_Desc_Code_Split {
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
        public string Manufacturer {
            get {
                if (string.IsNullOrEmpty(_manufacturer))
                {
                    var item = TestData.GetManufacturers().Where(t1 => Asset_Desc_Code_Split.Any(t2 => t1.Name?.ToUpper() == t2.ToUpper() || 
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
                //Asset_Desc_Code_Split.Where(t2 => TestData.GetManufacturers().Any(t1 => t2.Contains(t1.Code))).FirstOrDefault()
                //    return "Hewlet Packard (HP)";
                //else if (Asset_Desc_Code_Split.Contains("DELL"))
                //    return "Dell (DELL)";
                //else if (Asset_Desc_Code_Split.Contains("IBM"))
                //    return "IBM (IBM)";
                //else if (Asset_Desc_Code_Split.Contains("LEN"))
                //    return "Lenovo (LEN)";
                //else if (Asset_Desc_Code_Split.Contains("ACER"))
                //    return "ACER (ACER)";
                //else if (Asset_Desc_Code_Split.Contains("DIG"))
                //    return "DigiPos";
                //else if (Asset_Desc_Code_Split.Contains("J2X"))
                //    return "J2 Retailing (J2X)";
                //else return "";
            }
        }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string AssetName { get; set; }
        public string DisplayName { get; set; }
    }
}
