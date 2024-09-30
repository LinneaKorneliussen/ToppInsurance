using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
    public class EmployerRepository
    {
        private UnitOfWork unitOfWork;
        public EmployerRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        #region Register new employer method
        public void AddEmployer(string name, string phoneNumber, string emailAddress, string address, int zipCode, string city, EmployeeRole employeeRole, string password)
        {
            Employee employee = new Employee(name, phoneNumber, emailAddress, address, zipCode, city, employeeRole, password);
            unitOfWork.EmployeeRepository.Add(employee);
            unitOfWork.Save();
        }
        #endregion
        public List<Employee> GetAllEmployers()
        {
            return unitOfWork.EmployeeRepository.GetAll().ToList();
        }
    }
}
