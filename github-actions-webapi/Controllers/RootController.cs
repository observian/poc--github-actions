using System;
using Microsoft.AspNetCore.Mvc;

namespace github_actions_webapi.Controllers
{
	[ApiController]
	[Route("/")]
	public class RootController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			return Ok($"UTC time is {DateTime.UtcNow:MM/dd/yyyy hh:mm:ss}");
		}
	}
}