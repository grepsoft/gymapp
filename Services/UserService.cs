

using gymappyt.Data;
using gymappyt.Models;
using Microsoft.EntityFrameworkCore;

namespace gymappyt.Services;

public class UserService
{
    private readonly AppDbContext _db;
    public UserService(AppDbContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<UserModel> AddUserAsync(UserModel user)
    {
        _db.Members.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    // check if user exists by checking email asynchronously
    public async Task<bool> UserExistsAsync(string email)
    {
        return await _db.Members.AnyAsync(u => u.Email == email);
    }

    public async Task<UserModel> UpdateUserAsync(UserModel user)
    {
        // check if the user exists
        var existingUser = await _db.Members.FindAsync(user.Id);
        if (existingUser == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;

        await _db.SaveChangesAsync();
        return user;
    } 

    // method to upate a user membership plan
    public async Task<UserModel> UpdateUserMembershipAsync(int userId, MembershipType newMembershipPlan)
    {
        var user = await _db.Members.FindAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        user.MembershipPlan = newMembershipPlan;
        _db.Members.Update(user);
        await _db.SaveChangesAsync();
        return user;
    }
}
