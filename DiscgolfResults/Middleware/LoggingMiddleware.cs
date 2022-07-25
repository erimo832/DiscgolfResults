using Results.Domain.Common.Extensions;

namespace DiscgolfResults.Filters
{

    public class RequestLog
    {
        public string Ip { get; set; } = "";
        public string Path { get; set; } = "";
        public double DurationMs { get; set; }
    }

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

            var log = new RequestLog
            {
                 Ip = context?.Connection?.RemoteIpAddress?.ToString() ?? "",
                 Path = context?.Request?.Path ?? "",
                 DurationMs = DateTime.Now.Subtract(start).TotalMilliseconds
            };

            _logger.LogInformation(log.ToJson());
        }
    }
}
