namespace BrazilianAddresses.Communication.Responses
{
    public class UserLoginResponseJson : BaseResponseJson
    {
        public string Name { get; set; }

        public string Token { get; set; }
    }
}
