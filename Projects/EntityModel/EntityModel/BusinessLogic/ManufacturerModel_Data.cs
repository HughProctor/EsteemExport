using EntityModel.Mappers;
using EntityModel.Models;
using ESTReporting.EntityModel.Context;
using System.Collections.Generic;
using System.Linq;

namespace EntityModel.BusinessLogic
{
    public class ManufacturerModel_Data
    {
        private BAMEsteemExportContext db;
        public ManufacturerModel_Data()
        {
            db = new BAMEsteemExportContext();
            Manufacturers = Map.Map_Results(db.PartManufacturers.ToList());
        }
        

        public static List<string> manufacturerCodes = GetManufacturers().Select(a => a.Code).ToList();
        public static List<string> manufacturerNames = GetManufacturers().Select(a => a.Name.ToUpper()).ToList();
        public static List<ManufacturerModel> Manufacturers;
        public static List<ManufacturerModel> GetManufacturers()
        {
            return Manufacturers;// Map.Map_Results(db.PartManufacturers.ToList());
        }

        //public static List

        public static List<PartDescriptionModel> GetPartDescriptionModels()
        {
            var returnList = new List<PartDescriptionModel>() {
            new PartDescriptionModel()
            {
                EsteemCode = "CAB",
                Description = "Cables",
                FullDescription = "Cables EG Ide, SAS, Kettle lead"
            },
            new PartDescriptionModel()
            {
                EsteemCode = "BAT",
                Description = "Batteries",
                FullDescription = "Batteries EG Notebook, Coin, UPS"
            },
            new PartDescriptionModel()
            {
                EsteemCode = "COM",
                Description = "Com cards",
                FullDescription = "Discrete cards EG PCI, ISA, VGA, PCIE based cards"
            },
            new PartDescriptionModel()
            {
                EsteemCode = "CON",
                Description = "Convertors",
                FullDescription = "Convertors and gender changers"
            },
            new PartDescriptionModel()
            {
                EsteemCode = "CPT",
                Description = "Components",
                FullDescription = "Components EG Suface mount chips, Capacitors"
            },
            new PartDescriptionModel()
            {
                EsteemCode = "HDD",
                Description = "Hard drives Clone",
                FullDescription = "Hard Disk Drives following the clone convention"
            },
            new PartDescriptionModel() { EsteemCode = "HDD", Description =  "Hard drives MFG", FullDescription =   "Hard disk drives for servers and under manufacturers" },
            new PartDescriptionModel()
            {
                EsteemCode = "KBD",
                Description = "Keyboards clone",
                FullDescription =   "Keyboards,clone whole and part" },
            new PartDescriptionModel()
            {
                EsteemCode = "KBD",
                Description = "Keyboards specialist",
                FullDescription =   "Keyboards, specialist such as pos cherry alphameric" },
            new PartDescriptionModel()
            {
                EsteemCode = "MOU",
                Description = "Mouse",
                FullDescription =   "Mice, wholte and part" },
            new PartDescriptionModel()
            {
                EsteemCode = "LCD",
                Description = "LCD Screens",
                FullDescription =   "LCD Panels EG Notebook,POS" },
            new PartDescriptionModel()
            {
                EsteemCode = "MEM",
                Description = "Memory",
                FullDescription =   "Memory EG PC, Notebook, Server, POS" },
            new PartDescriptionModel()
            {
                EsteemCode = "MLB",
                Description = "Main logic boards",
                FullDescription =   "Main logic boards EG PC, Notebook, Server, POS" },
            new PartDescriptionModel()
            {
                EsteemCode = "MED",
                Description = "Media ",
                FullDescription =   "Media tapes, cd's" },
            new PartDescriptionModel()
            {
                EsteemCode = "NET",
                Description = "Network items",
                FullDescription =   "Network items EG Switches, KVM,Modems, print servers" },
            new PartDescriptionModel()
            {
                EsteemCode = "PSU",
                Description = "Power supply related",
                FullDescription =   "Power Supply EG PC, Notebook, Server, POS" },
            new PartDescriptionModel()
            {
                EsteemCode = "SCA",
                Description = "Scanner related",
                FullDescription =   "Scanners, whole and part" },
            new PartDescriptionModel()
            {
                EsteemCode = "TOU",
                Description = "Touch screen related",
                FullDescription =   "Touchscreens, Whole and part" },
            new PartDescriptionModel()
            {
                EsteemCode = "CDC",
                Description = "Cashdrawer complete",
                FullDescription =   "Complete cashdrawers" },
            new PartDescriptionModel()
            {
                EsteemCode = "CDP",
                Description = "Cashdrawer parts",
                FullDescription =   "Cashdrawer parts that don't fit into a primary code" },
            new PartDescriptionModel()
            {
                EsteemCode = "CHC",
                Description = "Chip and pin complete",
                FullDescription =   "Complete chip and pin devices" },
            new PartDescriptionModel()
            {
                EsteemCode = "CHP",
                Description = "Chip and Pin Parts",
                FullDescription =   "Chip and pin parts that don't fit into a primary code" },
            new PartDescriptionModel()
            {
                EsteemCode = "BAC",
                Description = "Back up complete",
                FullDescription =   "Complete back up devices EG RDX, DDS, LTO" },
            new PartDescriptionModel()
            {
                EsteemCode = "BAP",
                Description =  "Back up parts",
                FullDescription =   "Back up device parts that don't fit into a primary code" },
            new PartDescriptionModel()
            {
                EsteemCode = "HHT",
                Description = "Hand held terminals",
                FullDescription =   "Hand held terminals with screens and inputs(like couriers use)" },
            new PartDescriptionModel()
            {
                EsteemCode = "IMG",
                Description = "Imaging Devices",
                FullDescription =   "Devices to scan images such as flat bed scanners biometrics" },
            new PartDescriptionModel()
            {
                EsteemCode = "IHC",
                Description = "Inhouse Cleaning",
                FullDescription =  "Air dusters, cloths and cleaners" },
            new PartDescriptionModel()
            {
                EsteemCode = "IHT",
                Description = "Inhouse Tools",
                FullDescription =   "Repair centre tools engineer tools" },
            new PartDescriptionModel()
            {
                EsteemCode = "OPT",
                Description = "Optical drives",
                FullDescription =   "Complete optical devices EG CD, DVD, REV, Floppy" },
            new PartDescriptionModel()
            {
                EsteemCode = "KIO",
                Description = "Kiosk related",
                FullDescription =   "Kiosk parts that don't fit into a primary code" },
            new PartDescriptionModel()
            {
                EsteemCode = "PCC",
                Description = "PC complete",
                FullDescription =   "Complete Pc's of any form factor" },
            new PartDescriptionModel()
            {
                EsteemCode = "PCP",
                Description = "PC parts",
                FullDescription =   "Pc parts that don't fit into a primary code" },
            new PartDescriptionModel()
            {
                EsteemCode = "PRC",
                Description = "Printers Complete",
                FullDescription =   "Complete printers of all types EG Laser, DOT, Thermal" },
            new PartDescriptionModel()
            {
                EsteemCode = "PRP",
                Description = "Printer parts",
                FullDescription =   "Printer parts that don't all ready fit into a primary code" },
            new PartDescriptionModel()
            {
                EsteemCode = "MOC",
                Description =
                "Monitor complete",
                FullDescription =   "Complete monitors" },
            new PartDescriptionModel()
            {
                EsteemCode = "MOP",
                Description =
                "Monitor parts",
                FullDescription =   "Monitor parts EG Invertors, Stands, Buttons, Bezels" },
            new PartDescriptionModel()
            {
                EsteemCode = "NOC",
                Description =
                "Notebook complete",
                FullDescription =   "Complete notebooks of all types EG Netbook, Ultra" },
            new PartDescriptionModel()
            {
                EsteemCode = "NOP",
                Description = "Notebook parts",
                FullDescription =   "Notebook parts that don't fit into a primary code, docking stations" },
            new PartDescriptionModel()
            {
                EsteemCode = "OTH",
                Description = "Other parts",
                FullDescription =   "Parts not catergorised and new code creation is not warranted." },
            new PartDescriptionModel()
            {
                EsteemCode = "POC",
                Description =  "Pos complete",
                FullDescription =   "Complete POS units" },
            new PartDescriptionModel()
            {
                EsteemCode = "POP",
                Description = "Pos parts",
                FullDescription =   "POS unit parts that don't fit into a primary code" },
            new PartDescriptionModel()
            {
                EsteemCode = "SOF",
                Description = "Software",
                FullDescription =   "Software of all types EF FPP, Licence" },
            new PartDescriptionModel()
            {
                EsteemCode = "SVC",
                Description = "Server complete",
                FullDescription =   "Complete servers of any form factor and size" },
            new PartDescriptionModel()
            {
                EsteemCode = "SVP ",
                Description = "Server parts",
                FullDescription =   "Server parts that don't fit into a primary code" },
            new PartDescriptionModel()
            {
                EsteemCode = "TAC",
                Description = "Tablet Complete",
                FullDescription =   "Ipads etc" },
            new PartDescriptionModel()
            {
                EsteemCode = "TAP",
                Description = "Tablet Parts",
                FullDescription =   "Digitizers, screen protectors." },
            new PartDescriptionModel()
            {
                EsteemCode = "UPS ",
                Description = "UPS related",
                FullDescription =   "UPS, Whole and part" }
            };

            return returnList;
        }
    }
}
