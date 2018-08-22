using ESTReporting.EntityModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace ESTReporting.EntityModel.SeedData
{
    public static class PartManufacturer_Data
    {
        public static List<string> manufacturerCodes = GetManufacturers().Select(a => a.Code).ToList();
        public static List<string> manufacturerNames = GetManufacturers().Select(a => a.Name.ToUpper()).ToList();

        public static List<PartManufacturer> GetManufacturers()
        {
            var returnList = new List<PartManufacturer>()
            {
                new PartManufacturer() { Name = "Hewlet-Packard", Code = "HP", CodeEsteem = "HPX", CodeEsteemAlt = "HEW" },
                new PartManufacturer() { Name = "Dell", Code = "DELL", CodeEsteem = "DEL" },
                new PartManufacturer() { Name = "IBM", Code = "IBM", CodeEsteem = "IBM" },
                new PartManufacturer() { Name = "Lenovo", Code = "LEN", CodeEsteem = "LEN "},
                new PartManufacturer() { Name = "ACER", Code = "ACE", CodeEsteem = "ACE" },
                new PartManufacturer() { Name = "DigiPos", Code = "DIG", CodeEsteem = "DIG" },
                new PartManufacturer() { Name = "J2 Retailing", Code = "J2X", CodeEsteem = "J2X", CodeEsteemAlt = "J2" },
                new PartManufacturer() { Name = "OKI Printers", Code = "OKI", CodeEsteem = "OKI" },
                new PartManufacturer() { Name = "Lexmark",  Code = "LEX", CodeEsteem = "LEX" },
                new PartManufacturer() { Name = "Epson", Code = "EPS", CodeEsteem = "EPS" },
                new PartManufacturer() { Name = "Samsung", Code = "SAM", CodeEsteem = "SAM" },
                new PartManufacturer() { Name = "Honeywell / Metrologic", Code = "HON", CodeEsteem = "HON", CodeEsteemAlt = "MET" },
                new PartManufacturer() { Name = "Generic Clone Parts", Code = "CLO", CodeEsteem = "CLO", CodeEsteemAlt = "GEN" },
                new PartManufacturer() { Name = "NEC", Code = "NEC", CodeEsteem = "NEC", CodeEsteemAlt = "NEC" },
                new PartManufacturer() { Name = "Optoma", Code = "OPT", CodeEsteem = "OPT", CodeEsteemAlt = "OPT" },
                new PartManufacturer() { Name = "Apple", Code = "TAB", CodeEsteem = "TAB", CodeEsteemAlt = "IPAD" },
                new PartManufacturer() { Name = "Micros", Code = "MIC", CodeEsteem = "MIC" },
                new PartManufacturer() { Name = "Motorola", Code = "MOT", CodeEsteemAlt = "MOTO" },
                new PartManufacturer() { Name = "Cisco", Code = "CIS", CodeEsteemAlt = "CISC" },
                new PartManufacturer() { Name = "MICROSOFT", Code = "MIS", CodeEsteemAlt = "MS" },
                new PartManufacturer() { Name = "Topaz", Code = "TOP", CodeEsteemAlt = "TOPZ" },
                new PartManufacturer() { Name = "Tecdesk", Code = "TEC", CodeEsteemAlt = "TECD" },
                new PartManufacturer() { Name = "Brother", Code = "BRO", CodeEsteemAlt = "BRO" },
                new PartManufacturer() { Name = "Canon", Code = "CAN", CodeEsteemAlt = "CAN" },
                new PartManufacturer() { Name = "3COM", Code = "3CO", CodeEsteemAlt = "3COM" },
                new PartManufacturer() { Name = "Tenda", Code = "TEN", CodeEsteemAlt = "TEN" },
                new PartManufacturer() { Name = "Fujitsu", Code = "FUJ", CodeEsteemAlt = "FUJI" },
                new PartManufacturer() { Name = "Intel", Code = "INT", CodeEsteemAlt = "INTL" },
                new PartManufacturer() { Name = "Hitachi", Code = "HIT", CodeEsteemAlt = "HIT" },
                new PartManufacturer() { Name = "Verifone", Code = "VER", CodeEsteemAlt = "VER" },
                new PartManufacturer() { Name = "Ricoh", Code = "RIC", CodeEsteemAlt = "RIC" },
                new PartManufacturer() { Name = "Toshiba", Code = "TOS", CodeEsteemAlt = "TOSH" },
                new PartManufacturer() { Name = "TALLY", Code = "TAL", CodeEsteemAlt = "TAL" },
                new PartManufacturer() { Name = "iiyama", Code = "IIY", CodeEsteemAlt = "IIY" },
                new PartManufacturer() { Name = "Nokia", Code = "NOK", CodeEsteemAlt = "NOK" },
                new PartManufacturer() { Name = "Compaq", Code = "COM", CodeEsteemAlt = "COM" },
                new PartManufacturer() { Name = "Netgear", Code = "NET", CodeEsteemAlt = "NET" },
                new PartManufacturer() { Name = "Belkin", Code = "BEL", CodeEsteemAlt = "BELK" },
                new PartManufacturer() { Name = "Iomega", Code = "IOM", CodeEsteemAlt = "IOM" },
                new PartManufacturer() { Name = "Panasonic", Code = "PAN", CodeEsteemAlt = "PAN" },
                new PartManufacturer() { Name = "Phillips", Code = "PHI", CodeEsteemAlt = "PHI" },
                new PartManufacturer() { Name = "DLink", Code = "DLI", CodeEsteemAlt = "D-LINK" },
                new PartManufacturer() { Name = "Xerox", Code = "XER", CodeEsteemAlt = "XER" },
                new PartManufacturer() { Name = "SONY", Code = "SON", CodeEsteemAlt = "SON" },
                new PartManufacturer() { Name = "Bybox", Code = "BYB", CodeEsteemAlt = "BYB" },
                new PartManufacturer() { Name = "Buffalo", Code = "BUF", CodeEsteemAlt = "BUF" },
                new PartManufacturer() { Name = "Boca", Code = "BOC", CodeEsteemAlt = "BOC" },
                new PartManufacturer() { Name = "Axis", Code = "AXI", CodeEsteemAlt = "AXI" },
                new PartManufacturer() { Name = "Aures", Code = "AUR", CodeEsteemAlt = "AUR" },
                new PartManufacturer() { Name = "ASUS", Code = "ASU", CodeEsteemAlt = "ASU" },
                new PartManufacturer() { Name = "Mitel", Code = "MIT", CodeEsteemAlt = "MIT" },
                new PartManufacturer() { Name = "Novell", Code = "NOV", CodeEsteemAlt = "NOV" },
                new PartManufacturer() { Name = "Prologic", Code = "PRO", CodeEsteemAlt = "PRO" },
                new PartManufacturer() { Name = "Quantum", Code = "QUA", CodeEsteemAlt = "QUA" },
                new PartManufacturer() { Name = "QUME", Code = "QUM", CodeEsteemAlt = "QUM" },
                new PartManufacturer() { Name = "RACAL", Code = "RAC", CodeEsteemAlt = "RAC" },
                new PartManufacturer() { Name = "RAMESYS", Code = "RAM", CodeEsteemAlt = "RAM" },
                new PartManufacturer() { Name = "REALPOS", Code = "REP", CodeEsteemAlt = "REP" },
                new PartManufacturer() { Name = "REDSKY", Code = "RED", CodeEsteemAlt = "RED" },
                new PartManufacturer() { Name = "REED", Code = "REE", CodeEsteemAlt = "REE" },
                new PartManufacturer() { Name = "RETAIL", Code = "RET", CodeEsteemAlt = "RET" },
                new PartManufacturer() { Name = "SARIAN", Code = "SAR", CodeEsteemAlt = "SAR" },
                new PartManufacturer() { Name = "SB", Code = "SBX", CodeEsteemAlt = "SB" },
                new PartManufacturer() { Name = "HONEYWELL", Code = "HON", CodeEsteemAlt = "HON" },
                new PartManufacturer() { Name = "STAR", Code = "STA", CodeEsteemAlt = "STA" },
                new PartManufacturer() { Name = "SCANMAN", Code = "SCM", CodeEsteemAlt = "SCM" },
                new PartManufacturer() { Name = "SCANPARTNER", Code = "SCP", CodeEsteemAlt = "SCP" },
                new PartManufacturer() { Name = "SCANPLUS", Code = "SCS", CodeEsteemAlt = "SCS" },
                new PartManufacturer() { Name = "SEAGATE", Code = "SEA", CodeEsteemAlt = "SEA" },
                new PartManufacturer() { Name = "SHERWOOD", Code = "SHE", CodeEsteemAlt = "SHE" },
                new PartManufacturer() { Name = "SHIVA", Code = "SHI", CodeEsteemAlt = "SHI" },
                new PartManufacturer() { Name = "SIEMENS", Code = "SIE", CodeEsteemAlt = "SIE" },
                new PartManufacturer() { Name = "SILEX", Code = "SIL", CodeEsteemAlt = "SIL" },
                new PartManufacturer() { Name = "SIRACOM", Code = "SIR", CodeEsteemAlt = "SIR" },
                new PartManufacturer() { Name = "VERITAS", Code = "VER", CodeEsteemAlt = "VER" },
                new PartManufacturer() { Name = "APC", Code = "APC", CodeEsteemAlt = "APC" },
                new PartManufacturer() { Name = "APRICOT", Code = "APR", CodeEsteemAlt = "APR" },
                new PartManufacturer() { Name = "ARNEL", Code = "ARN", CodeEsteemAlt = "ARN" },
                new PartManufacturer() { Name = "ARNET", Code = "ART", CodeEsteemAlt = "ART" },
                new PartManufacturer() { Name = "ASROCK", Code = "ASR", CodeEsteemAlt = "ASR" },
                new PartManufacturer() { Name = "CLONE", Code = "CLO", CodeEsteemAlt = "CLO" },
            };

            return returnList;
        }

        //public static List

        public static List<ESTPartDescription> GetESTPartDescriptions()
        {
            var returnList = new List<ESTPartDescription>() {
            new ESTPartDescription()
            {
                EsteemCode = "CAB",
                Description = "Cables",
                FullDescription = "Cables EG Ide, SAS, Kettle lead"
            },
            new ESTPartDescription()
            {
                EsteemCode = "BAT",
                Description = "Batteries",
                FullDescription = "Batteries EG Notebook, Coin, UPS"
            },
            new ESTPartDescription()
            {
                EsteemCode = "COM",
                Description = "Com cards",
                FullDescription = "Discrete cards EG PCI, ISA, VGA, PCIE based cards"
            },
            new ESTPartDescription()
            {
                EsteemCode = "CON",
                Description = "Convertors",
                FullDescription = "Convertors and gender changers"
            },
            new ESTPartDescription()
            {
                EsteemCode = "CPT",
                Description = "Components",
                FullDescription = "Components EG Suface mount chips, Capacitors"
            },
            new ESTPartDescription()
            {
                EsteemCode = "HDD",
                Description = "Hard drives Clone",
                FullDescription = "Hard Disk Drives following the clone convention"
            },
            new ESTPartDescription() { EsteemCode = "HDD", Description =  "Hard drives MFG", FullDescription =   "Hard disk drives for servers and under manufacturers" },
            new ESTPartDescription()
            {
                EsteemCode = "KBD",
                Description = "Keyboards clone",
                FullDescription =   "Keyboards,clone whole and part" },
            new ESTPartDescription()
            {
                EsteemCode = "KBD",
                Description = "Keyboards specialist",
                FullDescription =   "Keyboards, specialist such as pos cherry alphameric" },
            new ESTPartDescription()
            {
                EsteemCode = "MOU",
                Description = "Mouse",
                FullDescription =   "Mice, wholte and part" },
            new ESTPartDescription()
            {
                EsteemCode = "LCD",
                Description = "LCD Screens",
                FullDescription =   "LCD Panels EG Notebook,POS" },
            new ESTPartDescription()
            {
                EsteemCode = "MEM",
                Description = "Memory",
                FullDescription =   "Memory EG PC, Notebook, Server, POS" },
            new ESTPartDescription()
            {
                EsteemCode = "MLB",
                Description = "Main logic boards",
                FullDescription =   "Main logic boards EG PC, Notebook, Server, POS" },
            new ESTPartDescription()
            {
                EsteemCode = "MED",
                Description = "Media ",
                FullDescription =   "Media tapes, cd's" },
            new ESTPartDescription()
            {
                EsteemCode = "NET",
                Description = "Network items",
                FullDescription =   "Network items EG Switches, KVM,Modems, print servers" },
            new ESTPartDescription()
            {
                EsteemCode = "PSU",
                Description = "Power supply related",
                FullDescription =   "Power Supply EG PC, Notebook, Server, POS" },
            new ESTPartDescription()
            {
                EsteemCode = "SCA",
                Description = "Scanner related",
                FullDescription =   "Scanners, whole and part" },
            new ESTPartDescription()
            {
                EsteemCode = "TOU",
                Description = "Touch screen related",
                FullDescription =   "Touchscreens, Whole and part" },
            new ESTPartDescription()
            {
                EsteemCode = "CDC",
                Description = "Cashdrawer complete",
                FullDescription =   "Complete cashdrawers" },
            new ESTPartDescription()
            {
                EsteemCode = "CDP",
                Description = "Cashdrawer parts",
                FullDescription =   "Cashdrawer parts that don't fit into a primary code" },
            new ESTPartDescription()
            {
                EsteemCode = "CHC",
                Description = "Chip and pin complete",
                FullDescription =   "Complete chip and pin devices" },
            new ESTPartDescription()
            {
                EsteemCode = "CHP",
                Description = "Chip and Pin Parts",
                FullDescription =   "Chip and pin parts that don't fit into a primary code" },
            new ESTPartDescription()
            {
                EsteemCode = "BAC",
                Description = "Back up complete",
                FullDescription =   "Complete back up devices EG RDX, DDS, LTO" },
            new ESTPartDescription()
            {
                EsteemCode = "BAP",
                Description =  "Back up parts",
                FullDescription =   "Back up device parts that don't fit into a primary code" },
            new ESTPartDescription()
            {
                EsteemCode = "HHT",
                Description = "Hand held terminals",
                FullDescription =   "Hand held terminals with screens and inputs(like couriers use)" },
            new ESTPartDescription()
            {
                EsteemCode = "IMG",
                Description = "Imaging Devices",
                FullDescription =   "Devices to scan images such as flat bed scanners biometrics" },
            new ESTPartDescription()
            {
                EsteemCode = "IHC",
                Description = "Inhouse Cleaning",
                FullDescription =  "Air dusters, cloths and cleaners" },
            new ESTPartDescription()
            {
                EsteemCode = "IHT",
                Description = "Inhouse Tools",
                FullDescription =   "Repair centre tools engineer tools" },
            new ESTPartDescription()
            {
                EsteemCode = "OPT",
                Description = "Optical drives",
                FullDescription =   "Complete optical devices EG CD, DVD, REV, Floppy" },
            new ESTPartDescription()
            {
                EsteemCode = "KIO",
                Description = "Kiosk related",
                FullDescription =   "Kiosk parts that don't fit into a primary code" },
            new ESTPartDescription()
            {
                EsteemCode = "PCC",
                Description = "PC complete",
                FullDescription =   "Complete Pc's of any form factor" },
            new ESTPartDescription()
            {
                EsteemCode = "PCP",
                Description = "PC parts",
                FullDescription =   "Pc parts that don't fit into a primary code" },
            new ESTPartDescription()
            {
                EsteemCode = "PRC",
                Description = "Printers Complete",
                FullDescription =   "Complete printers of all types EG Laser, DOT, Thermal" },
            new ESTPartDescription()
            {
                EsteemCode = "PRP",
                Description = "Printer parts",
                FullDescription =   "Printer parts that don't all ready fit into a primary code" },
            new ESTPartDescription()
            {
                EsteemCode = "MOC",
                Description =
                "Monitor complete",
                FullDescription =   "Complete monitors" },
            new ESTPartDescription()
            {
                EsteemCode = "MOP",
                Description =
                "Monitor parts",
                FullDescription =   "Monitor parts EG Invertors, Stands, Buttons, Bezels" },
            new ESTPartDescription()
            {
                EsteemCode = "NOC",
                Description =
                "Notebook complete",
                FullDescription =   "Complete notebooks of all types EG Netbook, Ultra" },
            new ESTPartDescription()
            {
                EsteemCode = "NOP",
                Description = "Notebook parts",
                FullDescription =   "Notebook parts that don't fit into a primary code, docking stations" },
            new ESTPartDescription()
            {
                EsteemCode = "OTH",
                Description = "Other parts",
                FullDescription =   "Parts not catergorised and new code creation is not warranted." },
            new ESTPartDescription()
            {
                EsteemCode = "POC",
                Description =  "Pos complete",
                FullDescription =   "Complete POS units" },
            new ESTPartDescription()
            {
                EsteemCode = "POP",
                Description = "Pos parts",
                FullDescription =   "POS unit parts that don't fit into a primary code" },
            new ESTPartDescription()
            {
                EsteemCode = "SOF",
                Description = "Software",
                FullDescription =   "Software of all types EF FPP, Licence" },
            new ESTPartDescription()
            {
                EsteemCode = "SVC",
                Description = "Server complete",
                FullDescription =   "Complete servers of any form factor and size" },
            new ESTPartDescription()
            {
                EsteemCode = "SVP ",
                Description = "Server parts",
                FullDescription =   "Server parts that don't fit into a primary code" },
            new ESTPartDescription()
            {
                EsteemCode = "TAC",
                Description = "Tablet Complete",
                FullDescription =   "Ipads etc" },
            new ESTPartDescription()
            {
                EsteemCode = "TAP",
                Description = "Tablet Parts",
                FullDescription =   "Digitizers, screen protectors." },
            new ESTPartDescription()
            {
                EsteemCode = "UPS ",
                Description = "UPS related",
                FullDescription =   "UPS, Whole and part" }
            };

            return returnList;
        }
    }
}
