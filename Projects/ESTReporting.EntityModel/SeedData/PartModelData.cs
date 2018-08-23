using ESTReporting.EntityModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace ESTReporting.EntityModel.SeedData
{
    public static class PartModelData
    {
        //public static List<string> manufacturerCodes = GetManufacturers().Select(a => a.Code).ToList();
        //public static List<string> manufacturerNames = GetManufacturers().Select(a => a.Name.ToUpper()).ToList();

        public static List<PartModel> GetModels()
        {
            var returnList = new List<PartModel>() {
                new PartModel()
                {
                    EsteemCode = "CAB",
                    Description = "Cables",
                    FullDescription = "Cables EG Ide, SAS, Kettle lead"
                },
                new PartModel()
                {
                    EsteemCode = "BAT",
                    Description = "Batteries",
                    FullDescription = "Batteries EG Notebook, Coin, UPS"
                },
                new PartModel()
                {
                    EsteemCode = "COM",
                    Description = "Com cards",
                    FullDescription = "Discrete cards EG PCI, ISA, VGA, PCIE based cards"
                },
                new PartModel()
                {
                    EsteemCode = "CON",
                    Description = "Convertors",
                    FullDescription = "Convertors and gender changers"
                },
                new PartModel()
                {
                    EsteemCode = "CPT",
                    Description = "Components",
                    FullDescription = "Components EG Suface mount chips, Capacitors"
                },
                new PartModel()
                {
                    EsteemCode = "HDD",
                    Description = "Hard drives Clone",
                    FullDescription = "Hard Disk Drives following the clone convention"
                },
                new PartModel() { EsteemCode = "HDD", Description =  "Hard drives MFG", FullDescription =   "Hard disk drives for servers and under manufacturers" },
                new PartModel()
                {
                    EsteemCode = "KBD",
                    Description = "Keyboards clone",
                    FullDescription =   "Keyboards,clone whole and part" },
                new PartModel()
                {
                    EsteemCode = "KBD",
                    Description = "Keyboards specialist",
                    FullDescription =   "Keyboards, specialist such as pos cherry alphameric" },
                new PartModel()
                {
                    EsteemCode = "MOU",
                    Description = "Mouse",
                    FullDescription =   "Mice, wholte and part" },
                new PartModel()
                {
                    EsteemCode = "LCD",
                    Description = "LCD Screens",
                    FullDescription =   "LCD Panels EG Notebook,POS" },
                new PartModel()
                {
                    EsteemCode = "MEM",
                    Description = "Memory",
                    FullDescription =   "Memory EG PC, Notebook, Server, POS" },
                new PartModel()
                {
                    EsteemCode = "MLB",
                    Description = "Main logic boards",
                    FullDescription =   "Main logic boards EG PC, Notebook, Server, POS" },
                new PartModel()
                {
                    EsteemCode = "MED",
                    Description = "Media ",
                    FullDescription =   "Media tapes, cd's" },
                new PartModel()
                {
                    EsteemCode = "NET",
                    Description = "Network items",
                    FullDescription =   "Network items EG Switches, KVM,Modems, print servers" },
                new PartModel()
                {
                    EsteemCode = "PSU",
                    Description = "Power supply related",
                    FullDescription =   "Power Supply EG PC, Notebook, Server, POS" },
                new PartModel()
                {
                    EsteemCode = "SCA",
                    Description = "Scanner related",
                    FullDescription =   "Scanners, whole and part" },
                new PartModel()
                {
                    EsteemCode = "TOU",
                    Description = "Touch screen related",
                    FullDescription =   "Touchscreens, Whole and part" },
                new PartModel()
                {
                    EsteemCode = "CDC",
                    Description = "Cashdrawer complete",
                    FullDescription =   "Complete cashdrawers" },
                new PartModel()
                {
                    EsteemCode = "CDP",
                    Description = "Cashdrawer parts",
                    FullDescription =   "Cashdrawer parts that don't fit into a primary code" },
                new PartModel()
                {
                    EsteemCode = "CHC",
                    Description = "Chip and pin complete",
                    FullDescription =   "Complete chip and pin devices" },
                new PartModel()
                {
                    EsteemCode = "CHP",
                    Description = "Chip and Pin Parts",
                    FullDescription =   "Chip and pin parts that don't fit into a primary code" },
                new PartModel()
                {
                    EsteemCode = "BAC",
                    Description = "Back up complete",
                    FullDescription =   "Complete back up devices EG RDX, DDS, LTO" },
                new PartModel()
                {
                    EsteemCode = "BAP",
                    Description =  "Back up parts",
                    FullDescription =   "Back up device parts that don't fit into a primary code" },
                new PartModel()
                {
                    EsteemCode = "HHT",
                    Description = "Hand held terminals",
                    FullDescription =   "Hand held terminals with screens and inputs(like couriers use)" },
                new PartModel()
                {
                    EsteemCode = "IMG",
                    Description = "Imaging Devices",
                    FullDescription =   "Devices to scan images such as flat bed scanners biometrics" },
                new PartModel()
                {
                    EsteemCode = "IHC",
                    Description = "Inhouse Cleaning",
                    FullDescription =  "Air dusters, cloths and cleaners" },
                new PartModel()
                {
                    EsteemCode = "IHT",
                    Description = "Inhouse Tools",
                    FullDescription =   "Repair centre tools engineer tools" },
                new PartModel()
                {
                    EsteemCode = "OPT",
                    Description = "Optical drives",
                    FullDescription =   "Complete optical devices EG CD, DVD, REV, Floppy" },
                new PartModel()
                {
                    EsteemCode = "KIO",
                    Description = "Kiosk related",
                    FullDescription =   "Kiosk parts that don't fit into a primary code" },
                new PartModel()
                {
                    EsteemCode = "PCC",
                    Description = "PC complete",
                    FullDescription =   "Complete Pc's of any form factor" },
                new PartModel()
                {
                    EsteemCode = "PCP",
                    Description = "PC parts",
                    FullDescription =   "Pc parts that don't fit into a primary code" },
                new PartModel()
                {
                    EsteemCode = "PRC",
                    Description = "Printers Complete",
                    FullDescription =   "Complete printers of all types EG Laser, DOT, Thermal" },
                new PartModel()
                {
                    EsteemCode = "PRP",
                    Description = "Printer parts",
                    FullDescription =   "Printer parts that don't all ready fit into a primary code" },
                new PartModel()
                {
                    EsteemCode = "MOC",
                    Description = "Monitor complete",
                    FullDescription =   "Complete monitors" },
                new PartModel()
                {
                    EsteemCode = "MOP",
                    Description = "Monitor parts",
                    FullDescription =   "Monitor parts EG Invertors, Stands, Buttons, Bezels" },
                new PartModel()
                {
                    EsteemCode = "NOC",
                    Description = "Notebook complete",
                    FullDescription =   "Complete notebooks of all types EG Netbook, Ultra" },
                new PartModel()
                {
                    EsteemCode = "NOP",
                    Description = "Notebook parts",
                    FullDescription =   "Notebook parts that don't fit into a primary code, docking stations" },
                new PartModel()
                {
                    EsteemCode = "OTH",
                    Description = "Other parts",
                    FullDescription = "Parts not catergorised and new code creation is not warranted." },
                new PartModel()
                {
                    EsteemCode = "POC",
                    Description = "Pos complete",
                    FullDescription = "Complete POS units" },
                new PartModel()
                {
                    EsteemCode = "POP",
                    Description = "Pos parts",
                    FullDescription =   "POS unit parts that don't fit into a primary code" },
                new PartModel()
                {
                    EsteemCode = "SOF",
                    Description = "Software",
                    FullDescription = "Software of all types EF FPP, Licence" },
                new PartModel()
                {
                    EsteemCode = "SVC",
                    Description = "Server complete",
                    FullDescription = "Complete servers of any form factor and size" },
                new PartModel()
                {
                    EsteemCode = "SVP ",
                    Description = "Server parts",
                    FullDescription =   "Server parts that don't fit into a primary code" },
                new PartModel()
                {
                    EsteemCode = "TAC",
                    Description = "Tablet Complete",
                    FullDescription =   "Ipads etc" },
                new PartModel()
                {
                    EsteemCode = "TAP",
                    Description = "Tablet Parts",
                    FullDescription =   "Digitizers, screen protectors." },
                new PartModel()
                {
                    EsteemCode = "UPS ",
                    Description = "UPS related",
                    FullDescription =   "UPS, Whole and part" }
            };

            return returnList;

        }
    }
}
