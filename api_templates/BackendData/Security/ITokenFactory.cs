namespace BackendData.Security;

public interface ITokenFactory
{
	AccessToken CreateAccessToken(User user);
}
