using TopInsuranceEntities;


namespace TopInsuranceDL
{
    public class LogInRepository
    {
        private UnitOfWork unitOfWork;

        public Employee LoggedIn { get; private set; }

        #region Authorize User method
        public Employee AuthorizeUser(string agencynumber, string password)
        {
      
            unitOfWork = UnitOfWork.GetInstance();

            Employee verifiedEmployee = unitOfWork.EmployeeRepository.FirstOrDefault(a => a.AgencyNumber == agencynumber);

            if (verifiedEmployee != null)
            {
                if (verifiedEmployee.GetHashedPassword(password) == verifiedEmployee.PasswordHash)
                {
                    LoggedIn = verifiedEmployee;
                    return verifiedEmployee; 
                }
            }

            LoggedIn = null;
            return null;
        }
        #endregion

    }

}
