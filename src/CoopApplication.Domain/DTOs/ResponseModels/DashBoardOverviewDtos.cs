using CoopApplication.Domain.Entities;
using CoopApplication.Domain.Enums;

namespace CoopApplication.Domain.DTOs.ResponseModels
{
    public record AdminDashBoardOverviewDto
    {
        public Guid AdminId { get; init; }
        public string FullName { get; init; }
        public string Role { get; init; }
        public IReadOnlyList<AssociationDto> AssociationAndTotalMembers { get; init; } = [];
        public int TotalMembers { get; init; }
        public decimal TotalShares { get; init; }
        public decimal TotalAmountSavedByMembers { get; init; }
        public decimal TotalAmountOfLoanIssued { get; init; }
        public decimal TotalAmountOfLoanRepaymentYetToBePaid { get; init; }
        public decimal TotalRepayments { get; init; }
    }

    public record ManagerDashBoardOverviewDto
    {
        public Guid AdminId { get; init; }
        public string FullName { get; init; }
        public string Role { get; init; }
        public Guid AssociationId { get; init; }
        public string AssociationName { get; init; }
        public int TotalMembers { get; init; }
        public decimal TotalShares { get; init; }
        public decimal TotalAmountSavedByMembers { get; init; }
        public decimal TotalAmountOfLoanIssued { get; init; }
        public decimal TotalAmountOfLoanRepaymentYetToBePaid { get; init; }
        public decimal TotalRepayments { get; init; }
    }


    public record UserDashBoardOverviewDto
    {
        public Guid UserId { get; set; }
        public string AssociationName { get; init; }
        public string Role { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public IReadOnlyList<LoanTakenDto> LoansTakens { get; init; } = [];
        public ICollection<TransactionDto> Transactions { get; init; } = [];
        public AccountDto Account { get; init; }

    }

    public record AccountDto
    {
        public decimal TotalShares { get; init; }
        public decimal SavingsBalance { get; init; }
        public decimal TotalInterestAccrued { get; init; }
    }

    public record TransactionDto
    {
        public string ReferenceNo { get; init; }
        public Guid Id { get; init; }
        public decimal Amount { get; init; }
        public TransactionType TransactionType { get; init; }
        public PaymentMethod PaymentMethod { get; init; }
        public DateTime Date { get; init; }
    }

    public record LoanTakenDto
    {
        public Guid UserId { get; init; }
        public LoanType LoanType { get; init; }
        public decimal PrincipalAmount { get; init; }
        public decimal TotalRepaymentAmount { get; init; }
        public decimal MonthlyPaymentAmount { get; init; }
        public decimal BalanceRemaining { get; init; } = 0;
        public LoanStatus Status { get; init; } = LoanStatus.Pending;
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public HashSet<LoanRepayment> LoanRepayments { get; init; } = [];

    }
}
