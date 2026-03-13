using LaundryBooking.Domain.Enums;

namespace LaundryBooking.Domain.Entities
{
    public class Booking
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CalendarEventId { get; set; } = string.Empty;
        public string HousingCooperativeId { get; set; } = string.Empty;
        public string ApartmentNumber { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Konverterar TimeSlot enum till ett läsbart tidspass som visa i UI
        public string TimeSlotDisplayText                                                                                              
        {                                                                                                                              
            get                                                                                                                        
            {                                                                                                                          
                return TimeSlot switch                                                                                                 
                {                                                                                                                    
                    TimeSlot.Morning => "07:00–12:00",                                                                          
                    TimeSlot.Afternoon => "12:00–17:00",                                                                 
                    TimeSlot.Evening => "17:00–21:00",
                    _ => TimeSlot.ToString() // "_" är ett default case vilket krävs för en switch
                };
            }
        }   
    }
}
