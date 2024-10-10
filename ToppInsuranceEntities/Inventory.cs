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

        private double _invValue;
        public double InvValue
        {
            get { return _invValue; }
            set
            {
                _invValue = value;
                InvPremium = CalculatePremium();  
            }
        }
        public double InvPremium { get; set; }
        public InventoryType InventoryType { get; set; }

        public int RealEstateInsuranceId { get; set; }
        public RealEstateInsurance RealEstateInsurance { get; set; }


        public Inventory(InventoryType type, double invValue)
        {
            InventoryType = type;
            InvValue = invValue;
            InvPremium = CalculatePremium();
        }

        public Inventory() {}

        private double CalculatePremium()
        {
            return InvValue * 0.002;
        }

    }
}
