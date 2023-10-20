using BrazilianAddresses.Domain.Enum;

namespace BrazilianAddresses.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public UserRoleEnum UserRole { get; set; }
    }
}
