using Gmail.Domain.Data;
using Gmail.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Gmail.Domain.Repository.UserRepositorys;
public interface IUserRepository
{
    Task<User?> GetUserDetailsAsync(long userId);
    Task<List<User>> GetAllUsersAsync();
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
    Task<User?> GetUserByEmail(string email);
}

public class UserRepository : IUserRepository
{
    private readonly GmailContext _context;

    public UserRepository(GmailContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserDetailsAsync(long userId)
    {
        return await _context.Users
            .Include(x => x.Folders)
            .ThenInclude(x => x.Emails)
            .ThenInclude(x => x.Recipients)
            .Include(x => x.Contacts)
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users
            .Include(x => x.Folders)
            .ThenInclude(x => x.Emails)
            .ThenInclude(x => x.Recipients)
            .Include(x => x.Contacts)
            .ToListAsync();
    }
    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users
            .Include(x => x.Folders)
            .ThenInclude(x => x.Emails)
            .ThenInclude(x => x.Recipients)
            .Include(x => x.Contacts)
            .FirstOrDefaultAsync(x => x.EmailAddress == email);
    }


    public async Task CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
