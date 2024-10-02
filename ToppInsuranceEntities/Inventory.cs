using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class Inventory
    {
        public int InventoryId { get; init; }
        public double InvValue { get; set; }
        public int InvPremium { get; set; }

        public int? RealEstateInsuranceId { get; set; }
        public RealEstateInsurance RealEstateInsurance { get; set; }
        public Inventory(double invValue, int invPremium)
        {
            InvValue = invValue;
            InvPremium = invPremium;
        }
    }
}
