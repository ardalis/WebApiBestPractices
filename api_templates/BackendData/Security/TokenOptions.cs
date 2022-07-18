namespace BackendData.Security;

public class TokenOptions
{
	public string Audience { get; set; }
	public string Issuer { get; set; }
	public long AccessTokenExpiration { get; set; }
	public string Secret { get; set; }
}
