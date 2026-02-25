namespace LaundryBooking.Core.Models
{
    public class HousingCooperative
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<string> ApartmentNumbers { get; set; } = new();
    }
}
