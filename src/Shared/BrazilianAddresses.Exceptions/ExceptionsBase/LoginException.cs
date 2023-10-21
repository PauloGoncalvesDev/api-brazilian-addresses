using BrazilianAddresses.Exceptions.ResourcesMessage;

namespace BrazilianAddresses.Exceptions.ExceptionsBase
{
    public class LoginException : BrazilianAddressesException
    {
        public LoginException() : base(APIMSG.LOGIN_ERROR) { }
    }
}
