using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Electronic_Invoice;



namespace InvoiceApi.Services
{
    public interface IInvoiceService
    {
        public IEnumerable<Invoice> GetAll(InvoiceFilterParameters filterParameters, PageParameters pageParameters);
        public Invoice GetById(int id);
        public void Add(Invoice invoice);
        public bool Change(int id, Invoice changeTo);
    }
}
