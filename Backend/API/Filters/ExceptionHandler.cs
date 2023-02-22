using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using API.Common;

namespace API.Filters
{

    public class ExceptionHandler : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                var exception = context.Exception;
                var exceptionType = exception.GetType();
                var exceptionDetails = exception.ToString();
                HttpStatusCode status = HttpStatusCode.InternalServerError;
                var message = exceptionDetails.ToString();
                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    message = "Access to the web api is not authorized";
                    status = HttpStatusCode.Unauthorized;
                }
                else if (exceptionType == typeof(HttpRequestException))
                {
                    message = "Internal Server Error";
                    status = HttpStatusCode.InternalServerError;
                }
                var statusCode = Convert.ToInt32(status);
                var responseData = new
                {
                    ErrorCode = statusCode,
                    ErrorMsg = message
                };
                context.Result = new ObjectResult(responseData)
                {
                    StatusCode = statusCode
                };
                context.ExceptionHandled = true;
            }
        }
        public void OnException(ExceptionContext context)
        {
            var apiError = new ErrorResponse
            {
                StatusCode = 500,
                Phrase = "Internal Server Error",
                Timestamp = DateTime.Now
            };
            apiError.Errors.Add(context.Exception.Message);

            context.Result = new JsonResult(apiError) { StatusCode = 500 };
        }
    }
}
