using BrazilianAddresses.Application.Services.Token;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories.UserRepository;
using Microsoft.AspNetCore.Http;

namespace BrazilianAddresses.Application.Services.LoggedUser
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly TokenController _tokenController;

        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public LoggedUser(IHttpContextAccessor httpContextAccessor, TokenController tokenController, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenController = tokenController;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task<User> GetLoggedUser()
        {
            string authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            string token = authorization["Bearer".Length..].Trim();

            string emailToken = _tokenController.GetEmailFromTokenJwt(token);

            User user = await _userReadOnlyRepository.GetUserByEmail(emailToken);

            return user;
        }
    }
}
