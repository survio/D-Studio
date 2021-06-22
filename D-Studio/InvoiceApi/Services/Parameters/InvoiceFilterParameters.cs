using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Electronic_Invoice;

namespace InvoiceApi.Services
{
    public class InvoiceFilterParameters
    {
        public ProcessingStatus? Status { get; set; }
        public PaymentWay? Payment { get; set; }
        public Sort OrderBy { get; set; }

        public enum Sort
        {
            Asc,
            Desc
        }


    }
}
