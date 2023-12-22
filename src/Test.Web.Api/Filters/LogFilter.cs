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

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogDebug(string.Format("Action Method {0} executing at {1}", context.ActionDescriptor.DisplayName, DateTime.Now.ToString()), "Log Action Filter Logs");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogDebug(string.Format("Action Method {0} executed at {1}",  context.ActionDescriptor.DisplayName, DateTime.Now.ToString()), "Log Action Filter Logs");
        }
    }
}
