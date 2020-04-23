using System.Collections.Generic;

namespace UserManagementRazorViews.Entities
{
    public class Title : Entity
    {
        public string Name { get; set; }
        public IList<UserTitle> UsersTitles {get; set; }
    }
}