using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class OmdbService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OmdbService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OmdbApi:ApiKey"];
    }

    public async Task<MovieDto?> GetMovieDetailsAsync(string title)
    {
        var response = await _httpClient.GetAsync($"?t={Uri.EscapeDataString(title)}&apikey={_apiKey}");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var movie = JsonSerializer.Deserialize<MovieDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return movie?.Response == "True" ? movie : null;
        }

        return null;
    }
}

public class MovieDto
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string Plot { get; set; }
    public string Response { get; set; }
}
