using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TopInsuranceEntities;
using Microsoft.EntityFrameworkCore;

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

        public string CalculateCreatePrivateInvoiceDocuments(PrivateCustomer customer, DateTime invoiceDate)
        {
            double totalAmount = 0;

            var expiringLifeInsurances = unitOfWork.LifeInsuranceRepository.GetAll()
                .Where(i => i.PrivateCustomer == customer && i.EndDate >= invoiceDate && i.EndDate <= invoiceDate.AddDays(20) && i.Status == Status.Aktiv)
                .ToList();

            var expiringSicknessAccidentInsurances = unitOfWork.SicknessAccidentInsuranceRepository.GetAll()
                .Where(i => i.PrivateCustomer == customer && i.EndDate >= invoiceDate && i.EndDate <= invoiceDate.AddDays(20) && i.Status == Status.Aktiv)
                .ToList();

            var allActiveInsurances = expiringLifeInsurances.Cast<Insurance>()
                .Concat(expiringSicknessAccidentInsurances)
                .ToList();

            if (!allActiveInsurances.Any())
            {
                return "Inga fakturor att skapa för denna privatkund.";
            }

            var privateInvoice = CreatePrivateInvoice(customer, allActiveInsurances, invoiceDate);

            SaveInvoicesToJson(new List<Invoice> { privateInvoice });
            unitOfWork.Save();

            return $"Fakturaunderlag skapat för {invoiceDate.ToShortDateString()} med totalt belopp: {privateInvoice.InvoiceTotalAmount} SEK.";
        }

        #endregion

        private PrivateInvoice CreatePrivateInvoice(PrivateCustomer customer, List<Insurance> insurances, DateTime invoiceDate)
        {
            // Variabel för att hålla den totala fakturabeloppet
            double totalAmount = 0;

            // Loopar igenom alla försäkringar och beräknar totalbeloppet
            foreach (var insurance in insurances)
            {
                // Kontrollera betalningsform och summera premien korrekt
                switch (insurance.Paymentform)
                {
                    case Paymentform.Månad:
                        totalAmount += insurance.Premium;
                        break;
                    case Paymentform.Kvartal:
                        totalAmount += insurance.Premium * 3;
                        break;
                    case Paymentform.Halvår:
                        totalAmount += insurance.Premium * 6;
                        break;
                    case Paymentform.År:
                        totalAmount += insurance.Premium * 12;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(insurance.Paymentform), "Ogiltig betalningsform.");
                }
            }

            return new PrivateInvoice
            {
                PrivateCustomer = customer,
                InvoiceTotalAmount = totalAmount,
                InvoiceDate = DateTime.Now,
            };
        }

        //Företagskunder
        public string CalculateCreateBusinessInvoiceDocuments(BusinessCustomer customer, DateTime invoiceDate)
        {
            double totalAmount = 0;

            DateTime startDateForExpiringInsurances = invoiceDate.AddMonths(1);
            DateTime endDateForExpiringInsurances = startDateForExpiringInsurances.AddDays(DateTime.DaysInMonth(startDateForExpiringInsurances.Year, startDateForExpiringInsurances.Month) - 1);

            var expiringVehicleInsurances = unitOfWork.VehicleInsuranceRepository.GetAll()
                .Where(i => i.BusinessCustomer == customer && i.EndDate >= startDateForExpiringInsurances && i.EndDate <= endDateForExpiringInsurances && i.Status == Status.Aktiv)
                .ToList();

            var expiringLiabilityInsurances = unitOfWork.LiabilityInsuranceRepository.GetAll()
                .Where(i => i.BusinessCustomer == customer && i.EndDate >= startDateForExpiringInsurances && i.EndDate <= endDateForExpiringInsurances && i.Status == Status.Aktiv)
                .ToList();

            var expiringRealEstateInsurances = unitOfWork.RealEstateInsuranceRepository.GetAll()
                .Where(i => i.BusinessCustomer == customer && i.EndDate >= startDateForExpiringInsurances && i.EndDate <= endDateForExpiringInsurances && i.Status == Status.Aktiv)
                .ToList();

            foreach (var realEstateInsurance in expiringRealEstateInsurances)
            {
                totalAmount += realEstateInsurance.Premium; // Lägg till försäkringspremien

                // Hämta inventarier som är kopplade till realestateInsurance
                var inventories = unitOfWork.InventoryRepository.GetAll()
                    .Where(inv => inv.RealEstateInsuranceId == realEstateInsurance.InsuranceId);

                // Lägg till inventariernas premiekostnad
                totalAmount += inventories.Sum(inv => inv.InvPremium);
            }

            // Lägg till andra försäkringar till totalbeloppet
            var allActiveInsurances = expiringVehicleInsurances.Cast<Insurance>()
                .Concat(expiringLiabilityInsurances)
                .Concat(expiringRealEstateInsurances)
                .ToList();

            foreach (var insurance in allActiveInsurances)
            {
                // Kontrollera betalningsform och summera premien korrekt
                totalAmount += CalculateInvoiceAmount(insurance.Premium, insurance.Paymentform);
            }

            if (totalAmount <= 0)
            {
                return "Inga fakturor att skapa för denna företagskund.";
            }

            // Skapa fakturan med det totala beloppet
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
                InvoiceTotalAmount = totalAmount, // Använd det beräknade totalbeloppet
                InvoiceDate = invoiceDate // Använd det angivna fakturadatumet
            };
        }

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
