using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
  public class ExceptionMiddleware
  {
    public readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    public readonly IHostEnvironment _environment;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
    {
      _environment = environment;
      _logger = logger;
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
         await _next(context);
        }
        catch(Exception exception){
          _logger.LogError(exception, exception.Message);
          context.Response.ContentType = "application/json";
          context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

          var response = _environment.IsDevelopment() ? new ApiException((int)HttpStatusCode.InternalServerError, exception.Message, exception.StackTrace.ToString())
          : new ApiException((int)HttpStatusCode.InternalServerError);

          
          var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
          var json = JsonSerializer.Serialize(response, options);
          await context.Response.WriteAsync(json);

        }
    }
  }
}