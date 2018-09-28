using BusinessModel.Mappers;
using BusinessModel.Models;
using BusinessModel.Services.Abstract;
using EntityModel.Repository.Abstract;
using ESTReporting.EntityModel.Context;
using ESTReporting.EntityModel.Models;
using MoreLinq;
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
        List<EST_BAM_ModelLookupBsm> _eST_BAM_Models;

        public EST_BAM_ModelLookupService()
        {
            bam_ApiClient = new BAM_ApiClient();
            Task.Run(() => bam_ApiClient.Setup()).Wait();

            _dbContext = new BAMEsteemExportContext();
            _eST_BAM_Models = GetBAM_ManufacturerList();
            //_bAM_ModelService = new BAM_ModelService();
            //_estService = new EST_Service();
        }

        public List<EST_BAM_ModelLookupBsm> GetBAM_Manufacturers() //List<BAM_Manufacturer>
        {
            var bamModelList = _bAM_ModelService.GetBAM_ModelDescriptions();
            var modelLookupList = Map.Map_Results(bamModelList).OrderBy(x => x.BAM_Name).ToList();
            modelLookupList.ForEach(item =>
            {
                _dbContext.EST_BAM_ModelLookup.AddOrUpdate(item);
            });
            _dbContext.SaveChanges();
            var returnList = Map.Map_Results(modelLookupList);

            return returnList;
        }

        public List<TempModel> GetEST_Manufacturers(IQueryBuilder queryBuilder) //List<BAM_Manufacturer>
        {
            var dataExport = _estService.GetExportData(queryBuilder);
            var modelDescriptions = dataExport.NewItemList.DistinctBy(x => x.Asset_Desc).Select(x => new TempModel { ModelName = x.Asset_Desc }).ToList();
            //modelDescriptions.AddRange(dataExport.AssetTagChangeList.DistinctBy(x => x.Audit_Prod_Desc).Select(x => new TempModel { ModelName = x.Audit_Prod_Desc }).ToList());
            //modelDescriptions.AddRange(dataExport.DeployedToBAMUserList.DistinctBy(x => x.Audit_Prod_Desc).Select(x => new TempModel { ModelName = x.Audit_Prod_Desc }).ToList());
            //modelDescriptions.AddRange(dataExport.RetiredAssetList.DistinctBy(x => x.Audit_Prod_Desc).Select(x => new TempModel { ModelName = x.Audit_Prod_Desc }).ToList());
            //modelDescriptions.AddRange(dataExport.ReturnedFromBAMList.DistinctBy(x => x.Audit_Prod_Desc).Select(x => new TempModel { ModelName = x.Audit_Prod_Desc }).ToList());
            //modelDescriptions.AddRange(dataExport.SwappedAssetList.DistinctBy(x => x.Audit_Prod_Desc).Select(x => new TempModel { ModelName = x.Audit_Prod_Desc }).ToList());
            return modelDescriptions;
        }

        public List<EST_BAM_ModelLookupBsm> GetBAM_ManufacturerList() //List<BAM_Manufacturer>
        {
            var modelLookupList = _dbContext.EST_BAM_ModelLookup.Where(x => x.IsActive == 1).ToList();
            var returnList = Map.Map_Results(modelLookupList);
            return returnList;
        }



        //public void SetManufacturer(HardwareTemplate bamTemplate)
        //{
        //    //var dataExport = _estService.GetExportData(queryBuilder);
        //    var modelDescriptions = dataExport.NewItemList.Select(x => new { ModelName = x.Audit_Prod_Desc }).Distinct();

        //    throw new NotImplementedException();
        //}

        public HardwareTemplate_Full SetModelData(HardwareTemplate_Full bamTemplate, string modelName, out bool success)
        {
            EST_BAM_ModelLookupBsm modelItem = null;
            success = false;
            foreach (var item in _eST_BAM_Models)
            {
                if (modelItem != null) break;
                if (item.BAM_Name == modelName) { modelItem = item; break; }
                var itemSplit = item.BAM_Name.ToUpper().Split(' ');
                var count = itemSplit.Length;
                var weight = 0;
                for (var i = 0; i < itemSplit.Length; i++)
                {
                    if (modelName.Contains(itemSplit[i])) weight++;
                };
                if (weight == 0) continue;
                if (weight == count) { modelItem = item; break; }
                if ((weight + 1) == count) { modelItem = item; break; }
                if ((weight + 2) == count && weight >= 3) { modelItem = item; break; }
            }
            if (modelItem != null) success = true;
            bamTemplate.Manufacturer = modelItem?.BAM_ManufacturerString;
            bamTemplate.Model = modelItem?.BAM_ModelString;
            bamTemplate.Target_HardwareAssetHasCatalogItem = new Target_HardwareAssetHasCatalogItem()
            {
                // Substring ;
                Id = modelItem?.BAM_BaseId
            }; 
            bamTemplate.HardwareAssetType = new HardwareAssetType()
            {
                Id = string.IsNullOrEmpty(modelItem?.BAM_ModelType) ? "" : 
                    !modelItem.BAM_ModelType.Contains(';') ? modelItem?.BAM_ModelType :
                        modelItem?.BAM_ModelType.Substring(0, modelItem.BAM_ModelType.IndexOf(';'))
            };
            return bamTemplate;
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

    public class TempModel
    {
        public string ModelName { get; set; }
    }
}
