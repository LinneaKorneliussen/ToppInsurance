using System.Numerics;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    public class EmployeeController
    {
        private EmployeeRepository employeeRepository;

        public EmployeeController()
        {
            employeeRepository = new EmployeeRepository();
        }

        #region SSN unique Method
        public bool SSNUnique(string ssn)
        {
            return employeeRepository.SSNUnique(ssn);
        }
        #endregion

        #region Register new employer method
        public void AddEmployee(string firstName, string lastName, string ssn, string phoneNumber, string emailAddress, string address, int zipCode, string city, EmployeeRole employeeRole, string password)
        {
            employeeRepository.AddEmployee(firstName, lastName, ssn, phoneNumber, emailAddress, address, zipCode, city, employeeRole, password);
        }
        #endregion

        #region Get all employees Method
        public List<Employee> GetAllSalesEmployees()
        {
            return employeeRepository.GetAllSalesEmployees();
        }
        #endregion

        #region Get Salesperson by Last Name or Agency Number
        public List<Employee> GetSalesEmployees(string searchEmployees)
        {
            return employeeRepository.GetSalesEmployees(searchEmployees);
        }
        #endregion
    }
}
