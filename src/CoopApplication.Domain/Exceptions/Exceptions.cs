namespace CoopApplication.api.Exceptions
{
    public class TransactionAlreadyExistException(string message) : Exception(message);
    public class PrincipalAmountValidationException(string message) : Exception(message);
    public class MinimumLoanRepaymentException(string message) : Exception(message);
    public class LoanMinimumRepaymentException(string message) : Exception(message);
    public class InvalidLoanStatusException(string message) : Exception(message);
    public class LoanMaximumAndMinimumAmountException(string message) : Exception(message);
    public class MonthlyRepaymentAmountException(string message) : Exception(message);
    public class LoanLiquidityPeriodException(string message) : Exception(message);
    public class LoanVersionValidationException(string message) : Exception(message);

}
