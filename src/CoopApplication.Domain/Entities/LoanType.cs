namespace CoopApplication.Domain.Entities
{
    public class LoanType : Auditable
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal MinimumLoanRepayment { get; set; }
        public decimal AnnualInterestRate { get; set; } 
        public int LiquidityPeriodInMonths { get; set; }
        public int LoanVersion { get; set; }
        public List<LoanType> PreviousLoanVersion { get; set; } = [];
        
        public LoanType(string name, string description, decimal minimumLoanRepayment, decimal annualInterestRate, int liquidityPeriodInMonths)
        {
            Name = name;
            MinimumLoanRepayment = minimumLoanRepayment;
            AnnualInterestRate = annualInterestRate;
            LiquidityPeriodInMonths = liquidityPeriodInMonths;
            LoanVersion = 1;
            Description = description;
        }
        
        public LoanType CreateNewVersionOfLoanType(string? description, int? previousLoanTypeVersionNumber, string? name, 
            decimal? minimumLoanRepayment, decimal? annualInterestRate, int? liquidityPeriod)
        {
            var newLoanType = new LoanType(
                name ?? Name,
                description ?? Description,
                minimumLoanRepayment ?? MinimumLoanRepayment,
                annualInterestRate ?? AnnualInterestRate,
                liquidityPeriod ?? LiquidityPeriodInMonths);
            newLoanType.LoanVersion = LoanVersion + 1;
            return newLoanType;
        }

        public void AddOldVersionofLoanType(LoanType OldVersion)
        {
            PreviousLoanVersion.Add(OldVersion);

        }


    }
}
