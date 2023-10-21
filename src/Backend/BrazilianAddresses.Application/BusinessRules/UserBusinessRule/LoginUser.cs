using BrazilianAddresses.Application.BusinessRules.UserBusinessRule.Interfaces;
using BrazilianAddresses.Application.Services.Cryptography;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories.UserRepository;
using BrazilianAddresses.Exceptions.ExceptionsBase;

namespace BrazilianAddresses.Application.BusinessRules.UserBusinessRule
{
    public class LoginUser : ILoginUser
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        private readonly PasswordEncryption _passwordEncryption;

        public LoginUser(IUserReadOnlyRepository userReadOnlyRepository, PasswordEncryption passwordEncryption)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _passwordEncryption = passwordEncryption;
        }

        public async Task<UserLoginResponseJson> Execute(UserLoginRequestJson userLoginRequest)
        {
            User user = await _userReadOnlyRepository.GetUserByEmail(userLoginRequest.Email);

            if (user == null)
                throw new LoginException();

            User loginUser = await _userReadOnlyRepository.GetUserLogin(user.Email, _passwordEncryption.Encrypt(user.Password, user.Salt));

            if (loginUser == null)
                throw new LoginException();

            return new UserLoginResponseJson
            {

            };
        }
    }
}
