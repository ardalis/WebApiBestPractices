using System.Text.Json;

namespace APIProjectTests;

public static class JsonOptionConstants
{
	public static JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		WriteIndented = true
	};
}
