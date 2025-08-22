namespace UserManagementAPI.Models
{
    public class User
    {
        public required int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; } 
        public required string Role { get; set; }
    }
}
