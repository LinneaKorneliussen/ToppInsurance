using TopInsuranceEntities;
using TopInsuranceDL;


namespace TopInsuranceBL
{
    /// <summary>
    /// LoginController class handles the authorization of users by verifying agency number and password credentials.
    /// This class acts as an intermediary between the presentation layer and the data access layer, allowing 
    /// access control for employees within the system.
    /// </summary>
    public class LoginController
    {
        private LogInRepository logInRepository;

        public LoginController()
        {
            logInRepository = new LogInRepository();
        }

        #region Authorize User Controller
        public Employee AuthorizeUser(string agencyNumber, string password)
        {
            return logInRepository.AuthorizeUser(agencyNumber, password);
        }
        #endregion

    }
}
