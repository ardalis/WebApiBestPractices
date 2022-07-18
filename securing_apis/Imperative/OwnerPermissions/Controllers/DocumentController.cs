using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwnerPermissions.Data;
using OwnerPermissions.Models;
using OwnerPermissions.Services;

namespace OwnerPermissions.Controllers
{
	/// <summary>
	/// See: https://github.com/dotnet/AspNetCore.Docs/tree/main/aspnetcore/security/authorization/resourcebased/samples/3_0/ResourceBasedAuthApp2
	/// </summary>
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class DocumentController : ControllerBase
	{
		private readonly IAuthorizationService _authorizationService;
		private readonly IDocumentRepository _documentRepository;
		private readonly ILogger<DocumentController> _logger;

		public DocumentController(IAuthorizationService authorizationService,
															IDocumentRepository documentRepository,
															ILogger<DocumentController> logger)
		{
			_authorizationService = authorizationService;
			_documentRepository = documentRepository;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetById(int documentId)
		{
			_logger.LogInformation($"Attempting to view documentId {documentId} as {User.Identity.Name}");
			Document document = _documentRepository.Find(documentId);

			if (document == null)
			{
				return new NotFoundResult();
			}

			var userIsAuthorized = (await _authorizationService
					.AuthorizeAsync(User, document, Operations.Read)).Succeeded;

			//if ((await _authorizationService
			//					.AuthorizeAsync(User, document, "EditPolicy")).Succeeded)
			//{
			//	return Ok(document);
			//}

			if (userIsAuthorized)
			{
				return Ok(document);
			}
			else
			{
				return new ChallengeResult();
			}
		}
	}
}
