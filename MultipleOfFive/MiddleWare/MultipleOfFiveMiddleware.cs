using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MultipleOfFive.Application.Exception;
using MultipleOfFive.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MultipleOfFive.MiddleWare
{
    public class MultipleOfFiveMiddleware
    {
        private readonly RequestDelegate _next;

        public MultipleOfFiveMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;
                    string[] errorArray = ((ValidationException)validationException).Failures.SelectMany(x => x.Value).ToArray();
                    var error = string.Join(";", errorArray);
                    result = JsonConvert.SerializeObject(Result.Fail(error));
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(Result.Fail(exception.Message));
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MultipleOfFiveMiddleware>();
        }
    }
}
