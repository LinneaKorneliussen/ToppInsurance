using System;
using System.Collections.Generic;
using System.Linq;

namespace TopInsuranceEntities
{
    public class LiabilityInsurance : Insurance
    {
        public string ContactPerson { get; set; }
        public string ContactPhNo { get; set; }
        public DeductibleLiability Deductible { get; set; } 
        public InsuranceAmount InsuranceAmount { get; set; } 
        public int MonthlyPremium { get; private set; }
        public int BusinessCustomerId { get; set; }
        public BusinessCustomer BusinessCustomer { get; private set; }

        public LiabilityInsurance(BusinessCustomer customer, DateTime startDate, DateTime endDate, InsuranceType type,
            Paymentform paymentform, string note, string contactPerson, string contactPhNo, DeductibleLiability deductible, InsuranceAmount insuranceAmount, Employee user) :
            base(startDate, endDate, type, paymentform, note, user)
        {
            BusinessCustomer = customer;
            ContactPerson = contactPerson;
            ContactPhNo = contactPhNo;
            Deductible = deductible;
            InsuranceAmount = insuranceAmount;
            MonthlyPremium = CalculateMonthlyPremium();
        }

        public LiabilityInsurance() { }

        private int CalculateMonthlyPremium()
        {
            switch (InsuranceAmount)
            {
                case InsuranceAmount.ThreeMillion:
                    switch (Deductible)
                    {
                        case DeductibleLiability.EttPrisbasbelopp: return 500;
                        case DeductibleLiability.HalvtPrisbasbelopp: return 600;
                        case DeductibleLiability.TreFjärdedelsPrisbasbelopp: return 700;
                        case DeductibleLiability.EnFjärdedelsPrisbasbelopp: return 800;
                    }
                    break;

                case InsuranceAmount.FiveMillion:
                    switch (Deductible)
                    {
                        case DeductibleLiability.EttPrisbasbelopp: return 900;
                        case DeductibleLiability.HalvtPrisbasbelopp: return 1200;
                        case DeductibleLiability.TreFjärdedelsPrisbasbelopp: return 1100;
                        case DeductibleLiability.EnFjärdedelsPrisbasbelopp: return 1300;
                    }
                    break;

                case InsuranceAmount.TenMillion:
                    switch (Deductible)
                    {
                        case DeductibleLiability.EttPrisbasbelopp: return 1400;
                        case DeductibleLiability.HalvtPrisbasbelopp: return 1700;
                        case DeductibleLiability.TreFjärdedelsPrisbasbelopp: return 1600;
                        case DeductibleLiability.EnFjärdedelsPrisbasbelopp: return 1800;
                    }
                    break;

                default:
                    throw new ArgumentException("Ogiltigt försäkringsbelopp eller självrisk");
            }

            throw new ArgumentException("Ogiltig kombination av försäkringsbelopp och självrisk");
        }



    }

}
