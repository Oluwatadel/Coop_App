namespace CoopApplication.Domain.Entities
{
    public class Account : Auditable
    {
        public Guid UserId { get; set; }
        public decimal TotalShares { get; set; }
        public decimal SavingsBalance { get; set; }
        public decimal TotalInterestAccrued { get; set; }
    }
}
