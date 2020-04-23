namespace UserManagementRazorViews.Entities
{
    public class UserTitle
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int TitleId { get; set; }
        public Title Title { get; set; }
    }
}