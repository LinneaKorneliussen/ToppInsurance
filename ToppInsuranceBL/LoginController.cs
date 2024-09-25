using TopInsuranceEntities;
using TopInsuranceDL;


namespace TopInsuranceBL
{
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
