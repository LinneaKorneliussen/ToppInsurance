using TopInsuranceEntities;


namespace TopInsuranceDL
{
    /// <summary>
    ///LogInRepository handles user login and authentication based on agency number and password.
    ///If the login is successful, the logged-in user is stored in the LoggedIn property; otherwise, it is set to null.
    /// </summary>
    
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
