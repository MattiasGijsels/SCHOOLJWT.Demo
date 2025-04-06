﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SCHOOLJWT.Demo.Entities;
using SCHOOLJWT.Demo.Model;
using SCHOOLJWT.Demo.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLJWT.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {

        [HttpPost ("register")]
        public async Task<ActionResult<User>> Register(UserDto request) 
        { 
            var user = await authService.RegisterAsync (request);
            if (user == null)
            {
                return BadRequest("Username already exists.");
            }
            return Ok(user);

        }

        [HttpPost("login")]

        public async Task<ActionResult<TokenResponseDto>> Login (UserDto request)

        {
            var result = await authService.LoginAsync (request);
            if (result == null) 
                return BadRequest("Invalid username or password.");

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint ()
        {
            return Ok("You are authenticated");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are an Admin now, tell your mum");
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokenAsync (request);
            if(result is null || result.AccesToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token.");

           return Ok(result);
        }


    }
}
