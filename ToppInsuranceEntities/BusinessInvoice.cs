using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class BusinessInvoice : Invoice
    {
        public BusinessCustomer BusinessCustomer { get; set; }
    }
}
