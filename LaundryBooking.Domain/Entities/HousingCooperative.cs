namespace LaundryBooking.Domain.Entities
{
    public class HousingCooperative
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public List<string> ApartmentNumbers { get; set; } = new();
    }
}
