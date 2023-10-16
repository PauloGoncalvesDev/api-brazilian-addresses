namespace BrazilianAddresses.Domain.Entitys
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public int Role { get; set; }
    }
}
