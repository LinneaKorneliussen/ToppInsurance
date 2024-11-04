using TopInsuranceEntities;

namespace TopInsuranceDL
{
    /// <summary>
    /// The VehicleRepository class manages operations related to vehicle insurance policies.
    /// It provides functionality to add new vehicle insurance records for business customers.
    /// Utilizing the unit of work pattern, it encapsulates data access logic for vehicle insurance,
    /// ensuring that changes are tracked and saved within a single transaction.
    /// </summary>

    public class VehicleRepository
    {
        private UnitOfWork unitOfWork;

        public VehicleRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

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
