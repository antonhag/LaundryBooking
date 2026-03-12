using LaundryBooking.Domain.Entities;
using LaundryBooking.Domain.Enums;

namespace LaundryBooking.Application.Interfaces;

public interface IBookingFacade
{
    Task<List<TimeSlot>> GetAvailableTimeSlotsAsync(DateOnly date, string housingCooperativeId);
    Task<bool> CreateBookingAsync(Booking booking);
}