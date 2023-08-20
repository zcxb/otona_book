using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OtonaBookApi.Common;

namespace OtonaBookApi.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var response = new ResponseResult<object>
                {
                    Code = ResponseResultCode.ERROR,
                    Message = ResponseResultCode.ERROR.ToString(),
                    SubCode = "unknown_error",
                    SubMessage = ex.Message,
                };
                if (ex is BizException bizEx)
                {
                    response.SubCode = bizEx.SubCode;
                    response.SubMessage = bizEx.SubMessage;
                }
                httpContext.Response.StatusCode = StatusCodes.Status200OK;
                httpContext.Response.ContentType = "application/json;charset=utf-8";
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareClassTemplate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}

