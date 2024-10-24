using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    public class InvoiceController
    {
        private InvoiceRepository invoiceRepository;

        public InvoiceController() 
        {
            invoiceRepository = new InvoiceRepository();
        }

        #region Calculate and create Invoice documents Method
        public string CalculateCreatePrivateInvoiceDocuments(PrivateCustomer customer, DateTime invoiceDate)
        {
            return invoiceRepository.CalculateCreatePrivateInvoiceDocuments(customer, invoiceDate);
        }

        //public string CalculateCreateBusinessInvoiceDocuments(BusinessCustomer businessCustomer, DateTime invoiceDate)
        //{
        //    return invoiceRepository.CalculateCreateBusinessInvoiceDocuments(businessCustomer, invoiceDate);
        //}
        #endregion

        #region Load Commissions from JSON Method
        public List<dynamic> LoadInvoicesFromJson()
        {
            return invoiceRepository.LoadInvoicesFromJson();
        }
        #endregion
    }
}
