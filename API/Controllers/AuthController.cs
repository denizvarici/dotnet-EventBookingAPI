using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.DTOs.User;
using Services.Jwt;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users
                .Include(u=>u.Role)
                .FirstOrDefaultAsync(u =>u.Email.Equals(dto.Email));
            if (user == null) return Unauthorized("Invalid credentials...");
            bool passwordTrue = BCrypt.Net.BCrypt.Verify(dto.Password,user.PasswordHash);
            if (!passwordTrue) return Unauthorized("Invalid credentials....");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
