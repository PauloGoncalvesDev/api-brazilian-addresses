using BrazilianAddresses.Communication.Responses;
using BrazilianAddresses.Exceptions.ExceptionsBase;
using BrazilianAddresses.Exceptions.ResourcesMessage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BrazilianAddresses.Api.Filters
{
    public class ExceptionFilters : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
           if(context.Exception is BrazilianAddressesException)
                HandleBrazilianAddressesException(context);
            else
                HandleDefaultException(context);
        }

        private void HandleBrazilianAddressesException(ExceptionContext context)
        {
            if (context.Exception is ValidationException)
                HandleValidationException(context);
        }

        private void HandleValidationException(ExceptionContext context)
        {
            ValidationException validationsErrors = context.Exception as ValidationException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(new ErrorBaseResponseJson(validationsErrors.ErrorMessages, false));
        }

        private void HandleDefaultException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ErrorBaseResponseJson(APIMSG.UNKNOW_ERROR, false));
        }
    }
}
