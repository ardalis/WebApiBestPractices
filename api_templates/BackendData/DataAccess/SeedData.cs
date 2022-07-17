using BackendData.Security;

namespace BackendData.DataAccess;

public static class SeedData
{
	public static List<Author> Authors()
	{
		int id = 1;

		var authors = new List<Author>()
						{
								new Author
								{
										Id = id++,
										Name="Steve Smith",
										PluralsightUrl="https://www.pluralsight.com/authors/steve-smith",
										TwitterAlias="ardalis"
								},
								new Author
								{
										Id = id++,
										Name="Julie Lerman",
										PluralsightUrl="https://www.pluralsight.com/authors/julie-lerman",
										TwitterAlias="julialerman"
								}
						};

		return authors;
	}

	public static List<User> Users()
	{
		int id = 1;

		var hasher = new PasswordHasher();

		var users = new List<User>
		{
			new User("test@test.com", hasher.HashPassword("123456")) { Id = id++}
		};

		return users;
	}
}
