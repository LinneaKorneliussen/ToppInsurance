using TopInsuranceDL;
using TopInsuranceEntities;

namespace ToppInsuranceDL
{
    public class LogInRepository
    {
        private UnitOfWork unitOfWork;

        public Employee LoggedIn { get; private set; }

        public Employee AuthorizeUser(int employeeId, string password)
        {
            unitOfWork = UnitOfWork.GetInstance();

            // Hämta den verifierade anställde baserat på användarnamn
            Employee verifiedEmployee = unitOfWork.Employees.FirstOrDefault(a => a.PersonId == employeeId);

            if (verifiedEmployee != null && verifiedEmployee.GetHashedPassword(password) == verifiedEmployee.PasswordHash)
            {
                LoggedIn = verifiedEmployee;
                return verifiedEmployee;
            }

            LoggedIn = null;
            return null;
        }
    }

}
