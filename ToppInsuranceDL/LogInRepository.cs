using TopInsuranceDL;
using TopInsuranceEntities;

namespace ToppInsuranceDL
{
    public class LogInRepository
    {
        private UnitOfWork unitOfWork;

        public LogInRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        public Employee AuthorizeUser(int agencyNumber, string password)
        {
            Employee verifiedEmployee = unitOfWork.Employees.FirstOrDefault(a => a.AgencyNumber == agencyNumber);

            if (verifiedEmployee != null && verifiedEmployee.GetHashedPassword(password) == verifiedEmployee.PasswordHash)
            {
                return verifiedEmployee;
            }
            return null;
        }

    }
}
