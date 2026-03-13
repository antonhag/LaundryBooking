using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using LaundryBooking.Domain.Enums;

namespace LaundryBooking.Maui.DataManager;

public class GoogleCalendarManager
{
    public static async Task<string?> CreateCalendarEvent(string accessToken, DateOnly date, TimeSlot timeSlot)
    {
        var client = new HttpClient();

        client.BaseAddress = new Uri("https://www.googleapis.com");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        int startHour;
        int endHour;

        switch (timeSlot)
        {
            case TimeSlot.Morning:
                startHour = 7;
                endHour = 12;
                break;
            case TimeSlot.Afternoon:
                startHour = 12;
                endHour = 17;
                break;
            case TimeSlot.Evening:
                startHour = 17;
                endHour = 21;
                break;
            default:
                startHour = 0;
                endHour = 0;
                break;
        }

        var startDate = date.ToDateTime((new TimeOnly(startHour, 0)));
        var endDate = date.ToDateTime((new TimeOnly(endHour, 0)));

        var body = JsonContent.Create(new
        {
            summary = $"Tvättid",
            start = new
            {
                dateTime = startDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                timeZone = "Europe/Stockholm"
            },
            end = new
            {
                dateTime = endDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                timeZone = "Europe/Stockholm"
            }
        });

        try
        {
            using HttpResponseMessage tokenResponse = await client.PostAsync(
                "https://www.googleapis.com/calendar/v3/calendars/primary/events",
                body);

            if (!tokenResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await tokenResponse.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<JsonElement>(json);
            return data.GetProperty("id").GetString();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static async Task<bool> DeleteCalendarEvent(string accessToken, string eventId)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        try
        {
            using HttpResponseMessage response = await client.DeleteAsync(
                $"https://www.googleapis.com/calendar/v3/calendars/primary/events/{eventId}");

            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
