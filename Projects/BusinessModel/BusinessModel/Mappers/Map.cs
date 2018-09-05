using AutoMapper;
using BusinessModel.Models;
using EntityModel;
using ESTReporting.EntityModel.Models;
//using ESTReporting.EntityModel.Models;
using ServiceModel.Models.BAM;
using System;
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
                cfg.CreateMap<HardwareTemplate, HardwareTemplate_Full>().IgnoreAllNonExisting();
                cfg.CreateMap<HardwareTemplate_Full, HardwareTemplate>().IgnoreAllNonExisting();
                cfg.CreateMap<SCAuditBsm, BAMDataModel>().IgnoreAllNonExisting();
                cfg.CreateMap<SCAuditDeployBsm, BAMDataModel>().IgnoreAllNonExisting();
                //cfg.CreateMap<BAM_HardwareTemplate, HardwareTemplate>().IgnoreAllNonExisting();
                //cfg.CreateMap<BAM_HardwareTemplate_Full, HardwareTemplate_Full>().IgnoreAllNonExisting();
                cfg.CreateMap<BAM_ReportingBsm, BAM_Reporting>().DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<SCAuditBsm, SCAudit>().IgnoreAllNonExisting();
                cfg.CreateMap<SCAuditDeployBsm, SCAuditDeploy>().IgnoreAllNonExisting();
                cfg.CreateMap<SCAuditBsm, EST_SCAudit>().DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<SCAuditDeployBsm, EST_SCAuditDeploy>().DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<HardwareTemplate, BAM_HardwareTemplate>()
                    .ForMember(dest => dest.LastModified, opt => opt.Condition(src => src.LastModified != default(DateTime)))
                    .DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<HardwareTemplate_Full, BAM_HardwareTemplate_Full>()
                    .ForMember(dest => dest.LastModified, opt => opt.Condition(src => src.LastModified != default(DateTime)))
                    .DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<TargetHardwareAssetHasCostCenter, BAM_TargetHardwareAssetHasCostCenter>().DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<TargetHardwareAssetHasLocation, BAM_TargetHardwareAssetHasLocation>().DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<TargetHardwareAssetHasPrimaryUser, BAM_TargetHardwareAssetHasPrimaryUser>().DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<AssetStatus, BAM_AssetStatus>()
                    .ForMember(src => src.CreatedDate, opt => opt.UseValue(DateTime.Now))
                    .ForMember(src => src.BAM_Id, dest => dest.MapFrom(o => o.Id))
                    .ForMember(src => src.Id, opt => opt.Ignore())
                    .DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<HardwareAssetStatus, BAM_HardwareAssetStatus>()
                    .ForMember(src => src.BAM_Id, dest => dest.MapFrom(o => o.Id))
                    .ForMember(src => src.Id, opt => opt.Ignore())
                    .DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<HardwareAssetType, BAM_HardwareAssetType>()
                    .ForMember(src => src.BAM_Id, dest => dest.MapFrom(o => o.Id))
                    .ForMember(src => src.Id, opt => opt.Ignore())
                    .DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<ManufacturerEnum, BAM_ManufacturerEnum>()
                    .ForMember(src => src.BAM_Id, dest => dest.MapFrom(o => o.Id))
                    .ForMember(src => src.Id, opt => opt.Ignore())
                    .DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<ModelEnum, BAM_ModelEnum>()
                    .ForMember(src => src.BAM_Id, dest => dest.MapFrom(o => o.Id))
                    .ForMember(src => src.Id, opt => opt.Ignore())
                    .DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<BAM_ReportingBsm, BAM_Deployments>().DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<ServiceProgressReportBsm, ServiceProgressReport>().DefaultValues().IgnoreAllNonExisting();
                cfg.CreateMap<ServiceProgressReport, ServiceProgressReportBsm>().IgnoreAllNonExisting();
                cfg.CreateMap<BAM_Manufacturer, EST_BAM_ModelLookup>()
                    .ForMember(src => src.BAM_Name, dest => dest.MapFrom(o => o.Name))
                    .ForMember(src => src.BAM_DisplayName, dest => dest.MapFrom(o => o.DisplayName))
                    .ForMember(src => src.BAM_ManufacturerString, dest => dest.MapFrom(o => o.ManufacturerString))
                    .ForMember(src => src.BAM_ModelString, dest => dest.MapFrom(o => o.ModelString))
                    .ForMember(src => src.BAM_BaseId, dest => dest.MapFrom(o => o.BaseId))
                    .DefaultValues().IgnoreAllNonExisting();
            });
            Mapper.AssertConfigurationIsValid();
        }

        public static HardwareTemplate Map_Results(HardwareTemplate_Full record)
        {
            return Mapper.Map<HardwareTemplate_Full, HardwareTemplate>(record);
        }

        public static HardwareTemplate_Full Map_Results(HardwareTemplate record)
        {
            return Mapper.Map<HardwareTemplate, HardwareTemplate_Full>(record);
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

        public static List<BAM_Reporting> Map_Results(List<BAM_ReportingBsm> records)
        {
            return Mapper.Map<List<BAM_ReportingBsm>, List<BAM_Reporting>>(records);
        }

        public static List<BAM_Deployments> Map_Results(List<BAM_ReportingBsm> records, bool bills)
        {
            return Mapper.Map<List<BAM_ReportingBsm>, List<BAM_Deployments>>(records);
        }

        public static ServiceProgressReport Map_Results(ServiceProgressReportBsm record)
        {
            return Mapper.Map<ServiceProgressReportBsm, ServiceProgressReport>(record);
        }

        public static ServiceProgressReportBsm Map_Results(ServiceProgressReport record)
        {
            return Mapper.Map<ServiceProgressReport, ServiceProgressReportBsm>(record);
        }

        public static List<EST_BAM_ModelLookup> Map_Results(List<BAM_Manufacturer> records)
        {
            return Mapper.Map<List<BAM_Manufacturer>, List<EST_BAM_ModelLookup>>(records);
        }

    }
}
