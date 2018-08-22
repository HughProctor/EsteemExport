using EntityModel.BusinessLogic;
using EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityModel
{
    public class SCAuditDeployExt : SCAuditDeploy, ISCBaseObject
    {
        public string Audit_Part_Num_New { get { return Audit_Part_Num.Replace("BNL-", "").Replace("-NEW", ""); } }

        private string _asset_Desc;
        public string Asset_Desc
        {
            get { return string.IsNullOrEmpty(_asset_Desc) ? _asset_Desc = Audit_Prod_Desc.Replace("BNL ", "") : _asset_Desc; }
            set { _asset_Desc = value.Replace("  ", " ").Trim(); }
        }
        public string Asset_Desc_Code { get { return Audit_Part_Code; } }
        public string Asset_Desc_Code_New { get; set; }
        public bool Asset_Desc_Code_Changed { get; set; } = false;

        public string Asset_Desc_Code_Pre { get { return string.IsNullOrEmpty(Asset_Desc_Code) ? "" : Asset_Desc_Code.Substring(0, 3); } }
        public string Asset_Desc_Code_Suf { get { return !string.IsNullOrEmpty(Asset_Desc_Code) && Asset_Desc_Code?.Length > 3 ? Asset_Desc_Code.Substring(3) : ""; } }

        private List<string> _asset_Desc_Code = new List<string>();
        private bool _orderedSplit = false;
        public List<string> Asset_Desc_Code_Split
        {
            get
            {
                if (_asset_Desc_Code != null && _asset_Desc_Code.Any())
                {
                    if (!_orderedSplit)
                    {
                        _asset_Desc_Code = _asset_Desc_Code.OrderBy(x => x, new ManufacturerComparer()).ThenByDescending(x => x, new SemiNumericComparer()).ToList();
                        _orderedSplit = true;
                    }
                    return _asset_Desc_Code;
                }
                _asset_Desc_Code = Asset_Desc?.ToUpper()?.Split(' ').Distinct().ToList();
                if (!_orderedSplit)
                {
                    _asset_Desc_Code = _asset_Desc_Code.OrderBy(x => x, new ManufacturerComparer()).ThenByDescending(x => x, new SemiNumericComparer()).ToList();
                    _orderedSplit = true;
                }
                return _asset_Desc_Code;
            }
            set { _asset_Desc_Code = value.Distinct().ToList(); }
        }

        private string _manufacturerCode;
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
                        _manufacturerCode = item.Code;
                        //if (string.IsNullOrEmpty(_model))
                        //{
                        //    List<string> temp = new List<string>();
                        //    Asset_Desc_Code_Split.ForEach(x =>
                        //    {
                        //        if (x != _manufacturerCode) temp.Add(x);
                        //    });
                        //    _model = temp.Count > 0 ? String.Join(" ", temp) : "";
                        //}

                        _manufacturer = item.Name; // + " (" + item.Code + ")";
                        Asset_Desc_Code_New = Asset_Desc_Code_Pre + item.CodeEsteem;
                        Asset_Desc_Code_Changed = Asset_Desc_Code_New != Asset_Desc_Code;
                    }
                }
                return _manufacturer;
            }
        }
        private string _model;
        public string Model { get { _model = String.Join(" ", Asset_Desc_Code_Split); return _model; } set { _model = value; } }
        public string SerialNumber { get { return (bool)Audit_Ser_Num?.Contains('/') ? Audit_Ser_Num.Substring(Audit_Ser_Num.IndexOf('/') + 1) : Audit_Ser_Num; } }
        public string AssetName { get { return (bool)Audit_Ser_Num?.Contains('/') ? Audit_Ser_Num.Substring(0, Audit_Ser_Num.IndexOf('/')) : Audit_Ser_Num; } }
        public string DisplayName { get { return String.Join(" ", Asset_Desc_Code_Split); } }
        private string requestUser;
        public string RequestUser
        {
            get
            {
                var userName = (bool)Audit_User?.Contains('/') ? Audit_User.Substring(0, Audit_User.IndexOf('/')).Trim() : Audit_User;
                var userNameSplit = userName.Split(' ');
                return userNameSplit.Length > 1 ? userNameSplit[1] + ", " + userNameSplit[0] : userName;
            }
        }
        public HWAssetStatus HWAssetStatus { get; set; }

        //private string requestUserFirstName;
        //public string RequestUserFirstName { get { return (bool)RequestUser?.Contains(' ') ? RequestUser.Split(' ')[0] : Audit_User; } }
        //private string requestUserLastName;
        //public string RequestUserlastName { get { return (bool)RequestUser?.Contains(' ') ? RequestUser.Split(' ')[1] : Audit_User; } }
    }
}
