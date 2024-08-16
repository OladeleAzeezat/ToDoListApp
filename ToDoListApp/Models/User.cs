﻿using Microsoft.AspNetCore.Identity;

namespace ToDoListApp.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Address { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
