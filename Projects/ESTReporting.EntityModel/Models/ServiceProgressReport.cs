﻿using System;

namespace ESTReporting.EntityModel.Models
{
    public class ServiceProgressReport : BaseObjectProperties
    {
        public DateTime? StartDateTime { get; set; }
        public DateTime? EsteemExtractDateTime { get; set; }
        public DateTime? BAMExportDateTime { get; set; }
        public bool ProcessSuccessFlag { get; set; }

        public int? NewItemCount { get; set; }
        public int? LocationChangeCount { get; set; }
        public int? AssetTagChangeCount { get; set; }
        public int? DeployedCount { get; set; }
        public int? ReturnedCount { get; set; }
        public int? RetiredCount { get; set; }
        public int? DisposedCount { get; set; }
        public int? SwappedCount { get; set; }

        public int? ExceptionCountTotal { get; set; }
        public DateTime? QueryStartParameters { get; set; }
        public DateTime? QueryEndParameters { get; set; }
        public string QueryString { get; set; }
        public int ServiceJobType { get; set; } 
    }
}
