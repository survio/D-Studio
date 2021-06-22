using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Electronic_Invoice;

namespace InvoiceApi.Services
{
    public static class Extensions
    {
        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> sourceIEnumerable, InvoiceFilterParameters.Sort sort) where T:Invoice
        {
            return sort == InvoiceFilterParameters.Sort.Asc
                ? sourceIEnumerable.OrderBy(x => x.Id)
                : sourceIEnumerable.OrderByDescending(x => x.Id);

        }
    }
}
