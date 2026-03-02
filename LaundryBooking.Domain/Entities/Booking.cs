using LaundryBooking.Domain.Enums;

namespace LaundryBooking.Domain.Entities
{
    public class Booking
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string HousingCooperativeId { get; set; } = string.Empty;
        public string ApartmentNumber { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
