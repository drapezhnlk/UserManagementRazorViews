using Microsoft.AspNetCore.Mvc.Filters;
using UserManagementRazorViews.Interfaces;

namespace UserManagementRazorViews.Filters
{
    public class LoggingExceptionFilter : BaseLoggingFilter, IExceptionFilter
    {
        public LoggingExceptionFilter(ILogHandler logHandler) : base(logHandler)
        {
        }
        
        public void OnException(ExceptionContext context)
        {
            Log("OnException done");
        }
    }
}
