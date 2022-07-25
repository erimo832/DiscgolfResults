namespace DiscgolfResults.Filters
{

    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger) 
        {
            _next = next;
            _logger = logger;          
        }

        public async Task Invoke(HttpContext context)
        {
            var start = DateTime.Now;
            
            // Call the next delegate/middleware in the pipeline.
            await _next(context);
            var log = $"Request from: {context?.Connection?.RemoteIpAddress?.ToString() ?? ""} to path: {context?.Request?.Path ?? ""} Duration: {DateTime.Now.Subtract(start)}";

            _logger.LogInformation(log);
        }
    }
}
