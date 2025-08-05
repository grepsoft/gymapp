

using gymappyt.Data;
using gymappyt.Models;
using Microsoft.EntityFrameworkCore;

namespace gymappyt.Services
{
    public class BookingService
    {
        private readonly AppDbContext _db;
        public BookingService(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<ClassBookingModel> BookClassAsync(int userId, int classId)
        {
            var gymClass = await _db.Set<GymClassModel>().FindAsync(classId);
            if (gymClass == null)
            {
                throw new ArgumentException("Class not found", nameof(classId));
            }

            var user = await _db.Set<UserModel>().FindAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found", nameof(userId));
            }

            if (!CanUserBookClass(user.MembershipPlan, gymClass.Membership))
            {
                throw new InvalidOperationException($"Your {user.MembershipPlan} membership does not allow booking {gymClass.Membership} classes. Please upgrade your membership.");
            }

            if (gymClass.IsFull)
            {
                throw new InvalidOperationException("Class is full");
            }

            var existingBooking = await _db.Set<ClassBookingModel>().FirstOrDefaultAsync(b => b.UserId == userId && b.Status == BookingStatus.Confirmed);

            if (existingBooking != null)
            {
                throw new InvalidOperationException("Alread booked for this class");
            }

            var booking = new ClassBookingModel
            {
                UserId = user.Id,
                ClassId = gymClass.Id,
                Status = BookingStatus.Confirmed,
                BookedAt = DateTime.UtcNow
            };

            _db.Set<ClassBookingModel>().Add(booking);
            gymClass.CurrentBookings++;
            await _db.SaveChangesAsync();

            return booking;
        }

        public async Task CancelBookingAsync(int bookingId, string? reason = null)
        {
            var booking = await _db.Set<ClassBookingModel>()
            .Include(b => b.Class)
            .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found");
            }

            booking.Status = BookingStatus.Cancelled;
            booking.CancelledAt = DateTime.UtcNow;
            booking.CancellationReason = reason;

            booking.Class.CurrentBookings--;
            await _db.SaveChangesAsync();
        }

        public async Task<List<ClassBookingModel>> GetUserBookingsAsync(int userId)
        {
            return await _db.Set<ClassBookingModel>()
                .Include(b => b.Class)
                .ThenInclude(c => c.Instructor)
                .Where(b => b.UserId == userId && b.Status != BookingStatus.Cancelled)
                .OrderByDescending(b => b.BookedAt)
                .ToListAsync();
        }

        private bool CanUserBookClass(MembershipType userMembershipType, MembershipType classMembership)
        {
            return userMembershipType >= classMembership;
        }
    }
}