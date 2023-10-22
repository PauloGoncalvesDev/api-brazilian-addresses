namespace BrazilianAddresses.Exceptions.ExceptionsBase
{
    public class ValidationException : BrazilianAddressesException
    {
        public List<string> ErrorMessages { get; set; }

        public ValidationException(List<string> errorMessages) => ErrorMessages = errorMessages;

        public ValidationException(string errorMessage) => ErrorMessages = new List<string>() { errorMessage };
    }
}