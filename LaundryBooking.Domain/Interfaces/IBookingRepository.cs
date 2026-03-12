using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetBookingsByDateAsync(DateOnly date, string housingCooperativeId);
        Task<List<Booking>> GetBookingsByApartmentAsync(string apartmentNumber);
        Task CreateBookingAsync(Booking booking);
        Task DeleteBookingAsync(string id);
    }
}
