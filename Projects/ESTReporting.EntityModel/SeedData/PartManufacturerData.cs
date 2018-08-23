using ESTReporting.EntityModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace ESTReporting.EntityModel.SeedData
{
    public static class PartManufacturerData
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
                new PartManufacturer() { Name = "Acer", Code = "ACE", CodeEsteem = "ACE" },
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
                new PartManufacturer() { Name = "Microsoft", Code = "MIS", CodeEsteemAlt = "MS" },
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
                new PartManufacturer() { Name = "Racal", Code = "RAC", CodeEsteemAlt = "RAC" },
                new PartManufacturer() { Name = "Ramesys", Code = "RAM", CodeEsteemAlt = "RAM" },
                new PartManufacturer() { Name = "Realpos", Code = "REP", CodeEsteemAlt = "REP" },
                new PartManufacturer() { Name = "Redsky", Code = "RED", CodeEsteemAlt = "RED" },
                new PartManufacturer() { Name = "Reed", Code = "REE", CodeEsteemAlt = "REE" },
                new PartManufacturer() { Name = "Retail", Code = "RET", CodeEsteemAlt = "RET" },
                new PartManufacturer() { Name = "Sarian", Code = "SAR", CodeEsteemAlt = "SAR" },
                new PartManufacturer() { Name = "SB", Code = "SBX", CodeEsteemAlt = "SB" },
                new PartManufacturer() { Name = "Honeywell", Code = "HON", CodeEsteemAlt = "HON" },
                new PartManufacturer() { Name = "Star", Code = "STA", CodeEsteemAlt = "STA" },
                new PartManufacturer() { Name = "Scanman", Code = "SCM", CodeEsteemAlt = "SCM" },
                new PartManufacturer() { Name = "Scanpartner", Code = "SCP", CodeEsteemAlt = "SCP" },
                new PartManufacturer() { Name = "Scanplus", Code = "SCS", CodeEsteemAlt = "SCS" },
                new PartManufacturer() { Name = "Seagate", Code = "SEA", CodeEsteemAlt = "SEA" },
                new PartManufacturer() { Name = "Sherwood", Code = "SHE", CodeEsteemAlt = "SHE" },
                new PartManufacturer() { Name = "Shiva", Code = "SHI", CodeEsteemAlt = "SHI" },
                new PartManufacturer() { Name = "Siemens", Code = "SIE", CodeEsteemAlt = "SIE" },
                new PartManufacturer() { Name = "Silex", Code = "SIL", CodeEsteemAlt = "SIL" },
                new PartManufacturer() { Name = "Siracom", Code = "SIR", CodeEsteemAlt = "SIR" },
                new PartManufacturer() { Name = "Veritas", Code = "VER", CodeEsteemAlt = "VER" },
                new PartManufacturer() { Name = "APC", Code = "APC", CodeEsteemAlt = "APC" },
                new PartManufacturer() { Name = "Apricot", Code = "APR", CodeEsteemAlt = "APR" },
                new PartManufacturer() { Name = "Arnel", Code = "ARN", CodeEsteemAlt = "ARN" },
                new PartManufacturer() { Name = "Arnet", Code = "ART", CodeEsteemAlt = "ART" },
                new PartManufacturer() { Name = "Asrock", Code = "ASR", CodeEsteemAlt = "ASR" },
                new PartManufacturer() { Name = "Clone", Code = "CLO", CodeEsteemAlt = "CLO" },
            };

            return returnList;
        }
    }
}
