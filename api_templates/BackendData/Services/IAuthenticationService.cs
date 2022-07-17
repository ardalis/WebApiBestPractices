using Ardalis.Result;
using BackendData.Security;

namespace BackendData.Services;

public interface IAuthenticationService
{
	Task<Result<AccessToken>> CreateAccessTokenAsync(string email, string password, CancellationToken cancellationToken);
}

