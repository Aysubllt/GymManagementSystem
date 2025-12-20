using System.Text;
using Newtonsoft.Json;

public class GroqApiService
{
    // Buraya Groq'tan aldığın anahtarı yapıştır
    private readonly string _apiKey = "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";
    private readonly string _apiUrl = "https://api.groq.com/openai/v1/chat/completions";

    public async Task<string> GetGymRecommendationAsync(string prompt)
    {
        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var payload = new
            {
                model = "llama-3.3-70b-versatile", // Çok güçlü ve ücretsiz bir model
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.7
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(_apiUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                dynamic result = JsonConvert.DeserializeObject(responseString);
                return result.choices[0].message.content;
            }

            return $"Groq Hatası: {response.StatusCode} - {responseString}";
        }
        catch (Exception ex)
        {
            return $"Bağlantı hatası: {ex.Message}";
        }
    }
}