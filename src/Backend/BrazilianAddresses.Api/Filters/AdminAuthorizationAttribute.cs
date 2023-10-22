using BrazilianAddresses.Application.Services.Token;
using BrazilianAddresses.Communication.Responses;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories.UserRepository;
using BrazilianAddresses.Exceptions.ExceptionsBase;
using BrazilianAddresses.Exceptions.ResourcesMessage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace BrazilianAddresses.Api.Filters
{
    public class AdminAuthorizationAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly TokenController _tokenController;

        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        private readonly string _rolesAdmin = "Admin";

        public AdminAuthorizationAttribute(TokenController tokenController, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _tokenController = tokenController;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                string token = GetTokenFromRequest(context);

                string email = _tokenController.GetEmailFromTokenJwt(token);

                User user = await _userReadOnlyRepository.GetUserByEmail(email);

                if (user == null)
                    throw new AuthorizationException();

                string role = _tokenController.GetRoleFromTokenJwt(token);

                if (Enum.GetName(user.Role) != role || Enum.GetName(user.Role) != _rolesAdmin)
                    throw new AuthorizationException();
            }
            catch (SecurityTokenExpiredException)
            {
                ReturnsExceptionTokenExpired(context);
            }
            catch
            {
                ReturnsExceptionUserWithoutAdminAuthorization(context);
            }
        }

        private static string GetTokenFromRequest(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token))
                throw new AuthorizationException();

            return token["Bearer".Length..].Trim();
        }

        private static void ReturnsExceptionTokenExpired(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorBaseResponseJson(APIMSG.TOKEN_EXPIRED, false));
        }

        private static void ReturnsExceptionUserWithoutAdminAuthorization(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorBaseResponseJson(APIMSG.USER_UNAUTHORIZED, false));
        }
    }
}
