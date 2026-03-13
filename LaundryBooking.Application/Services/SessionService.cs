namespace LaundryBooking.Application.Services
{
    public class SessionService // Singleton design pattern
    {
        private static readonly SessionService _instance = new();

        public static SessionService GetSession()
        {
            return _instance;
        }
        
        // private set för att endast kunna ändra värdena via SetSession
        public string ApartmentNumber { get; private set; } = string.Empty;
        public string HousingCooperativeId { get; private set; } = string.Empty;
        public string GivenName { get; private set; } = string.Empty;
        public string AccessToken { get; private set; } = string.Empty;
        
        private SessionService()
        {
        }

        public void SetSession(string apartmentNumber, string housingCooperativeId, string givenName = "", string accessToken = "")
        {
            ApartmentNumber = apartmentNumber;
            HousingCooperativeId = housingCooperativeId;
            GivenName = givenName;
            AccessToken = accessToken;
        }

        public void ClearSession()
        {
            ApartmentNumber = string.Empty;
            HousingCooperativeId = string.Empty;
            GivenName = string.Empty;
        }
    }
}
