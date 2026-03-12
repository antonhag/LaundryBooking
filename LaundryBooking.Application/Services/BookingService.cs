using LaundryBooking.Application.Interfaces;
using LaundryBooking.Domain.Entities;
using LaundryBooking.Domain.Interfaces;

namespace LaundryBooking.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<Booking>> GetBookingsByDateAsync(DateOnly date, string housingCooperativeId)
        {
            var bookingRepository = await _bookingRepository.GetBookingsByDateAsync(date, housingCooperativeId);
            return bookingRepository;
        }

        public async Task<List<Booking>> GetBookingsByApartmentAsync(string apartmentNumber)
        {
            var bookings =  await _bookingRepository.GetBookingsByApartmentAsync(apartmentNumber);
            return bookings;
        }

        public async Task<bool> CreateBookingAsync(Booking booking)
        {
            var existingBookings = await _bookingRepository.GetBookingsByDateAsync(booking.Date, booking.HousingCooperativeId);

            bool slotTaken = existingBookings.Any(b => b.TimeSlot == booking.TimeSlot);
            if (slotTaken)
            {
                return false;
            }
            
            bool apartmentAlreadyBooked = existingBookings.Any(b => b.ApartmentNumber == booking.ApartmentNumber);
            if (apartmentAlreadyBooked)
            {
                return false;
            }

            await _bookingRepository.CreateBookingAsync(booking);
            return true;
        }

        public async Task DeleteBookingAsync(string id)
        {
            await _bookingRepository.DeleteBookingAsync(id);
        }
    }
}
