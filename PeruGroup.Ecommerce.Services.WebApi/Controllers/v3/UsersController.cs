using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand;
using PeruGroup.Ecommerce.Services.WebApi.Helpers;
using PeruGroup.Ecommerce.Transversal.Commons;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PeruGroup.Ecommerce.Services.WebApi.Controllers.v3
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AppSettings _appSettings;

        public UsersController(IMediator mediator, IOptions<AppSettings> appSettings)
        {
            _mediator = mediator;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] CreateUserTokenCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.IsSuccess)
            {
                if (response.Data != null)
                {
                    response.Data.Token = GenerateToken(response);
                    return Ok(response);
                }
                else
                {
                    return NotFound(response);
                }
            }

            return BadRequest(response);
        }

        private string GenerateToken(Response<UserDto> user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_appSettings.Secret!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Data!.UserId.ToString()),
                    new Claim("username", user.Data!.UserName!),
                    new Claim("firstName", user.Data!.FirstName!),
                    new Claim("lastName", user.Data!.LastName!)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
