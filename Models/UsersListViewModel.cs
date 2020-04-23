using System.Collections.Generic;

namespace UserManagementRazorViews.Models
{
    public class UsersListViewModel : BaseViewModel
    {
        public IEnumerable<UsersListItemViewModel> UserRecords { get; set; }
    }
}
