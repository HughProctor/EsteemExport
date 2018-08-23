using AutoMapper;
using BusinessModel.Models;
using EntityModel;
using ServiceModel.Models.BAM;
using System.Collections.Generic;
using System.Linq;

namespace BusinessModel.Mappers
{
    public static class Map
    {
        public static void Init()
        {
            Mapper.Initialize(cfg => {
                cfg.AllowNullCollections = true;
                cfg.CreateMap<SCAudit, SCAuditBsm>().IgnoreAllNonExisting();
                cfg.CreateMap<SCAuditDeploy, SCAuditDeployBsm>().IgnoreAllNonExisting();
                cfg.CreateMap<BAM_HardwareTemplate, BAM_HardwareTemplate_Full>().IgnoreAllNonExisting();
                cfg.CreateMap<BAM_HardwareTemplate_Full, BAM_HardwareTemplate>().IgnoreAllNonExisting();
                cfg.CreateMap<SCAuditBsm, BAMDataModel>().IgnoreAllNonExisting();
                cfg.CreateMap<SCAuditDeployBsm, BAMDataModel>().IgnoreAllNonExisting();

            });
            Mapper.AssertConfigurationIsValid();
        }

        //public static List<SCAuditExt> Map_Results(List<SCAudit> records)
        //{
        //    var mappedList = new List<SCAuditExt>();
        //    if (records == null || !records.Any()) return mappedList;

        //    mappedList = Mapper.Map<List<SCAudit>, List<SCAuditExt>>(records);
        //    mappedList.ForEach(item => item.Asset_Desc_Code_Split = item.Asset_Desc.ToUpper().Split(' ').ToList());

        //    return mappedList;
        //}

        //public static List<SCAuditDeployExt> Map_Results(List<SCAuditDeploy> records)
        //{
        //    var mappedList = new List<SCAuditDeployExt>();
        //    if (records == null || !records.Any()) return mappedList;

        //    mappedList = Mapper.Map<List<SCAuditDeploy>, List<SCAuditDeployExt>>(records);
        //    mappedList.ForEach(item => item.Asset_Desc_Code_Split = item.Asset_Desc.ToUpper().Split(' ').ToList());

        //    return mappedList;
        //}

        public static BAM_HardwareTemplate Map_Results(BAM_HardwareTemplate_Full record)
        {
            return Mapper.Map<BAM_HardwareTemplate_Full, BAM_HardwareTemplate>(record);
        }

        public static BAM_HardwareTemplate_Full Map_Results(BAM_HardwareTemplate record)
        {
            return Mapper.Map<BAM_HardwareTemplate, BAM_HardwareTemplate_Full>(record);
        }

        public static SCAuditBsm Map_Results(SCAudit record)
        {
            return Mapper.Map<SCAudit, SCAuditBsm>(record);
        }

        public static SCAuditDeployBsm Map_Results(SCAuditDeploy record)
        {
            return Mapper.Map<SCAuditDeploy, SCAuditDeployBsm>(record);
        }

        public static BAMDataModel Map_Results(SCAuditBsm record)
        {
            return Mapper.Map<SCAuditBsm, BAMDataModel>(record);
        }

        public static BAMDataModel Map_Results(SCAuditDeployBsm record)
        {
            return Mapper.Map<SCAuditDeployBsm, BAMDataModel>(record);
        }

        public static List<BAMDataModel> Map_Results(List<SCAuditBsm> record)
        {
            return Mapper.Map<List<SCAuditBsm>, List<BAMDataModel>>(record);
        }

        public static List<BAMDataModel> Map_Results(List<SCAuditDeployBsm> record)
        {
            return Mapper.Map<List<SCAuditDeployBsm>, List<BAMDataModel>>(record);
        }

    }
}
