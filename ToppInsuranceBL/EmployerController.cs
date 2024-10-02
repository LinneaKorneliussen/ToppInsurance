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

        #region Register new employer method
        public void AddEmployer(string firstName, string lastName, string phoneNumber, string emailAddress, string address, int zipCode, string city, EmployeeRole employeeRole, string password)
        {
            //Skapar ett stanrdarvärde för employerrole 
            Employee newEmployer = new Employee(firstName, lastName, phoneNumber, emailAddress, address, zipCode, city, employeeRole, password);

            employerRepository.AddEmployer(firstName, lastName, phoneNumber, emailAddress, address, zipCode, city, employeeRole, password);
        }
        #endregion

        #region Get all employers customers Method
        public List<Employee> GetAllEmployers()
        {
            return employerRepository.GetAllEmployers()
                             .Where(e => e.EmployeeRole == EmployeeRole.Säljare)
                             .ToList();
        }

        #endregion
    }
}
