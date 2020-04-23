using System.Collections.Generic;

namespace UserManagementRazorViews.Interfaces
{
    public interface ILogHandler
    {
        void AddRecordToLog(string record);
        IList<string> GetAllLogRecords();
    }
}
