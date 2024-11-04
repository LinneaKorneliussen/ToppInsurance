using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    /// <summary>
    /// InvoiceController class provides methods for calculating and creating invoice documents for private and 
    /// business customers and loading invoices from JSON. This class acts as an intermediary 
    /// between the presentation layer and data access layer for handling invoices.
    /// </summary>
    public class InvoiceController
    {
       
        private InvoiceRepository invoiceRepository;

        public InvoiceController() 
        {
            invoiceRepository = new InvoiceRepository();
        }

        #region Calculate and create Invoice documents Method
        public string CalculateCreatePrivateInvoiceDocuments(PrivateCustomer privateCustomer, DateTime invoiceDate)
        {
            return invoiceRepository.CalculateCreatePrivateInvoiceDocuments(privateCustomer, invoiceDate);
        }

        public string CalculateCreateBusinessInvoiceDocuments(BusinessCustomer businessCustomer, DateTime invoiceDate)
        {
            return invoiceRepository.CalculateCreateBusinessInvoiceDocuments(businessCustomer, invoiceDate);
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
