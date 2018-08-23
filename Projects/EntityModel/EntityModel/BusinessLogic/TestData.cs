using EntityModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace EntityModel.BusinessLogic
{
    public class ManufacturerModel_Data
    {
        public static List<string> manufacturerCodes = GetManufacturers().Select(a => a.Code).ToList();
        public static List<string> manufacturerNames = GetManufacturers().Select(a => a.Name.ToUpper()).ToList();

        public static List<ManufacturerModel> GetManufacturers()
        {
            var returnList = new List<ManufacturerModel>()
            {
                new ManufacturerModel() { Name = "Hewlet-Packard", Code = "HP", CodeEsteem = "HPX", CodeEsteemAlt = "HEW" },
                new ManufacturerModel() { Name = "Dell", Code = "DELL", CodeEsteem = "DEL" },
                new ManufacturerModel() { Name = "IBM", Code = "IBM", CodeEsteem = "IBM" },
                new ManufacturerModel() { Name = "Lenovo", Code = "LEN", CodeEsteem = "LEN "},
                new ManufacturerModel() { Name = "ACER", Code = "ACE", CodeEsteem = "ACE" },
                new ManufacturerModel() { Name = "DigiPos", Code = "DIG", CodeEsteem = "DIG" },
                new ManufacturerModel() { Name = "J2 Retailing", Code = "J2X", CodeEsteem = "J2X", CodeEsteemAlt = "J2" },
                new ManufacturerModel() { Name = "OKI Printers", Code = "OKI", CodeEsteem = "OKI" },
                new ManufacturerModel() { Name = "Lexmark",  Code = "LEX", CodeEsteem = "LEX" },
                new ManufacturerModel() { Name = "Epson", Code = "EPS", CodeEsteem = "EPS" },
                new ManufacturerModel() { Name = "Samsung", Code = "SAM", CodeEsteem = "SAM" },
                new ManufacturerModel() { Name = "Honeywell / Metrologic", Code = "HON", CodeEsteem = "HON", CodeEsteemAlt = "MET" },
                new ManufacturerModel() { Name = "Generic Clone Parts", Code = "CLO", CodeEsteem = "CLO", CodeEsteemAlt = "GEN" },
                new ManufacturerModel() { Name = "NEC", Code = "NEC", CodeEsteem = "NEC", CodeEsteemAlt = "NEC" },
                new ManufacturerModel() { Name = "Optoma", Code = "OPT", CodeEsteem = "OPT", CodeEsteemAlt = "OPT" },
                new ManufacturerModel() { Name = "Apple", Code = "TAB", CodeEsteem = "TAB", CodeEsteemAlt = "IPAD" },
                new ManufacturerModel() { Name = "Micros", Code = "MIC", CodeEsteem = "MIC" },
                new ManufacturerModel() { Name = "Motorola", Code = "MOT", CodeEsteemAlt = "MOTO" },
                new ManufacturerModel() { Name = "Cisco", Code = "CIS", CodeEsteemAlt = "CISC" },
                new ManufacturerModel() { Name = "MICROSOFT", Code = "MIS", CodeEsteemAlt = "MS" },
                new ManufacturerModel() { Name = "Topaz", Code = "TOP", CodeEsteemAlt = "TOPZ" },
                new ManufacturerModel() { Name = "Tecdesk", Code = "TEC", CodeEsteemAlt = "TECD" },
                new ManufacturerModel() { Name = "Brother", Code = "BRO", CodeEsteemAlt = "BRO" },
                new ManufacturerModel() { Name = "Canon", Code = "CAN", CodeEsteemAlt = "CAN" },
                new ManufacturerModel() { Name = "3COM", Code = "3CO", CodeEsteemAlt = "3COM" },
                new ManufacturerModel() { Name = "Tenda", Code = "TEN", CodeEsteemAlt = "TEN" },
                new ManufacturerModel() { Name = "Fujitsu", Code = "FUJ", CodeEsteemAlt = "FUJI" },
                new ManufacturerModel() { Name = "Intel", Code = "INT", CodeEsteemAlt = "INTL" },
                new ManufacturerModel() { Name = "Hitachi", Code = "HIT", CodeEsteemAlt = "HIT" },
                new ManufacturerModel() { Name = "Verifone", Code = "VER", CodeEsteemAlt = "VER" },
                new ManufacturerModel() { Name = "Ricoh", Code = "RIC", CodeEsteemAlt = "RIC" },
                new ManufacturerModel() { Name = "Toshiba", Code = "TOS", CodeEsteemAlt = "TOSH" },
                new ManufacturerModel() { Name = "TALLY", Code = "TAL", CodeEsteemAlt = "TAL" },
                new ManufacturerModel() { Name = "iiyama", Code = "IIY", CodeEsteemAlt = "IIY" },
                new ManufacturerModel() { Name = "Nokia", Code = "NOK", CodeEsteemAlt = "NOK" },
                new ManufacturerModel() { Name = "Compaq", Code = "COM", CodeEsteemAlt = "COM" },
                new ManufacturerModel() { Name = "Netgear", Code = "NET", CodeEsteemAlt = "NET" },
                new ManufacturerModel() { Name = "Belkin", Code = "BEL", CodeEsteemAlt = "BELK" },
                new ManufacturerModel() { Name = "Iomega", Code = "IOM", CodeEsteemAlt = "IOM" },
                new ManufacturerModel() { Name = "Panasonic", Code = "PAN", CodeEsteemAlt = "PAN" },
                new ManufacturerModel() { Name = "Phillips", Code = "PHI", CodeEsteemAlt = "PHI" },
                new ManufacturerModel() { Name = "DLink", Code = "DLI", CodeEsteemAlt = "D-LINK" },
                new ManufacturerModel() { Name = "Xerox", Code = "XER", CodeEsteemAlt = "XER" },
                new ManufacturerModel() { Name = "SONY", Code = "SON", CodeEsteemAlt = "SON" },
                new ManufacturerModel() { Name = "Bybox", Code = "BYB", CodeEsteemAlt = "BYB" },
                new ManufacturerModel() { Name = "Buffalo", Code = "BUF", CodeEsteemAlt = "BUF" },
                new ManufacturerModel() { Name = "Boca", Code = "BOC", CodeEsteemAlt = "BOC" },
                new ManufacturerModel() { Name = "Axis", Code = "AXI", CodeEsteemAlt = "AXI" },
                new ManufacturerModel() { Name = "Aures", Code = "AUR", CodeEsteemAlt = "AUR" },
                new ManufacturerModel() { Name = "ASUS", Code = "ASU", CodeEsteemAlt = "ASU" },
                new ManufacturerModel() { Name = "Mitel", Code = "MIT", CodeEsteemAlt = "MIT" },
                new ManufacturerModel() { Name = "Novell", Code = "NOV", CodeEsteemAlt = "NOV" },
                new ManufacturerModel() { Name = "Prologic", Code = "PRO", CodeEsteemAlt = "PRO" },
                new ManufacturerModel() { Name = "Quantum", Code = "QUA", CodeEsteemAlt = "QUA" },
                new ManufacturerModel() { Name = "Qume", Code = "QUM", CodeEsteemAlt = "QUM" },
                new ManufacturerModel() { Name = "Racal", Code = "RAC", CodeEsteemAlt = "RAC" },
                new ManufacturerModel() { Name = "Ramesys", Code = "RAM", CodeEsteemAlt = "RAM" },
                new ManufacturerModel() { Name = "Realpos", Code = "REP", CodeEsteemAlt = "REP" },
                new ManufacturerModel() { Name = "Redsky", Code = "RED", CodeEsteemAlt = "RED" },
                new ManufacturerModel() { Name = "Reed", Code = "REE", CodeEsteemAlt = "REE" },
                new ManufacturerModel() { Name = "Retail", Code = "RET", CodeEsteemAlt = "RET" },
                new ManufacturerModel() { Name = "Sarian", Code = "SAR", CodeEsteemAlt = "SAR" },
                new ManufacturerModel() { Name = "SB", Code = "SBX", CodeEsteemAlt = "SB" },
                new ManufacturerModel() { Name = "Honeywell", Code = "HON", CodeEsteemAlt = "HON" },
                new ManufacturerModel() { Name = "Star", Code = "STA", CodeEsteemAlt = "STA" },
                new ManufacturerModel() { Name = "Scanman", Code = "SCM", CodeEsteemAlt = "SCM" },
                new ManufacturerModel() { Name = "Scanpartner", Code = "SCP", CodeEsteemAlt = "SCP" },
                new ManufacturerModel() { Name = "Scanplus", Code = "SCS", CodeEsteemAlt = "SCS" },
                new ManufacturerModel() { Name = "Seagate", Code = "SEA", CodeEsteemAlt = "SEA" },
                new ManufacturerModel() { Name = "Sherwood", Code = "SHE", CodeEsteemAlt = "SHE" },
                new ManufacturerModel() { Name = "Shiva", Code = "SHI", CodeEsteemAlt = "SHI" },
                new ManufacturerModel() { Name = "Siemens", Code = "SIE", CodeEsteemAlt = "SIE" },
                new ManufacturerModel() { Name = "Silex", Code = "SIL", CodeEsteemAlt = "SIL" },
                new ManufacturerModel() { Name = "Siracom", Code = "SIR", CodeEsteemAlt = "SIR" },
                new ManufacturerModel() { Name = "Veritas", Code = "VER", CodeEsteemAlt = "VER" },
                new ManufacturerModel() { Name = "Apc", Code = "APC", CodeEsteemAlt = "APC" },
                new ManufacturerModel() { Name = "Apricot", Code = "APR", CodeEsteemAlt = "APR" },
                new ManufacturerModel() { Name = "Arnel", Code = "ARN", CodeEsteemAlt = "ARN" },
                new ManufacturerModel() { Name = "Arnet", Code = "ART", CodeEsteemAlt = "ART" },
                new ManufacturerModel() { Name = "Asrock", Code = "ASR", CodeEsteemAlt = "ASR" },
                new ManufacturerModel() { Name = "Clone", Code = "CLO", CodeEsteemAlt = "CLO" },
            };

            return returnList;
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
                Description = "Monitor complete",
                FullDescription =   "Complete monitors" },
            new PartDescriptionModel()
            {
                EsteemCode = "MOP",
                Description = "Monitor parts",
                FullDescription =   "Monitor parts EG Invertors, Stands, Buttons, Bezels" },
            new PartDescriptionModel()
            {
                EsteemCode = "NOC",
                Description = "Notebook complete",
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
                FullDescription = "Parts not catergorised and new code creation is not warranted." },
            new PartDescriptionModel()
            {
                EsteemCode = "POC",
                Description = "Pos complete",
                FullDescription = "Complete POS units" },
            new PartDescriptionModel()
            {
                EsteemCode = "POP",
                Description = "Pos parts",
                FullDescription =   "POS unit parts that don't fit into a primary code" },
            new PartDescriptionModel()
            {
                EsteemCode = "SOF",
                Description = "Software",
                FullDescription = "Software of all types EF FPP, Licence" },
            new PartDescriptionModel()
            {
                EsteemCode = "SVC",
                Description = "Server complete",
                FullDescription = "Complete servers of any form factor and size" },
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
