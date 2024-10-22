using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public abstract class Invoice
    {
        public DateTime InvoiceDate { get; set; }
        public double InvoiceTotalAmount { get; set; }

    }
}
