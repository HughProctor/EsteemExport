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
using System;
using System.Threading.Tasks;
using ServiceModel;

namespace BusinessModel.Services
{
    public class BAM_Service : IBAM_Service, IDisposable
    {
        IEST_Service _estService;
        IBAM_HardwareAssetServices _hardwareAssetService;
        List<BAM_ReportingBsm> _reportings;
        List<BAM_ReportingBsm> _billables;
        IReportingService _reportingService;
        ServiceProgressReportBsm _progressReport;
        BAM_ApiClient bam_ApiClient;
        IEST_BAM_ModelLookupService _BAM_ModelLookupService;
        //public EST_DataExportModel _dataExport { get; set; }

        public BAM_Service() : this(null, null, null, null, null) { }

        public BAM_Service(IEST_Service estService, IBAM_HardwareAssetServices bAM_HardwareAsset, IBAM_AssetStatusService assetStatusService, IBAM_UserService userService, IReportingService reportingService)
        {
            bam_ApiClient = new BAM_ApiClient();
            Task.Run(() => bam_ApiClient.Setup()).Wait();
            
            _estService = estService ?? new EST_Service();
            _hardwareAssetService = bAM_HardwareAsset ?? new BAM_HardwareAssetServices(bam_ApiClient);
            _reportingService = reportingService ?? new ReportingService();
            _reportings = new List<BAM_ReportingBsm>();
            _billables = new List<BAM_ReportingBsm>();
            _progressReport = new ServiceProgressReportBsm();
            _BAM_ModelLookupService = new EST_BAM_ModelLookupService();
        }

        public async Task<EST_DataExportModel> ExportDataToBAM(IQueryBuilder queryBuilder)
        {
            return await ExportDataToBAM(queryBuilder, 0);
        }

