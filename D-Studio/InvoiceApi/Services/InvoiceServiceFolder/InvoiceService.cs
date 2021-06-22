using Electronic_Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess;
using UseCases;

namespace InvoiceApi.Services
{
    public class InvoiceService : IInvoiceService
    {
        public  InvoiceService()
        {
            useCasesAPI = new UseCasesAPI(new Csv()); //здесь указывать какой репозиторий использовать Stab, CSV, EF
        }                                                      //new InvoiceRepository(new InvoiceDbContext())
        private UseCasesAPI useCasesAPI;                       //new Csv()
                                                               //new Stab() -просто заглушка на этап разработки

        public IEnumerable<Invoice> GetAll(InvoiceFilterParameters filterParameters, PageParameters pageParameters)
        {
            var resultEnumerable = useCasesAPI.GetAllInvoices();
            if (filterParameters != null)
            {
                var filterFunction = getFuncToFilter(filterParameters);
                if (filterFunction != null)
                    resultEnumerable = resultEnumerable.Where(filterFunction);
                resultEnumerable = resultEnumerable.OrderBy(filterParameters.OrderBy);
            }
            if (pageParameters!=null && pageParameters.ContentLength != 0)
                resultEnumerable = resultEnumerable.Take(pageParameters.ContentLength);
            return resultEnumerable;
        }

        public Invoice GetById(int id)
        {
            return useCasesAPI.GetInvoiceById(id);
        }

        public void Add(Invoice invoice)
        {
            useCasesAPI.AddInvoice(invoice);
        }

        public bool Change(int id, Invoice changeTo)
        {
          var findInvoice =  useCasesAPI.GetAllInvoices().FirstOrDefault(invoice => invoice.Id == id);
          if (findInvoice == null) return false;
          useCasesAPI.ChangeInvoice(findInvoice, changeTo);
          return true;
        }

        private Func<Invoice, bool> getFuncToFilter(InvoiceFilterParameters filterParameters)
        {
            var binaryLambdaExpressions = new List<BinaryExpression>();
            var notNullPropertiesInParameters = filterParameters.GetType().GetProperties().Where(x => x.GetValue(filterParameters) != null);
            var invoiceLambdaParameter = Expression.Parameter(typeof(Invoice), "invoice");
            foreach (var property in notNullPropertiesInParameters)
            {
                if (typeof(Invoice).GetProperties().All(x => x.Name != property.Name)) continue;
                var memberLambdaExpression = Expression.Property(invoiceLambdaParameter, typeof(Invoice), property.Name);
                binaryLambdaExpressions.Add(Expression.Equal(memberLambdaExpression, Expression.Convert(Expression.Constant(property.GetValue(filterParameters)), memberLambdaExpression.Type)));
            }
            if(binaryLambdaExpressions.Count==0) return null;
            var resultExpression = binaryLambdaExpressions.Aggregate(Expression.And);
            var lambda = Expression.Lambda<Func<Invoice, bool>>(resultExpression, invoiceLambdaParameter);
            return lambda.Compile();
        }
    }
}