

using gymappyt.Data;
using gymappyt.Models;
using Microsoft.EntityFrameworkCore;

namespace gymappyt.Services
{
    public class GymClassService
    {
        private readonly AppDbContext _db;

        public GymClassService(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        // add a class
        public async Task<bool> AddClassAsync(GymClassModel gymClass)
        {
            _db.Set<GymClassModel>().Add(gymClass);
            return await _db.SaveChangesAsync() > 0;
        }

        // get all classes
        public async Task<List<GymClassModel>> GetClassesAsync(bool includeInstructor = false)
        {
            if (includeInstructor)
            {
                return await _db.Set<GymClassModel>()
                    .Include(c => c.Instructor)
                    .ToListAsync();
            }
            return await _db.Set<GymClassModel>().ToListAsync();
        }

        // get by id
        public async Task<GymClassModel?> GetClassByIdAsync(int id)
        {
            return await _db.Set<GymClassModel>()
            .Include(c => c.Instructor)
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        // update a class
        public async Task<bool> UpdateClassAsync(GymClassModel gymClass)
        {
            var existing = await _db.Set<GymClassModel>().FindAsync(gymClass.Id);
            if (existing == null)
                return false;

            // Update properties
            existing.Name = gymClass.Name;
            existing.Description = gymClass.Description;
            existing.Type = gymClass.Type;
            existing.Level = gymClass.Level;
            existing.Membership = gymClass.Membership;
            existing.InstructorId = gymClass.InstructorId;
            existing.StartTime = gymClass.StartTime;
            existing.EndTime = gymClass.EndTime;
            existing.MaxCapacity = gymClass.MaxCapacity;
            existing.IsActive = gymClass.IsActive;

            await _db.SaveChangesAsync();
            return true;
        }

        // delete a class
        public async Task<bool> DeleteClassAsync(GymClassModel gymClass)
        {
            _db.Set<GymClassModel>().Remove(gymClass);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}