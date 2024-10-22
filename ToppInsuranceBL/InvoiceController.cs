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
        public string CalculateCreateInvoiceDocuments(DateTime invoiceDate)
        {
            return invoiceRepository.CalculateCreateInvoiceDocuments(invoiceDate);
        }
        #endregion

        #region Load Commissions from JSON Method
        public List<dynamic> LoadInvoicesFromJson()
        {
            return invoiceRepository.LoadInvoicesFromJson();
        }
        #endregion
    }
}
