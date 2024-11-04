using Microsoft.EntityFrameworkCore;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
    /// <summary>
    /// The ProspectRepository class manages prospect data for both private and business customers within the system.
    /// It provides functionality to add notes for customers, retrieve customers with associated notes, and search for prospective customers based on certain criteria.
    /// This class interacts with the data layer through the unit of work pattern, enabling efficient data access and ensuring data consistency.
    /// </summary>

    public class ProspectRepository
    {
        private UnitOfWork unitOfWork;

        public ProspectRepository() 
        { 
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Add note
        public void AddPCNote(string note, Employee employee, PrivateCustomer? privateCustomer, BusinessCustomer? businessCustomer)
        {
            if (privateCustomer == null)
            {
                throw new ArgumentNullException(nameof(privateCustomer), "Ingen privatkund angiven.");
            }

            ProspectInformation prospectPrivateInformation = new ProspectInformation
            {
                SigningDate = DateTime.Now, 
                Note = note, 
                EmployeeId = employee.PersonId, 
                PrivateCustomerId = privateCustomer.PersonId, 
                BusinessCustomerId = null 
            };

            unitOfWork.ProspectRepository.Add(prospectPrivateInformation);
            unitOfWork.Save();
        }


        public void AddBCNote(string note, Employee employee, PrivateCustomer? privateCustomer, BusinessCustomer? businessCustomer)
        {
            if (businessCustomer == null)
            {
                throw new ArgumentNullException(nameof(businessCustomer), "Ingen företagskund angiven.");
            }

            ProspectInformation prospectBusinessInformation = new ProspectInformation
            {
                SigningDate = DateTime.Now,
                Note = note,
                EmployeeId = employee.PersonId,
                PrivateCustomerId = null,
                BusinessCustomerId = businessCustomer.PersonId
            };

            unitOfWork.ProspectRepository.Add(prospectBusinessInformation);
            unitOfWork.Save();
        }

        #endregion

        #region Get business Customer with notes
        public List<ProspectInformation> BusinessCustomerProspect(BusinessCustomer customer)
        {
            return unitOfWork.ProspectRepository.GetAllQueryable()
                .Include(p => p.Employee)
                .Where(p => p.BusinessCustomerId == customer.PersonId)
                .ToList();
        }

        #endregion

        #region Get private Customer with notes
        public List<ProspectInformation> PrivateCustomerProspect(PrivateCustomer customer)
        {
            return unitOfWork.ProspectRepository.GetAllQueryable()
                .Include(p => p.Employee)
                .Where(pc => pc.PrivateCustomerId == customer.PersonId)
                .ToList();
        }

        #endregion

        #region Get Private Customer Prospect Method 
        public List<PrivateCustomer> GetPrivateCustomerProspects(string searchPrivateCustomers)
        {
            List<PrivateCustomer> privateProspects = new List<PrivateCustomer>();
            var privateCustomers = unitOfWork.PCRepository.GetAll();

            var activeLifeInsurances = unitOfWork.LifeInsuranceRepository.GetAll()
                .Where(l => l.Status == Status.Aktiv)
                .ToList();

            var activeSicknessAccidentInsurances = unitOfWork.SicknessAccidentInsuranceRepository.GetAll()
                .Where(s => s.Status == Status.Aktiv)
                .ToList();

            foreach (var customer in privateCustomers)
            {
                int totalInsuranceCount = 0;

                totalInsuranceCount += activeLifeInsurances
                    .Count(l => l.PrivateCustomerId == customer.PersonId);

                totalInsuranceCount += activeSicknessAccidentInsurances
                    .Count(s => s.PrivateCustomerId == customer.PersonId);

                if (totalInsuranceCount <= 1)
                {
                    privateProspects.Add(customer);
                }
            }

            bool isNumericSearch = int.TryParse(searchPrivateCustomers, out int searchNumber);
            var matchingProspects = privateProspects.Where(c =>
                (c.FirstName.Contains(searchPrivateCustomers, StringComparison.OrdinalIgnoreCase) ||
                 c.LastName.Contains(searchPrivateCustomers, StringComparison.OrdinalIgnoreCase)) ||
                (isNumericSearch && c.SSN.ToString().Contains(searchPrivateCustomers))
            ).ToList();

            return matchingProspects;
        }
        #endregion

        #region Get Business Customer with only one insurance Method 
        public List<BusinessCustomer> GetBusinessCustomerProspects(string searchBusinessCustomers)
        {
            List<BusinessCustomer> businessProspects = new List<BusinessCustomer>();
            var businessCustomers = unitOfWork.BCRepository.GetAll().ToList();

            var activeLiabilityInsurance = unitOfWork.LiabilityInsuranceRepository.GetAll()
                .Where(l => l.Status == Status.Aktiv)
                .ToList();

            var activeVehicleInsurance = unitOfWork.VehicleInsuranceRepository.GetAll()
                .Where(s => s.Status == Status.Aktiv)
                .ToList();

            var activeRealEstateInsurance = unitOfWork.RealEstateInsuranceRepository.GetAll()
                .Where(s => s.Status == Status.Aktiv)
                .ToList();

            foreach (var customer in businessCustomers)
            {
                int totalInsuranceCount = 0;

                totalInsuranceCount += activeLiabilityInsurance
                    .Count(l => l.BusinessCustomerId == customer.PersonId);

                totalInsuranceCount += activeVehicleInsurance
                    .Count(s => s.BusinessCustomerId == customer.PersonId);

                totalInsuranceCount += activeRealEstateInsurance
                    .Count(s => s.BusinessCustomerId == customer.PersonId);

                if (totalInsuranceCount <= 1)
                {
                    businessProspects.Add(customer);
                }
            }

            bool isNumericSearch = int.TryParse(searchBusinessCustomers, out int searchNumber);
            var matchingProspects = businessProspects.Where(c =>
                c.CompanyName.Contains(searchBusinessCustomers, StringComparison.OrdinalIgnoreCase) ||
                (isNumericSearch && c.Organizationalnumber.ToString().Contains(searchBusinessCustomers))
            ).ToList();

            return matchingProspects;
        }
        #endregion
    }
}
