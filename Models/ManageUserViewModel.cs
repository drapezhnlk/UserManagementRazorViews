using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UserManagementRazorViews.Models
{
    public class ManageUserViewModel : BaseViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public IList<string> UserTitles { get; set; }
        public DateTime UserBirthDate { get; set; }
        public SelectList AllCompanies { get; set; }
        public IFormFile Photo { get; set; }
        public int SelectedCompanyId { get; set; }
        public bool IsCreatePage { get; set; }
    }
}