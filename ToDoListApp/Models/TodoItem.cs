using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public virtual User user { get; set; }
    }
}
