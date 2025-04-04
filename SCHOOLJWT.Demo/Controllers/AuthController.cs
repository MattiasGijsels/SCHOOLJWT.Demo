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

        public async Task<ActionResult<string>> Login (UserDto request)

        {
            var token = await authService.LoginAsync (request);
            if (token == null) 
                return BadRequest("Invalid username or password.");

            return Ok(token);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint ()
        {
            return Ok("You are authenticated");
        }
    }
}
