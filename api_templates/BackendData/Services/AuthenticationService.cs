using Ardalis.Result;
using BackendData.Security;

namespace BackendData.Services;
public class AuthenticationService : IAuthenticationService
{
	private readonly IAsyncRepository<User> _repository;
	private readonly IPasswordHasher _passwordHasher;
	private readonly ITokenFactory _tokenFactory;

	public AuthenticationService(IAsyncRepository<User> repository,
		IPasswordHasher passwordHasher,
		ITokenFactory tokenFactory)
	{
		_repository = repository;
		_passwordHasher = passwordHasher;
		_tokenFactory = tokenFactory;
	}

	public async Task<Result<AccessToken>> CreateAccessTokenAsync(string email, string password, 
		CancellationToken cancellationToken)
	{
		var hashedPassword = _passwordHasher.HashPassword(password);

		var users = await _repository.ListAllAsync(cancellationToken);

		var user = (await _repository
			.ListByExpressionAsync(u => u.Email == email, cancellationToken))
			.FirstOrDefault();

		if (user == null || !_passwordHasher.PasswordMatches(password, user.Password)) return Result.NotFound();

		return _tokenFactory.CreateAccessToken(user);
	}
 
}

