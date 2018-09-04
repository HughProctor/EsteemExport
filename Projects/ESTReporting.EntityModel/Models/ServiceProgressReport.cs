﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int? ExceptionCountTotal { get; set; }
        public DateTime? QueryStartParameters { get; set; }
        public DateTime? QueryEndParameters { get; set; }
        public string QueryString { get; set; }
    }
}
