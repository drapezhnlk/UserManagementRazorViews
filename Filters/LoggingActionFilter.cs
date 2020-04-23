using Microsoft.AspNetCore.Mvc.Filters;
using UserManagementRazorViews.Interfaces;

namespace UserManagementRazorViews.Filters
{
    public class LoggingActionFilter : BaseLoggingFilter, IActionFilter
    {
        public LoggingActionFilter(ILogHandler logHandler) : base(logHandler) {}

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Log("OnActionExecuted done");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Log("OnActionExecuting done");
        }
    }
}
