

using gymappyt.Data;
using gymappyt.Models;
using Microsoft.EntityFrameworkCore;

namespace gymappyt.Services
{
    public class InstructorService
    {
        private readonly AppDbContext _db;

        public InstructorService(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<bool> AddInstructorAsync(InstructorModel instructor)
        {
            if (instructor == null || string.IsNullOrWhiteSpace(instructor.FirstName) || string.IsNullOrWhiteSpace(instructor.Email))
            {
                return false;
            }

            _db.Instructors.Add(instructor);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<InstructorModel>> GetInstructorsAsync()
        {
            return await _db.Instructors.ToListAsync();
        }

        // get by instructor id
        public async Task<InstructorModel?> GetInstructorByIdAsync(int id)
        {
            return await _db.Instructors.FirstOrDefaultAsync(i => i.Id == id);
        }

        // update instructor
        public async Task<bool> UpdateInstructorAsync(InstructorModel instructor)
        {
            if (instructor == null || string.IsNullOrWhiteSpace(instructor.FirstName) || string.IsNullOrWhiteSpace(instructor.Email))
            {
                return false;
            }

            var existing = await _db.Instructors.FindAsync(instructor.Id);
            if (existing == null)
                return false;

            // Update properties
            existing.FirstName = instructor.FirstName;
            existing.LastName = instructor.LastName;
            existing.Email = instructor.Email;
            existing.Phone = instructor.Phone;
            existing.Bio = instructor.Bio;
            existing.YearsOfExperience = instructor.YearsOfExperience;
            existing.IsActive = instructor.IsActive;

            await _db.SaveChangesAsync();
            return true;
        }

        // delete instructor
        public async Task<bool> DeleteInstructorAsync(int id)
        {
            var instructor = await _db.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return false;
            }

            _db.Instructors.Remove(instructor);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}