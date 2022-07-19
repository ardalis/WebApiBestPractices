namespace BackendData.Security;

public abstract class JsonWebToken
{
	public string TokenString { get; protected set; }
	public long Expiration { get; protected set; }

	public JsonWebToken(string tokenString, long expiration)
	{
		if (string.IsNullOrWhiteSpace(tokenString))
			throw new ArgumentException("Invalid token.");

		if (expiration <= 0)
			throw new ArgumentException("Invalid expiration.");

		TokenString = tokenString;
		Expiration = expiration;
	}

	public bool IsExpired() => DateTime.UtcNow.Ticks > Expiration;
}
