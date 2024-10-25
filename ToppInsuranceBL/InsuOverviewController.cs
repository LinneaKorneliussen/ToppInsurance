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
