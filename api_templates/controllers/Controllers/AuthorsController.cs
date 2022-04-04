using controllers.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace controllers.Controllers
{
	public class AuthorsController : ControllerBase
	{
		public ActionResult<List<AuthorDto>> Index()
		{
			return new List<AuthorDto>();
		}
	}
}
