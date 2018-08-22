﻿using BusinessModel.Models;
using BusinessModel.Services.Abstract;
using BusinessModel.Mappers;
using EntityModel;
using EntityModel.Repository.Abstract;
using ServiceModel.Models;
using ServiceModel.Models.BAM;
using ServiceModel.Models.BAM.Abstract;
using ServiceModel.Models.Esteem;
using ServiceModel.Services;
using ServiceModel.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using ServiceModel.Extensions;

namespace BusinessModel.Services
{
    public class BAM_Service
    {
        IEST_Service _estService;
        IBAM_HardwareAssetServices _hardwareAssetService;
        IBAM_AssetStatusService _assetStatusService;
        IBAM_UserService _userService;

        public BAM_Service() : this(new EST_Service(), new BAM_HardwareAssetServices(), new BAM_AssetStatusService(), new BAM_UserService()) { }

        public BAM_Service(IEST_Service estService, IBAM_HardwareAssetServices bAM_HardwareAsset, IBAM_AssetStatusService assetStatusService, IBAM_UserService userService)
        {
            _estService = estService ?? new EST_Service();
            _hardwareAssetService = bAM_HardwareAsset ?? new BAM_HardwareAssetServices();
            _assetStatusService = assetStatusService ?? new BAM_AssetStatusService();
            _assetStatusService = assetStatusService ?? new BAM_AssetStatusService();
            _userService = userService ?? new BAM_UserService();
        }

        public EST_DataExportModel ExportDataToBAM(IQueryBuilder queryBuilder)
        {
            var dataExport = _estService.GetExportData(queryBuilder);

            //dataExport = Process_NewItemList(dataExport);
            //dataExport = Process_LocationChangeList(dataExport);
            dataExport = Process_AssetTagChangeList(dataExport);
            //dataExport = Process_DeployedToBAMUserList(dataExport);
            //dataExport = Process_ReturnedFromBAMUserList(dataExport);

            return dataExport;
        }

        internal EST_DataExportModel Process_NewItemList(EST_DataExportModel model)
        {
            if (model == null) return model;
            var returnList = new List<HardwareTemplate>();

            model.NewItemList.ForEach(asset =>
            {
                // Create New Item Template - set default values
                BAM_HardwareTemplate bamTemplate = CreateNewItem(asset);
                _hardwareAssetService.UpdateTemplate(bamTemplate, null);
                model.NewItemList.Remove(asset);
            });
            return model;
        }

        private BAM_HardwareTemplate CreateNewItem(ISCBaseObject asset)
        {
            var bamTemplate = _hardwareAssetService.CreateNewTemplate();

            bamTemplate.Manufacturer = asset.Manufacturer;
            bamTemplate.Model = asset.Model;
            bamTemplate.SerialNumber = asset.SerialNumber;
            bamTemplate.AssetTag = asset.AssetName;
            bamTemplate.DisplayName = asset.DisplayName;
            bamTemplate.HardwareAssetStatus = _assetStatusService.GetAssetStatusTemplate(EST_HWAssetStatus.NewItem);
            bamTemplate.Description = "Hugh Testing";
            bamTemplate.HardwareAssetType = new HardwareAssetType()
            {
                Id = "b4a14ffd-52c8-064f-c936-67616c245b35",
                Name = "Computer"
            };
            return bamTemplate;
        }

        internal EST_DataExportModel Process_LocationChangeList(EST_DataExportModel model)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditExt>();

