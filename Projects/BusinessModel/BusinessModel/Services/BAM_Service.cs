using BusinessModel.Models;
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
using System;
using System.Threading.Tasks;

namespace BusinessModel.Services
{
    public class BAM_Service : IBAM_Service, IDisposable
    {
        IEST_Service _estService;
        IBAM_HardwareAssetServices _hardwareAssetService;
        IBAM_AssetStatusService _assetStatusService;
        IBAM_UserService _userService;
        List<BAM_ReportingBsm> _reportings;
        List<BAM_ReportingBsm> _billables;
        IReportingService _reportingService;
        ServiceProgressReportBsm _progressReport;
        BAM_ApiClient bam_ApiClient;

        public BAM_Service() : this(null, null, null, null, null) { }

        public BAM_Service(IEST_Service estService, IBAM_HardwareAssetServices bAM_HardwareAsset, IBAM_AssetStatusService assetStatusService, IBAM_UserService userService, IReportingService reportingService)
        {
            bam_ApiClient = new BAM_ApiClient();
            Task.Run(() => bam_ApiClient.Setup()).Wait();
            
            _estService = estService ?? new EST_Service();
            _hardwareAssetService = bAM_HardwareAsset ?? new BAM_HardwareAssetServices(bam_ApiClient);
            _assetStatusService = assetStatusService ?? new BAM_AssetStatusService(bam_ApiClient);
            _userService = userService ?? new BAM_UserService(bam_ApiClient);
            _reportingService = reportingService ?? new ReportingService();
            _reportings = new List<BAM_ReportingBsm>();
            _billables = new List<BAM_ReportingBsm>();
            _progressReport = new ServiceProgressReportBsm();
        }

        public async Task<EST_DataExportModel> ExportDataToBAM(IQueryBuilder queryBuilder)
        {
            _progressReport.StartDateTime = DateTime.Now;
            _progressReport = _reportingService.ServiceProgressReporting(_progressReport);

            // Query, cleanse and subset the data
            var dataExport = _estService.GetExportData(queryBuilder);
            queryBuilder = _estService._queryBuilder;

            _progressReport.EsteemExtractDateTime = DateTime.Now;
            _progressReport.QueryStartParameters = queryBuilder.StartDate;
            _progressReport.QueryEndParameters = queryBuilder.EndDate;
            _progressReport.QueryString = queryBuilder.LastQueryString;

            _progressReport = _reportingService.ServiceProgressReporting(_progressReport);

            // Process the Data
            var newItemTask = Process_NewItemList(dataExport, _progressReport);
            //var locationTask = Process_LocationChangeList(dataExport, _progressReport);
            //var assetTagTask = Process_AssetTagChangeList(dataExport, _progressReport);
            //var deployTask = Process_DeployedToBAMUserList(dataExport, _progressReport);
            //var returnTask = Process_ReturnedFromBAMUserList(dataExport, _progressReport);
            //dataExport = (await Task.WhenAll(newItemTask, locationTask, assetTagTask, deployTask, returnTask)).First();
            dataExport = (await Task.WhenAll(newItemTask)).First();

            _progressReport.BAMExportDateTime = DateTime.Now;
            _progressReport = _reportingService.ServiceProgressReporting(_progressReport);

            // Save any Exceptions to the BAMEsteemExportDB
            await _reportingService.ProcessExceptions(_reportings);
            await _reportingService.ProcessBillables(_billables);
            _progressReport = _reportingService.ServiceProgressReporting(_progressReport);

            _progressReport.BAMExportDateTime = DateTime.Now;
            _progressReport.NewItemCount = dataExport.NewItemList.Count;
            _progressReport.LocationChangeCount = dataExport.LocationChangeList.Count;
            _progressReport.AssetTagChangeCount = dataExport.AssetTagChangeList.Count;
            _progressReport.DeployedCount = dataExport.DeployedToBAMUserList.Count;
            _progressReport.ReturnedCount = dataExport.ReturnedFromBAMUserList.Count;
            _progressReport.ExceptionCountTotal = _progressReport.NewItemCount + _progressReport.LocationChangeCount +
                _progressReport.AssetTagChangeCount + _progressReport.DeployedCount + _progressReport.ReturnedCount;
            _progressReport.ProcessSuccessFlag = _progressReport.ExceptionCountTotal == 0 ? true : false;
            _progressReport = _reportingService.ServiceProgressReporting(_progressReport);

            return dataExport;
        }

        internal async Task<EST_DataExportModel> Process_NewItemList(EST_DataExportModel model, ServiceProgressReportBsm serviceProgressReport)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditBsm>();

            model.NewItemList.ToList().ForEach(asset =>
            {
                // Check the Item doesn't already exist in the system
                var existingBamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                // If so.. add it to the Updates - Change Location List
                if (existingBamTemplate != null)
                {
                    model.LocationChangeList.Add(asset);
                    return;
                }
                // Create New Item Template - set default values
                HardwareTemplate bamTemplate = CreateNewItem(asset);
                _hardwareAssetService.UpdateTemplate(bamTemplate, null);

                var updatedbamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (updatedbamTemplate == null)
                    if (updatedbamTemplate?.Target_HardwareAssetHasLocation?.DisplayName != "Esteem")
                    {
                        _reportings.Add(new BAM_ReportingBsm()
                        {
                            ServiceProgressReportId = serviceProgressReport.Id,
                            SerialNumber = asset.SerialNumber,
                            AssetStatus = EST_HWAssetStatus.NewItem,
                            SCAudit_Item = asset,
                            BAM_HardwareTemplate_Exception = updatedbamTemplate,
                        });
                        returnList.Add(asset);
                    }
            });
            return model;
        }

