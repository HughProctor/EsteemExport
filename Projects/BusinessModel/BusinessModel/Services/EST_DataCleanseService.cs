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
        private List<PartManufacturer> Manufacturers;

        public EST_DataCleanseService()
        {
            dbContext = new BAMEsteemExportContext();
            Manufacturers = dbContext.PartManufacturers.ToList();
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
                var scAuditBsm = new SCAuditBsm()
                {
                    Audit_Part_Num_New = item.Audit_Part_Num?.Replace("BNL-", "").Replace("-NEW", ""),
                    AssetName = item.Audit_Prod_Desc?.Replace("BNL ", "")?.Replace("  ", " ")?.Trim(),
                };
                scAuditBsm.Asset_Desc_Code_Split = scAuditBsm.Asset_Desc?.ToUpper()?.Split(' ').Distinct().ToList();
                scAuditBsm.Manufacturer = Manufacturers.Where(t1 => scAuditBsm.Asset_Desc_Code_Split.Any(t2 => t1.Name?.ToUpper() == t2.ToUpper() ||
                        t1.Code?.ToUpper() == t2.ToUpper() || t1.CodeEsteem?.ToUpper() == t2.ToUpper()
                        || t1.CodeEsteemAlt?.ToUpper() == t2.ToUpper()))?.FirstOrDefault()?.Name;

                scAuditBsm.SerialNumber = (bool)item.Audit_Ser_Num?.Contains('/') ? item.Audit_Ser_Num.Substring(item.Audit_Ser_Num.IndexOf('/') + 1) : item.Audit_Ser_Num;
                scAuditBsm.DisplayName = scAuditBsm.AssetName;
                scAuditBsm.RequestUser = (bool)item.Audit_User?.Contains('/') ? item.Audit_User.Substring(0, item.Audit_User.IndexOf('/')).Trim() : item.Audit_User;
                returnList.Add(scAuditBsm);
            });

            return returnList;
        }

        public List<SCAuditDeployBsm> Process_SCAuditDeployList(List<SCAuditDeploy> sCAudits)
        {
            var returnList = new List<SCAuditDeployBsm>();
            sCAudits.ForEach(item =>
            {
                var scAuditBsm = new SCAuditDeployBsm()
                {
                    Audit_Part_Num_New = item.Audit_Part_Num?.Replace("BNL-", "").Replace("-NEW", ""),
                    AssetName = item.Audit_Prod_Desc?.Replace("BNL ", "")?.Replace("  ", " ")?.Trim(),
                };
                scAuditBsm.Asset_Desc_Code_Split = scAuditBsm.Asset_Desc?.ToUpper()?.Split(' ').Distinct().ToList();
                scAuditBsm.Asset_Desc_Code_Pre = string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) ? "" : scAuditBsm.Asset_Desc_Code.Substring(0, 3);
                scAuditBsm.Asset_Desc_Code_Suf = !string.IsNullOrEmpty(scAuditBsm.Asset_Desc_Code) && scAuditBsm.Asset_Desc_Code?.Length > 3 ? scAuditBsm.Asset_Desc_Code.Substring(3) : "";

                scAuditBsm.Manufacturer = Manufacturers.Where(t1 => scAuditBsm.Asset_Desc_Code_Split.Any(t2 => t1.Name?.ToUpper() == t2.ToUpper() ||
                        t1.Code?.ToUpper() == t2.ToUpper() || t1.CodeEsteem?.ToUpper() == t2.ToUpper()
                        || t1.CodeEsteemAlt?.ToUpper() == t2.ToUpper()))?.FirstOrDefault()?.Name;

                scAuditBsm.SerialNumber = (bool)item.Audit_Ser_Num?.Contains('/') ? item.Audit_Ser_Num.Substring(item.Audit_Ser_Num.IndexOf('/') + 1) : item.Audit_Ser_Num;
                scAuditBsm.DisplayName = scAuditBsm.AssetName;
                scAuditBsm.RequestUser = (bool)item.Audit_User?.Contains('/') ? item.Audit_User.Substring(0, item.Audit_User.IndexOf('/')).Trim() : item.Audit_User;
                returnList.Add(scAuditBsm);
            });

            return returnList;
        }

    }
}
