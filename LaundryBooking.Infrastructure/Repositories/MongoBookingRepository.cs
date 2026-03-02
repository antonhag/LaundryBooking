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

        public async Task<List<Booking>> GetBookingsByDateAsync(DateOnly date)
        {
            var booking = await _bookings.Find(b => b.Date == date).ToListAsync();
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

        public async Task DeleteBookingAsync(string id)
        {
            await _bookings.DeleteOneAsync(b => b.Id == id);
        }
    }
}
