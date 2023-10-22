namespace BrazilianAddresses.Communication.Responses
{
    public class ErrorBaseResponseJson
    {
        public ErrorBaseResponseJson(List<string> errorMessages, bool success)
        {
            Messages = errorMessages;
            Success = success;
        }

        public ErrorBaseResponseJson(string errorMessages, bool success)
        {
            Messages = new List<string> { errorMessages };
            Success = success;
        }

        public List<string> Messages { get; set; }

        public bool Success { get; set; }
    }
}
