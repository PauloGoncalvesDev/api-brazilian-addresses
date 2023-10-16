namespace BrazilianAddresses.Exceptions.ExceptionsBase
{
    public class ValidationException : BrazilianAddressesException
    {
        public ValidationException(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public List<string> ErrorMessages { get; set; }
    }
}
