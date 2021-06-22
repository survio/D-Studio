using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Electronic_Invoice;

namespace InvoiceApi.Services
{
    public class InvoiceParametersEqualityComparer 
    {
        public static bool Equals(InvoiceFilterParameters invoiceFilterParameters, Invoice invoice)
        {
        
            if (ReferenceEquals(invoiceFilterParameters, null) || ReferenceEquals(invoice, null)) return false;
            return (invoiceFilterParameters.Payment==null || invoice.Payment == invoiceFilterParameters.Payment) && (invoice.Status == invoiceFilterParameters.Status|| invoiceFilterParameters.Status==null);
        }

        public static bool Equals(Invoice invoice, InvoiceFilterParameters invoiceFilterParameters )
        {
            return Equals(invoiceFilterParameters, invoice);
        }
    }
}
