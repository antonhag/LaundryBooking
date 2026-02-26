namespace LaundryBooking.Core.Services
{
    public class SessionService
    {
        public string ApartmentNumber { get; private set; } = string.Empty; // private set för att endast kunna ändra värdena genom SetSession
        public string HousingCooperativeId { get; private set; } = string.Empty;

        public void SetSession(string apartmentNumber, string housingCooperativeId)
        {
            ApartmentNumber = apartmentNumber;
            HousingCooperativeId = housingCooperativeId;
        }

        public void ClearSession()
        {
            ApartmentNumber = string.Empty;
            HousingCooperativeId = string.Empty;
        }
    }
}
