using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceEntities;

namespace TopInsuranceDL
{
    public class ProspectRepository
    {
        private UnitOfWork unitOfWork;

        public ProspectRepository() 
        { 
            unitOfWork = UnitOfWork.GetInstance();
        }


        #region Add note
        public void AddPCNote(string note, Employee employee, PrivateCustomer? privateCustomer, BusinessCustomer? businessCustomer)
        {
            if (privateCustomer == null)
            {
                throw new ArgumentNullException(nameof(privateCustomer), "Ingen privatkund angiven.");
            }

            ProspectInformation prospectPrivateInformation = new ProspectInformation
            {
                SigningDate = DateTime.Now, 
                Note = note, 
                EmployeeId = employee.PersonId, 
                PrivateCustomerId = privateCustomer.PersonId, 
                BusinessCustomerId = null 
            };

            unitOfWork.ProspectRepository.Add(prospectPrivateInformation);
            unitOfWork.Save();
        }


        public void AddBCNote(string note, Employee employee, PrivateCustomer? privateCustomer, BusinessCustomer? businessCustomer)
        {
            if (businessCustomer == null)
            {
                throw new ArgumentNullException(nameof(businessCustomer), "Ingen företagskund angiven.");
            }

            ProspectInformation prospectBusinessInformation = new ProspectInformation
            {
                SigningDate = DateTime.Now,
                Note = note,
                EmployeeId = employee.PersonId,
                PrivateCustomerId = null,
                BusinessCustomerId = businessCustomer.PersonId
            };

            unitOfWork.ProspectRepository.Add(prospectBusinessInformation);
            unitOfWork.Save();
        }

        #endregion
    }
}
