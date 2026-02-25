using LaundryBooking.Core.Models;

namespace LaundryBooking.Core.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetBookingsByDateAsync(DateOnly date);
        Task<List<Booking>> GetBookingsByApartmentAsync(string apartmentNumber);
        Task CreateBookingAsync(Booking booking);
        Task DeleteBookingAsync(string id);
    }
}
