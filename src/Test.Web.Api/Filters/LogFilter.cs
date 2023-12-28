using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using Test.Web.Api.Controllers;

namespace Test.Web.Api.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        private readonly ILogger<LogFilter> _logger;

        public LogFilter(ILogger<LogFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context) => _logger.LogDebug(
                $"Action Method {context.ActionDescriptor.DisplayName} executing at {DateTime.Now}", "Log Action Filter Logs");

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogDebug(
                $"Action Method {context.ActionDescriptor.DisplayName} executed at {DateTime.Now}", "Log Action Filter Logs");
        }
    }
}
