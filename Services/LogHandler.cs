using System.Collections.Generic;
using System.Linq;
using UserManagementRazorViews.Interfaces;

namespace UserManagementRazorViews.Services
{
    public class LogHandler : ILogHandler
    {
        private readonly IList<string> _log;
        
        public LogHandler()
        {
            _log = new List<string>();
        }
        
        public void AddRecordToLog(string record)
        {
            _log.Add(record);
        }

        public IList<string> GetAllLogRecords()
        {
            return _log.ToList();
        }
    }
}
