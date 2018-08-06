using System.Collections.Generic;
using System.Linq;

namespace EntityModel.BusinessLogic
{
    public class ManufacturerComparer : IComparer<string>
    {
        public int Compare(string s1, string s2)
        {
            if (s1 == s2) return 0;
            if (TestData.GetManufacturers().Any(x => x.Code == s1) || TestData.GetManufacturers().Any(x => x.Name == s1) 
                || TestData.GetManufacturers().Any(x => x.CodeEsteem == s1) || TestData.GetManufacturers().Any(x => x.CodeEsteemAlt == s1))
                return -1;
            else if (TestData.GetManufacturers().Any(x => x.Code == s2) || TestData.GetManufacturers().Any(x => x.Name == s2)
                || TestData.GetManufacturers().Any(x => x.CodeEsteem == s2) || TestData.GetManufacturers().Any(x => x.CodeEsteemAlt == s2))
                return 1;
            return 0;
        }
    }
}
