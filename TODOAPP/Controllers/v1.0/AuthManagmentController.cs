using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODOAPP.Controllers.Data;
using TODOAPP.Data.Services;
using TODOAPP.Models.DTOs.Requests;
using TODOAPP.Models.DTOs.Responses;

namespace TODOAPP.Controllers.V1_0
{
	[ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthManagmentController:ControllerBase
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly UserManager<IdentityUser> _userManager;
		private readonly ApiDbContext _context;
        public AuthManagmentController(IJwtGenerator jwtGenerator, UserManager<IdentityUser> userManager, ApiDbContext context)
        {
            _jwtGenerator = jwtGenerator;
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
		{
			if (await _userManager.FindByEmailAsync(request.Email) is not null)
			{
				return BadRequest(new LoginResponse { Errors = new List<string> { "User with this email already exists" } });
			}
			var newUser = new IdentityUser
			{
				Email = request.Email,
				UserName = request.UserName
			};

			var isCreated = await _userManager.CreateAsync(newUser, request.Password);
			if (!isCreated.Succeeded)
			{
				return BadRequest(new RegisterResponse { Errors = isCreated.Errors.Select(e => e.Description).ToList() });
			}
			return Ok(await _jwtGenerator.GenerateToken(newUser));
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			var existingUser = await _userManager.FindByEmailAsync(request.Email);
			if (existingUser is null)
			{
				return BadRequest(new LoginResponse { Errors = new List<string> { "Invalid login request" } });
			}
			var isCorrect = await _userManager.CheckPasswordAsync(existingUser, request.Password);
			if (!isCorrect)
			{
				return BadRequest(new LoginResponse { Errors = new List<string> { "Invalid login request" } });
			}
			return Ok(await _jwtGenerator.GenerateToken(existingUser));
		}
     
	    [HttpPost("refresh-token")]
		public async Task<IActionResult> Refresh([FromBody] TokenRequest request)
        {
            var storedRefreshToken= await _context.RefreshTokens.FirstOrDefaultAsync(t=>t.Token==request.RefreshToken);
            if (storedRefreshToken is null)
            {
                return NotFound(new AuthResult { Errors = new List<string> { "Refresh token is not found" } });
            }

            if (storedRefreshToken.IsRevoked)
            {
                return BadRequest(new AuthResult { Errors = new List<string> { "Invalid refresh token" } });
            }

            if (storedRefreshToken.ExpirationDate < DateTime.UtcNow)// in this case if refresh token is expired then the user should login again
            {
                return BadRequest(new AuthResult { Errors = new List<string> { "Refresh token is expired" } });
            }

			storedRefreshToken.IsRevoked=true;
		    _context.Update(storedRefreshToken);
			await _context.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(storedRefreshToken.UserId);
            if (user is null)
            {
                return Unauthorized(new AuthResult { Errors = new List<string> { "Invalid refresh token" } });
            }
			
            var newToken = await _jwtGenerator.GenerateToken(user);
            await _context.SaveChangesAsync();
            return Ok(newToken);
        }
		
        [Authorize]
		[HttpPost("revoke-token")]
		public async Task<IActionResult> RevokeTokens([FromBody] TokenRequest request)
        {
            var storedRefreshToken= await _context.RefreshTokens.FirstOrDefaultAsync(t=>t.Token==request.RefreshToken);
			if(storedRefreshToken is null)
            {
                return NotFound(new AuthResult{Errors=new List<string>{"Refresh token is not found"}});
            }
			storedRefreshToken.IsRevoked=true;
			_context.RefreshTokens.Update(storedRefreshToken);
			await _context.SaveChangesAsync();
			return Ok(new {Success=true, Message="Refresh token revoked successfully"});
        }
        }
}