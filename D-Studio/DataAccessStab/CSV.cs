using Electronic_Invoice;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UseCases;

namespace DataAccess
{
    public class Csv : IRepository<Invoice>
    {
        private List<Invoice> csvInvociesList;
        private FileStream csvFileStream;
        private StreamWriter csvStreamWriter;
       
        public Csv()
        {
            csvFileStream = File.Open(@"C:\Users\User\source\repos\InvoiceApi\bin\Debug\net5.0\Invoices.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader  csvStreamReader = new StreamReader(csvFileStream);
            csvStreamWriter = new StreamWriter(csvFileStream);
            csvStreamWriter.AutoFlush = true;
            csvInvociesList = new List<Invoice>();
            string stringLine;
            while ((stringLine = csvStreamReader.ReadLine()) != null)
            {
                csvInvociesList.Add(csvStringToInvoice(stringLine));
            }
        }

        ~Csv()
        {
            csvFileStream.Dispose();
        }

        public IEnumerable<Invoice> GetAllInvoices()=> csvInvociesList;
        
        public Invoice GetInvoiceById(int id)=> csvInvociesList.FirstOrDefault(x => x.Id == id);
    
        public void AddInvoice(Invoice invoice)
        {
            csvInvociesList.Add(invoice);
            writeInvociesListToCSV();
        }
        public void ChangeInvoice(Invoice changeInvoice, Invoice changeTo)
        {
            csvInvociesList[csvInvociesList.FindIndex(x => x.Id == changeInvoice.Id)] = changeTo;
            writeInvociesListToCSV();
        }

        private Invoice csvStringToInvoice(string csvString)
        {
            if (csvString.All(x => x == ' ')) return null;
            var csvData = csvString.Split(';');
            return new Invoice()
            {
                Id = int.Parse(csvData[1]),
                LastChangeDateTime = DateTime.Parse(csvData[0]),
                Payment = (PaymentWay)int.Parse(csvData[2]),
                Status = (ProcessingStatus)int.Parse(csvData[4]),
                Total = decimal.Parse(csvData[3], CultureInfo.InvariantCulture)
            };
        }

        private string invoiceToString(Invoice invoice)
        {
            return $"{invoice.LastChangeDateTime.ToString(("yyyy-MM-dd HH:mm:ss"))};{invoice.Id.ToString().PadLeft(4, '0')};{(int)invoice.Payment};{invoice.Total.ToString(CultureInfo.InvariantCulture)};{(int)invoice.Status}";
        }

        private void writeInvociesListToCSV()
        {
            csvStreamWriter.BaseStream.Position = 0;
            foreach (var invoice in csvInvociesList)
            {
                csvStreamWriter.WriteLine(invoiceToString(invoice));
            }
        }
    }
}