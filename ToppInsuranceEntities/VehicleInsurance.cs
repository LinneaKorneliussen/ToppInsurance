using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class VehicleInsurance : Insurance
    {
        public DeductibleVehicle DeductibleVehicle { get; set; }
        public CoverageType CoverageType { get; set; }
        public RiskZone RiskZone { get; set; }
        public Vehicle Vehicle { get; set; }
        public int BusinessCustomerId { get; set; }
        public BusinessCustomer BusinessCustomer { get; private set; }

        public VehicleInsurance(BusinessCustomer customer, Vehicle car, DeductibleVehicle deductible, CoverageType coverageType, RiskZone riskZone,
            DateTime startDate, DateTime endDate, InsuranceType type, Paymentform paymentform, string note, Employee user) :
            base(startDate, endDate, type, paymentform, note, user)
        {
            BusinessCustomer = customer;
            Vehicle = car;
            DeductibleVehicle = deductible;
            CoverageType = coverageType;
            RiskZone = riskZone;
            Premium = CalculateMonthlyPremium(); 
        }

        public VehicleInsurance() { }

        public int CalculateMonthlyPremium()
        {
            Premium = GetBasePremium(DeductibleVehicle, CoverageType);
            double riskFactor = GetRiskFactor(RiskZone);
            return (int)(Premium * riskFactor);
        }
        private int GetBasePremium(DeductibleVehicle deductible, CoverageType coverageType)
        {
            switch (deductible)
            {
                case DeductibleVehicle.DV1:
                    switch (coverageType)
                    {
                        case CoverageType.Trafik: return 350; 
                        case CoverageType.Halv: return 550; 
                        case CoverageType.Hel: return 800; 
                    }
                    break;

                case DeductibleVehicle.DV2:
                    switch (coverageType)
                    {
                        case CoverageType.Trafik: return 300; 
                        case CoverageType.Halv: return 450; 
                        case CoverageType.Hel: return 700; 
                    }
                    break;

                case DeductibleVehicle.DV3:
                    switch (coverageType)
                    {
                        case CoverageType.Trafik: return 250; 
                        case CoverageType.Halv: return 400; 
                        case CoverageType.Hel: return 600; 
                    }
                    break;

                default:
                    throw new ArgumentException("Ogiltig självrisk");
            }

            throw new ArgumentException("Ogiltig kombination av självrisk och omfattning");
        }

        private double GetRiskFactor(RiskZone riskZone)
        {
            switch (riskZone)
            {
                case RiskZone.Z1: return 1.3; 
                case RiskZone.Z2: return 1.2; 
                case RiskZone.Z3: return 1.1; 
                case RiskZone.Z4: return 1.0; 
                default:
                    throw new ArgumentException("Ogiltig riskzon");
            }
        }
    }





}
