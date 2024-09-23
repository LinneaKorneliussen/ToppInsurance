using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceDL
{
    public class LifeInsuranceRepository
    {
        private UnitOfWork unitOfWork;
        public LifeInsuranceRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }
    }
}