        public async Task<EST_DataExportModel> ExportDataToBAM(IQueryBuilder queryBuilder, int jobType)
        {
            _progressReport.StartDateTime = DateTime.Now;
            _progressReport.ServiceJobType = jobType;
            _progressReport = _reportingService.ServiceProgressReporting(_progressReport);

            // Query, cleanse and subset the data
            //if (_dataExport == null)
             var   _dataExport = _estService.GetExportData(queryBuilder);

            queryBuilder = _estService._queryBuilder;

            _progressReport.EsteemExtractDateTime = DateTime.Now;
            _progressReport.QueryStartParameters = queryBuilder.StartDate;
            _progressReport.QueryEndParameters = queryBuilder.EndDate;
            _progressReport.QueryString = queryBuilder.LastQueryString;
            _progressReport = _reportingService.ServiceProgressReporting(_progressReport);

            // Process the Data
            var newItemTask = Process_NewItemList(_dataExport, _progressReport);
            //var locationTask = Process_LocationChangeList(dataExport, _progressReport); Depreciated
            var assetTagTask = Process_AssetTagChangeList(_dataExport, _progressReport);
            var deployTask = Process_DeployedToBAMUserList(_dataExport, _progressReport);
            //var returnTask = Process_ReturnedFromBAMUserList(dataExport, _progressReport); Depreciated

            var returnNewTask = Process_ReturnedFromBAMList(_dataExport, _progressReport);
            var retiredTask = Process_RetiredAssetList(_dataExport, _progressReport);
            var disposedTask = Process_DisplosedAssetList(_dataExport, _progressReport);
            var swappedTask = Process_SwappedAssetList(_dataExport, _progressReport);

            _dataExport = (await Task.WhenAll(newItemTask, assetTagTask, deployTask, //locationTask,returnTask,
                    returnNewTask, retiredTask, disposedTask, swappedTask
                )).First();
            //dataExport = (await Task.WhenAll(returnTask)).First();

            _progressReport.BAMExportDateTime = DateTime.Now;
            _progressReport = _reportingService.ServiceProgressReporting(_progressReport);

            // Save any Exceptions to the BAMEsteemExportDB
            await _reportingService.ProcessExceptions(_reportings);
            await _reportingService.ProcessBillables(_billables);
            _progressReport = _reportingService.ServiceProgressReporting(_progressReport);

            _progressReport.BAMExportDateTime = DateTime.Now;
            _progressReport.NewItemCount = _dataExport.NewItemList.Count;
            _progressReport.LocationChangeCount = _dataExport.LocationChangeList.Count;
            _progressReport.AssetTagChangeCount = _dataExport.AssetTagChangeList.Count;
            _progressReport.DeployedCount = _dataExport.DeployedToBAMUserList.Count;
            _progressReport.ReturnedCount = _dataExport.ReturnedFromBAMList.Count;


            _progressReport.ExceptionCountTotal = _progressReport.NewItemCount + _progressReport.LocationChangeCount +
                _progressReport.AssetTagChangeCount + _progressReport.DeployedCount + _progressReport.ReturnedCount;
            _progressReport.ProcessSuccessFlag = _progressReport.ExceptionCountTotal == 0 ? true : false;
            _progressReport = _reportingService.ServiceProgressReporting(_progressReport);

            return _dataExport;
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
                if (existingBamTemplate != null && existingBamTemplate.Target_HardwareAssetHasLocation?.DisplayName != "Esteem")
                {
                    model.LocationChangeList.Add(asset);
                    return;
                }
                else if (existingBamTemplate != null)
                    return;

                // Create New Item Template - set default values
                HardwareTemplate_Full bamTemplate = CreateNewItem(asset);

                var hasModelRecord = false;
                bamTemplate = _BAM_ModelLookupService.SetModelData(bamTemplate, asset.Asset_Desc, out hasModelRecord);

                if (hasModelRecord)
                {
                    var returnItems = _hardwareAssetService.InsertTemplate(bamTemplate).FirstOrDefault();
                }

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
                            ExceptionMessage = "Failed to Update"
                        });
                        returnList.Add(asset);
                    }
            });
            return model;
        }

        private HardwareTemplate_Full CreateNewItem(ISCBaseObject asset)
        {
            var bamTemplate = _hardwareAssetService.CreateNewTemplate();

            bamTemplate.Manufacturer = asset.Manufacturer;
            bamTemplate.Model = asset.Model;
            bamTemplate.SerialNumber = asset.SerialNumber;
            bamTemplate.AssetTag = string.Empty;  // = asset.AssetName;
            bamTemplate.Name = asset.SerialNumber;  //asset.AssetName;
            bamTemplate.DisplayName = asset.SerialNumber; //asset.DisplayName;
            bamTemplate = _hardwareAssetService.SetHardwareAssetStatus(bamTemplate, EST_HWAssetStatus.NewItem);
            //bamTemplate.Description = "Hugh Testing";
            bamTemplate.Notes = asset.AssetName + " Created by Esteem to BAM Export";
            bamTemplate.Target_HardwareAssetHasLocation = new TargetHardwareAssetHasLocation()
            {
                BaseId = "ae7423eb-0952-d69c-4d7d-77f1699bfe92",
            };

            //bamTemplate.HardwareAssetType = new HardwareAssetType()
            //{
            //    Id = "b4a14ffd-52c8-064f-c936-67616c245b35",
            //    Name = "Computer"
            //};

            return bamTemplate;
        }

        internal async Task<EST_DataExportModel> Process_LocationChangeList(EST_DataExportModel model, ServiceProgressReportBsm serviceProgressReport)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditBsm>();

            model.LocationChangeList.ToList().ForEach(asset => {
                HardwareTemplate_Full newHardwareAsset;

                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();

                // If the Location is already set to Esteem then do nothing
                if (bamTemplate != null && bamTemplate.Target_HardwareAssetHasLocation?.DisplayName == "Esteem")
                    return;

                if (bamTemplate != null)
                    newHardwareAsset = CloneObject.Clone(bamTemplate);
                else
                    newHardwareAsset = CreateNewItem(asset);

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
                        ExceptionMessage = "Failed to Update"
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
                HardwareTemplate newHardwareAsset;

                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();

                // If the Asset isn't null and the Tag is already correct then just return
                if (bamTemplate != null && bamTemplate.AssetTag == asset.Audit_Part_Num)
                    return;
                else if (bamTemplate != null)
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
                        ExceptionMessage = "Failed to Update"
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
                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();

                var user = _hardwareAssetService.GetUser(asset.RequestUser);
                if (user == null || !user.Name.Contains(asset.RequestUser)) {
                    _reportings.Add(new BAM_ReportingBsm()
                    {
                        ServiceProgressReportId = serviceProgressReport.Id,
                        SerialNumber = asset.SerialNumber,
                        AssetStatus = EST_HWAssetStatus.Deployed,
                        SCAuditDeploy_Item = asset,
                        BAM_HardwareTemplate_Exception = bamTemplate,
                        ExceptionMessage = "No User Found in BAM"
                    });
                    returnList.Add(asset);
                    return;
                }

                // The asset is not null and User is correct, HardwareStatus is Deployed and location is null then return
                if (bamTemplate != null && (bool)bamTemplate?.OwnedBy?.DisplayName?.Contains(user.Name)
                    && bamTemplate?.HardwareAssetStatus?.Name == EST_HWAssetStatus.Deployed.ToBAMString() 
                    && bamTemplate?.AssetTag == asset.AssetTag && bamTemplate?.Name == asset.AssetTag && bamTemplate?.DisplayName == asset.AssetTag
                    && bamTemplate?.Target_HardwareAssetHasLocation == null)
                    return;
                else if (bamTemplate != null)
                    newHardwareAsset = CloneObject.Clone(bamTemplate);
                else
                    newHardwareAsset = CreateNewItem(asset);

                newHardwareAsset.Target_HardwareAssetHasLocation = null;
                newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(newHardwareAsset, EST_HWAssetStatus.Deployed);
                newHardwareAsset = _hardwareAssetService.SetUser(newHardwareAsset, user);

                newHardwareAsset = _hardwareAssetService.SetCostCode(newHardwareAsset, asset.CostCode);


                newHardwareAsset.AssetTag = asset.AssetTag;
                newHardwareAsset.Name = asset.AssetTag;
                newHardwareAsset.DisplayName = asset.AssetTag;

                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);

                // Check Update was successful
                var updatedbamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (!(bool)updatedbamTemplate?.OwnedBy?.DisplayName?.Contains(asset.RequestUser))
                {
                    _reportings.Add(new BAM_ReportingBsm()
                    {
                        ServiceProgressReportId = serviceProgressReport.Id,
                        SerialNumber = asset.SerialNumber,
                        AssetStatus = EST_HWAssetStatus.Deployed,
                        SCAuditDeploy_Item = asset,
                        BAM_HardwareTemplate_Exception = updatedbamTemplate,
                        ExceptionMessage = "Failed to Update"
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

        /// <summary>
        /// Depreciated
        /// </summary>
        /// <param name="model"></param>
        /// <param name="serviceProgressReport"></param>
        /// <returns></returns>
        internal async Task<EST_DataExportModel> Process_ReturnedFromBAMUserList(EST_DataExportModel model, ServiceProgressReportBsm serviceProgressReport)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditDeployBsm>();

            model.ReturnedFromBAMUserList.ToList().ForEach(asset => {
                HardwareTemplate_Full newHardwareAsset;

                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();

                // The asset is not null and User is null, HardwareStatus is correct and location is Esteem then return
                if (bamTemplate != null && bamTemplate?.Target_HardwareAssetHasPrimaryUser == null
                    && bamTemplate?.HardwareAssetStatus?.Name == 
                            ((bool)asset.Audit_Dest_Site_Num?.Contains("BNLSCRAP") ? EST_HWAssetStatus.Disposed.ToBAMString() :
                            (bool)asset.Audit_Dest_Site_Num?.Contains("LTX BAD") ? EST_HWAssetStatus.Retired.ToBAMString() :
                            EST_HWAssetStatus.Returned.ToBAMString())

                    && bamTemplate?.Target_HardwareAssetHasLocation?.DisplayName == "Esteem")
                    return;
                else if (bamTemplate != null)
                    newHardwareAsset = CloneObject.Clone(bamTemplate);
                else
                    newHardwareAsset = CreateNewItem(asset);

                // Set Hardware Status is BAD, Scrapped or Returned
                if (asset.Audit_Dest_Site_Num != null && (bool)asset.Audit_Dest_Site_Num?.Contains("BNLSCRAP"))
                    newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(newHardwareAsset, EST_HWAssetStatus.Disposed);
                else if (asset.Audit_Dest_Site_Num != null && (bool)asset.Audit_Dest_Site_Num?.Contains("LTX BAD"))
                    newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(newHardwareAsset, EST_HWAssetStatus.Retired);
                else
                    newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(newHardwareAsset, EST_HWAssetStatus.Returned);

                // Set Location to Esteem and User to Null
                newHardwareAsset = _hardwareAssetService.SetLocation(newHardwareAsset, "Esteem");
                newHardwareAsset.Target_HardwareAssetHasPrimaryUser = null;

                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);

                // Check Update was successful
                var updatedbamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (updatedbamTemplate?.Target_HardwareAssetHasLocation?.DisplayName != "Esteem")
                {
                    _reportings.Add(new BAM_ReportingBsm()
                    {
                        ServiceProgressReportId = serviceProgressReport.Id,
                        SerialNumber = asset.SerialNumber,
                        AssetStatus = EST_HWAssetStatus.Returned,
                        SCAuditDeploy_Item = asset,
                        BAM_HardwareTemplate_Exception = updatedbamTemplate,
                        ExceptionMessage = "Failed to Update"
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

        internal async Task<EST_DataExportModel> Process_ReturnedFromBAMList(EST_DataExportModel model, ServiceProgressReportBsm serviceProgressReport)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditBsm>();
            model.ReturnedFromBAMList.ToList().ForEach(asset =>
            {
                HardwareTemplate_Full newHardwareAsset;

                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();

                if (bamTemplate != null)
                    newHardwareAsset = CloneObject.Clone(bamTemplate);
                else
                    newHardwareAsset = CreateNewItem(asset);

                newHardwareAsset.OwnedBy = null;
                newHardwareAsset.Target_HardwareAssetHasCostCenter = null;
                newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(newHardwareAsset, EST_HWAssetStatus.Returned);
                newHardwareAsset = _hardwareAssetService.SetLocation(newHardwareAsset, "Esteem");

                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);

                // Check Update was successful
                var updatedbamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (updatedbamTemplate?.Target_HardwareAssetHasLocation?.DisplayName != "Esteem")
                {
                    _reportings.Add(new BAM_ReportingBsm()
                    {
                        ServiceProgressReportId = serviceProgressReport.Id,
                        SerialNumber = asset.SerialNumber,
                        AssetStatus = EST_HWAssetStatus.Retired,
                        SCAudit_Item = asset,
                        BAM_HardwareTemplate_Exception = updatedbamTemplate,
                        ExceptionMessage = "Failed to Update"
                    });
                    returnList.Add(asset);
                }
                _billables.Add(new BAM_ReportingBsm()
                {
                    ServiceProgressReportId = serviceProgressReport.Id,
                    SerialNumber = asset.SerialNumber,
                    AssetStatus = EST_HWAssetStatus.Retired,
                    SCAudit_Item = asset,
                    BAM_HardwareTemplate_Exception = updatedbamTemplate,
                });
            });
            model.ReturnedFromBAMList = returnList;
            return model;
        }

        internal async Task<EST_DataExportModel> Process_RetiredAssetList(EST_DataExportModel model, ServiceProgressReportBsm serviceProgressReport)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditBsm>();
            model.RetiredAssetList.ToList().ForEach(asset =>
            {
                HardwareTemplate_Full newHardwareAsset;

                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();

                if (bamTemplate != null)
                    newHardwareAsset = CloneObject.Clone(bamTemplate);
                else
                    newHardwareAsset = CreateNewItem(asset);

                newHardwareAsset.OwnedBy = null;
                newHardwareAsset.Target_HardwareAssetHasCostCenter = null;
                newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(newHardwareAsset, EST_HWAssetStatus.Retired);
                newHardwareAsset = _hardwareAssetService.SetLocation(newHardwareAsset, "Esteem");

                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);

                // Check Update was successful
                var updatedbamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (updatedbamTemplate?.Target_HardwareAssetHasLocation?.DisplayName != "Esteem")
                {
                    _reportings.Add(new BAM_ReportingBsm()
                    {
                        ServiceProgressReportId = serviceProgressReport.Id,
                        SerialNumber = asset.SerialNumber,
                        AssetStatus = EST_HWAssetStatus.Retired,
                        SCAudit_Item = asset,
                        BAM_HardwareTemplate_Exception = updatedbamTemplate,
                        ExceptionMessage = "Failed to Update"
                    });
                    returnList.Add(asset);
                }
                _billables.Add(new BAM_ReportingBsm()
                {
                    ServiceProgressReportId = serviceProgressReport.Id,
                    SerialNumber = asset.SerialNumber,
                    AssetStatus = EST_HWAssetStatus.Retired,
                    SCAudit_Item = asset,
                    BAM_HardwareTemplate_Exception = updatedbamTemplate,
                });
            });
            model.RetiredAssetList = returnList;
            return model;
        }

        internal async Task<EST_DataExportModel> Process_DisplosedAssetList(EST_DataExportModel model, ServiceProgressReportBsm serviceProgressReport)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditBsm>();
            model.DisplosedAssetList.ToList().ForEach(asset =>
            {
                HardwareTemplate_Full newHardwareAsset;

                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();

                if (bamTemplate != null)
                    newHardwareAsset = CloneObject.Clone(bamTemplate);
                else
                    newHardwareAsset = CreateNewItem(asset);

                newHardwareAsset.OwnedBy = null;
                newHardwareAsset.Target_HardwareAssetHasCostCenter = null;
                newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(newHardwareAsset, EST_HWAssetStatus.Disposed);
                newHardwareAsset.Target_HardwareAssetHasLocation = null;
                newHardwareAsset.DisposalDate = DateTime.Now;
                newHardwareAsset.DisposalReference = "Esteem";

                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);

                // Check Update was successful
                var updatedbamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (updatedbamTemplate?.Target_HardwareAssetHasLocation?.DisplayName != "Esteem")
                {
                    _reportings.Add(new BAM_ReportingBsm()
                    {
                        ServiceProgressReportId = serviceProgressReport.Id,
                        SerialNumber = asset.SerialNumber,
                        AssetStatus = EST_HWAssetStatus.Disposed,
                        SCAudit_Item = asset,
                        BAM_HardwareTemplate_Exception = updatedbamTemplate,
                        ExceptionMessage = "Failed to Update"
                    });
                    returnList.Add(asset);
                }
                _billables.Add(new BAM_ReportingBsm()
                {
                    ServiceProgressReportId = serviceProgressReport.Id,
                    SerialNumber = asset.SerialNumber,
                    AssetStatus = EST_HWAssetStatus.Disposed,
                    SCAudit_Item = asset,
                    BAM_HardwareTemplate_Exception = updatedbamTemplate,
                });
            });
            model.DisplosedAssetList = returnList;
            return model;
        }

        internal async Task<EST_DataExportModel> Process_SwappedAssetList(EST_DataExportModel model, ServiceProgressReportBsm serviceProgressReport)
        {
            if (model == null) return model;
            var returnList = new List<SCAuditDeployBsm>();
            model.SwappedAssetList.ToList().ForEach(asset =>
            {
                HardwareTemplate_Full newHardwareAsset;
                HardwareTemplate_Full newReturnedHardwareAsset;

                var bamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();

                if (bamTemplate != null)
                    newHardwareAsset = CloneObject.Clone(bamTemplate);
                else
                    newHardwareAsset = CreateNewItem(asset);

                var returnedBamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumberReturned).FirstOrDefault();

                if (returnedBamTemplate != null)
                    newReturnedHardwareAsset = CloneObject.Clone(returnedBamTemplate);
                else
                    newReturnedHardwareAsset = CreateNewItem(asset);

                #region Deploy
                newHardwareAsset.Target_HardwareAssetHasLocation = null;
                newHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(newHardwareAsset, EST_HWAssetStatus.Deployed);
                newHardwareAsset.OwnedBy = newReturnedHardwareAsset.OwnedBy;
                newHardwareAsset.Target_HardwareAssetHasCostCenter = newReturnedHardwareAsset.Target_HardwareAssetHasCostCenter;

                newHardwareAsset.AssetTag = asset.AssetTag;
                newHardwareAsset.Name = asset.AssetTag;
                newHardwareAsset.DisplayName = asset.AssetTag;

                _hardwareAssetService.UpdateTemplate(newHardwareAsset, bamTemplate);
                #endregion Deploy

                #region Returned
                newReturnedHardwareAsset.OwnedBy = null;
                newReturnedHardwareAsset.Target_HardwareAssetHasCostCenter = null;
                newReturnedHardwareAsset = _hardwareAssetService.SetHardwareAssetStatus(newReturnedHardwareAsset, EST_HWAssetStatus.Returned);
                newReturnedHardwareAsset = _hardwareAssetService.SetLocation(newReturnedHardwareAsset, "Esteem");

                _hardwareAssetService.UpdateTemplate(newReturnedHardwareAsset, returnedBamTemplate);
                #endregion Returned

                // Check Update was successful
                var updatedbamTemplate = _hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();
                if (updatedbamTemplate?.Target_HardwareAssetHasLocation?.DisplayName != "Esteem")
                {
                    _reportings.Add(new BAM_ReportingBsm()
                    {
                        ServiceProgressReportId = serviceProgressReport.Id,
                        SerialNumber = asset.SerialNumber,
                        AssetStatus = EST_HWAssetStatus.Returned,
                        SCAuditDeploy_Item = asset,
                        BAM_HardwareTemplate_Exception = updatedbamTemplate,
                        ExceptionMessage = "Failed to Update"
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
            model.SwappedAssetList = returnList;
            return model;
        }

        public void Dispose()
        {

        }
    }
}