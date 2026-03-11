using CoopApplication.Domain.Enums;

namespace CoopApplication.Domain.DTOs.ResponseModels
{
    public record TransactionFilterDto
    {
        public Guid Id { get; set; }
        public string? FullName { get; init; }
        public string? AssociationName { get; init; }
        public DateTime? Date { get; init; }
        public decimal? Amount { get; init; }
        public TransactionType? TransactionType { get; init; }
        public PaymentMethod? PaymentMethod { get; init; }
    }

    public record LoanTypeResponse(Guid Id, string Name, string Description, decimal MinimumLoanRepayment,
        decimal AnnualInterestRate, int LiquidityPeriodInMonths, int LoanVersion);
}
