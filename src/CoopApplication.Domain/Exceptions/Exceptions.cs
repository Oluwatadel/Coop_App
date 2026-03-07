namespace CoopApplication.api.Exceptions
{
    public class TransactionAlreadyExistException(string message) : Exception(message);
    public class PrincipalAmountValidationException(string message) : Exception(message);
    public class MinimumLoanRepaymentException(string message) : Exception(message);
    public class LoanMinimumRepaymentException(string message) : Exception(message);
    public class InvalidLoanStatusException(string message) : Exception(message);
}
