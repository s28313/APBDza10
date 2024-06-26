using kol2APBD.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace kol2APBD.Controllers;

[ApiController]
[Route("api/auth")]
public class RefreshTokenController : ControllerBase
{
    private readonly IAuthService _authService;

    public RefreshTokenController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        var newAccessToken = await _authService.RefreshAccessToken(refreshToken);

        if (newAccessToken == null)
            return Unauthorized(new { message = "Invalid refresh token" });

        return Ok(new { AccessToken = newAccessToken });
    }
}