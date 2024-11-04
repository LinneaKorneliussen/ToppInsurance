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
        public string CalculateCreatePrivateInvoiceDocuments(PrivateCustomer privateCustomer, DateTime invoiceDate)
        {
            DateTime monthStart = new DateTime(invoiceDate.Year, invoiceDate.Month, 1);
            DateTime monthEnd = new DateTime(invoiceDate.Year, invoiceDate.Month, DateTime.DaysInMonth(invoiceDate.Year, invoiceDate.Month));

            if (PInvoiceExists(privateCustomer, invoiceDate))
            {
                return $"Fakturaunderlag för {privateCustomer.FirstName} {privateCustomer.LastName} på datumet {invoiceDate.ToShortDateString()} existerar redan.";
            }

            double totalAmount = 0;

            var allActiveLifeInsurances = unitOfWork.LifeInsuranceRepository.GetAll()
                .Where(i => i.PrivateCustomer == privateCustomer && i.Status == Status.Aktiv)
                .ToList();
            foreach (var insurance in allActiveLifeInsurances)
            {
                if (insurance.StartDate <= monthEnd && insurance.EndDate >= monthStart)
                {
                    if (insurance.Paymentform == Paymentform.Månad && insurance.StartDate >= monthStart && insurance.StartDate <= monthEnd)
                    {
                        totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform) * 2;
                    }
                    else if (insurance.Paymentform == Paymentform.Månad && insurance.StartDate < monthStart)
                    {
                        totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                    }
                    else if (insurance.Paymentform == Paymentform.Kvartal && insurance.StartDate < monthStart)
                    {
                        if ((monthStart.Month - insurance.StartDate.Month) % 3 == 0)
                        {
                            totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                        }
                    }
                    else if (insurance.Paymentform == Paymentform.Halvår && insurance.StartDate < monthStart)
                    {
                        if ((monthStart.Month - insurance.StartDate.Month) % 6 == 0)
                        {
                            totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                        }
                    }
                    else if (insurance.Paymentform != Paymentform.Månad && insurance.StartDate >= monthStart && insurance.StartDate <= monthEnd)
                    {
                        totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                    }
                }
            
        }

            var allActiveSicknessAccidentInsurances = unitOfWork.SicknessAccidentInsuranceRepository.GetAll()
                .Where(i => i.PrivateCustomer == privateCustomer && i.Status == Status.Aktiv)
                .ToList();

            foreach (var insurance in allActiveSicknessAccidentInsurances)
            {
                if (insurance.StartDate <= monthEnd && insurance.EndDate >= monthStart)
                {
                    if (insurance.Paymentform == Paymentform.Månad && insurance.StartDate >= monthStart && insurance.StartDate <= monthEnd)
                    {
                        totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform) * 2;
                    }
                    else if (insurance.Paymentform == Paymentform.Månad && insurance.StartDate < monthStart)
                    {
                        totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                    }
                    else if (insurance.Paymentform == Paymentform.Kvartal && insurance.StartDate < monthStart)
                    {
                        if ((monthStart.Month - insurance.StartDate.Month) % 3 == 0)
                        {
                            totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                        }
                    }
                    else if (insurance.Paymentform == Paymentform.Halvår && insurance.StartDate < monthStart)
                    {
                        if ((monthStart.Month - insurance.StartDate.Month) % 6 == 0)
                        {
                            totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                        }
                    }
                    else if (insurance.Paymentform != Paymentform.Månad && insurance.StartDate >= monthStart && insurance.StartDate <= monthEnd)
                    {
                        totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                    }
                }
            }

            if (totalAmount <= 0)
            {
                return "Inga fakturor att skapa för denna privatkund.";
            }

            var privateInvoice = CreatePrivateInvoice(privateCustomer, totalAmount, invoiceDate);
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
        public string CalculateCreateBusinessInvoiceDocuments(BusinessCustomer businessCustomer, DateTime invoiceDate)
        {
            if (BInvoiceExists(businessCustomer, invoiceDate))
            {
                return $"Fakturan för företaget {businessCustomer.CompanyName} på datumet {invoiceDate.ToShortDateString()} existerar redan.";
            }

            double totalAmount = 0;

            DateTime monthStart = new DateTime(invoiceDate.Year, invoiceDate.Month, 1);
            DateTime monthEnd = new DateTime(invoiceDate.Year, invoiceDate.Month, DateTime.DaysInMonth(invoiceDate.Year, invoiceDate.Month));

            var allActiveVehicleInsurances = unitOfWork.VehicleInsuranceRepository.GetAll()
                .Where(i => i.BusinessCustomer == businessCustomer && i.Status == Status.Aktiv)
                .ToList();

            foreach (var insurance in allActiveVehicleInsurances)
            {
                if (insurance.StartDate <= monthEnd && insurance.EndDate >= monthStart)
                {
                    if (insurance.Paymentform == Paymentform.Månad && insurance.StartDate >= monthStart && insurance.StartDate <= monthEnd)
                    {
                        totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform) * 2;
                    }
                    else if (insurance.Paymentform == Paymentform.Månad && insurance.StartDate < monthStart)
                    {
                        totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                    }
                    else if (insurance.Paymentform == Paymentform.Kvartal && insurance.StartDate < monthStart)
                    {
                        if ((monthStart.Month - insurance.StartDate.Month) % 3 == 0)
                        {
                            totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                        }
                    }
                    else if (insurance.Paymentform == Paymentform.Halvår && insurance.StartDate < monthStart)
                    {
                        if ((monthStart.Month - insurance.StartDate.Month) % 6 == 0)
                        {
                            totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                        }
                    }
                    else if (insurance.Paymentform != Paymentform.Månad && insurance.StartDate >= monthStart && insurance.StartDate <= monthEnd)
                    {
                        totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                    }
                }
            }

            var allActiveLiabilityInsurances = unitOfWork.LiabilityInsuranceRepository.GetAll()
                .Where(i => i.BusinessCustomer == businessCustomer && i.Status == Status.Aktiv)
                .ToList();

            foreach (var insurance in allActiveLiabilityInsurances)
            {
                if (insurance.StartDate <= monthEnd && insurance.EndDate >= monthStart)
                {
                    if (insurance.Paymentform == Paymentform.Månad && insurance.StartDate >= monthStart && insurance.StartDate <= monthEnd)
                    {
                        totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform) * 2;
                    }
                    else if (insurance.Paymentform == Paymentform.Månad && insurance.StartDate < monthStart)
                    {
                        totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                    }
                    else if (insurance.Paymentform == Paymentform.Kvartal && insurance.StartDate < monthStart)
                    {
                        if ((monthStart.Month - insurance.StartDate.Month) % 3 == 0)
                        {
                            totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                        }
                    }
                    else if (insurance.Paymentform == Paymentform.Halvår && insurance.StartDate < monthStart)
                    {
                        if ((monthStart.Month - insurance.StartDate.Month) % 6 == 0)
                        {
                            totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                        }
                    }
                    else if (insurance.Paymentform != Paymentform.Månad && insurance.StartDate >= monthStart && insurance.StartDate <= monthEnd)
                    {
                        totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
                    }
                }
            }

            var allActiveRealEstateInsurances = unitOfWork.RealEstateInsuranceRepository.GetAll()
                .Where(i => i.BusinessCustomer == businessCustomer && i.Status == Status.Aktiv)
                .ToList();

            foreach (var realEstateInsurance in allActiveRealEstateInsurances)
            {
                if (realEstateInsurance.StartDate <= monthEnd && realEstateInsurance.EndDate >= monthStart)
                {
                    var totalInventoryPremium = unitOfWork.InventoryRepository.GetAll()
                        .Where(inv => inv.RealEstateInsuranceId == realEstateInsurance.InsuranceId)
                        .Sum(inv => inv.InvPremium);

                    var combinedPremium = realEstateInsurance.Premium + totalInventoryPremium;

                    if (realEstateInsurance.Paymentform == Paymentform.Månad && realEstateInsurance.StartDate >= monthStart && realEstateInsurance.StartDate <= monthEnd)
                    {
                        totalAmount += CalculateInvoiceAmount(combinedPremium, realEstateInsurance.Paymentform) * 2;
                    }
                    else if (realEstateInsurance.Paymentform == Paymentform.Månad && realEstateInsurance.StartDate < monthStart)
                    {
                        totalAmount += CalculateInvoiceAmount(combinedPremium, realEstateInsurance.Paymentform);
                    }
                    else if (realEstateInsurance.Paymentform == Paymentform.Kvartal && realEstateInsurance.StartDate < monthStart)
                    {
                        if ((monthStart.Month - realEstateInsurance.StartDate.Month) % 3 == 0)
                        {
                            totalAmount += CalculateInvoiceAmount(combinedPremium, realEstateInsurance.Paymentform);
                        }
                    }
                    else if (realEstateInsurance.Paymentform == Paymentform.Halvår && realEstateInsurance.StartDate < monthStart)
                    {
                        if ((monthStart.Month - realEstateInsurance.StartDate.Month) % 6 == 0)
                        {
                            totalAmount += CalculateInvoiceAmount(combinedPremium, realEstateInsurance.Paymentform);
                        }
                    }
                    else if (realEstateInsurance.Paymentform != Paymentform.Månad && realEstateInsurance.StartDate >= monthStart && realEstateInsurance.StartDate <= monthEnd)
                    {
                        totalAmount += CalculateInvoiceAmount(combinedPremium, realEstateInsurance.Paymentform);
                    }
                }
            }

            if (totalAmount <= 0)
            {
                return "Inga fakturor att skapa för denna företagskund.";
            }

            var businessInvoice = CreateBusinessInvoice(businessCustomer, totalAmount, invoiceDate);
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
