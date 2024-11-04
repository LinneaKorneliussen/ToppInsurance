using TopInsuranceEntities;
using Microsoft.EntityFrameworkCore;

namespace TopInsuranceDL
{
    /// <summary>
    /// The InsuOverviewRepository provides access to insurance data for both 
    /// private and business customers within the insurance system. It offers methods 
    /// to retrieve specific types of insurance policies associated with a given customer.
    /// This class interacts with various insurance repositories through the unit of 
    /// work pattern, ensuring efficient data retrieval and management.
    /// </summary>
    
    public class InsuOverviewRepository
    {
        private UnitOfWork unitOfWork;

        public InsuOverviewRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Get all insurances for priavte customer Methods
        public LifeInsurance GetLifeInsurance(PrivateCustomer privateCustomer)
        {
            return unitOfWork.LifeInsuranceRepository.FirstOrDefault(l => l.PrivateCustomerId == privateCustomer.PersonId);
        }

        public List<SicknessAccidentInsurance> GetAllSicknessInsurances(PrivateCustomer privateCustomer)
        {
            return unitOfWork.SicknessAccidentInsuranceRepository.GetAll().Where(s => s.PrivateCustomerId == privateCustomer.PersonId).ToList();
        }
        #endregion

        #region Get all insurances for business customer Methods
        public List<LiabilityInsurance> GetAllLiabilityInsurances(BusinessCustomer businessCustomer)
        {
            return unitOfWork.LiabilityInsuranceRepository.GetAll().Where(l => l.BusinessCustomerId == businessCustomer.PersonId).ToList();
        }

        public List<VehicleInsurance> GetAllVehicleInsurances(BusinessCustomer businessCustomer)
        {
            return unitOfWork.VehicleInsuranceRepository
                .GetAllQueryable() 
                .Include(v => v.Car) 
                .Where(v => v.BusinessCustomerId == businessCustomer.PersonId)
                .ToList(); 
        }

        public List<RealEstateInsurance> GetAllRealEstateInsurances(BusinessCustomer businessCustomer)
        {
            return unitOfWork.RealEstateInsuranceRepository.GetAll().Where(r => r.BusinessCustomerId == businessCustomer.PersonId).ToList();
        }

        #endregion
    }
}
