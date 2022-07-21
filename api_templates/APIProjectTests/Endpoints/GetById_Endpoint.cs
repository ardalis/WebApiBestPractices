using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApiBestPractices.Endpoints.Endpoints.Authors;
using BackendData;
using BackendData.DataAccess;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using Ardalis.HttpClientTestExtensions;

namespace APIProjectTests.Endpoints;

public class GetById_Endpoint
{
	private readonly ITestOutputHelper _testOutputHelper;
	private readonly ILogger _logger;
	private readonly HttpClient _client;
	private List<Author> Authors { get; set; } = new();

	public GetById_Endpoint(ITestOutputHelper testOutputHelper)
	{
		_testOutputHelper = testOutputHelper;
		_logger = XUnitLogger.CreateLogger<Swagger>(_testOutputHelper);
		_client = new WebApiApplication(_testOutputHelper).CreateClient();
		Authors = SeedData.Authors();
	}

	[Fact]
	public async Task ReturnsSeededAuthorWithId1()
	{
		// Arrange (done in WebApiApplication)

		// Act
		var response = await _client.GetAsync(Routes.Authors.Get(Authors.FirstOrDefault().Id));
		response.EnsureSuccessStatusCode();
		var stringResult = await response.Content.ReadAsStringAsync();
		_logger.LogInformation(stringResult);
		var result = JsonSerializer.Deserialize<AuthorDto>(stringResult,
			JsonOptionConstants.SerializerOptions);

		// Assert
		Assert.NotNull(result);

		string expectedAuthorName = Authors.FirstOrDefault().Name;
		Assert.Equal(expectedAuthorName, result.Name);
	}

	[Fact]
	public async Task ReturnsSeededAuthorWithId1UsingLocalHelper()
	{
		// Arrange (done in WebApiApplication)

		// Act
		var result = await GetAndDeserialize(Routes.Authors.Get(Authors.FirstOrDefault().Id));

		// Assert
		Assert.NotNull(result);

		string expectedAuthorName = Authors.FirstOrDefault().Name;
		Assert.Equal(expectedAuthorName, result.Name);
	}

	private async Task<AuthorDto> GetAndDeserialize(string route)
	{
		var response = await _client.GetAsync(route);
		response.EnsureSuccessStatusCode();
		var stringResult = await response.Content.ReadAsStringAsync();
		_logger.LogInformation(stringResult);
		var result = JsonSerializer.Deserialize<AuthorDto>(stringResult,
			JsonOptionConstants.SerializerOptions);
		return result;
	}

	[Fact]
	public async Task ReturnsNotFoundGivenInvalidId()
	{
		// Arrange
		int invalidId = -1;

		// Act
		var response = await _client.GetAsync(Routes.Authors.Get(invalidId));

		// Assert
		Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
	}

	[Fact]
	public async Task ReturnsNotFoundGivenInvalidIdWithHelper()
	{
		int invalidId = -1;

		await _client.GetAndEnsureNotFound(Routes.Authors.Get(invalidId), _testOutputHelper);
	}
}
