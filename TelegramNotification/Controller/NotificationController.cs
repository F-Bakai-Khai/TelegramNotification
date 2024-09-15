using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelegramNotification.DataBase;
using TelegramNotification.DataBase.Model;
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
            var users = await _context.Users
                .Include(x => x.UserSettings)
                .ToListAsync();
            var result = users.Select(x => new UserResponseDto()
            {
                NickName = x.NickName,
                TelegramId = x.TelegramId,
                CreationDate = x.CreationDate,
                IsEnableNotification = x.UserSettings.IsEnableNotification
            });
            return Ok(result);
        }

        [HttpPost("notify")]
        public async Task<IActionResult> NotifyUsers([FromBody] string message)
        {
            var users = await _context.Users
                .Include(x => x.UserSettings)
                .Where(x => x.UserSettings.IsEnableNotification)
                .ToListAsync();
            
            foreach (var user in users)
            {
                System.Console.WriteLine($"Notification sent to {user.NickName}: {message}");
            }

            return Ok("Notification sent to all users.");
        }
    }
}