using Microsoft.AspNetCore.Mvc;

namespace cookie_forwarding.Controllers;

[ApiController]
[Route("[controller]")]
public class CookieForwardingController : ControllerBase
{
    private readonly ILogger<CookieForwardingController> _logger;
    private readonly ICookieForwardingService _cookieForwardingService;

    public CookieForwardingController(
        ILogger<CookieForwardingController> logger,
        ICookieForwardingService cookieForwardingService)
    {
        _logger = logger;
        _cookieForwardingService = cookieForwardingService;
    }

    [HttpGet]
    public async Task<HttpBinResponse> Get() => await _cookieForwardingService.Get();
}
