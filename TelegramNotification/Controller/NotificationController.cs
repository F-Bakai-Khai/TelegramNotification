using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelegramNotification.DataBase;
using TelegramNotification.DTO;

namespace TelegramNotification.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] UserDto newUser)
        {
            var newUser_ = new DataBase.Model.User()
            {
                NickName = newUser.NickName,
                TelegramId = newUser.TelegramId,
                CreationDate = DateTime.Now
            };
            _context.Users.Add(newUser_);
            await _context.SaveChangesAsync();
            return Ok(newUser);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok($"User {id} deleted.");
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("notify")]
        public async Task<IActionResult> NotifyUsers([FromBody] string message)
        {
            var users = await _context.Users.ToListAsync();


            foreach (var user in users)
            {
                
                System.Console.WriteLine($"Notification sent to {user.NickName}: {message}");
            }

            return Ok("Notification sent to all users.");
        }
    }
}
