using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
    public class VehicleRepository
    {
        private UnitOfWork unitOfWork;

        public VehicleRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Search Business Customer Method 
        public List<BusinessCustomer> SearchBusinessCustomer(string searchTerm)
        {
            List<BusinessCustomer> allCustomers = unitOfWork.BCRepository.GetAll().ToList();

            bool isNumericSearch = int.TryParse(searchTerm, out int searchNumber);

            var matchingCustomers = allCustomers.Where(c =>
                c.CompanyName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (isNumericSearch && c.Organizationalnumber.ToString().Contains(searchTerm))
            ).ToList();

            return matchingCustomers;
        }
        #endregion

        #region Add Vehicle Insurance Method
        public void AddVehicleInsurance(BusinessCustomer customer, Vehicle car, DeductibleVehicle deductible, CoverageType coverageType,
            RiskZone riskZone, DateTime startDate, DateTime endDate, InsuranceType type, Paymentform paymentform, string note, Employee user)
        {
            VehicleInsurance vehicleInsurance = new VehicleInsurance(customer, car, deductible, coverageType, riskZone, startDate, endDate, type, paymentform, note, user);

            unitOfWork.VehicleInsuranceRepository.Add(vehicleInsurance);
            unitOfWork.Save();
        }
        #endregion
    }
}
