using BrazilianAddresses.Application.BusinessRules.UserBusinessRule.Interfaces;
using BrazilianAddresses.Application.Services.Cryptography;
using BrazilianAddresses.Application.Services.Token;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories.UserRepository;
using BrazilianAddresses.Exceptions.ExceptionsBase;
using BrazilianAddresses.Exceptions.ResourcesMessage;

namespace BrazilianAddresses.Application.BusinessRules.UserBusinessRule
{
    public class LoginUser : ILoginUser
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        private readonly PasswordEncryption _passwordEncryption;

        private readonly TokenController _tokenController;

        public LoginUser(IUserReadOnlyRepository userReadOnlyRepository, PasswordEncryption passwordEncryption, TokenController tokenController)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _passwordEncryption = passwordEncryption;
            _tokenController = tokenController;
        }

        public async Task<UserLoginResponseJson> Execute(UserLoginRequestJson userLoginRequest)
        {
            User user = await _userReadOnlyRepository.GetUserByEmail(userLoginRequest.Email);

            if (user == null)
                throw new LoginException(APIMSG.LOGIN_ERROR);

            User loginUser = await _userReadOnlyRepository.GetUserLogin(user.Email, _passwordEncryption.Encrypt(user.Password, user.Salt));

            if (loginUser == null)
                throw new LoginException(APIMSG.LOGIN_ERROR);

            return new UserLoginResponseJson
            {
                Message = APIMSG.LOGIN_COMPLETED,
                Success = true,
                Token = _tokenController.GenerateTokenJwt(loginUser.Email)
            };
        }
    }
}
