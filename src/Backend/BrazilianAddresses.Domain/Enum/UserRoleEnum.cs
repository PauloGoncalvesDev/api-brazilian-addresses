using System.ComponentModel;

namespace BrazilianAddresses.Domain.Enum
{
    public enum UserRoleEnum
    {
        [Description("Administrador")]
        Admin = 0,
        [Description("Cliente")]
        Client = 1
    }
}
