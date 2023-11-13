using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Utilities.Securities;

public class JwtTokenGenerator : IJwtTokenGenerator
{
	private readonly IConfiguration configuration;

	public JwtTokenGenerator(IConfiguration configuration)
	{
		this.configuration = configuration;
	}

	public string GenerateTokenForUser(User user)
	{
		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			// new Claim(JwtRegisteredClaimNames.Sub, user.Name),
			// new Claim(JwtRegisteredClaimNames.Email, user.Email),
			//new Claim("UserCreatedDate", user.CreatedDate.ToString("yyyy-MM-dd")),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
			configuration["Jwt:Issuer"],
			claims,
			expires: DateTime.Now.AddMinutes(120),
			signingCredentials: credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}