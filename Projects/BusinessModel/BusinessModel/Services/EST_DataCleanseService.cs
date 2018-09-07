using AutoMapper;
using BusinessModel.BusinessLogic;
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
        private ManufacturerComparer manufacturerComparer;

        public EST_DataCleanseService()
        {
            dbContext = new BAMEsteemExportContext();
            PartManufacturers = dbContext.PartManufacturers.ToList();
            PartModels = dbContext.PartModels.Where(x => x.IsInScope == true).ToList();
            manufacturerComparer = new ManufacturerComparer()
            {
                Manufacturers = PartManufacturers
            };
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


                scAuditBsm.Asset_Desc_Code = scAuditBsm.Audit_Prod_Desc_Alt;
                scAuditBsm.Asset_Desc = scAuditBsm.Audit_Prod_Desc?.Replace("HPX", "HP").Replace("TAC", "").Replace("BNL", "").Replace("MOBILE", "")
                    .Replace("WSTATION", "").Replace("WSTA", "").Replace("  ", " ").Replace("CLOUD MANAGED", "")//.Replace("ELITE", "ELITEBOOK")
                    .Trim();

                scAuditBsm.Asset_Desc_Code_Pre = string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) ? "" : scAuditBsm.Asset_Desc_Code.Substring(0, 3);
                scAuditBsm.Asset_Desc_Code_Suf = !string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) && scAuditBsm.Asset_Desc_Code?.Length > 3 ? scAuditBsm.Asset_Desc_Code.Substring(3) : "";

                scAuditBsm.Asset_Desc_Code_Split = scAuditBsm.Asset_Desc?.ToUpper()?.Split(' ').Distinct().ToList();
                //scAuditBsm.Asset_Desc_Code_Split = scAuditBsm.Asset_Desc?.ToUpper()?.Split(' ').Distinct().OrderBy(x => x, new SemiNumericComparer()).ToList();
                //scAuditBsm.Asset_Desc_Code_Split = scAuditBsm.Asset_Desc?.ToUpper()?.Split(' ').Distinct().OrderBy(x => x, manufacturerComparer).ThenByDescending(x => x, new SemiNumericComparer()).ToList();

                var model = PartModels.Where(mod => 
                    scAuditBsm.Asset_Desc_Code_Pre == mod.EsteemCode || scAuditBsm.Asset_Desc_Code_Pre == mod.EsteemCodeAlt)?.FirstOrDefault();

                /*----------------------------- If no Model exists then we're skipping the item ---- We don't want it --------------*/
                // Important Business Logic
                if (model == null)
                    return;

                scAuditBsm.Model = model?.Name ?? model?.Description ?? "";
                scAuditBsm.Audit_Part_Num_New = scAuditBsm.Audit_Part_Num?.Replace("BNL-", "").Replace("-NEW", "");
                scAuditBsm.AssetName = scAuditBsm.Audit_Prod_Desc?.Replace("BNL ", "")?.Replace("  ", " ")?.Trim();
                scAuditBsm.Manufacturer = PartManufacturers.Where(manu => scAuditBsm.Asset_Desc_Code_Split.Any(code => manu.Name?.ToUpper() == code.ToUpper() ||
                        manu.Code?.ToUpper() == code.ToUpper() || manu.CodeEsteem?.ToUpper() == code.ToUpper()
                        || manu.CodeEsteemAlt?.ToUpper() == code.ToUpper()))?.FirstOrDefault()?.Name;

                // Clean Asset Tag
                scAuditBsm.AssetTag = (bool)scAuditBsm.Audit_Ser_Num?.Contains('/') ?
                    scAuditBsm.Audit_Ser_Num.Substring(0, scAuditBsm.Audit_Ser_Num.IndexOf('/')) : scAuditBsm.Audit_Ser_Num;
                scAuditBsm.AssetTag = scAuditBsm.AssetTag.Trim();

                // Clean SerialNumber
                scAuditBsm.SerialNumber = (bool)scAuditBsm.Audit_Ser_Num?.Contains('/') ?
                    scAuditBsm.Audit_Ser_Num.Substring(scAuditBsm.Audit_Ser_Num.IndexOf('/') + 1) : scAuditBsm.Audit_Ser_Num;
                scAuditBsm.SerialNumber = scAuditBsm.SerialNumber.Trim();

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


                scAuditBsm.Asset_Desc_Code = scAuditBsm.Audit_Part_Code;
                scAuditBsm.Asset_Desc = scAuditBsm.Audit_Prod_Desc?.Replace("HPX", "HP").Replace("TAC", "").Replace("BNL", "").Replace("MOBILE", "")
                    .Replace("WSTATION", "").Replace("WSTA", "").Replace("  ", " ").Replace("CLOUD MANAGED", "")//.Replace("ELITE", "ELITEBOOK")
                    .Trim();

                scAuditBsm.Asset_Desc_Code_Pre = string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) ? "" : scAuditBsm.Asset_Desc_Code.Substring(0, 3);
                scAuditBsm.Asset_Desc_Code_Suf = !string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) && scAuditBsm.Asset_Desc_Code?.Length > 3 ? scAuditBsm.Asset_Desc_Code.Substring(3) : "";

                scAuditBsm.Asset_Desc_Code_Split = scAuditBsm.Asset_Desc?.ToUpper()?.Split(' ').Distinct().ToList();
                //scAuditBsm.Asset_Desc_Code_Split = scAuditBsm.Asset_Desc?.ToUpper()?.Split(' ').Distinct().OrderBy(x => x, new SemiNumericComparer()).ToList();
                //scAuditBsm.Asset_Desc_Code_Split = scAuditBsm.Asset_Desc?.ToUpper()?.Split(' ').Distinct().OrderBy(x => x, manufacturerComparer).ThenByDescending(x => x, new SemiNumericComparer()).ToList();
                scAuditBsm.Asset_Desc_Code_Pre = string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) ? "" : scAuditBsm.Asset_Desc_Code.Substring(0, 3);
                scAuditBsm.Asset_Desc_Code_Suf = !string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) && scAuditBsm.Asset_Desc_Code?.Length > 3 ? scAuditBsm.Asset_Desc_Code.Substring(3) : "";

                var model = PartModels.Where(mod =>
                    scAuditBsm.Asset_Desc_Code_Pre == mod.EsteemCode || scAuditBsm.Asset_Desc_Code_Pre == mod.EsteemCodeAlt)?.FirstOrDefault();

                /*----------------------------- If no Model exists then we're skipping the item ---- We don't want it --------------*/
                // Important Business Logic
                if (model == null) return;

                scAuditBsm.Model = model?.Name ?? model?.Description ?? "";
                scAuditBsm.Audit_Part_Num_New = scAuditBsm.Audit_Part_Num?.Replace("BNL-", "").Replace("-NEW", "");
                scAuditBsm.AssetName = scAuditBsm.Audit_Prod_Desc?.Replace("BNL ", "")?.Replace("  ", " ")?.Trim();
                scAuditBsm.Manufacturer = PartManufacturers.Where(t1 => scAuditBsm.Asset_Desc_Code_Split.Any(t2 => t1.Name?.ToUpper() == t2.ToUpper() ||
                        t1.Code?.ToUpper() == t2.ToUpper() || t1.CodeEsteem?.ToUpper() == t2.ToUpper()
                        || t1.CodeEsteemAlt?.ToUpper() == t2.ToUpper()))?.FirstOrDefault()?.Name;


                // Clean Asset Tag
                scAuditBsm.AssetTag = (bool)scAuditBsm.Audit_Ser_Num?.Contains('/') ? 
                    scAuditBsm.Audit_Ser_Num.Substring(0, scAuditBsm.Audit_Ser_Num.IndexOf('/')) : scAuditBsm.Audit_Ser_Num;
                scAuditBsm.AssetTag = scAuditBsm.AssetTag.Trim();

                // Clean SerialNumber
                scAuditBsm.SerialNumber = (bool)scAuditBsm.Audit_Ser_Num?.Contains('/') ? 
                    scAuditBsm.Audit_Ser_Num.Substring(scAuditBsm.Audit_Ser_Num.IndexOf('/') + 1) : scAuditBsm.Audit_Ser_Num;
                scAuditBsm.SerialNumber = scAuditBsm.SerialNumber.Trim();

                // Clean SerialNumberReturned
                scAuditBsm.SerialNumberReturned = (bool)scAuditBsm.Audit_Ser_Num_Returned?.Contains('/') ?
                    scAuditBsm.Audit_Ser_Num_Returned.Substring(scAuditBsm.Audit_Ser_Num_Returned.IndexOf('/') + 1) : scAuditBsm.Audit_Ser_Num_Returned;
                scAuditBsm.SerialNumberReturned = scAuditBsm.SerialNumberReturned.Trim();

                scAuditBsm.DisplayName = scAuditBsm.AssetName;
                var userName = (bool)scAuditBsm.Audit_User?.Contains('/') ? scAuditBsm.Audit_User.Substring(0, scAuditBsm.Audit_User.IndexOf('/')).Trim() : scAuditBsm.Audit_User;
                var userNameSplit = userName.Split(' ');
                scAuditBsm.RequestUser = userNameSplit.Length > 1 ? userNameSplit[1] + ", " + userNameSplit[0] : userName;

                scAuditBsm.CostCode = string.IsNullOrEmpty(scAuditBsm.Audit_Cost_Code) ? scAuditBsm.Audit_Cost_Code : 
                    scAuditBsm.Audit_Cost_Code.Contains('/') ? scAuditBsm.Audit_Cost_Code.Split('/')?[1] :
                        scAuditBsm.Audit_Cost_Code.Contains('\\') ? scAuditBsm.Audit_Cost_Code.Split('\\')?[1] : scAuditBsm.Audit_Cost_Code;

                returnList.Add(scAuditBsm);
            });

            return returnList;
        }

    }
}
