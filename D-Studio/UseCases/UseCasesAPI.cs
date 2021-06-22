using System.Collections.Generic;
using Electronic_Invoice;
    //по легенде это наше приложение, в котором работают пользователи
namespace UseCases
{
    public class UseCasesAPI
    {
        private IRepository<Invoice> repository;

        public UseCasesAPI(IRepository<Invoice> repository)
        {
            this.repository = repository;
        }

        public Invoice GetInvoiceById(int id)
        {
            return repository.GetInvoiceById(id);
        }

        public IEnumerable<Invoice> GetAllInvoices()
        {
            return repository.GetAllInvoices();
        }

        public void AddInvoice(Invoice invoice)
        {
            repository.AddInvoice(invoice); //можно предположить, что в системе, по логике должны происходить некоторые события на добавление счета, если заставить InvoiceAPI работать напрямую с репозиторием, то система ничего не узнает о добавлении, или о других действиях.
            
        }

        public void ChangeInvoice(Invoice changeInvoice, Invoice changeToInvoice)
        {
            repository.ChangeInvoice(changeInvoice, changeToInvoice);
        }

    }
}