        private HardwareTemplate CreateNewItem(ISCBaseObject asset)
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

        internal async Task<EST_DataExportModel> Process_LocationChangeList(EST_DataExportModel model, ServiceProgressReportBsm serviceProgressReport)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditBsm>();

            model.LocationChangeList.ToList().ForEach(asset => {
                HardwareTemplate_Full newHardwareAsset;

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
                {
                    _reportings.Add(new BAM_ReportingBsm()
                    {
                        ServiceProgressReportId = serviceProgressReport.Id,
                        SerialNumber = asset.SerialNumber,
                        AssetStatus = EST_HWAssetStatus.LocationChanged,
                        SCAudit_Item = asset,
                        BAM_HardwareTemplate_Exception = newHardwareAsset,
                    });
                    returnList.Add(asset);
                }
            });
            model.LocationChangeList = returnList;
            return model;
        }

        internal async Task<EST_DataExportModel> Process_AssetTagChangeList(EST_DataExportModel model, ServiceProgressReportBsm serviceProgressReport)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditBsm>();

            model.AssetTagChangeList.ToList().ForEach(asset => {
                ServiceModel.Models.BAM.HardwareTemplate newHardwareAsset;

                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (bamTemplate != null)
                    newHardwareAsset = CloneObject.Clone(bamTemplate);
                else
                    newHardwareAsset = CreateNewItem(asset);

                newHardwareAsset = _hardwareAssetService.SetAssetTag(newHardwareAsset, asset.Audit_Part_Num);
                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);

                // Check Update was successful
                var updatedbamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (updatedbamTemplate?.AssetTag != asset.Audit_Part_Num)
                {
                    _reportings.Add(new BAM_ReportingBsm()
                    {
                        ServiceProgressReportId = serviceProgressReport.Id,
                        SerialNumber = asset.SerialNumber,
                        AssetStatus = EST_HWAssetStatus.AssetTagChanged,
                        SCAudit_Item = asset,
                        BAM_HardwareTemplate_Exception = updatedbamTemplate,
                    });
                    returnList.Add(asset);
                }
            });
            model.AssetTagChangeList = returnList;
            return model;
        }

        internal async Task<EST_DataExportModel> Process_DeployedToBAMUserList(EST_DataExportModel model, ServiceProgressReportBsm serviceProgressReport)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditDeployBsm>();

            model.DeployedToBAMUserList.ToList().ForEach(asset => {
                HardwareTemplate_Full newHardwareAsset;
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
                {
                    _reportings.Add(new BAM_ReportingBsm()
                    {
                        ServiceProgressReportId = serviceProgressReport.Id,
                        SerialNumber = asset.SerialNumber,
                        AssetStatus = EST_HWAssetStatus.Deployed,
                        SCAuditDeploy_Item = asset,
                        BAM_HardwareTemplate_Exception = updatedbamTemplate,
                    });
                    returnList.Add(asset);
                }
                _billables.Add(new BAM_ReportingBsm()
                {
                    ServiceProgressReportId = serviceProgressReport.Id,
                    SerialNumber = asset.SerialNumber,
                    AssetStatus = EST_HWAssetStatus.Deployed,
                    SCAuditDeploy_Item = asset,
                    BAM_HardwareTemplate_Exception = updatedbamTemplate,
                });

            });
            model.DeployedToBAMUserList = returnList;
            return model;
        }

        internal async Task<EST_DataExportModel> Process_ReturnedFromBAMUserList(EST_DataExportModel model, ServiceProgressReportBsm serviceProgressReport)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditDeployBsm>();

            model.ReturnedFromBAMUserList.ToList().ForEach(asset => {
                HardwareTemplate_Full newHardwareAsset;

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
                {
                    _reportings.Add(new BAM_ReportingBsm()
                    {
                        ServiceProgressReportId = serviceProgressReport.Id,
                        SerialNumber = asset.SerialNumber,
                        AssetStatus = EST_HWAssetStatus.Returned,
                        SCAuditDeploy_Item = asset,
                        BAM_HardwareTemplate_Exception = updatedbamTemplate,
                    });
                    returnList.Add(asset);
                }
                _billables.Add(new BAM_ReportingBsm()
                {
                    ServiceProgressReportId = serviceProgressReport.Id,
                    SerialNumber = asset.SerialNumber,
                    AssetStatus = EST_HWAssetStatus.Returned,
                    SCAuditDeploy_Item = asset,
                    BAM_HardwareTemplate_Exception = updatedbamTemplate,
                });
            });
            model.ReturnedFromBAMUserList = returnList;
            return model;
        }

        public void Dispose()
        {

        }
    }
}
