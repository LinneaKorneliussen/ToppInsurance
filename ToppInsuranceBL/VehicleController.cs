using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    public class VehicleController
    {
        private VehicleRepository vehicleRepository;

        public VehicleController()
        {
            vehicleRepository = new VehicleRepository();
        }

        #region Search Business Customers Method
        public List<BusinessCustomer> SearchBusinessCustomer(string searchTerm)
        {
            return vehicleRepository.SearchBusinessCustomer(searchTerm);
        }
        #endregion

        #region Add Vehicle Insurance Method
        public void AddVehicleInsurance(BusinessCustomer customer, Vehicle car, DeductibleVehicle deductible,
            CoverageType coverageType, RiskZone riskZone, DateTime startDate, DateTime endDate,
            InsuranceType type, Paymentform paymentform, string note, Employee user)
        {
            vehicleRepository.AddVehicleInsurance(customer, car, deductible, coverageType, riskZone, startDate, endDate, type, paymentform, note, user);
        }
        #endregion
    }
}
