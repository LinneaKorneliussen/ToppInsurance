using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    internal class SalesController
    {
        private SalesRepository salesRepository;

        public SalesController()
        {
            salesRepository = new SalesRepository();
        }
        #region Get all sales
        public List<Sales> GetSales()
        {
            return salesRepository.GetSales().ToList();
        }
        #endregion
        #region Search Sales by a specific sales agent
        public List<Sales> GetSalesBySalesAgent(string agencyNumber)
        {
            return salesRepository.GetSalesBySalesAgent(agencyNumber);
        }
        #endregion

        #region Add a new sales Method
        public void AddSale(int insuranceId, string agencyNumber, int amount, double commissionRate)
        {
            salesRepository.AddSale(insuranceId, agencyNumber, amount, commissionRate);
        }
        #endregion


        #region  Calculate the total commission by salesagent
        public double CalculateCommissionBySalesAgent(string agencyNumber)
        {
            List<Sales> sales = salesRepository.GetSales();
            double totalCommission = 0;

            foreach (var sale in sales)
                if (sale.AgencyNumber.Equals(agencyNumber, StringComparison.OrdinalIgnoreCase))
                {
                    totalCommission += sale.GetCommission();
                }
            return totalCommission;
        }
        #endregion
    }
}



