using CoopApplication.api.Exceptions;

namespace CoopApplication.Domain.Entities
{
    public class LoanType : Auditable
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public decimal MaximumLoanAmount { get; set; }
        public decimal MinimumLoanAmount { get; set; }
        public decimal MinimumLoanRepayment { get; set; }
        public decimal AnnualInterestRate { get; set; } 
        public int LiquidityPeriodInMonths { get; set; }
        public int LoanVersion { get; set; }
        public Guid? PreviousLoanVersionId { get; set; }
        
        public LoanType()
        {

        }
        public LoanType(string name, string? description, decimal maximumLoanAmount, decimal minimumLoanAmount, decimal minimumLoanRepayment, decimal annualInterestRate, int liquidityPeriodInMonths)
        {
            Name = name;
            AnnualInterestRate = annualInterestRate;
            ValidateRepaymentMonth(liquidityPeriodInMonths);
            LoanVersion = 1;
            Description = description;
            ValidateMinimumAndMaximumLoanAmount(minimumLoanAmount, maximumLoanAmount);
            ValidateMonthlyRepayment(minimumLoanRepayment, maximumLoanAmount, annualInterestRate, liquidityPeriodInMonths);

        }

        public LoanType CreateNewVersionOfLoanType(string? description, LoanType oldVersion, string name, 
            decimal? minimumLoanRepayment, decimal? annualInterestRate, int? liquidityPeriod, decimal? maximumLoanAmount, decimal? minimuloanAmount)
        {
            if(PreviousLoanVersionId.HasValue)
            {
                throw new LoanVersionValidationException("Loan type can only be editted once");
            }
            var newLoanType = new LoanType(
                name,
                description ?? Description,
                maximumLoanAmount ?? MaximumLoanAmount,
                minimuloanAmount ?? MinimumLoanAmount,
                minimumLoanRepayment ?? MinimumLoanRepayment,
                annualInterestRate ?? AnnualInterestRate,
                liquidityPeriod ?? LiquidityPeriodInMonths
            )
            {
                LoanVersion = oldVersion.LoanVersion + 1
            };
            AddOldVersionofLoanType(oldVersion);
            return newLoanType;
        }

        public void AddOldVersionofLoanType(LoanType OldVersion)
        {
            PreviousLoanVersionId = OldVersion.Id;

        }

        public void ValidateMinimumAndMaximumLoanAmount(decimal? minimumLoanAmount, decimal? maximumLoanAmount)
        {
            if (minimumLoanAmount.HasValue && minimumLoanAmount.Value <= 0)
                throw new LoanMaximumAndMinimumAmountException("Minimum loan amount cannot be 0 or less than 0");
            if (maximumLoanAmount.HasValue && maximumLoanAmount.Value <= 0)
                throw new LoanMaximumAndMinimumAmountException($"Maximum loan amount cannot be 0 or less than {minimumLoanAmount}");
            var min = minimumLoanAmount ?? MinimumLoanAmount;
            var max = maximumLoanAmount ?? MaximumLoanAmount;

            if(min > max)
            {
                throw new LoanMaximumAndMinimumAmountException(
                    $"Minimum loan amount {min} cannot be greater than maximum loan amount {max}");
            }
            MinimumLoanAmount = min;
            MaximumLoanAmount = max;

        }

        public void ValidateMonthlyRepayment(decimal monthlyRepaymentAmount,
            decimal loanAmount, decimal annualInterestRate, int liquidityPeriod)
        {
            var monthlyRepayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, liquidityPeriod);
            if(monthlyRepaymentAmount < monthlyRepayment)
            {
                throw new MonthlyRepaymentAmountException(
                    $"The monthly repayment amount based on your loan Amount and Annual interest rate should not be less than normal: #{monthlyRepayment}");
            }
            MinimumLoanRepayment = monthlyRepayment;
        }
        private decimal CalculateMonthlyRepayment(decimal loanAmount, decimal annualInterestRate, int liquidityPeriod)
        {
            var liquidityPeriodInYears = liquidityPeriod / 12m;
            var totalInterestInPercentage = annualInterestRate * liquidityPeriodInYears;
            var totalInterest = (totalInterestInPercentage / 100) * loanAmount;
            var totalRepaymentAmount = (totalInterest + loanAmount) ;
            return totalRepaymentAmount / liquidityPeriod;
        }

        private void ValidateRepaymentMonth(int liquidityPeriod)
        {
            
            if(liquidityPeriod <= 6)
            {
                throw new LoanLiquidityPeriodException("Loan liquidity period cannot be less than 6 months");
            }
            LiquidityPeriodInMonths = liquidityPeriod;
        }
    }
}
