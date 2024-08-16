namespace ToDoListApp.Models
{
    public class Register
    {
        public string FirstName { get; set; }
        public required string LastName { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
