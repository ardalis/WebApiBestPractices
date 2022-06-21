namespace controllers.ApiModels
{
	// records don't work with XML serialization - requires parameterless constructor
	//public record AuthorDto(int Id, string Name, string TwitterAlias);

	[Serializable]
	public class AuthorDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string TwitterAlias { get; set; }

		public AuthorDto()
		{
		}

		public AuthorDto(int id, string name, string twitterAlias)
		{
			Id = id;
			Name = name;
			TwitterAlias = twitterAlias;
		}
	}
}
