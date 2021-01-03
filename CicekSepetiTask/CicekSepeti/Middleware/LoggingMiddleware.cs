using CicekSepeti.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;

        public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            this._loggerFactory = loggerFactory;

        }

        public async Task Invoke(HttpContext context)
        {
            var logger = this._loggerFactory.CreateLogger<LoggingMiddleware>();
            using (logger.BeginScope<LoggingMiddleware>(this))
            {
                var log = new Log
                {
                    Path = context.Request.Path,
                    Method = context.Request.Method,
                    QueryString = context.Request.QueryString.ToString()
                };

             
                if (context.Request.Method == "POST")
                {
                    context.Request.EnableBuffering();
                    var body = await new StreamReader(context.Request.Body)
                                                        .ReadToEndAsync();
                    context.Request.Body.Position = 0;
                    log.Payload = body;
                }

                log.RequestedOn = DateTime.Now;
                logger.LogInformation("Before request");
                await this._next.Invoke(context);
                logger.LogInformation("After request");

                using (Stream originalRequest = context.Response.Body)
                {
                    try
                    {
                        using (var memStream = new MemoryStream())
                        {
                            context.Response.Body = memStream;
                        
                            memStream.Position = 0;
                           
                            var response = await new StreamReader(memStream)
                                                                    .ReadToEndAsync();
                          
                            log.Response = response;
                            log.ResponseCode = context.Response.StatusCode.ToString();
                            log.IsSuccessStatusCode = (
                                  context.Response.StatusCode == 200 ||
                                  context.Response.StatusCode == 201);
                            log.RespondedOn = DateTime.Now;
                            
                            memStream.Position = 0;

                            await memStream.CopyToAsync(originalRequest);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    finally
                    {                       
                        context.Response.Body = originalRequest;
                    }
                }
            }
        }
    }    
}
