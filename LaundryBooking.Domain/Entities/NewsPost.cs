namespace LaundryBooking.Domain.Entities;

public class NewsPost
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string HousingCooperativeId { get; set; } = string.Empty;  
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}