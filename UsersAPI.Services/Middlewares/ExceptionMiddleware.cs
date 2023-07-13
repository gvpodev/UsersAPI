using System.Net;
using UsersAPI.Api.Models;

namespace UsersAPI.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate? _requestDelegate;

        public ExceptionMiddleware(RequestDelegate? requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (ApplicationException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = exception switch
            {
                ApplicationException => (int)HttpStatusCode.BadRequest,
                not null => (int)HttpStatusCode.InternalServerError,
                _ => context.Response.StatusCode
            };

            context.Response.ContentType = "application/json";

            var errorResultModel = new ErrorResultModel
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            };

            await context.Response.WriteAsync(errorResultModel.ToString());
        }
    }
}
