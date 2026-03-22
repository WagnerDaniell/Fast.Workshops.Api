using Fast.Workshops.Domain.Exceptions;

namespace Fast.Workshops.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        } 

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    title = "Erro de validação",
                    status = 400,
                    detail = string.Join(" | ", ex.Errors)
                });
            }

            catch (UnauthorizedException ex)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                    title = "Não autorizado",
                    status = 401,
                    detail = ex.Message
                });
            }

            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    title = "Não encontrado",
                    status = 404,
                    detail = ex.Message
                });
            }

            catch (ConflictException ex)
            {
                context.Response.StatusCode = 409;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.8", 
                    title = "Conflito",
                    status = 409,
                    detail = ex.Message
                });
            }

            catch (Exception)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    title = "Erro interno",
                    status = 500,
                    detail = "Ocorreu um erro inesperado no servidor."
                });
            }
        }
    }
}
