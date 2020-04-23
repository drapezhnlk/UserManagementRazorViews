using System;
using System.Collections.Generic;

namespace UserManagementRazorViews.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Company Company { get; set; }
        public IList<UserTitle> UsersTitles { get; set; }
        public string Photo { get; set; }
    }
}