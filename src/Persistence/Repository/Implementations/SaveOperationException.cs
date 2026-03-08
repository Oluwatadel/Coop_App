
namespace CoopApplication.Persistence.Repository.Implementations
{
    [Serializable]
    internal class SaveOperationException : Exception
    {
        public SaveOperationException()
        {
        }

        public SaveOperationException(string? message) : base(message)
        {
        }

        public SaveOperationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}