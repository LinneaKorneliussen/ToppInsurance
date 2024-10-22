using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        #region Calculate and create Invoice documents
        public string CalculateCreateInvoiceDocuments(DateTime invoiceDate)
        {
            double totalAmount = 0;

            var expiringLifeInsurances = unitOfWork.LifeInsuranceRepository.GetAll()
                .Where(i => i.EndDate == invoiceDate && i.Status == Status.Aktiv)
                .ToList();

            var expiringSicknessAccidentInsurances = unitOfWork.SicknessAccidentInsuranceRepository.GetAll()
                .Where(i => i.EndDate == invoiceDate && i.Status == Status.Aktiv)
                .ToList();

            var expiringLiabilityInsurances = unitOfWork.LiabilityInsuranceRepository.GetAll()
                .Where(i => i.EndDate == invoiceDate && i.Status == Status.Aktiv)
                .ToList();

            var expiringVehicleInsurances = unitOfWork.VehicleInsuranceRepository.GetAll()
                .Where(i => i.EndDate == invoiceDate && i.Status == Status.Aktiv)
                .ToList();

            var expiringRealEstateInsurances = unitOfWork.RealEstateInsuranceRepository.GetAll()
                .Where(i => i.EndDate == invoiceDate && i.Status == Status.Aktiv)
                .ToList();

            var privateInsurancesGrouped = new List<IGrouping<PrivateCustomer, Insurance>>();

            privateInsurancesGrouped.AddRange(expiringLifeInsurances
                .OfType<LifeInsurance>()
                .GroupBy(i => i.PrivateCustomer)
                .Cast<IGrouping<PrivateCustomer, Insurance>>() 
            );

            privateInsurancesGrouped.AddRange(expiringSicknessAccidentInsurances
                .OfType<SicknessAccidentInsurance>()
                .GroupBy(i => i.PrivateCustomer)
                .Cast<IGrouping<PrivateCustomer, Insurance>>() 
            );

            var businessInsurancesGrouped = new List<IGrouping<BusinessCustomer, Insurance>>();

            businessInsurancesGrouped.AddRange(expiringLiabilityInsurances
                .OfType<LiabilityInsurance>()
                .GroupBy(i => i.BusinessCustomer)
                .Cast<IGrouping<BusinessCustomer, Insurance>>() 
            );

            businessInsurancesGrouped.AddRange(expiringVehicleInsurances
                .OfType<VehicleInsurance>()
                .GroupBy(i => i.BusinessCustomer)
                .Cast<IGrouping<BusinessCustomer, Insurance>>() 
            );

            businessInsurancesGrouped.AddRange(expiringRealEstateInsurances
                .OfType<RealEstateInsurance>()
                .GroupBy(i => i.BusinessCustomer)
                .Cast<IGrouping<BusinessCustomer, Insurance>>()
            );


            if (!privateInsurancesGrouped.Any() && !businessInsurancesGrouped.Any())
            {
                return "Inga fakturor att skapa för detta datum.";
            }

            var invoices = new List<Invoice>();

            foreach (var insuranceGroup in privateInsurancesGrouped)
            {
                var privateInvoice = CreatePrivateInvoice(insuranceGroup.Key, insuranceGroup.ToList());
                invoices.Add(privateInvoice);
                totalAmount += privateInvoice.InvoiceTotalAmount;
            }

            foreach (var insuranceGroup in businessInsurancesGrouped)
            {
                var businessInvoice = CreateBusinessInvoice(insuranceGroup.Key, insuranceGroup.ToList());
                invoices.Add(businessInvoice);
                totalAmount += businessInvoice.InvoiceTotalAmount;
            }

            SaveInvoicesToJson(invoices);
            unitOfWork.Save();

            return $"Fakturaunderlag skapat för {invoiceDate.ToShortDateString()} med totalt belopp: {totalAmount} SEK.";
        }
        #endregion

        private PrivateInvoice CreatePrivateInvoice(PrivateCustomer customer, List<Insurance> insurances)
        {
            double totalAmount = insurances.Sum(i => i.Premium);

            return new PrivateInvoice
            {
                PrivateCustomer = customer,
                InvoiceTotalAmount = totalAmount,
                InvoiceDate = DateTime.Now, 
            };
        }

        private BusinessInvoice CreateBusinessInvoice(BusinessCustomer businessCustomer, List<Insurance> insurances)
        {
            double totalAmount = insurances.Sum(i => i.Premium);

            return new BusinessInvoice
            {
                BusinessCustomer = businessCustomer,
                InvoiceTotalAmount = totalAmount,
                InvoiceDate = DateTime.Now, 
            };
        }


        #region Save Invoices to JSON
        public void SaveInvoicesToJson(List<Invoice> invoices)
        {
            string filePath = "invoices.json";
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
                        CustomerType = "Private",
                        FirstName = privateInvoice.PrivateCustomer.FirstName,
                        LastName = privateInvoice.PrivateCustomer.LastName,
                        TotalAmount = privateInvoice.InvoiceTotalAmount,
                        InvoiceDate = privateInvoice.InvoiceDate
                    };
                }
                else if (invoice is BusinessInvoice businessInvoice)
                {
                    invoiceData = new
                    {
                        CustomerType = "Business",
                        CompanyName = businessInvoice.BusinessCustomer.CompanyName,
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
            string filePath = "invoices.json";

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
