using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
    public class EmployeeRepository
    {
        private UnitOfWork unitOfWork;
        public EmployeeRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region SSN unique check Method
        public bool SSNUnique(string ssn)
        {
            bool isUnique = !unitOfWork.EmployeeRepository.Any(p => p.SSN == ssn);
            return isUnique;
        }
        #endregion

        #region Register new employee method
        public void AddEmployee(string firstName, string lastName, string ssn, string phoneNumber, string emailAddress, string address, int zipCode, string city, EmployeeRole employeeRole, string password)
        {
            Employee employee = new Employee(firstName, lastName, ssn, phoneNumber, emailAddress, address, zipCode, city, employeeRole, password);
            unitOfWork.EmployeeRepository.Add(employee);
            unitOfWork.Save();
        }
        #endregion

        #region Get all employees Method
        public List<Employee> GetAllEmployees()
        {
            return unitOfWork.EmployeeRepository.GetAll().ToList();
        }
        #endregion

        #region Get Salesperson by Last Name or Agency Number
        public List<Employee> GetSalespersonsByLastNameOrAgencyNumber(string searchText)
        {
            return unitOfWork.EmployeeRepository.GetAll()
                .Where(e => e.EmployeeRole == EmployeeRole.Säljare &&
                           (e.LastName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                            e.AgencyNumber.Equals(searchText, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }
        #endregion
    }
}
