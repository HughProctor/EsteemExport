using System;
using System.Linq;
using BusinessModel.Services;
using EntityModel.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceModel;
using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;
using ServiceModel.Services;
using ServiceModel.Services.Abstract;

namespace BusinessModel.Test
{
    [TestClass]
    public class EST_Service_Tests
    {
        [TestMethod]
        public void E00_EstService_GetExportData()
        {
            var estService = new EST_Service();
            var queryBuilder = new QueryBuilder();

            var dataExport = estService.GetExportData(null);

            Assert.IsNotNull(dataExport, "DataExport model returned null");
            Assert.IsNotNull(dataExport.NewItemList, "NewItemList model returned null");
            Assert.IsNotNull(dataExport.LocationChangeList, "LocationChangeList model returned null");
            Assert.IsNotNull(dataExport.AssetTagChangeList, "AssetTagChangeList model returned null");
            Assert.IsNotNull(dataExport.DeployedToBAMUserList, "DeployedToBAMUserList model returned null");
            Assert.IsNotNull(dataExport.ReturnedFromBAMUserList, "ReturnedFromBAMUserList model returned null");
            Assert.IsTrue(dataExport.NewItemList.Any(), "NewItemList didn't return any items");
            Assert.IsTrue(dataExport.LocationChangeList.Any(), "LocationChangeList didn't return any items");
            Assert.IsTrue(dataExport.AssetTagChangeList.Any(), "AssetTagChangeList didn't return any items");
            Assert.IsTrue(dataExport.DeployedToBAMUserList.Any(), "DeployedToBAMUserList didn't return any items");
            Assert.IsTrue(dataExport.ReturnedFromBAMUserList.Any(), "ReturnedFromBAMUserList didn't return any items");
        }

        [TestMethod]
        public void E00_EstService_GetExportData_Exists_in_BAMService()
        {
            var estService = new EST_Service();
            var queryBuilder = new QueryBuilder();

            var dataExport = estService.GetExportData(null);

            Assert.IsNotNull(dataExport, "DataExport model returned null");
            Assert.IsNotNull(dataExport.DeployedToBAMUserList, "DeployedToBAMUserList model returned null");
            Assert.IsTrue(dataExport.DeployedToBAMUserList.Any(), "DeployedToBAMUserList didn't return any items");

            var asset = dataExport.DeployedToBAMUserList.First();
            var hardwareAssetService = new BAM_HardwareAssetServices();
            var bamAssetList = hardwareAssetService.GetHardwareAsset(asset.SerialNumber);

            Assert.IsNotNull(bamAssetList, "BAM Asset is null");
            Assert.IsTrue(bamAssetList.Count == 1, "Get BAM Asset didn't return 1 item " + bamAssetList.Count.ToString());
            var bamAsset = bamAssetList.First();

            Assert.IsTrue(bamAsset.SerialNumber.Contains(asset.SerialNumber), "BAM and Esteem Asset Serial Numbers do not match");
        }

        [TestMethod]
        public void E00_EstService_GetExportData_Exists_in_BAMService_Random()
        {
            var estService = new EST_Service();
            var queryBuilder = new QueryBuilder
            {
                StartDateString = "01/01/2017",
                EndDateString = "30/01/2017",
                PageCount = 100
            };

            var dataExport = estService.GetExportData(queryBuilder);

            Assert.IsNotNull(dataExport, "DataExport model returned null");
            Assert.IsNotNull(dataExport.DeployedToBAMUserList, "DeployedToBAMUserList model returned null");
            Assert.IsTrue(dataExport.DeployedToBAMUserList.Any(), "DeployedToBAMUserList didn't return any items");

            var rand = new Random();

            var assetList = dataExport.DeployedToBAMUserList;
            var asset = assetList[rand.Next(assetList.Count)];
            var hardwareAssetService = new BAM_HardwareAssetServices();
            var bamAssetList = hardwareAssetService.GetHardwareAsset(asset.SerialNumber);

            Assert.IsNotNull(bamAssetList, "BAM Asset is null");
            Assert.IsTrue(bamAssetList.Count == 1, "Get BAM Asset didn't return 1 item " + bamAssetList.Count.ToString());
            var bamAsset = bamAssetList.First();

            Assert.IsTrue(bamAsset.SerialNumber.Contains(asset.SerialNumber), "BAM and Esteem Asset Serial Numbers do not match");
        }

