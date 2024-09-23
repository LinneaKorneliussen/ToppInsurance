using ToppInsuranceDL;
using TopInsuranceEntities;

namespace ToppInsuranceBL
{
    public class LoginController
    {
        private LogInRepository logInRepository;

        public LoginController()
        {
            logInRepository = new LogInRepository();
        }

        public Employee AuthorizeUser(int agencyNumber, string password)
        {
            return logInRepository.AuthorizeUser(agencyNumber, password);
        }


    }
}
