using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceModel.Models.BAM;
using ServiceModel.Models.Esteem;
using ServiceModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Test.BAM_API_Tests
{
    [TestClass]
    public class BAM__UpdateHardwareItem_Tests : BaseTestClient
    {
        private string _typePrefix = "BAM_API_";

        [TestMethod]
        public async Task B00_GetAssetItem_SetHardwareStatus_Test()
        {
            var hardwareAssetTemplateId = "c0c58e7f-7865-55cc-4600-753305b9be64";
            var serialNumber = "CNU0183F33"; // "BAM -L-00"; //0852
            var serialNumberFull = "CND7506PT8"; // "BAM -L-00"; //0852

            var hardwareAssetService = new BAM_HardwareAssetServices();
            var result = hardwareAssetService.GetHardwareAsset(serialNumber);

            Assert.IsNotNull(result, "Result didn't deserialize to BAM_HardwareTemplate_Full");
            Assert.IsTrue(result.Any(), "Result didn't any results");
            Assert.IsTrue(result.First().SerialNumber == serialNumber, "SerialNumbers don't match");

            var hardwareAsset = result.First();
            var newHardwareAsset = hardwareAssetService.SetHardwareAssetStatus(hardwareAsset, EST_HWAssetStatus.Deployed);

            Assert.IsNotNull(newHardwareAsset, "Updated Hardware Asset returned null");
            Assert.IsFalse(newHardwareAsset.Equals(hardwareAsset));
            Assert.IsTrue(newHardwareAsset.HardwareAssetStatus.Name != hardwareAsset.HardwareAssetStatus.Name);
            Assert.IsTrue(newHardwareAsset.HardwareAssetStatus.Name == "Deployed");
        }

        [TestMethod]
        public async Task B01_GetAssetItem_SetHardwareStatus_Test()
        {
            var hardwareAssetTemplateId = "c0c58e7f-7865-55cc-4600-753305b9be64";
            var serialNumber = "CNU0183F33"; // "BAM -L-00"; //0852
            var serialNumberFull = "CND7506PT8"; // "BAM -L-00"; //0852

            var hardwareAssetService = new BAM_HardwareAssetServices();
            var result = hardwareAssetService.GetHardwareAsset(serialNumber);

            Assert.IsNotNull(result, "Result didn't deserialize to BAM_HardwareTemplate_Full");
            Assert.IsTrue(result.Any(), "Result didn't any results");
            Assert.IsTrue(result.First().SerialNumber == serialNumber, "SerialNumbers don't match");

            var originalhardwareAsset = result.First();
            var newHardwareAsset = hardwareAssetService.SetHardwareAssetStatus(originalhardwareAsset, EST_HWAssetStatus.Deployed);

            Assert.IsNotNull(newHardwareAsset, "Updated Hardware Asset returned null");
            Assert.IsFalse(newHardwareAsset.Equals(originalhardwareAsset));
            Assert.IsTrue(newHardwareAsset.HardwareAssetStatus.Name != originalhardwareAsset.HardwareAssetStatus.Name);
            Assert.IsTrue(newHardwareAsset.HardwareAssetStatus.Name == "Deployed");
            List<BAM_HardwareTemplate> hardwareAssetList = new List<BAM_HardwareTemplate>();
            try
            {
                hardwareAssetList = hardwareAssetService.UpdateTemplate(newHardwareAsset, originalhardwareAsset);
            }
            catch (Exception exp)
            {
                hardwareAssetList = null;
                Assert.IsNull(exp, exp.Message);
            }
            Assert.IsNotNull(hardwareAssetList, "Return list is null");
            Assert.IsTrue(hardwareAssetList.Count > 1, "Return list doesn't include 2 records");

            Assert.IsTrue(hardwareAssetList[0].HardwareAssetStatus.Name != hardwareAssetList[1].HardwareAssetStatus.Name);
            Assert.IsTrue(hardwareAssetList[0].HardwareAssetStatus.Name == "Deployed");
        }
    }
}
