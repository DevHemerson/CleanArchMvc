using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IAuthenticate _authenticate;
    private readonly IConfiguration _configuration;

    public TokenController(IAuthenticate authenticate, IConfiguration configuration)
    {
        _authenticate = authenticate ??
            throw new ArgumentNullException(nameof(authenticate));
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<ActionResult<UserToken>> Register([FromBody] LoginModel model)
    {
        var result = await _authenticate.RegisterUser(model.Email, model.Password);
        if (result)
        {            
            return Ok($"User {model.Email} was create successfully");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid register attempt.");
            return BadRequest("Invalid register attempt.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel model)
    {
        var result = await _authenticate.Authenticate(model.Email, model.Password);
        if (result)
        {
            var token = GenerateToken(model);
            return Ok(token);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest("Invalid login attempt.");
        }
    }

    private UserToken GenerateToken(LoginModel model)
    {
        //declarações do usuário
        var claims = new[]
        {
            new Claim("email", model.Email),
            new Claim("meu valor", "Admin"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //gera a chave
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        //gerar assinatura digital
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //definir tempo de expiração
        var expiration = DateTime.UtcNow.AddMinutes(10);
        //gerar token
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}
