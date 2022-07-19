using Ardalis.ApiEndpoints;
using BackendData;
using BackendData.Security;
using BackendData.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiBestPractices.Endpoints.Endpoints.Account;

public class Login : EndpointBaseAsync
		.WithRequest<LoginUserCommand>
		.WithActionResult<TokenResponse>
{
	private readonly IAsyncRepository<User> _repository;
	private readonly IPasswordHasher _passwordHasher;
	private readonly IAuthenticationService _authenticationService;

	public Login(IAsyncRepository<User> repository,
		IPasswordHasher passwordHasher,
		IAuthenticationService authenticationService)
	{
		_repository = repository;
		_passwordHasher = passwordHasher;
		_authenticationService = authenticationService;
	}

	/// <summary>
	/// Authenticates a user and returns a JWT Token
	/// </summary>
	[HttpPost("[namespace]/Login")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public override async Task<ActionResult<TokenResponse>> HandleAsync(LoginUserCommand request,
		CancellationToken cancellationToken = default)
	{
		var tokenResult = await _authenticationService.CreateAccessTokenAsync(request.Email,
			request.Password, cancellationToken);

		if (!tokenResult.IsSuccess)
		{
			return BadRequest("Invalid credentials.");
		}

		var response = new TokenResponse() { AccessToken = tokenResult.Value };

		return Ok(response);
	}
}
