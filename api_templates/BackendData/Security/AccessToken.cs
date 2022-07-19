namespace BackendData.Security;

public class AccessToken : JsonWebToken
{
	public AccessToken(string tokenString, long expiration) :
		base(tokenString, expiration)
	{
	}
}
