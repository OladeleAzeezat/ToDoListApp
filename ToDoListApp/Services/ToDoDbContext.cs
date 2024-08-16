using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public class ToDoDbContext : IdentityDbContext<User>
    {
        public ToDoDbContext(DbContextOptions options) : base(options) { }
        
            public DbSet<TodoItem> ToDoItems { get; set; }
    

    
    }
}
