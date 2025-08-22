public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path;

        // Allow unauthenticated access to Swagger and root
        if (path.StartsWithSegments("/swagger") || path.StartsWithSegments("/"))
        {
            await _next(context);
            return;
        }

        // Example: Check for a custom header
        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: API key is missing.");
            return;
        }

        var validApiKey = "your-secret-key";
        if (!string.Equals(extractedApiKey, validApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Invalid API key.");
            return;
        }

        await _next(context);
    }
}

