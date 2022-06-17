using System.Text.Json;
using controllers.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace controllers.Controllers;

[ApiController]
[Route("Demo")]
public class DemoController : ControllerBase
{
	// different methods that return an author

	[HttpGet("getobject")]
	public object GetObject()
	{
		return GetBook();
	}

	// returns a 204 no content with no body
	// response type header is not set
	[HttpGet("getobjectnull")]
	public object GetObjectNull()
	{
		return null;
	}

	// will set response type to text/plain
	[HttpGet("getstring")]
	public string GetString()
	{
		return JsonSerializer.Serialize(GetBook());
	}

	[HttpGet("getjson")]
	public JsonResult GetJson()
	{
		return new JsonResult(GetBook());
	}

	[HttpGet("getiactionresult")]
	public IActionResult GetIActionResult()
	{
		return Ok(GetBook());
	}

	[HttpGet("getiactionresult<T>")]
	public ActionResult<BookDto> GetActionResultOfT()
	{
		return Ok(GetBook());
	}

	private BookDto GetBook()
	{
		return new BookDto(1, "1234-5678", "Warren Piece", "Steve Smith");
	}
}
