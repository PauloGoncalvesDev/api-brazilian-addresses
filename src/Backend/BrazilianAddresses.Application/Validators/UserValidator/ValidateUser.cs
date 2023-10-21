using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Domain.Enum;
using BrazilianAddresses.Exceptions.ResourcesMessage;
using FluentValidation;

namespace BrazilianAddresses.Application.Validators.UserValidator
{
    public class ValidateUser : AbstractValidator<UserRequestJson>
    {
        public ValidateUser()
        {
            ValidateEmail();

            ValidatePassword();

            ValidateUserRole();
        }

        private void ValidateEmail()
        {
            RuleFor(r => r.Email).NotEmpty().WithMessage(string.Format(APIMSG.EMPTY_EMAIL));
            When(r => !string.IsNullOrWhiteSpace(r.Email), () =>
            {
                RuleFor(r => r.Email).EmailAddress().WithMessage(APIMSG.INVALID_EMAIL);
            });
        }

        private void ValidatePassword()
        {
            RuleFor(r => r.Password).SetValidator(new ValidatePasswordDefault());
        }

        private void ValidateUserRole()
        {
            RuleFor(r => r.UserRole).NotEmpty().WithMessage(APIMSG.EMPTY_USERROLE)
                .Must(userRole => ExistsUserRole(userRole?.Replace(" ", string.Empty))).WithMessage(APIMSG.INVALID_USERROLE); ;
            
        }

        private bool ExistsUserRole(string userRole)
        {
            return Enum.TryParse(userRole, out UserRoleEnum userRoleConverted) && Enum.IsDefined(typeof(UserRoleEnum), userRoleConverted);
        }
    }
}
