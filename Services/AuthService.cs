

using gymappyt.Data;
using gymappyt.Models;
using Microsoft.EntityFrameworkCore;


namespace gymappyt.Services;
public class AuthService
{
    private static UserModel? _currentUser;
    private readonly AppDbContext _db;
    public AuthService(AppDbContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        var user = await _db.Members.FirstOrDefaultAsync(u => u.Email == email && u.password == password);

        if (user != null)
        {
            _currentUser = user;
            return true;
        }

        return false;
    }

    public Task LogoutAsync()
    {
        _currentUser = null;
        return Task.CompletedTask;
    }

    public Task<UserModel?> GetCurrentUserAsync() => Task.FromResult(_currentUser);
    
    public bool IsLoggedIn => _currentUser != null;
    public bool IsActive => _currentUser?.IsActive ?? false;
}