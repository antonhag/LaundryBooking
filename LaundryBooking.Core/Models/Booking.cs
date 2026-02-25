using LaundryBooking.Core.Enums;

namespace LaundryBooking.Core.Models
{
    public class Booking
    {
        public string Id { get; set; } = string.Empty;
        public string HousingCooperativeId { get; set; } = string.Empty;
        public string ApartmentNumber { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
