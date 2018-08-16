using System.Collections.Generic;
using System.Linq;

namespace EntityModel.BusinessLogic
{
    public class ManufacturerComparer : IComparer<string>
    {
        public int Compare(string s1, string s2)
        {
            if (s1 == s2) return 0;
            if (ManufacturerModel_Data.GetManufacturers().Any(x => x.Code == s1) || ManufacturerModel_Data.GetManufacturers().Any(x => x.Name == s1) 
                || ManufacturerModel_Data.GetManufacturers().Any(x => x.CodeEsteem == s1) || ManufacturerModel_Data.GetManufacturers().Any(x => x.CodeEsteemAlt == s1))
                return -1;
            else if (ManufacturerModel_Data.GetManufacturers().Any(x => x.Code == s2) || ManufacturerModel_Data.GetManufacturers().Any(x => x.Name == s2)
                || ManufacturerModel_Data.GetManufacturers().Any(x => x.CodeEsteem == s2) || ManufacturerModel_Data.GetManufacturers().Any(x => x.CodeEsteemAlt == s2))
                return 1;
            return 0;
        }
    }
}
