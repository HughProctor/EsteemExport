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
                new PartManufacturer() { Name = "Motorola", CodeEsteem = "MOT", CodeEsteemAlt = "MOTO" },
                new PartManufacturer() { Name = "Cisco", CodeEsteem = "CIS", CodeEsteemAlt = "CISC" },
                new PartManufacturer() { Name = "Microsoft", CodeEsteem = "MIS", CodeEsteemAlt = "MS" },
                new PartManufacturer() { Name = "Topaz", CodeEsteem = "TOP", CodeEsteemAlt = "TOPZ" },
                new PartManufacturer() { Name = "Tecdesk", CodeEsteem = "TEC", CodeEsteemAlt = "TECD" },
                new PartManufacturer() { Name = "Brother", CodeEsteem = "BRO", CodeEsteemAlt = "BRO" },
                new PartManufacturer() { Name = "Canon", CodeEsteem = "CAN", CodeEsteemAlt = "CAN" },
                new PartManufacturer() { Name = "3COM", CodeEsteem = "3CO", CodeEsteemAlt = "3COM" },
                new PartManufacturer() { Name = "Tenda", CodeEsteem = "TEN", CodeEsteemAlt = "TEN" },
                new PartManufacturer() { Name = "Fujitsu", CodeEsteem = "FUJ", CodeEsteemAlt = "FUJI" },
                new PartManufacturer() { Name = "Intel", CodeEsteem = "INT", CodeEsteemAlt = "INTL" },
                new PartManufacturer() { Name = "Hitachi", CodeEsteem = "HIT", CodeEsteemAlt = "HIT" },
                new PartManufacturer() { Name = "Verifone", CodeEsteem = "VER", CodeEsteemAlt = "VER" },
                new PartManufacturer() { Name = "Ricoh", CodeEsteem = "RIC", CodeEsteemAlt = "RIC" },
                new PartManufacturer() { Name = "Toshiba", CodeEsteem = "TOS", CodeEsteemAlt = "TOSH" },
                new PartManufacturer() { Name = "TALLY", CodeEsteem = "TAL", CodeEsteemAlt = "TAL" },
                new PartManufacturer() { Name = "iiyama", CodeEsteem = "IIY", CodeEsteemAlt = "IIY" },
                new PartManufacturer() { Name = "Nokia", CodeEsteem = "NOK", CodeEsteemAlt = "NOK" },
                new PartManufacturer() { Name = "Compaq", CodeEsteem = "COM", CodeEsteemAlt = "COM" },
                new PartManufacturer() { Name = "Netgear", CodeEsteem = "NET", CodeEsteemAlt = "NET" },
                new PartManufacturer() { Name = "Belkin", CodeEsteem = "BEL", CodeEsteemAlt = "BELK" },
                new PartManufacturer() { Name = "Iomega", CodeEsteem = "IOM", CodeEsteemAlt = "IOM" },
                new PartManufacturer() { Name = "Panasonic", CodeEsteem = "PAN", CodeEsteemAlt = "PAN" },
                new PartManufacturer() { Name = "Phillips", CodeEsteem = "PHI", CodeEsteemAlt = "PHI" },
                new PartManufacturer() { Name = "DLink", CodeEsteem = "DLI", CodeEsteemAlt = "D-LINK" },
                new PartManufacturer() { Name = "Xerox", CodeEsteem = "XER", CodeEsteemAlt = "XER" },
                new PartManufacturer() { Name = "SONY", CodeEsteem = "SON", CodeEsteemAlt = "SON" },
                new PartManufacturer() { Name = "Bybox", CodeEsteem = "BYB", CodeEsteemAlt = "BYB" },
                new PartManufacturer() { Name = "Buffalo", CodeEsteem = "BUF", CodeEsteemAlt = "BUF" },
                new PartManufacturer() { Name = "Boca", CodeEsteem = "BOC", CodeEsteemAlt = "BOC" },
                new PartManufacturer() { Name = "Axis", CodeEsteem = "AXI", CodeEsteemAlt = "AXI" },
                new PartManufacturer() { Name = "Aures", CodeEsteem = "AUR", CodeEsteemAlt = "AUR" },
                new PartManufacturer() { Name = "ASUS", CodeEsteem = "ASU", CodeEsteemAlt = "ASU" },
                new PartManufacturer() { Name = "Mitel", CodeEsteem = "MIT", CodeEsteemAlt = "MIT" },
                new PartManufacturer() { Name = "Novell", CodeEsteem = "NOV", CodeEsteemAlt = "NOV" },
                new PartManufacturer() { Name = "Prologic", CodeEsteem = "PRO", CodeEsteemAlt = "PRO" },
                new PartManufacturer() { Name = "Quantum", CodeEsteem = "QUA", CodeEsteemAlt = "QUA" },
                new PartManufacturer() { Name = "QUME", CodeEsteem = "QUM", CodeEsteemAlt = "QUM" },
                new PartManufacturer() { Name = "Racal", CodeEsteem = "RAC", CodeEsteemAlt = "RAC" },
                new PartManufacturer() { Name = "Ramesys", CodeEsteem = "RAM", CodeEsteemAlt = "RAM" },
                new PartManufacturer() { Name = "Realpos", CodeEsteem = "REP", CodeEsteemAlt = "REP" },
                new PartManufacturer() { Name = "Redsky", CodeEsteem = "RED", CodeEsteemAlt = "RED" },
                new PartManufacturer() { Name = "Reed", CodeEsteem = "REE", CodeEsteemAlt = "REE" },
                new PartManufacturer() { Name = "Retail", CodeEsteem = "RET", CodeEsteemAlt = "RET" },
                new PartManufacturer() { Name = "Sarian", CodeEsteem = "SAR", CodeEsteemAlt = "SAR" },
                new PartManufacturer() { Name = "SB", CodeEsteem = "SBX", CodeEsteemAlt = "SB" },
                new PartManufacturer() { Name = "Honeywell", CodeEsteem = "HON", CodeEsteemAlt = "HON" },
                new PartManufacturer() { Name = "Star", CodeEsteem = "STA", CodeEsteemAlt = "STA" },
                new PartManufacturer() { Name = "Scanman", CodeEsteem = "SCM", CodeEsteemAlt = "SCM" },
                new PartManufacturer() { Name = "Scanpartner", CodeEsteem = "SCP", CodeEsteemAlt = "SCP" },
                new PartManufacturer() { Name = "Scanplus", CodeEsteem = "SCS", CodeEsteemAlt = "SCS" },
                new PartManufacturer() { Name = "Seagate", CodeEsteem = "SEA", CodeEsteemAlt = "SEA" },
                new PartManufacturer() { Name = "Sherwood", CodeEsteem = "SHE", CodeEsteemAlt = "SHE" },
                new PartManufacturer() { Name = "Shiva", CodeEsteem = "SHI", CodeEsteemAlt = "SHI" },
                new PartManufacturer() { Name = "Siemens", CodeEsteem = "SIE", CodeEsteemAlt = "SIE" },
                new PartManufacturer() { Name = "Silex", CodeEsteem = "SIL", CodeEsteemAlt = "SIL" },
                new PartManufacturer() { Name = "Siracom", CodeEsteem = "SIR", CodeEsteemAlt = "SIR" },
                new PartManufacturer() { Name = "Veritas", CodeEsteem = "VER", CodeEsteemAlt = "VER" },
                new PartManufacturer() { Name = "APC", CodeEsteem = "APC", CodeEsteemAlt = "APC" },
                new PartManufacturer() { Name = "Apricot", CodeEsteem = "APR", CodeEsteemAlt = "APR" },
                new PartManufacturer() { Name = "Arnel", CodeEsteem = "ARN", CodeEsteemAlt = "ARN" },
                new PartManufacturer() { Name = "Arnet", CodeEsteem = "ART", CodeEsteemAlt = "ART" },
                new PartManufacturer() { Name = "Asrock", CodeEsteem = "ASR", CodeEsteemAlt = "ASR" },
                new PartManufacturer() { Name = "Clone", CodeEsteem = "CLO", CodeEsteemAlt = "CLO" },
            };

            return returnList;
        }
    }
}
