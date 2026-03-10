using LaundryBooking.Domain.Enums;

namespace LaundryBooking.Domain.Entities
{
    public class IssueReport
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string HousingCooperativeId { get; set; } = string.Empty;
        public string ApartmentNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IssueStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
