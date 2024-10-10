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
        public double InvPremium { get; set; }
        public InventoryType InventoryType { get; set; }

        public int RealEstateInsuranceId { get; set; }
        public RealEstateInsurance RealEstateInsurance { get; set; }


        public Inventory(InventoryType type, double invValue)
        {
            InventoryType = type;
            InvValue = invValue;
            CalculatePremium();
        }

        public Inventory() { }

        private void CalculatePremium()
        {
            InvPremium = InvValue * 0.002;
        }

    }
}
