namespace CoopApplication.Domain.Entities
{
    public class LoanRepayment
    {
        public Guid Id { get; set; }
        public Guid LoanId { get; set; }
        public Guid TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
