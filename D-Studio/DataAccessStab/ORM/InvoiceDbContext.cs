using System.Collections.Generic;
using System.IO;
using System.Linq;
using Electronic_Invoice;
using Microsoft.EntityFrameworkCore;
using UseCases;


namespace DataAccess
{
    public class InvoiceDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            var connectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\InvoiceDB.mdf;Integrated Security=True;Connect Timeout=30;Initial Catalog=InvoiceDB";
            optionsBuilder.UseSqlServer(connectionString, x => x.EnableRetryOnFailure());
        }
        public DbSet<Invoice> Invoices { get; set; }

      
    }
}