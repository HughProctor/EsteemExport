using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace ServiceModel.Mappers
{
    public static class Map
    {
        public static void Init()
        {
            Mapper.Initialize(cfg => {
                cfg.AllowNullCollections = true;
                cfg.CreateMap<SCAudit, SCAuditExt>().IgnoreAllNonExisting();
                cfg.CreateMap<SCAuditDeploy, SCAuditDeployExt>().IgnoreAllNonExisting();
            });
            Mapper.AssertConfigurationIsValid();
        }

        public static List<SCAuditExt> Map_Results(List<SCAudit> records)
        {
            var mappedList = new List<SCAuditExt>();
            if (records == null || !records.Any()) return mappedList;

            mappedList = Mapper.Map<List<SCAudit>, List<SCAuditExt>>(records);
            mappedList.ForEach(item => item.Asset_Desc_Code_Split = item.Asset_Desc.ToUpper().Split(' ').ToList());

            return mappedList;
        }

        public static List<SCAuditDeployExt> Map_Results(List<SCAuditDeploy> records)
        {
            var mappedList = new List<SCAuditDeployExt>();
            if (records == null || !records.Any()) return mappedList;

            mappedList = Mapper.Map<List<SCAuditDeploy>, List<SCAuditDeployExt>>(records);
            mappedList.ForEach(item => item.Asset_Desc_Code_Split = item.Asset_Desc.ToUpper().Split(' ').ToList());

            return mappedList;
        }

    }
}
