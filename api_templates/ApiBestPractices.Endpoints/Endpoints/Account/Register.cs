using Ardalis.ApiEndpoints;
using BackendData;
using Microsoft.AspNetCore.Mvc;

namespace ApiBestPractices.Endpoints.Endpoints.Account;

public class Register : EndpointBaseAsync
		.WithRequest<RegisterUserCommand>
		.WithActionResult
{
	private readonly IAsyncRepository<User> _repository;
	private readonly IPasswordHasher _passwordHasher;

	public Register(IAsyncRepository<User> repository,
		IPasswordHasher passwordHasher)
	{
		_repository = repository;
		_passwordHasher = passwordHasher;
	}

	/// <summary>
	/// Creates a new User
	/// </summary>
	[HttpPost("[namespace]")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public override async Task<ActionResult> HandleAsync(RegisterUserCommand request,
		CancellationToken cancellationToken = default)
	{
		var user = new User(request.Email, _passwordHasher.HashPassword(request.Password));

		await _repository.AddAsync(user, cancellationToken);

		return Ok(user.Id);
	}
}
