using BusinessModel.Models;
using BusinessModel.Services.Abstract;
using EntityModel.Repository.Abstract;
using ServiceModel.Models;
using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;
using ServiceModel.Services;
using ServiceModel.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool ExportDataToBAM(IQueryBuilder queryBuilder)
        {
            var returnValue = true;

            var dataExport = _estService.GetExportData(queryBuilder);


            return returnValue;
        }

        internal void Process_NewItemList(EST_DataExportModel model)
        {
            var returnList = new List<HardwareTemplate>();
            if (model == null) return;
            if (!model.NewItemList.Any()) return;
            model.NewItemList.ForEach(asset => {
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
                _hardwareAssetService.UpdateTemplate(bamTemplate, null);
            });
        }

        internal void Process_LocationChangeList(EST_DataExportModel model)
        {
            var returnList = new List<HardwareTemplate>();
            if (model == null) return;
            if (!model.NewItemList.Any()) return;
            model.LocationChangeList.ForEach(asset => {
                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (bamTemplate == null) return;

                var newHardwareAsset = _hardwareAssetService.SetLocation(bamTemplate, asset.Audit_Dest_Site_Num);
                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);
            });
        }

        internal void Process_AssetTagChangeList(EST_DataExportModel model)
        {
            var returnList = new List<HardwareTemplate>();
            if (model == null) return;
            if (!model.NewItemList.Any()) return;
            model.LocationChangeList.ForEach(asset => {
                var bamTemplate = _hardwareAssetService.GetHardwareAsset(asset.SerialNumber).FirstOrDefault();
                if (bamTemplate == null) return;

                var newHardwareAsset = _hardwareAssetService.SetAssetTag(bamTemplate, asset.Audit_Part_Num);
                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);
            });
        }

        internal void Process_DeployedToBAMUserList(EST_DataExportModel model)
        {
            var returnList = new List<HardwareTemplate>();
            if (model == null) return;
            if (!model.NewItemList.Any()) return;
            model.DeployedToBAMUserList.ForEach(asset => {
                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (bamTemplate == null) return;

                var newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(bamTemplate, EST_HWAssetStatus.Deployed);

                var user = _userService.GetUser(asset.RequestUser);
                if (user == null) return;

                newHardwareAsset = _hardwareAssetService.SetHardwareAssetPrimaryUser(newHardwareAsset, user);

                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);
            });
        }

        internal void Process_ReturnedFromBAMUserList(EST_DataExportModel model)
        {
            var returnList = new List<HardwareTemplate>();
            if (model == null) return;
            if (!model.NewItemList.Any()) return;
            model.ReturnedFromBAMUserList.ForEach(asset => {
                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (bamTemplate == null) return;

                var newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(bamTemplate, EST_HWAssetStatus.Returned);

                newHardwareAsset = _hardwareAssetService.SetHardwareAssetPrimaryUser(newHardwareAsset, null);

                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);
            });
        }
    }
}
