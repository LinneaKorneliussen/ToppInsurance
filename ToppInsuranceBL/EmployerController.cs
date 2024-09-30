using System.Numerics;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    public class EmployerController
    {
        private EmployerRepository employerRepository;

        public EmployerController()
        {
            employerRepository = new EmployerRepository();
        }
    }
}
