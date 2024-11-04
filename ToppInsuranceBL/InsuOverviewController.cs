using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    /// <summary>
    /// InsuOverviewController class provides methods to retrieve insurance details for both private and 
    /// business customers. It allows access to different types of insurances, such as life, sickness, 
    /// liability, vehicle and real estate insurances, depending on the customer type. This class acts as an intermediary 
    /// between the presentation layer and data access layer for managing and viewing insurance data.
    /// </summary>
    public class InsuOverviewController
    {
        private InsuOverviewRepository insuOverviewRepository;

        public InsuOverviewController()
        {
            insuOverviewRepository = new InsuOverviewRepository();
        }


        #region Get all insurances for priavte customer Methods
        public LifeInsurance GetLifeInsurance(PrivateCustomer privateCustomer)
        {
            return insuOverviewRepository.GetLifeInsurance(privateCustomer);
        }

        public List<SicknessAccidentInsurance> GetAllSicknessInsurances(PrivateCustomer privateCustomer)
        {
            return insuOverviewRepository.GetAllSicknessInsurances(privateCustomer);
        }
        #endregion

        #region Get all insurances for business customer Methods
        public List<LiabilityInsurance> GetAllLiabilityInsurances(BusinessCustomer businessCustomer)
        {
            return insuOverviewRepository.GetAllLiabilityInsurances(businessCustomer);
        }

        public List<VehicleInsurance> GetAllVehicleInsurances(BusinessCustomer businessCustomer)
        {
            return insuOverviewRepository.GetAllVehicleInsurances(businessCustomer);
        }
        public List<RealEstateInsurance> GetAllRealEstateInsurances(BusinessCustomer businessCustomer)
        {
            return insuOverviewRepository.GetAllRealEstateInsurances(businessCustomer);
        }

        #endregion

    }
}
