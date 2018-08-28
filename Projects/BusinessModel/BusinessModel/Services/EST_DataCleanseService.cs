using AutoMapper;
using BusinessModel.Mappers;
using BusinessModel.Models;
using BusinessModel.Services.Abstract;
using EntityModel;
using ESTReporting.EntityModel.Context;
using ESTReporting.EntityModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessModel.Services
{
    public class EST_DataCleanseService : IEST_DataCleanseService
    {
        private BAMEsteemExportContext dbContext;
        private List<PartManufacturer> PartManufacturers;
        private List<PartModel> PartModels;

        public EST_DataCleanseService()
        {
            dbContext = new BAMEsteemExportContext();
            PartManufacturers = dbContext.PartManufacturers.Where(x => x.Name == "Cables").ToList();
            PartModels = dbContext.PartModels.ToList();
        }

        public List<BAMDataModel> ProcessSCAuditList(List<SCAudit> sCAudits)
        {
            return Map.Map_Results(Process_SCAuditList(sCAudits));
        }

        public List<SCAuditBsm> Process_SCAuditList(List<SCAudit> sCAudits)
        {
            var returnList = new List<SCAuditBsm>();
            sCAudits.ForEach(item =>
            {
                var scAuditBsm = new SCAuditBsm();
                scAuditBsm = Map.Map_Results(item);

                scAuditBsm.Audit_Part_Num_New = scAuditBsm.Audit_Part_Num?.Replace("BNL-", "").Replace("-NEW", "");
                scAuditBsm.AssetName = scAuditBsm.Audit_Prod_Desc?.Replace("BNL ", "")?.Replace("  ", " ")?.Trim();
                scAuditBsm.Asset_Desc = scAuditBsm.Audit_Prod_Desc?.Replace("BNL ", "");

                scAuditBsm.Asset_Desc_Code = scAuditBsm.Audit_Prod_Desc_Alt;

                scAuditBsm.Asset_Desc_Code_Pre = string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) ? "" : scAuditBsm.Asset_Desc_Code.Substring(0, 3);
                scAuditBsm.Asset_Desc_Code_Suf = !string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) && scAuditBsm.Asset_Desc_Code?.Length > 3 ? scAuditBsm.Asset_Desc_Code.Substring(3) : "";

                scAuditBsm.Asset_Desc_Code_Split = scAuditBsm.Asset_Desc?.ToUpper()?.Split(' ').Distinct().ToList();

                scAuditBsm.Manufacturer = PartManufacturers.Where(manu => scAuditBsm.Asset_Desc_Code_Split.Any(code => manu.Name?.ToUpper() == code.ToUpper() ||
                        manu.Code?.ToUpper() == code.ToUpper() || manu.CodeEsteem?.ToUpper() == code.ToUpper()
                        || manu.CodeEsteemAlt?.ToUpper() == code.ToUpper()))?.FirstOrDefault()?.Name;

                var model = PartModels.Where(mod => 
                    scAuditBsm.Asset_Desc_Code_Pre == mod.EsteemCode || scAuditBsm.Asset_Desc_Code_Pre == mod.EsteemCodeAlt)?.FirstOrDefault();
                //if (model != null)
                scAuditBsm.Model = model?.Name ?? model?.Description ?? "";

                scAuditBsm.SerialNumber = (bool)item.Audit_Ser_Num?.Contains('/') ? item.Audit_Ser_Num.Substring(item.Audit_Ser_Num.IndexOf('/') + 1) : (item.Audit_Ser_Num ?? "");
                scAuditBsm.DisplayName = scAuditBsm.AssetName;
                var userName = (bool)scAuditBsm.Audit_User?.Contains('/') ? scAuditBsm.Audit_User.Substring(0, scAuditBsm.Audit_User.IndexOf('/')).Trim() : (scAuditBsm.Audit_User ?? "");
                var userNameSplit = userName.Split(' ');
                scAuditBsm.RequestUser = userNameSplit.Length > 1 ? userNameSplit[1] + ", " + userNameSplit[0] : userName;
                returnList.Add(scAuditBsm);
            });

            return returnList;
        }

        public List<SCAuditDeployBsm> Process_SCAuditDeployList(List<SCAuditDeploy> sCAudits)
        {
            var returnList = new List<SCAuditDeployBsm>();
            sCAudits.ForEach(item =>
            {
                var scAuditBsm = new SCAuditDeployBsm();
                scAuditBsm = Map.Map_Results(item);

                scAuditBsm.Audit_Part_Num_New = scAuditBsm.Audit_Part_Num?.Replace("BNL-", "").Replace("-NEW", "");
                scAuditBsm.AssetName = scAuditBsm.Audit_Prod_Desc?.Replace("BNL ", "")?.Replace("  ", " ")?.Trim();
                scAuditBsm.Asset_Desc = scAuditBsm.Audit_Prod_Desc?.Replace("BNL ", "");

                scAuditBsm.Asset_Desc_Code = scAuditBsm.Audit_Part_Code;

                scAuditBsm.Asset_Desc_Code_Pre = string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) ? "" : scAuditBsm.Asset_Desc_Code.Substring(0, 3);
                scAuditBsm.Asset_Desc_Code_Suf = !string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) && scAuditBsm.Asset_Desc_Code?.Length > 3 ? scAuditBsm.Asset_Desc_Code.Substring(3) : "";

                scAuditBsm.Asset_Desc_Code_Split = scAuditBsm.Asset_Desc?.ToUpper()?.Split(' ').Distinct().ToList();
                scAuditBsm.Asset_Desc_Code_Pre = string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) ? "" : scAuditBsm.Asset_Desc_Code.Substring(0, 3);
                scAuditBsm.Asset_Desc_Code_Suf = !string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) && scAuditBsm.Asset_Desc_Code?.Length > 3 ? scAuditBsm.Asset_Desc_Code.Substring(3) : "";

                scAuditBsm.Manufacturer = PartManufacturers.Where(t1 => scAuditBsm.Asset_Desc_Code_Split.Any(t2 => t1.Name?.ToUpper() == t2.ToUpper() ||
                        t1.Code?.ToUpper() == t2.ToUpper() || t1.CodeEsteem?.ToUpper() == t2.ToUpper()
                        || t1.CodeEsteemAlt?.ToUpper() == t2.ToUpper()))?.FirstOrDefault()?.Name;

                var model = PartModels.Where(mod =>
                    scAuditBsm.Asset_Desc_Code_Pre == mod.EsteemCode || scAuditBsm.Asset_Desc_Code_Pre == mod.EsteemCodeAlt)?.FirstOrDefault();
                scAuditBsm.Model = model?.Name ?? model?.Description ?? "";

                scAuditBsm.SerialNumber = (bool)scAuditBsm.Audit_Ser_Num?.Contains('/') ? scAuditBsm.Audit_Ser_Num.Substring(scAuditBsm.Audit_Ser_Num.IndexOf('/') + 1) : scAuditBsm.Audit_Ser_Num;
                scAuditBsm.DisplayName = scAuditBsm.AssetName;
                var userName = (bool)scAuditBsm.Audit_User?.Contains('/') ? scAuditBsm.Audit_User.Substring(0, scAuditBsm.Audit_User.IndexOf('/')).Trim() : scAuditBsm.Audit_User;
                var userNameSplit = userName.Split(' ');
                scAuditBsm.RequestUser = userNameSplit.Length > 1 ? userNameSplit[1] + ", " + userNameSplit[0] : userName;

                //scAuditBsm.CostCode = scAuditBsm.Audit_Cost_Code?
                returnList.Add(scAuditBsm);
            });

            return returnList;
        }

    }
}
