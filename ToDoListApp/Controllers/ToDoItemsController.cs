using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoListApp.Models;
using ToDoListApp.Services;

namespace ToDoListApp.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly ToDoDbContext _context;
        private readonly UserManager<User> _userManager;

        public ToDoItemsController(ToDoDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {

            // Fetch the list of to-do items from the database
            var todoItems = _context.ToDoItems.ToList();

            // Pass the list of items to the view
            return View(todoItems);
            //return View();
        }


        // POST: ToDoItems/CreateTodo
        [HttpPost("api/CreateTodo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTodo([FromBody] TodoItem todoModel) // Use [FromBody] to bind JSON
        {
            if (todoModel == null)
            {
                return BadRequest(new { error = "Received data is null." });
            }

            if (string.IsNullOrEmpty(todoModel.UserId))
            {
                return BadRequest(new { error = "User email is required." });
            } // Retrieve the user based on the email provided in the todoModel
                var user = await _userManager.FindByEmailAsync(todoModel.UserId);

            if (user == null)
            {
                return BadRequest(new { error = "User not found." });
            }
            todoModel.UserId = user.Id;
            // Validate the model


            // Add the item and save changes
          //  todoModel.UserId = _userManager.GetUserId(User);
            _context.ToDoItems.Add(todoModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "To-do item created successfully!" });

        }

        // PUT: api/EditById/5
        [HttpPut("api/EditById/{id}")]
        public async Task<IActionResult> EditById(int id, [FromBody] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }

            try
            {
               // var user = await _userManager.FindByEmailAsync(todoItem.UserId);

                var toDoItem = await _context.ToDoItems
                    .FirstOrDefaultAsync(m => m.Id == todoItem.Id);
                if (toDoItem != null)
                {
                    toDoItem.Title = todoItem.Title;
                    toDoItem.Description = todoItem.Description;
                    toDoItem.IsCompleted = todoItem.IsCompleted;
                    toDoItem.CreatedAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "To-do item updated successfully!" }); // Return success response

                }


                // _context.Update(toDoItem);

                //todoItem.Title = user.
                //itemId.IsCompleted = todoItem.IsCompleted;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemExists(todoItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return BadRequest(new { error = "Invalid input data." }); // Return error response
        }

        // GET: ToDoItems/Edit/5
        [HttpGet("ToDoItems/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var todoItem = _context.ToDoItems.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }



        // DELETE: ToDoItems/Delete/{id}
        [HttpDelete("api/DeleteTodo/{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todoItem = await _context.ToDoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound(new { error = "To-do item not found." });
            }

            _context.ToDoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "To-do item deleted successfully!" });
        }


        private bool ToDoItemExists(int id)
        {
            return _context.ToDoItems.Any(e => e.Id == id);
        }

    }
}
