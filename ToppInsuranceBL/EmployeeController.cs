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

        #region Register new employer method
        public void AddEmployer(string firstName, string lastName, string phoneNumber, string emailAddress, string address, int zipCode, string city, EmployeeRole employeeRole, string password)
        {
            Employee newEmployer = new Employee(firstName, lastName, phoneNumber, emailAddress, address, zipCode, city, employeeRole, password);

            employeeRepository.AddEmployee(firstName, lastName, phoneNumber, emailAddress, address, zipCode, city, employeeRole, password);
        }
        #endregion

        #region Get all employers customers Method
        public List<Employee> GetAllEmployers()
        {
            return employeeRepository.GetAllEmployees()
                             .Where(e => e.EmployeeRole == EmployeeRole.Säljare)
                             .ToList();
        }
        #endregion

        #region Get Salesperson by Last Name or Agency Number
        public List<Employee> GetSalespersonsByLastNameOrAgencyNumber(string searchText)
        {
            return employeeRepository.GetSalespersonsByLastNameOrAgencyNumber(searchText);
        }
        #endregion
    }
}
