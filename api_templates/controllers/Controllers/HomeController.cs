using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace controllers.Controllers;

public class HomeController : ControllerBase
{
[ApiExplorerSettings(IgnoreApi = true)]
[Route("/error")]
public IActionResult HandleError() =>
	Problem();

	[ApiExplorerSettings(IgnoreApi = true)]
	[Route("/error-development")]
	public IActionResult HandleErrorDevelopment(
		[FromServices] IHostEnvironment hostEnvironment)
	{
		if (!hostEnvironment.IsDevelopment())
		{
			return NotFound();
		}

		var exceptionHandlerFeature =
				HttpContext.Features.Get<IExceptionHandlerFeature>()!;

		return Problem(
				detail: exceptionHandlerFeature.Error.StackTrace,
				title: exceptionHandlerFeature.Error.Message);
	}

	// sample endpoint that throws
	[HttpGet("/throw")]
	public IActionResult Throw() =>
		throw new Exception("Sample exception.");
}
