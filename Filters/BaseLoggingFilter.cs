using System;
using System.Collections.Generic;
using UserManagementRazorViews.Interfaces;

namespace UserManagementRazorViews.Filters
{
    public abstract class BaseLoggingFilter
    {
        private readonly ILogHandler _logHandler;
        private readonly string _dateTimeFormat;

        protected BaseLoggingFilter(ILogHandler logHandler)
        {
            _logHandler = logHandler;
            _dateTimeFormat = "dd-MM-yy HH:mm:ss";
        }

        protected IList<string> LogRecords => _logHandler.GetAllLogRecords();

        protected void Log(string info)
        {
            _logHandler.AddRecordToLog($"{DateTime.Now.ToString(_dateTimeFormat)} - {info}");
        }
    }
}
