using Microsoft.AspNetCore.Mvc.Filters;
using UserManagementRazorViews.Interfaces;

namespace UserManagementRazorViews.Filters
{
    public class LoggingResultFilter : BaseLoggingFilter, IResultFilter
    {
        public LoggingResultFilter(ILogHandler logHandler) : base(logHandler)
        {
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Log("OnResultExecuted done");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Log("OnResultExecuting done");
        }
    }
}