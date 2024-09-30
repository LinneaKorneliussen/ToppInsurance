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
    }
}
