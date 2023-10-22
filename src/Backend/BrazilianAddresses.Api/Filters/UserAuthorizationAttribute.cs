using BrazilianAddresses.Application.Services.Token;
using BrazilianAddresses.Domain.Repositories.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BrazilianAddresses.Api.Filters
{
    public class UserAuthorizationAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly TokenController _tokenController;

        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public UserAuthorizationAttribute(TokenController tokenController, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _tokenController = tokenController;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
