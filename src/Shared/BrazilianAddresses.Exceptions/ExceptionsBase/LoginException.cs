using BrazilianAddresses.Exceptions.ResourcesMessage;

namespace BrazilianAddresses.Exceptions.ExceptionsBase
{
    public class LoginException : BrazilianAddressesException
    {
        public List<string> ErrorMessages { get; set; }

        public LoginException(string errorMessage) => ErrorMessages = new List<string>() { errorMessage };
    }
}
