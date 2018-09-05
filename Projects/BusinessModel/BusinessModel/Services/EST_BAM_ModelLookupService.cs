using BusinessModel.Mappers;
using BusinessModel.Services.Abstract;
using EntityModel.Repository.Abstract;
using ESTReporting.EntityModel.Context;
using ESTReporting.EntityModel.Models;
using ServiceModel.Models.BAM;
using ServiceModel.Services;
using ServiceModel.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Services
{
    public class EST_BAM_ModelLookupService : IEST_BAM_ModelLookupService
    {
        BAM_ApiClient bam_ApiClient;
        IBAM_ModelService _bAM_ModelService;
        IEST_Service _estService;
        BAMEsteemExportContext _dbContext;

        public EST_BAM_ModelLookupService()
        {
            bam_ApiClient = new BAM_ApiClient();
            Task.Run(() => bam_ApiClient.Setup()).Wait();

            _dbContext = new BAMEsteemExportContext();
            _bAM_ModelService = new BAM_ModelService();
            _estService = new EST_Service();
        }

        public void GetBAM_Manufacturers() //List<BAM_Manufacturer>
        {
            var bamModelList = _bAM_ModelService.GetBAM_ModelDescriptions();
            var modelLookupList = Map.Map_Results(bamModelList).OrderBy(x => x.BAM_Name).ToList();
            modelLookupList.ForEach(item =>
            {
                _dbContext.EST_BAM_ModelLookup.AddOrUpdate(item);
            });
        }

        public void GetEST_Manufacturers(IQueryBuilder queryBuilder) //List<BAM_Manufacturer>
        {
            var dataExport = _estService.GetExportData(queryBuilder);
            var modelDescriptions = dataExport.NewItemList.Select(x => new { ModelName = x.Audit_Prod_Desc }).Distinct();

        }

        //public void SetManufacturer(HardwareTemplate bamTemplate)
        //{
        //    //var dataExport = _estService.GetExportData(queryBuilder);
        //    var modelDescriptions = dataExport.NewItemList.Select(x => new { ModelName = x.Audit_Prod_Desc }).Distinct();

        //    throw new NotImplementedException();
        //}

        public HardwareTemplate_Full SetModelData(HardwareTemplate_Full bamTemplate)
        {
            var modelItem = new EST_BAM_ModelLookup();
            bamTemplate.Manufacturer = modelItem.BAM_ManufacturerString;
            bamTemplate.Model = modelItem.BAM_ModelString;
            bamTemplate.Target_HardwareAssetHasCatalogItem = new Target_HardwareAssetHasCatalogItem()
            {
                // Substring ;
                Id = modelItem.BAM_BaseId
            }; 
            bamTemplate.HardwareAssetType = new HardwareAssetType()
            {
                Id = modelItem.BAM_ModelType.Substring(0, modelItem.BAM_ModelType.IndexOf(';'))
            };
            return bamTemplate;
        }

        void IEST_BAM_ModelLookupService.SetModelData(HardwareTemplate_Full bamTemplate)
        {
            throw new NotImplementedException();
        }

        //public void SetType(HardwareTemplate bamTemplate)
        //{
        //    bamTemplate  
        //    //bamTemplate.HardwareAssetType = new HardwareAssetType()
        //    //{
        //    //    Id = "b4a14ffd-52c8-064f-c936-67616c245b35",
        //    //    Name = "Computer"
        //    //};
        //    throw new NotImplementedException();
        //}
    }

}
