using form_builder.DataAccess;
using form_builder.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
namespace form_builder.Services
{
public class UserService
{
    private readonly Database _context;

    public UserService(Database context)
    {
        _context = context;
    }

    public async Task<UserModel> GetUser(string username, string password)
    {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        return user;
    }
        public UserModel GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

    }
}
