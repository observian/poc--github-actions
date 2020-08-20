using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace github_actions_webapi.Controllers
{
	[ApiController]
	[Route("/")]
	public class RootController : ControllerBase
	{
		public RootController()
		{
		}
		
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			using var client = new AmazonSimpleSystemsManagementClient();
			var param = await client.GetParametersByPathAsync(new GetParametersByPathRequest
			{
				Path = Environment.GetEnvironmentVariable("SSM_PARAMETER_PATH"),
				Recursive = true,
				WithDecryption = true
			});
			
			var tehSecret = param.Parameters.FirstOrDefault(x => x.Name.Contains("test-value"));
			return Ok($"UTC time is {DateTime.UtcNow:MM/dd/yyyy hh:mm:ss} and secret value is {(tehSecret?.Value ?? "not found")}");
		}
	}
}