            model.LocationChangeList.ForEach(asset => {
                BAM_HardwareTemplate_Full newHardwareAsset;

                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (bamTemplate != null)
                    newHardwareAsset = CloneObject.Clone(bamTemplate);
                else
                    newHardwareAsset = Map.Map_Results(CreateNewItem(asset));

                newHardwareAsset = _hardwareAssetService.SetLocation(newHardwareAsset, asset.Audit_Dest_Site_Num);
                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);

                // Check Update was successful
                var updatedbamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (updatedbamTemplate?.Target_HardwareAssetHasLocation?.DisplayName != "Esteem")
                    returnList.Add(asset);
            });
            model.LocationChangeList = returnList;
            return model;
        }

        internal EST_DataExportModel Process_AssetTagChangeList(EST_DataExportModel model)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditExt>();

            model.AssetTagChangeList.ForEach(asset => {
                BAM_HardwareTemplate newHardwareAsset;

                var bamTemplate = _hardwareAssetService.GetHardwareAsset(asset.SerialNumber).FirstOrDefault();
                if (bamTemplate != null)
                    newHardwareAsset = CloneObject.Clone(bamTemplate);
                else
                    newHardwareAsset = CreateNewItem(asset);

                newHardwareAsset = _hardwareAssetService.SetAssetTag(newHardwareAsset, asset.Audit_Part_Num);
                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);

                // Check Update was successful
                var updatedbamTemplate = _hardwareAssetService.GetHardwareAsset(asset.SerialNumber).FirstOrDefault();
                if (updatedbamTemplate?.AssetTag != asset.Audit_Part_Num)
                    returnList.Add(asset);
            });
            model.AssetTagChangeList = returnList;
            return model;
        }

        internal EST_DataExportModel Process_DeployedToBAMUserList(EST_DataExportModel model)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditDeployExt>();

            model.DeployedToBAMUserList.ForEach(asset => {
                BAM_HardwareTemplate_Full newHardwareAsset;
                if (string.IsNullOrEmpty(asset.RequestUser)) {
                    returnList.Add(asset); return;
                }
                var user = _userService.GetUser(asset.RequestUser);
                if (user == null || user.Name != asset.RequestUser) {
                    returnList.Add(asset); return;
                }

                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (bamTemplate != null)
                    newHardwareAsset = CloneObject.Clone(bamTemplate);
                else
                    newHardwareAsset = Map.Map_Results(CreateNewItem(asset));

                newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(newHardwareAsset, EST_HWAssetStatus.Deployed);

                newHardwareAsset = _hardwareAssetService.SetHardwareAssetPrimaryUser(newHardwareAsset, user);

                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);

                // Check Update was successful
                var updatedbamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (updatedbamTemplate?.Target_HardwareAssetHasPrimaryUser?.DisplayName != asset.RequestUser)
                    returnList.Add(asset);
            });
            model.DeployedToBAMUserList = returnList;
            return model;
        }

        internal EST_DataExportModel Process_ReturnedFromBAMUserList(EST_DataExportModel model)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditDeployExt>();

            model.ReturnedFromBAMUserList.ForEach(asset => {
                BAM_HardwareTemplate_Full newHardwareAsset;

                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (bamTemplate != null)
                    newHardwareAsset = CloneObject.Clone(bamTemplate);
                else
                    newHardwareAsset = Map.Map_Results(CreateNewItem(asset));

                // Set Hardware Status is BAD, Scrapped or Returned
                if (asset.Audit_Dest_Site_Num != null && asset.Audit_Dest_Site_Num.Contains("BNLSCRAP"))
                    newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(newHardwareAsset, EST_HWAssetStatus.Disposed);
                else if (asset.Audit_Dest_Site_Num != null && asset.Audit_Dest_Site_Num.Contains("LTX BAD"))
                    newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(newHardwareAsset, EST_HWAssetStatus.Retired);
                else
                    newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(newHardwareAsset, EST_HWAssetStatus.Returned);

                // Set Location to Esteem and User to Null
                newHardwareAsset = _hardwareAssetService.SetLocation(newHardwareAsset, "Esteem");
                newHardwareAsset.Target_HardwareAssetHasPrimaryUser = null;

                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);

                // Check Update was successful
                var updatedbamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (updatedbamTemplate?.Target_HardwareAssetHasLocation?.DisplayName == "Esteem")
                    returnList.Add(asset);
            });
            model.ReturnedFromBAMUserList = returnList;
            return model;
        }
    }
}
