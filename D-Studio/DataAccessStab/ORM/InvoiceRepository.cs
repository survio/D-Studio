using System.Collections.Generic;
using System.Linq;
using Electronic_Invoice;
using Microsoft.EntityFrameworkCore;
using UseCases;

namespace DataAccess
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        public InvoiceRepository(InvoiceDbContext invoiceDbContext)
        {
            this.invoiceDbContext = invoiceDbContext;
            table = invoiceDbContext.Set<Invoice>();
        }

        private InvoiceDbContext invoiceDbContext;
        private DbSet<Invoice> table;
        public IEnumerable<Invoice> GetAllInvoices()
        {
            return table;
        }

        public Invoice GetInvoiceById(int id)
        {
            return table.Find(id);
        }

        public void AddInvoice(Invoice invoice)
        {
            table.Add(invoice);
            invoiceDbContext.SaveChanges();
        }

        public void ChangeInvoice(Invoice changeInvoice, Invoice changeTo)
        {
            var invoiceToChange = table.FirstOrDefault(x => x.Id == changeInvoice.Id);
            if (invoiceToChange == null) return;
            if (changeInvoice.Id == changeTo.Id)
                invoiceDbContext.Entry(invoiceToChange).CurrentValues.SetValues(changeTo);
            else
            {
                invoiceDbContext.Entry(invoiceToChange).State = EntityState.Deleted;
                table.Add(changeTo);
            }
            invoiceDbContext.SaveChanges();
        }
    }
}