        [TestMethod]
        public void E00_EstService_GetExportData_Exists_in_BAMService_Update()
        {
            var updateAssetStatus = EST_HWAssetStatus.Retired;

            var estService = new EST_Service();
            var queryBuilder = new QueryBuilder
            {
                StartDateString = "01/01/2017",
                EndDateString = "30/01/2017",
                PageCount = 100
            };

            // Get Records from Esteem System
            var dataExport = estService.GetExportData(queryBuilder);
            var originalModifiedDate = new DateTime();
            var updatedModifiedDate = new DateTime();

            Assert.IsNotNull(dataExport, "DataExport model returned null");
            Assert.IsNotNull(dataExport.DeployedToBAMUserList, "DeployedToBAMUserList model returned null");
            Assert.IsTrue(dataExport.DeployedToBAMUserList.Any(), "DeployedToBAMUserList didn't return any items");

            var rand = new Random();
            // Select a Random record
            var assetList = dataExport.DeployedToBAMUserList;
            var asset = assetList[rand.Next(assetList.Count)];

            // Get respective record from BAM Api
            var hardwareAssetService = new BAM_HardwareAssetServices();
            var bamAssetList = hardwareAssetService.GetHardwareAsset(asset.SerialNumber);

            Assert.IsNotNull(bamAssetList, "BAM Asset is null");
            Assert.IsTrue(bamAssetList.Count == 1, "Get BAM Asset didn't return 1 item " + bamAssetList.Count.ToString());
            var bamAsset = bamAssetList.First();

            Assert.IsTrue(bamAsset.SerialNumber.Contains(asset.SerialNumber), "BAM and Esteem Asset Serial Numbers do not match");

            // Set the BAM record to new AssetStatus
            var newHardwareAsset = hardwareAssetService.SetHardwareAssetStatus(bamAsset, updateAssetStatus);
            // Update BAM record on BAM Api
            var hardwareAssetList = hardwareAssetService.UpdateTemplate(newHardwareAsset, bamAsset);

            Assert.IsNotNull(hardwareAssetList, "Return list is null");
            Assert.IsTrue(hardwareAssetList.Count > 1, "Return list doesn't include 2 records");

            // Get the newly updated BAM record
            var updatedHardwareAsset = hardwareAssetService.GetHardwareAsset(asset.SerialNumber).FirstOrDefault();

            Assert.IsNotNull(updatedHardwareAsset, "Updated Asset is null");
            Assert.IsTrue(updatedHardwareAsset.SerialNumber == asset.SerialNumber, "SerialNumbers don't match");

            updatedModifiedDate = updatedHardwareAsset.LastModified;

            // Check Updates worked successfully
            Assert.IsTrue(updatedModifiedDate != originalModifiedDate, "Original and Updated LastModified Date are the same");
            Assert.IsTrue(updatedModifiedDate > originalModifiedDate, "Updated LastModified Date is not greater that the Original");
            Assert.IsTrue(hardwareAssetList[0].HardwareAssetStatus.Name == updateAssetStatus.ToBAMString(), "Updated Asset status doesn't equal BAM AssetStatus Enum");
        }

        [TestMethod]
        public void E00_EstService_GetExportData_Exists_in_BAMService_UpdateDeployedToUser()
        {
            var updateAssetStatus = EST_HWAssetStatus.Deployed;

            var estService = new EST_Service();
            var queryBuilder = new QueryBuilder
            {
                StartDateString = "01/01/2017",
                EndDateString = "30/01/2017",
                PageCount = 100
            };

            // Get Records from Esteem System
            var dataExport = estService.GetExportData(queryBuilder);
            var originalModifiedDate = new DateTime();
            var updatedModifiedDate = new DateTime();

            Assert.IsNotNull(dataExport, "DataExport model returned null");
            Assert.IsNotNull(dataExport.DeployedToBAMUserList, "DeployedToBAMUserList model returned null");
            Assert.IsTrue(dataExport.DeployedToBAMUserList.Any(), "DeployedToBAMUserList didn't return any items");

            var rand = new Random();
            // Select a Random record
            var assetList = dataExport.DeployedToBAMUserList;
            var asset = assetList[rand.Next(assetList.Count)];

            // Get respective record from BAM Api
            var hardwareAssetService = new BAM_HardwareAssetServices();
            var bamAssetList = hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber);

            Assert.IsNotNull(bamAssetList, "BAM Asset is null");
            Assert.IsTrue(bamAssetList.Count == 1, "Get BAM Asset didn't return 1 item " + bamAssetList.Count.ToString());
            var bamAsset = bamAssetList.First();


            Assert.IsTrue(bamAsset.SerialNumber.Contains(asset.SerialNumber), "BAM and Esteem Asset Serial Numbers do not match");

            // Set the BAM record to new AssetStatus
            var newHardwareAsset = hardwareAssetService.SetHardwareAssetStatus(bamAsset, updateAssetStatus);

            // Get respective User record from BAM Api
            IBAM_UserService userService = new BAM_UserService();
            var user = userService.GetUser(asset.RequestUser);
            Assert.IsNotNull(user, "User item is null");
            Assert.IsTrue(user.Name.Contains(asset.RequestUser), "user record is not the same");

            newHardwareAsset = hardwareAssetService.SetHardwareAssetPrimaryUser(newHardwareAsset, user);

            // Update BAM record on BAM Api
            var hardwareAssetList = hardwareAssetService.UpdateTemplate(newHardwareAsset, bamAsset);

            Assert.IsNotNull(hardwareAssetList, "Return list is null");
            Assert.IsTrue(hardwareAssetList.Count > 1, "Return list doesn't include 2 records");

            // Get the newly updated BAM record
            var updatedHardwareAsset = hardwareAssetService.GetHardwareAsset_Full(asset.SerialNumber).FirstOrDefault();

            Assert.IsNotNull(updatedHardwareAsset, "Updated Asset is null");
            Assert.IsTrue(updatedHardwareAsset.SerialNumber == asset.SerialNumber, "SerialNumbers don't match");

            updatedModifiedDate = updatedHardwareAsset.LastModified;

            // Check Updates worked successfully
            Assert.IsTrue(updatedModifiedDate != originalModifiedDate, "Original and Updated LastModified Date are the same");
            Assert.IsTrue(updatedModifiedDate > originalModifiedDate, "Updated LastModified Date is not greater that the Original");
            Assert.IsTrue(hardwareAssetList[0].HardwareAssetStatus.Name == updateAssetStatus.ToBAMString(), "Updated Asset status doesn't equal BAM AssetStatus Enum");

            Assert.IsNotNull(updatedHardwareAsset.Target_HardwareAssetHasPrimaryUser, "User record is null");
            Assert.IsTrue(updatedHardwareAsset.Target_HardwareAssetHasPrimaryUser.DisplayName.Contains(asset.RequestUser), "Updated User is not the same :" + updatedHardwareAsset.Target_HardwareAssetHasPrimaryUser.DisplayName + " " + asset.RequestUser);
        }
    }
}
