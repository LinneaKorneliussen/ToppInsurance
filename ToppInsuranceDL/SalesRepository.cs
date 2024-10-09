using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
    public class SalesRepository
    {
        private UnitOfWork unitOfWork;
        private List<Sales> addSale;
        private List<Sales> getSales;
        private List<Sales> getSalesBySalesAgent;




        public SalesRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
            addSale = new List<Sales> { };
            getSales = new List<Sales> { };
            getSalesBySalesAgent = new List<Sales> { };
        }
        #region Register a new sale
        public void AddSale(int insuranceId, string agencyNumber, int amount, double commission)
        {
            Sales sales = new Sales(insuranceId, agencyNumber, amount, commission);
            unitOfWork.SalesRepository.Add(sales);
            unitOfWork.Save();
        }
        #endregion
        public List<Sales> GetSales()
        {
            return unitOfWork.SalesRepository.GetAll().ToList();
        }
        //get all sales for a specific sales agent
        public List<Sales> GetSalesBySalesAgent(string agencyNumber)
        {

            return unitOfWork.SalesRepository.GetAll().ToList();
        }


    }
}


