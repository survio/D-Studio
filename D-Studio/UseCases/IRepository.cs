using System.Collections.Generic;
using Electronic_Invoice;

namespace UseCases
{
    public interface IRepository<T> where T:Invoice

    {
    public IEnumerable<T> GetAllInvoices(); 
    public T GetInvoiceById(int id);
    public void AddInvoice(T invoice);
    public void ChangeInvoice(T changeInvoice, T changeTo);

    }
}