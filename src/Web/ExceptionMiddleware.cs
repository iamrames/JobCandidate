namespace JobCandidate.Web;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
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
        catch (Exception exception)
        {
            var (status, key) = exception switch
            {
                ArgumentException argEx => (StatusCodes.Status400BadRequest, argEx.ParamName ?? "Argument Exception"),
                InvalidOperationException => (StatusCodes.Status400BadRequest, "Invalid operation"),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
            };

            if (status == StatusCodes.Status500InternalServerError)
            {
                _logger.LogError(exception, exception.Message);
            }

            var message = exception.Message;
            if (exception.InnerException != null)
            {
                message += $" | {exception.InnerException.Message}";
            }

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = status;
            await response.WriteAsJsonAsync(new
            {
                Errors = new Dictionary<string, string[]>()
                {
                    { key, new[] { message } }
                }
            });
        }
    }
}
