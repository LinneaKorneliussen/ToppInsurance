using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    /// <summary>
    /// VehicleController class provides methods for managing vehicle insurance for business customers.
    /// It allows the addition of vehicle insurance policies by interfacing with the VehicleRepository.
    /// This class acts as an intermediary between the presentation layer and the data access layer for handling vehicle insurance.
    /// </summary>
    public class VehicleController
    {
        private VehicleRepository vehicleRepository;

        public VehicleController()
        {
            vehicleRepository = new VehicleRepository();
        }

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
