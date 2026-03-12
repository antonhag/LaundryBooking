using System.Net.Http.Headers;
using System.Text.Json;

namespace LaundryBooking.Maui.DataManager;

public class GoogleAuthManager
{
    // Byter ut authorization code mot en access token och hämtar användarens förnamn
    public static async Task<string> GetGivenNameAsync(string code, string codeVerifier, string clientId)
    {
        var client = new HttpClient();

        // Byter ut authorization code mot en access token via Googles token-endpoint
        // code_verifier skickas med för att verifiera PKCE-flödet
        using HttpResponseMessage tokenResponse = await client.PostAsync(
            "https://oauth2.googleapis.com/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["code"] = code,
                ["client_id"] = clientId,
                ["redirect_uri"] = "com.companyname.laundrybooking.maui://",
                ["code_verifier"] = codeVerifier,
                ["grant_type"] = "authorization_code"
            }));

        string tokenJson = await tokenResponse.Content.ReadAsStringAsync();

        if (!tokenResponse.IsSuccessStatusCode)
        {
            return string.Empty;
        }

        var tokenData = JsonSerializer.Deserialize<JsonElement>(tokenJson);
        var accessToken = tokenData.GetProperty("access_token").GetString()!;

        // Anropar Googles userinfo-API med access token för att hämta användarens profilinfo
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        using HttpResponseMessage userInfoResponse = await client.GetAsync(
            "https://www.googleapis.com/oauth2/v3/userinfo");

        string userInfoJson = await userInfoResponse.Content.ReadAsStringAsync();
        var userInfo = JsonSerializer.Deserialize<JsonElement>(userInfoJson);

        return userInfo.GetProperty("given_name").GetString() ?? string.Empty;
    }
}
