using Microsoft.EntityFrameworkCore;

namespace gymappyt.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Models.UserModel> Members { get; set; }
    public DbSet<Models.InstructorModel> Instructors { get; set; }
    // Define DbSets for your entities here
    // public DbSet<YourEntity> YourEntities { get; set; }
}