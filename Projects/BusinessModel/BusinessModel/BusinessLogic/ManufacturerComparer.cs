using ESTReporting.EntityModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessModel.BusinessLogic
{
    public class ManufacturerComparer : IComparer<string>
    {
        public List<PartManufacturer> Manufacturers { get; set; }
        public int Compare(string s1, string s2)
        {
            if (s1 == s2) return 0;
            //if (Manufacturers == null || !Manufacturers.Any()) return 0;
            if (Manufacturers.Any(x => x.Code == s1) || Manufacturers.Any(x => x.Name == s1) 
                || Manufacturers.Any(x => x.CodeEsteem == s1) || Manufacturers.Any(x => x.CodeEsteemAlt == s1))
                return -1;
            else if (Manufacturers.Any(x => x.Code == s2) || Manufacturers.Any(x => x.Name == s2)
                || Manufacturers.Any(x => x.CodeEsteem == s2) || Manufacturers.Any(x => x.CodeEsteemAlt == s2))
                return 1;
            return 0;
        }
    }
}
