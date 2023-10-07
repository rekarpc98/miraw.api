using Microsoft.AspNetCore.Mvc;

namespace Miraw.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
	[HttpGet]
	public IActionResult Get()
	{
		return Ok("OK");
	}
}