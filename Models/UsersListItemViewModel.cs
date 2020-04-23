using System;
using System.Collections.Generic;

namespace UserManagementRazorViews.Models
{
    public class UsersListItemViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public IList<string> UserTitles { get; set; }
        public DateTime BirthDate { get; set; }
        public string CompanyName { get; set; }
        public string Photo { get; set; }
    }
}