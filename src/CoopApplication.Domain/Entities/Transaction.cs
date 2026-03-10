using CoopApplication.Domain.Enums;

namespace CoopApplication.Domain.Entities
{
    public class Transaction : Auditable
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime Date { get; set; }

    }
}
