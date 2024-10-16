using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopInsuranceDL;
using TopInsuranceEntities;

namespace TopInsuranceBL
{
    public class ProspectController
    {
        private ProspectRepository prospectRepository;

        public ProspectController() 
        {
            prospectRepository = new ProspectRepository();
        }

        #region Add note
        
        public void AddPCNote(string note, Employee employee, PrivateCustomer? privateCustomer, BusinessCustomer? businessCustomer)
        {
            prospectRepository.AddPCNote(note, employee, privateCustomer, businessCustomer);
        }

        public void AddBCNote(string note, Employee employee, PrivateCustomer? privateCustomer, BusinessCustomer? businessCustomer)
        {
            prospectRepository.AddBCNote(note, employee, privateCustomer, businessCustomer);
        }
        #endregion



    }
}
