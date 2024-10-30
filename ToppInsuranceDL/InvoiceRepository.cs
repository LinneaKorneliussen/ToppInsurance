using Newtonsoft.Json;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
    public class InvoiceRepository
    {
        private UnitOfWork unitOfWork;

        public InvoiceRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Calculate and create private Invoice documents Method
        public string CalculateCreatePrivateInvoiceDocuments(PrivateCustomer customer, DateTime invoiceDate)
        {
            if (PInvoiceExists(customer, invoiceDate))
            {
                return $"Fakturaunderlag för {customer.FirstName} {customer.LastName} på datumet {invoiceDate.ToShortDateString()} existerar redan.";
            }

            double totalAmount = 0;

            var allActiveLifeInsurances = unitOfWork.LifeInsuranceRepository.GetAll()
                .Where(i => i.PrivateCustomer == customer && i.StartDate <= invoiceDate && i.Status == Status.Aktiv)
                .ToList();

            foreach (var insurance in allActiveLifeInsurances)
            {
                if ((insurance.Paymentform == Paymentform.Månad) ||
                    (insurance.Paymentform != Paymentform.Månad && invoiceDate >= insurance.StartDate && invoiceDate <= insurance.StartDate.AddDays(20)))
                {
                    totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                }
            }

            var allActiveSicknessAccidentInsurances = unitOfWork.SicknessAccidentInsuranceRepository.GetAll()
                .Where(i => i.PrivateCustomer == customer && i.StartDate <= invoiceDate && i.Status == Status.Aktiv)
                .ToList();

            foreach (var insurance in allActiveSicknessAccidentInsurances)
            {
                if ((insurance.Paymentform == Paymentform.Månad) ||
                    (insurance.Paymentform != Paymentform.Månad && invoiceDate >= insurance.StartDate && invoiceDate <= insurance.StartDate.AddDays(20)))
                {
                    totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                }
            }

            if (totalAmount <= 0)
            {
                return "Inga fakturor att skapa för denna privatkund.";
            }

            var privateInvoice = CreatePrivateInvoice(customer, totalAmount, invoiceDate);
            SaveInvoicesToJson(new List<Invoice> { privateInvoice });
            unitOfWork.Save();

            return $"Fakturaunderlag skapat för {invoiceDate.ToShortDateString()} med totalt belopp: {privateInvoice.InvoiceTotalAmount} SEK.";
        }

        private PrivateInvoice CreatePrivateInvoice(PrivateCustomer customer, double totalAmount, DateTime invoiceDate)
        {
            return new PrivateInvoice
            {
                PrivateCustomer = customer,
                InvoiceTotalAmount = totalAmount,
                InvoiceDate = invoiceDate,
            };
        }
        #endregion

        #region Calculate and create business Invoice documents Method
        public string CalculateCreateBusinessInvoiceDocuments(BusinessCustomer customer, DateTime invoiceDate)
        {
            if (BInvoiceExists(customer, invoiceDate))
            {
                return $"Fakturan för företaget {customer.CompanyName} på datumet {invoiceDate.ToShortDateString()} existerar redan.";
            }

            double totalAmount = 0;

            var allActiveVehicleInsurances = unitOfWork.VehicleInsuranceRepository.GetAll()
                .Where(i => i.BusinessCustomer == customer && i.StartDate <= invoiceDate && i.Status == Status.Aktiv)
                .ToList();

            foreach (var insurance in allActiveVehicleInsurances)
            {
                if ((insurance.Paymentform == Paymentform.Månad) ||
                    (insurance.Paymentform != Paymentform.Månad && invoiceDate >= insurance.StartDate && invoiceDate <= insurance.StartDate.AddDays(20)))
                {
                    totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                }
            }

            var allActiveLiabilityInsurances = unitOfWork.LiabilityInsuranceRepository.GetAll()
                .Where(i => i.BusinessCustomer == customer && i.StartDate <= invoiceDate && i.Status == Status.Aktiv)
                .ToList();

            foreach (var insurance in allActiveLiabilityInsurances)
            {
                if ((insurance.Paymentform == Paymentform.Månad) ||
                    (insurance.Paymentform != Paymentform.Månad && invoiceDate >= insurance.StartDate && invoiceDate <= insurance.StartDate.AddDays(20)))
                {
                    totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                }
            }

            var allActiveRealEstateInsurances = unitOfWork.RealEstateInsuranceRepository.GetAll()
                .Where(i => i.BusinessCustomer == customer && i.StartDate <= invoiceDate && i.Status == Status.Aktiv)
                .ToList();

            foreach (var realEstateInsurance in allActiveRealEstateInsurances)
            {
                var totalInventoryPremium = unitOfWork.InventoryRepository.GetAll()
                    .Where(inv => inv.RealEstateInsuranceId == realEstateInsurance.InsuranceId)
                    .Sum(inv => inv.InvPremium);

                var combinedPremium = realEstateInsurance.Premium + totalInventoryPremium;
                totalAmount += CalculateInvoiceAmount(combinedPremium, realEstateInsurance.Paymentform);
            }

            if (totalAmount <= 0)
            {
                return "Inga fakturor att skapa för denna företagskund.";
            }

            var businessInvoice = CreateBusinessInvoice(customer, totalAmount, invoiceDate);
            SaveInvoicesToJson(new List<Invoice> { businessInvoice });
            unitOfWork.Save();

            return $"Fakturaunderlag skapat för {invoiceDate.ToShortDateString()} med totalt belopp: {businessInvoice.InvoiceTotalAmount} SEK.";
        }
        private BusinessInvoice CreateBusinessInvoice(BusinessCustomer customer, double totalAmount, DateTime invoiceDate)
        {
            return new BusinessInvoice
            {
                BusinessCustomer = customer,
                InvoiceTotalAmount = totalAmount, 
                InvoiceDate = invoiceDate 
            };
        }
        #endregion

        #region Calculate Invoices Method
        private double CalculateInvoiceAmount(double premium, Paymentform paymentForm)
        {
            switch (paymentForm)
            {
                case Paymentform.Månad:
                    return premium;
                case Paymentform.Kvartal:
                    return premium * 3;
                case Paymentform.Halvår:
                    return premium * 6;
                case Paymentform.År:
                    return premium * 12;
                default:
                    throw new ArgumentOutOfRangeException(nameof(paymentForm), "Ogiltig betalningsform.");
            }
        }
        #endregion

        #region Check existing invoices Method
        private bool PInvoiceExists(PrivateCustomer customer, DateTime invoiceDate)
        {
            var existingInvoices = LoadInvoicesFromJson();

            return existingInvoices.Any(invoice =>
                invoice.CustomerType == "Privatförsäkring" &&
                invoice.Name == $"{customer.FirstName} {customer.LastName}" &&
                invoice.Number == customer.SSN &&
                invoice.InvoiceDate.ToString("yyyy-MM-dd") == invoiceDate.ToString("yyyy-MM-dd")
            );
        }

        private bool BInvoiceExists(BusinessCustomer customer, DateTime invoiceDate)
        {
            var existingInvoices = LoadInvoicesFromJson();

            string organizationalNumberString = customer.Organizationalnumber.ToString();

            return existingInvoices.Any(invoice =>
                invoice.CustomerType == "Företagsförsäkring" &&
                invoice.Name == customer.CompanyName &&
                invoice.Number == organizationalNumberString && 
                invoice.InvoiceDate.ToString("yyyy-MM-dd") == invoiceDate.ToString("yyyy-MM-dd")
            );
        }
        #endregion

        #region Save Invoices to JSON
        public void SaveInvoicesToJson(List<Invoice> invoices)
        {
            string filePath = "InvoicesReport.json";
            List<dynamic> invoicesList = new List<dynamic>();

            if (File.Exists(filePath))
            {
                string existingJson = File.ReadAllText(filePath);

                if (!string.IsNullOrEmpty(existingJson))
                {
                    invoicesList = JsonConvert.DeserializeObject<List<dynamic>>(existingJson) ?? new List<dynamic>();
                }
            }

            foreach (var invoice in invoices)
            {
                dynamic invoiceData;

                if (invoice is PrivateInvoice privateInvoice)
                {
                    invoiceData = new
                    {
                        CustomerType = "Privatförsäkring",
                        Name = $"{privateInvoice.PrivateCustomer.FirstName} {privateInvoice.PrivateCustomer.LastName}",
                        Number = privateInvoice.PrivateCustomer.SSN,
                        TotalAmount = privateInvoice.InvoiceTotalAmount,
                        InvoiceDate = privateInvoice.InvoiceDate
                    };
                }
                else if (invoice is BusinessInvoice businessInvoice)
                {
                    invoiceData = new
                    {
                        CustomerType = "Företagsförsäkring",
                        Name = businessInvoice.BusinessCustomer.CompanyName,
                        Number = businessInvoice.BusinessCustomer.Organizationalnumber,
                        TotalAmount = businessInvoice.InvoiceTotalAmount,
                        InvoiceDate = businessInvoice.InvoiceDate
                    };
                }
                else
                {
                    continue;
                }

                invoicesList.Add(invoiceData);
            }

            string json = JsonConvert.SerializeObject(invoicesList, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        #endregion

        #region Load Commission From Json Method
        public List<dynamic> LoadInvoicesFromJson()
        {
            string filePath = "InvoicesReport.json";

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(new List<dynamic>(), Formatting.Indented));
            }

            string json = File.ReadAllText(filePath);

            try
            {
                var invoiceDataList = JsonConvert.DeserializeObject<List<dynamic>>(json);
                return invoiceDataList ?? new List<dynamic>();
            }
            catch (JsonException jsonEx)
            {
                throw new InvalidOperationException("Fel vid deserialisering av JSON-innehållet.", jsonEx);
            }
        }
        #endregion
    }
}
