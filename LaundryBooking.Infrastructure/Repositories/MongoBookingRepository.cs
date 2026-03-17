using LaundryBooking.Infrastructure.Data;
using LaundryBooking.Domain.Interfaces;
using LaundryBooking.Domain.Entities;
using MongoDB.Driver;

namespace LaundryBooking.Infrastructure.Repositories
{
    public class MongoBookingRepository : IBookingRepository 
    {
        private readonly IMongoCollection<Booking> _bookings; // Kan bara bli initierad en gång, i konstruktorn

        public MongoBookingRepository(MongoDbContext context)
        {
            _bookings = context.Bookings;
        }

        public async Task<List<Booking>> GetBookingsByDateAsync(DateOnly date, string housingCooperativeId)
        {
            var booking = await _bookings.Find(b => b.Date == date && b.HousingCooperativeId == housingCooperativeId).ToListAsync();
            return booking;
        }

        public async Task<List<Booking>> GetBookingsByApartmentAsync(string apartmentNumber)
        {
            var bookings = await _bookings.Find(b => b.ApartmentNumber == apartmentNumber).ToListAsync();
            return bookings;
        }

        public async Task CreateBookingAsync(Booking booking)
        {
            await _bookings.InsertOneAsync(booking);
        }

        public async Task UpdateCalendarEventIdAsync(string id, string calendarEventId)
        {
            var update = Builders<Booking>.Update.Set(b => b.CalendarEventId, calendarEventId);
            await _bookings.UpdateOneAsync(b => b.Id == id, update);
        }

        public async Task DeleteBookingAsync(string id)
        {
            await _bookings.DeleteOneAsync(b => b.Id == id);
        }
    }
}
