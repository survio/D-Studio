using System;
using System.Collections.Generic;
using Electronic_Invoice;
using UseCases;

namespace DataAccess
{
    public class Stab:IRepository<Invoice>
    {
      
        private List<Invoice> list = new List<Invoice>()
        {
            new Invoice(){Id = 1,Status = ProcessingStatus.New,Payment = PaymentWay.Check},
            new Invoice(){Id = 2,Status = ProcessingStatus.New,Payment = PaymentWay.CreditCard},
            new Invoice(){Id = 3,Status = ProcessingStatus.Canceled,Payment = PaymentWay.CreditCard},
            new Invoice(){Id = 4,Status = ProcessingStatus.Canceled,Payment = PaymentWay.DebitCard},
            new Invoice(){Id = 5,Status = ProcessingStatus.PaidUp,Payment = PaymentWay.Check},
            new Invoice(){Id = 6,Status = ProcessingStatus.Canceled, Payment = PaymentWay.CreditCard},
        };

      
        public IEnumerable<Invoice> GetAllInvoices()
        {
          return  list.ToArray();
        }

        public Invoice GetInvoiceById(int id)
        {
            return list.Find(invoice => invoice.Id == id);
        }

        public void AddInvoice(Invoice invoice)
        {
          list.Add(invoice);
        }

        public void ChangeInvoice(Invoice changeInvoice, Invoice changeTo)
        {
            list[list.IndexOf(changeInvoice)] = changeTo;
        }
    }
}
