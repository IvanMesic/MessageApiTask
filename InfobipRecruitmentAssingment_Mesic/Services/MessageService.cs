using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using InfobipRecruitmentAssingment_Mesic;

public class MessageService
{
    private readonly string _apiUrl;
    private readonly string _apiKey;

    public MessageService(string apiUrl, string apiKey)
    {
        _apiUrl = apiUrl;
        _apiKey = apiKey;
    }

    public async Task<string> SendAsync(RequestMessage message)
    {
        var request = CreateHttpRequest(message);

        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.SendAsync(request);

            return await ProcessResponseAsync(response);
        }
    }

    private HttpRequestMessage CreateHttpRequest(RequestMessage message)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _apiUrl);

        var jsonContent = JsonSerializer.Serialize(new { messages = new List<RequestMessage> { message } });

        request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        request.Headers.Authorization = new AuthenticationHeaderValue("App", _apiKey);

        return request;
    }

    private async Task<string> ProcessResponseAsync(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            return $"HTTP Error: {response.StatusCode}";
        }

        return await response.Content.ReadAsStringAsync();
    }


    
}
