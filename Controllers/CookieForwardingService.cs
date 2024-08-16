using System.Net;
using Newtonsoft.Json;

namespace cookie_forwarding.Controllers;

public interface ICookieForwardingService
{
    Task<HttpBinResponse> Get();
}

public class CookieForwardingService: ICookieForwardingService {
    private readonly HttpClient _httpClient;
    private readonly ILogger<CookieForwardingService> _logger;

    public CookieForwardingService(HttpClient httpClient, ILogger<CookieForwardingService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<HttpBinResponse> Get()
    {
        var response = await _httpClient.GetAsync("https://httpbin.org/get");
        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<HttpBinResponse>(await response.Content.ReadAsStringAsync());
        }{
           throw new Exception($"Failed to get response from httpbin.org. Status code: {response.StatusCode}");
        }
    }
}