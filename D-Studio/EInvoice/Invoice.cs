using System;

namespace Electronic_Invoice
{
    public class Invoice
    {
        public DateTime CreateDateTime { get; set; }
        public DateTime LastChangeDateTime { get; set; }
        public int Id { get; set; }
        public decimal Total { get; set; }
        public ProcessingStatus Status { get; set; } = ProcessingStatus.New;
        public PaymentWay Payment { get; set; }
    }

   public enum ProcessingStatus
    {
        New=1,
        PaidUp,
        Canceled
    }

  public  enum PaymentWay
    {
        CreditCard=1,
        DebitCard,
        Check
    }
}
