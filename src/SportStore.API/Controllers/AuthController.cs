using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SportStore.Application.DTOs;
using SportStore.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SportStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null) return BadRequest("Email đã tồn tại!");

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return StatusCode(500, "Đăng ký thất bại!");

            // TẠO ROLE NẾU CHƯA CÓ TRONG DATABASE
            if (!await _roleManager.RoleExistsAsync("Customer"))
                await _roleManager.CreateAsync(new IdentityRole("Customer"));

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            // Gán quyền mặc định là Customer cho user mới đăng ký
            await _userManager.AddToRoleAsync(user, "Customer");

            return Ok("Đăng ký thành công!");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // 1. Lấy danh sách các quyền (Roles) của user này
                var userRoles = await _userManager.GetRolesAsync(user);

                // 2. Tạo danh sách Claims cơ bản
                var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

                // 3. Nhét tất cả quyền của user vào Claims
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = GenerateNewJsonWebToken(authClaims);
                return Ok(new { Token = token });
            }
            return Unauthorized("Email hoặc mật khẩu không đúng!");
        }

        private string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                expires: DateTime.Now.AddDays(Convert.ToDouble(jwtSettings["ExpireDays"])),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpGet("my-profile")]
        [Authorize] // Yêu cầu phải có Token hợp lệ
        public async Task<IActionResult> GetMyProfile()
        {
            // Lấy ID của user từ Token (dựa vào ClaimTypes.NameIdentifier mà chúng ta đã set lúc Login)
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Token không hợp lệ hoặc không chứa ID.");

            // Truy vấn database để lấy thông tin mới nhất
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("Không tìm thấy người dùng.");

            // Trả về thông tin (không trả về Password Hash nhé!)
            return Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Roles = await _userManager.GetRolesAsync(user) // Lấy luôn danh sách quyền nếu sau này có
            });
        }
    }
}
