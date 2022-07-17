namespace BackendData;

public class User : BaseEntity
{
	public string Email { get; set; }
	public string Password { get; set; }

	public User(string email, string password)
	{
		Email = email;
		Password = password;
	}
}
