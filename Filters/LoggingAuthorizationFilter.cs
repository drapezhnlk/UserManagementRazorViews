using Microsoft.AspNetCore.Mvc.Filters;
using UserManagementRazorViews.Interfaces;

namespace UserManagementRazorViews.Filters
{
    public class LoggingAuthorizationFilter : BaseLoggingFilter,  IAuthorizationFilter
    {   
        public LoggingAuthorizationFilter(ILogHandler logHandler) : base(logHandler) {}

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Log("OnAuthorization done");
        }
    }
}
