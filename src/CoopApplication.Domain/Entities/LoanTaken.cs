using CoopApplication.api.Exceptions;
using CoopApplication.Domain.Enums;
using System.Linq;

namespace CoopApplication.Domain.Entities
{
    public class LoanTaken : Auditable
    {
        public Guid UserId { get; set; }
        public LoanType LoanType { get; set; }
        public decimal PrincipalAmount { get; private set; }
        public decimal TotalRepaymentAmount { get; private set; } = 0;
        public decimal MonthlyPaymentAmount { get; private set; } = 0;
        public decimal BalanceRemaining { get; private set; } = 0;
        public LoanStatus Status { get; set; } = LoanStatus.Pending;
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; set; }
        public HashSet<LoanRepayment> LoanRepayments { get; set; } = [];

        public LoanTaken(Guid userId, LoanType loanType, decimal principalAmount,
            LoanStatus status, DateTime startDate, DateTime endDate)
        {
            UserId = userId;
            LoanType = loanType;
            AddPrincipalAmount(principalAmount);
            Status = status;
            SetLoanStartDate(startDate);
            EndDate = endDate;
            CalculateRepaymentAndAmount();
        }
        private void CalculateRepaymentAndAmount()
        {
            var liquidityPeriod = LoanType.LiquidityPeriodInMonths;
            var years = liquidityPeriod / 12m;
            var interestRate = years * LoanType.AnnualInterestRate;
            var interest = PrincipalAmount * interestRate;
            TotalRepaymentAmount = PrincipalAmount + interest;
            BalanceRemaining = TotalRepaymentAmount;
            CalculateMonthlyRepaymentAmount(liquidityPeriod, TotalRepaymentAmount);
        }
        private void CalculateMonthlyRepaymentAmount(int months, decimal totalRepaymentAmount)
        {
            var amountPayable = totalRepaymentAmount / months;
            var minimunRepaymentForCurrentLoan = LoanType.MinimumLoanRepayment;
                //if(amountPayable < minimunRepaymentForCurrentLoan)
                //{
                //    MonthlyPaymentAmount = LoanType.MinimumLoanRepayment;
                //}
                //else
                //{
                //    MonthlyPaymentAmount = amountPayable;
                //}
            MonthlyPaymentAmount = amountPayable;
        }

        private void AddPrincipalAmount(decimal amount)
        {
            if (amount < 0)
                throw new PrincipalAmountValidationException("Loan Principal Amount cannot be less than 0");
            PrincipalAmount = amount;
        }

        public void RepaymentTransaction(LoanRepayment loanRepayment)
        {
            var repayment = LoanRepayments.Any(a => a.Id == loanRepayment.Id
                && a.Amount == loanRepayment.Amount
                && a.Date == loanRepayment.Date);
            if (repayment)
            {
                throw new TransactionAlreadyExistException($"Repayment transaction  with Id: {loanRepayment.Id}already existed");
            }

            if(loanRepayment.Amount < MonthlyPaymentAmount && BalanceRemaining > loanRepayment.Amount)
            {
                throw new LoanMinimumRepaymentException($"Amount #{loanRepayment.Amount} you are entering must not be less than: #{MonthlyPaymentAmount}");
            }
            BalanceRemaining -= loanRepayment.Amount;
            LoanRepayments.Add(loanRepayment);
            if(BalanceRemaining <= 0)
            {
                Status = LoanStatus.PaidOff;
            }
        }

        public void ApproveLoan(DateTime startDate, Guid approver)
        {
            if(Status != LoanStatus.Pending)
            {
                throw new InvalidLoanStatusException("Only loan that are pending can be approved");
            }
            SetLoanStartDate(startDate);
            Status = LoanStatus.Approved;
            ModifiedAt = DateTime.UtcNow;
            Modifier = approver;
        }
        private void SetLoanStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }

    }
}
