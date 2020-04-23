using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserManagementRazorViews.Interfaces;

namespace UserManagementRazorViews.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly ILogHandler _logHandler;
        
        protected BaseController(ILogHandler logHandler)
        {
            _logHandler = logHandler;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewData["Log"] = _logHandler.GetAllLogRecords();
        }
    }
}