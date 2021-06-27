using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Exceptions;
using Core.Wrappers;

namespace RestAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };

                switch (error)
                {
                    case ApiException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    
                    case HttpRequestException e:
                        if (e.StatusCode == HttpStatusCode.NotFound)
                            response.StatusCode = (int) e.StatusCode;
                        else if (e.StatusCode == HttpStatusCode.BadRequest)
                            response.StatusCode = (int) e.StatusCode;
                        else
                            response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
