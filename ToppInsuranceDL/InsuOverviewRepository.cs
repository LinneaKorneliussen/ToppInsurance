using TopInsuranceEntities;
using Microsoft.EntityFrameworkCore;

namespace TopInsuranceDL
{
    public class InsuOverviewRepository
    {
        private UnitOfWork unitOfWork;

        public InsuOverviewRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Get all insurances for priavte customer Methods
        public LifeInsurance GetLifeInsurance(PrivateCustomer customer)
        {
            return unitOfWork.LifeInsuranceRepository.FirstOrDefault(l => l.PrivateCustomerId == customer.PersonId);
        }

        public List<SicknessAccidentInsurance> GetAllSicknessInsurances(PrivateCustomer customer)
        {
            return unitOfWork.SicknessAccidentInsuranceRepository.GetAll().Where(s => s.PrivateCustomerId == customer.PersonId).ToList();
        }
        #endregion

        #region Get all insurances for business customer Methods
        public List<LiabilityInsurance> GetAllLiabilityInsurances(BusinessCustomer customer)
        {
            return unitOfWork.LiabilityInsuranceRepository.GetAll().Where(l => l.BusinessCustomerId == customer.PersonId).ToList();
        }

        public List<VehicleInsurance> GetAllVehicleInsurances(BusinessCustomer customer)
        {
            return unitOfWork.VehicleInsuranceRepository
                .GetAllQueryable() 
                .Include(v => v.Car) 
                .Where(v => v.BusinessCustomerId == customer.PersonId)
                .ToList(); 
        }

        public List<RealEstateInsurance> GetAllRealEstateInsurances(BusinessCustomer customer)
        {
            return unitOfWork.RealEstateInsuranceRepository.GetAll().Where(r => r.BusinessCustomerId == customer.PersonId).ToList();
        }

        #endregion
    }
}
