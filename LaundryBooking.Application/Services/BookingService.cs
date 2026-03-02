using LaundryBooking.Domain.Entities;
using LaundryBooking.Domain.Interfaces;

namespace LaundryBooking.Application.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<Booking>> GetBookingsByDateAsync(DateOnly date)
        {
            var bookingRepository =  await _bookingRepository.GetBookingsByDateAsync(date);
            return bookingRepository;
        }

        public async Task<List<Booking>> GetBookingsByApartmentAsync(string apartmentNumber)
        {
            var bookings =  await _bookingRepository.GetBookingsByApartmentAsync(apartmentNumber);
            return bookings;
        }

        public async Task<bool> CreateBookingAsync(Booking booking)
        {
            var existingBookings = await _bookingRepository.GetBookingsByDateAsync(booking.Date);

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
