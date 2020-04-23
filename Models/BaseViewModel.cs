using System.Collections.Generic;

namespace UserManagementRazorViews.Models
{
    public abstract class BaseViewModel
    {
        public IEnumerable<string> Log { get; set; }
    }
}
