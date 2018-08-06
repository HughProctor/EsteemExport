using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel.Test
{
    public class Manufacturer
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string CodeEsteem { get; set; }
        public string CodeEsteemAlt { get; set; }
    }

    public  class ManufacturerList
    {
        public static List<Manufacturer> GetManufacturers()
        {
            var returnList = new List<Manufacturer>()
            {
                new Manufacturer() { Name = "Hewlet Packard", Code = "HP", CodeEsteem = "HPX", CodeEsteemAlt = "HEW" },
                new Manufacturer() { Name = "Dell", Code = "DELL", CodeEsteem = "DEL" },
                new Manufacturer() { Name = "IBM", Code = "IBM", CodeEsteem = "IBM" },
                new Manufacturer() { Name = "Lenovo", Code = "LEN", CodeEsteem = "LEN "},
                new Manufacturer() { Name = "ACER", Code = "ACE", CodeEsteem = "ACE" },
                new Manufacturer() { Name = "DigiPos", Code = "DIG", CodeEsteem = "DIG" },
                new Manufacturer() { Name = "J2 Retailing", Code = "J2X", CodeEsteem = "J2X", CodeEsteemAlt = "J2" },
                new Manufacturer() { Name = "OKI Printers", Code = "OKI", CodeEsteem = "OKI" },
                new Manufacturer() { Name = "Lexmark",  Code = "LEX", CodeEsteem = "LEX" },
                new Manufacturer() { Name = "Epson", Code = "EPS", CodeEsteem = "EPS" },
                new Manufacturer() { Name = "Samsung", Code = "SAM", CodeEsteem = "SAM" },
                new Manufacturer() { Name = "Honeywell / Metrologic", Code = "HON", CodeEsteem = "HON", CodeEsteemAlt = "MET" },
                new Manufacturer() { Name = "Generic Clone Parts", Code = "CLO", CodeEsteem = "CLO", CodeEsteemAlt = "GEN" }
            };

            return returnList;
        }


        public List<PartDescription> GetPartDescriptions()
        {
            var returnList = new List<PartDescription>() {
            new PartDescription()
            {
                EsteemCode = "CAB",
                Description = "Cables",
                FullDescription = "Cables EG Ide, SAS, Kettle lead"
            },
            new PartDescription()
            {
                EsteemCode = "BAT",
                Description = "Batteries",
                FullDescription = "Batteries EG Notebook, Coin, UPS"
            },
            new PartDescription()
            {
                EsteemCode = "COM",
                Description = "Com cards",
                FullDescription = "Discrete cards EG PCI, ISA, VGA, PCIE based cards"
            },
            new PartDescription()
            {
                EsteemCode = "CON",
                Description = "Convertors",
                FullDescription = "Convertors and gender changers"
            },
            new PartDescription()
            {
                EsteemCode = "CPT",
                Description = "Components",
                FullDescription = "Components EG Suface mount chips, Capacitors"
            },
            new PartDescription()
            {
                EsteemCode = "HDD",
                Description = "Hard drives Clone",
                FullDescription = "Hard Disk Drives following the clone convention"
            },
            new PartDescription() { EsteemCode = "HDD", Description =  "Hard drives MFG", FullDescription =   "Hard disk drives for servers and under manufacturers" },
            new PartDescription()
            {
                EsteemCode = "KBD",
                Description = "Keyboards clone",
                FullDescription =   "Keyboards,clone whole and part" },
            new PartDescription()
            {
                EsteemCode = "KBD",
                Description = "Keyboards specialist",
                FullDescription =   "Keyboards, specialist such as pos cherry alphameric" },
            new PartDescription()
            {
                EsteemCode = "MOU",
                Description = "Mouse",
                FullDescription =   "Mice, wholte and part" },
            new PartDescription()
            {
                EsteemCode = "LCD",
                Description = "LCD Screens",
                FullDescription =   "LCD Panels EG Notebook,POS" },
            new PartDescription()
            {
                EsteemCode = "MEM",
                Description = "Memory",
                FullDescription =   "Memory EG PC, Notebook, Server, POS" },
            new PartDescription()
            {
                EsteemCode = "MLB",
                Description = "Main logic boards",
                FullDescription =   "Main logic boards EG PC, Notebook, Server, POS" },
            new PartDescription()
            {
                EsteemCode = "MED",
                Description = "Media ",
                FullDescription =   "Media tapes, cd's" },
            new PartDescription()
            {
                EsteemCode = "NET",
                Description = "Network items",
                FullDescription =   "Network items EG Switches, KVM,Modems, print servers" },
            new PartDescription()
            {
                EsteemCode = "PSU",
                Description = "Power supply related",
                FullDescription =   "Power Supply EG PC, Notebook, Server, POS" },
            new PartDescription()
            {
                EsteemCode = "SCA",
                Description = "Scanner related",
                FullDescription =   "Scanners, whole and part" },
            new PartDescription()
            {
                EsteemCode = "TOU",
                Description = "Touch screen related",
                FullDescription =   "Touchscreens, Whole and part" },
            new PartDescription()
            {
                EsteemCode = "CDC",
                Description = "Cashdrawer complete",
                FullDescription =   "Complete cashdrawers" },
            new PartDescription()
            {
                EsteemCode = "CDP",
                Description = "Cashdrawer parts",
                FullDescription =   "Cashdrawer parts that don't fit into a primary code" },
            new PartDescription()
            {
                EsteemCode = "CHC",
                Description = "Chip and pin complete",
                FullDescription =   "Complete chip and pin devices" },
            new PartDescription()
            {
                EsteemCode = "CHP",
                Description = "Chip and Pin Parts",
                FullDescription =   "Chip and pin parts that don't fit into a primary code" },
            new PartDescription()
            {
                EsteemCode = "BAC",
                Description = "Back up complete",
                FullDescription =   "Complete back up devices EG RDX, DDS, LTO" },
            new PartDescription()
            {
                EsteemCode = "BAP",
                Description =  "Back up parts",
                FullDescription =   "Back up device parts that don't fit into a primary code" },
            new PartDescription()
            {
                EsteemCode = "HHT",
                Description = "Hand held terminals",
                FullDescription =   "Hand held terminals with screens and inputs(like couriers use)" },
            new PartDescription()
            {
                EsteemCode = "IMG",
                Description = "Imaging Devices",
                FullDescription =   "Devices to scan images such as flat bed scanners biometrics" },
            new PartDescription()
            {
                EsteemCode = "IHC",
                Description = "Inhouse Cleaning",
                FullDescription =  "Air dusters, cloths and cleaners" },
            new PartDescription()
            {
                EsteemCode = "IHT",
                Description = "Inhouse Tools",
                FullDescription =   "Repair centre tools engineer tools" },
            new PartDescription()
            {
                EsteemCode = "OPT",
                Description = "Optical drives",
                FullDescription =   "Complete optical devices EG CD, DVD, REV, Floppy" },
            new PartDescription()
            {
                EsteemCode = "KIO",
                Description = "Kiosk related",
                FullDescription =   "Kiosk parts that don't fit into a primary code" },
            new PartDescription()
            {
                EsteemCode = "PCC",
                Description = "PC complete",
                FullDescription =   "Complete Pc's of any form factor" },
            new PartDescription()
            {
                EsteemCode = "PCP",
                Description = "PC parts",
                FullDescription =   "Pc parts that don't fit into a primary code" },
            new PartDescription()
            {
                EsteemCode = "PRC",
                Description = "Printers Complete",
                FullDescription =   "Complete printers of all types EG Laser, DOT, Thermal" },
            new PartDescription()
            {
                EsteemCode = "PRP",
                Description = "Printer parts",
                FullDescription =   "Printer parts that don't all ready fit into a primary code" },
            new PartDescription()
            {
                EsteemCode = "MOC",
                Description =
                "Monitor complete",
                FullDescription =   "Complete monitors" },
            new PartDescription()
            {
                EsteemCode = "MOP",
                Description =
                "Monitor parts",
                FullDescription =   "Monitor parts EG Invertors, Stands, Buttons, Bezels" },
            new PartDescription()
            {
                EsteemCode = "NOC",
                Description =
                "Notebook complete",
                FullDescription =   "Complete notebooks of all types EG Netbook, Ultra" },
            new PartDescription()
            {
                EsteemCode = "NOP",
                Description = "Notebook parts",
                FullDescription =   "Notebook parts that don't fit into a primary code, docking stations" },
            new PartDescription()
            {
                EsteemCode = "OTH",
                Description = "Other parts",
                FullDescription =   "Parts not catergorised and new code creation is not warranted." },
            new PartDescription()
            {
                EsteemCode = "POC",
                Description =  "Pos complete",
                FullDescription =   "Complete POS units" },
            new PartDescription()
            {
                EsteemCode = "POP",
                Description = "Pos parts",
                FullDescription =   "POS unit parts that don't fit into a primary code" },
            new PartDescription()
            {
                EsteemCode = "SOF",
                Description = "Software",
                FullDescription =   "Software of all types EF FPP, Licence" },
            new PartDescription()
            {
                EsteemCode = "SVC",
                Description = "Server complete",
                FullDescription =   "Complete servers of any form factor and size" },
            new PartDescription()
            {
                EsteemCode = "SVP ",
                Description = "Server parts",
                FullDescription =   "Server parts that don't fit into a primary code" },
            new PartDescription()
            {
                EsteemCode = "TAC",
                Description = "Tablet Complete",
                FullDescription =   "Ipads etc" },
            new PartDescription()
            {
                EsteemCode = "TAP",
                Description = "Tablet Parts",
                FullDescription =   "Digitizers, screen protectors." },
            new PartDescription()
            {
                EsteemCode = "UPS ",
                Description = "UPS related",
                FullDescription =   "UPS, Whole and part" }
            };

            return returnList;
        }
    }
}