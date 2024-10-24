using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    public class InsuOverviewController
    {
        private InsuOverviewRepository insuOverviewRepository;

        public InsuOverviewController()
        {
            insuOverviewRepository = new InsuOverviewRepository();
        }


        #region Get all insurances for priavte customer Methods
        public LifeInsurance GetLifeInsurance(PrivateCustomer customer)
        {
            return insuOverviewRepository.GetLifeInsurance(customer);
        }

        public List<SicknessAccidentInsurance> GetAllSicknessInsurances(PrivateCustomer customer)
        {
            return insuOverviewRepository.GetAllSicknessInsurances(customer);
        }
        #endregion

        #region Get all insurances for business customer Methods
        public List<LiabilityInsurance> GetAllLiabilityInsurances(BusinessCustomer customer)
        {
            return insuOverviewRepository.GetAllLiabilityInsurances(customer);
        }

        public List<VehicleInsurance> GetAllVehicleInsurances(BusinessCustomer customer)
        {
            return insuOverviewRepository.GetAllVehicleInsurances(customer);
        }
        public List<RealEstateInsurance> GetAllRealEstateInsurances(BusinessCustomer customer)
        {
            return insuOverviewRepository.GetAllRealEstateInsurances(customer);
        }

        #endregion

    }
}
