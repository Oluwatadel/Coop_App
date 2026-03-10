namespace CoopApplication.api.Exceptions
{
    public class NotFoundException (string message) : Exception(message);
    public class SaveOperationException (string message) : Exception(message);
    public class AlreadyExistsException(string message) : Exception(message);
    public class TransactionAmountException(string message) : Exception(message);
}
