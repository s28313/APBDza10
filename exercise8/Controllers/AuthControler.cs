using kol2APBD.DTOs;
using kol2APBD.Models;
using kol2APBD.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace kol2APBD.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        var user = await _authService.AuthenticateUser(request.Login, request.Password);

        if (user == null)
            return Unauthorized(new { message = "Invalid login or password" });

        var accessToken = await _authService.GenerateAccessToken(user);
        var refreshToken = await _authService.GenerateRefreshToken(user);

        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
    }
}