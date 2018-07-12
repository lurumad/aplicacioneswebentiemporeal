using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WhatsApp.Model;
using WhatsApp.Services;

namespace WhatsApp.Controllers
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsAppController : ControllerBase
    {
        private readonly IChatService userService;
        private static readonly SecurityKey SigningKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());
        private static readonly JwtSecurityTokenHandler JwtTokenHandler = new JwtSecurityTokenHandler();
        public static readonly SigningCredentials SigningCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);
        public const string Issuer = "WhatsAppJwt";
        public const string Audience = "WhatsAppJwt";

        public WhatsAppController(IChatService userService)
        {
            this.userService = userService ?? throw new System.ArgumentNullException(nameof(userService));
        }

        [Route("users/me")]
        [Authorize]
        public ActionResult<WhatsAppResponse> Me()
        {
            return Ok(userService.GetMe(User.Identity.Name));
        }

        [Route("token")]
        public ActionResult<string> Login(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Username is required");
            }

            var user = userService.GetUserBy(name);

            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.NameIdentifier, name)
            };

            var claimsIdentity = new ClaimsIdentity(claims);

            var token = JwtTokenHandler.CreateJwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                subject: claimsIdentity,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: SigningCredentials);

            return Ok(token);
        }
    }
}
