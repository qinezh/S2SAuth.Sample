using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace S2SAuth.Sample.Service
{
    internal class AuthorizeExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ArgumentException ae:
                    context.Result = new BadRequestObjectResult(ToError(ae));
                    break;
                case Exception e:
                    context.Result = new ObjectResult(ToError(e))
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                    break;
                default:
                    break;
            }
        }

        public static object ToError(Exception exception)
           => new
           {
               Message = exception.InnerException == null ? exception.Message : exception.InnerException.Message
           };
    }
}