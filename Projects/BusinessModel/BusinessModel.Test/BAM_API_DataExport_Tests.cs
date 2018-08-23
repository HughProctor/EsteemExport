using BusinessModel.Services;
using EntityModel;
using EntityModel.Repository;
using Infrastructure.FileExport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceModel.Models.BAM;
using ServiceModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Test
{
    [TestClass]
    public class BAM_API_DataExport_Tests
    {
        private string _typePrefix = "BAMAPI_";

        [TestMethod]
        public void B01_ExportData()
        {
            var estService = new EST_Service();
            var hardwareAssetService = new BAM_HardwareAssetServices();
            var apiTemplateList = new List<BAM_HardwareTemplate_Full>();
            var notExistInBam = new List<SCAuditBsm>();

            var queryBuilder = new QueryBuilder
            {
                StartDateString = "01/01/2017",
                EndDateString = "01/01/2018",
                PageCount = 1000000
            };

            // Get Records from Esteem System
            var dataExport = estService.GetExportData(queryBuilder);
            //dataExport.NewItemList.ForEach(asset =>
            //{
            //    var bamAssetList = hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
            //    if (bamAssetList != null) apiTemplateList.Add(bamAssetList);
            //});
            //dataExport.AssetTagChangeList.ForEach(asset =>
            //{
            //    var bamAssetList = hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
            //    if (bamAssetList != null) apiTemplateList.Add(bamAssetList);
            //});
            dataExport.LocationChangeList.Take(50).ToList().ForEach(asset =>
            {
                
                var bamAssetList = hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (bamAssetList != null) apiTemplateList.Add(bamAssetList);
                else notExistInBam.Add(asset);
            });
            //dataExport.DeployedToBAMUserList.ForEach(asset =>
            //{
            //    var bamAssetList = hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
            //    if (bamAssetList != null) apiTemplateList.Add(bamAssetList);
            //});
            //dataExport.ReturnedFromBAMUserList.ForEach(asset =>
            //{
            //    var bamAssetList = hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
            //    if (bamAssetList != null) apiTemplateList.Add(bamAssetList);
            //});

            JSON_FileExport.WriteFile(_typePrefix + "B01_ExportData", apiTemplateList, apiTemplateList.Count);
            JSON_FileExport.WriteFile(_typePrefix + "B01_ExportData_NotExistBAM", notExistInBam, notExistInBam.Count);
        }

        [TestMethod]
        public void B01_ExportData_AssetTag()
        {
            var estService = new EST_Service();
            var hardwareAssetService = new BAM_HardwareAssetServices();
            var apiTemplateList = new List<BAM_HardwareTemplate_Full>();

            var queryBuilder = new QueryBuilder
            {
                StartDateString = "01/01/2017",
                EndDateString = "30/01/2017",
                PageCount = 1000
            };

            // Get Records from Esteem System
            var dataExport = estService.GetExportData(queryBuilder);
            dataExport.AssetTagChangeList.ForEach(asset =>
            {
                var bamAssetList = hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (bamAssetList != null) apiTemplateList.Add(bamAssetList);
            });

            JSON_FileExport.WriteFile(_typePrefix + "B01_ExportData", apiTemplateList, apiTemplateList.Count);
        }
    }
}
