namespace BackendData.Security;

public abstract class JsonWebToken
{
	public string Token { get; protected set; }
	public long Expiration { get; protected set; }

	public JsonWebToken(string token, long expiration)
	{
		if (string.IsNullOrWhiteSpace(token))
			throw new ArgumentException("Invalid token.");

		if (expiration <= 0)
			throw new ArgumentException("Invalid expiration.");

		Token = token;
		Expiration = expiration;
	}

	public bool IsExpired() => DateTime.UtcNow.Ticks > Expiration;
}

public class AccessToken : JsonWebToken
{
	public AccessToken(string token, long expiration) :
		base(token, expiration)
	{
	}
}
