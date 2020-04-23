using Microsoft.AspNetCore.Mvc.Filters;
using UserManagementRazorViews.Interfaces;

namespace UserManagementRazorViews.Filters
{
    public class LoggingResourceFilter : BaseLoggingFilter, IResourceFilter
    {
        public LoggingResourceFilter(ILogHandler logHandler) : base(logHandler) {}

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Log("OnResourceExecuted done");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Log("OnResourceExecuting done");
        }
    }
}
