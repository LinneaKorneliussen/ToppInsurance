using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class PrivateInvoice : Invoice
    {
        public PrivateCustomer PrivateCustomer { get; set; }


    }
}
