namespace CoopApplication.Domain.Entities
{
    public class LoanType : Auditable
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int MinimumLoanRepayment { get; set; }
        public decimal AnnualInterestRate { get; set; } 
        public int LiquidityPeriodInMonths { get; set; }
        public int LoanVersion { get; set; }
        public List<LoanType> PreviousLoanVersion { get; set; } = [];
        
        public LoanType(string name, string description, int minimumLoanRepayment, decimal annualInterestRate, int liquidityPeriodInMonths)
        {
            Name = name;
            MinimumLoanRepayment = minimumLoanRepayment;
            AnnualInterestRate = annualInterestRate;
            LiquidityPeriodInMonths = liquidityPeriodInMonths;
            LoanVersion = 1;
            Description = description;
        }
        
        public LoanType CreateNewVersionOfLoanType(string description, int previousLoanTypeVersionNumber, string name, 
            int minimumLoanRepayment, decimal annualInterestRate, int liquidityPeriod)
        {
            var loanType = new LoanType(description, name, minimumLoanRepayment, annualInterestRate, liquidityPeriod);
            int versionNumber = previousLoanTypeVersionNumber + 1;
            LoanVersion = versionNumber;
            return loanType;
        }

        public void AddOldVersionofLoanType(LoanType OldVersion)
        {
            PreviousLoanVersion.Add(OldVersion);

        }
    }
}
