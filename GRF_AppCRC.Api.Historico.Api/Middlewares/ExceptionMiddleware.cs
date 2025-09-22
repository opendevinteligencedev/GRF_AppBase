using GRF_AppCRC.Api.Historico.Domain.Interfaces;
using System.Net;

namespace GRF_AppCRC.Api.Historico.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.Error("Erro não tratado capturado pelo middleware", ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                message = "Ocorreu um erro inesperado",
                details = ex.Message
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
