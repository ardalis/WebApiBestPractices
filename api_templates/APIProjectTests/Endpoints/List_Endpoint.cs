using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions;
using BackendData.DataAccess;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace APIProjectTests.Endpoints;

internal record AuthorListResult(int Id, string Name, string TwitterAlias);

public class List_Endpoint
{
	private readonly ITestOutputHelper _testOutputHelper;
	private readonly ILogger _logger;
	private readonly HttpClient _client;

	public List_Endpoint(ITestOutputHelper testOutputHelper)
	{
		_testOutputHelper = testOutputHelper;
		_logger = XUnitLogger.CreateLogger<Swagger>(_testOutputHelper);
		_client = new WebApiApplication(_testOutputHelper).CreateClient(new() { BaseAddress = new Uri("https://localhost")} );
	}

	[Fact]
	public async Task GetAuthors()
	{
		var response = await _client.GetAsync("/api/Authors");

		_logger.LogInformation($"URL: {response.RequestMessage.RequestUri.AbsoluteUri}");

		response.EnsureSuccessStatusCode();
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);

		var result = await _client.GetAndDeserialize<IEnumerable<AuthorListResult>>(Routes.Authors.List());
		Assert.NotNull(result);
		Assert.Equal(SeedData.Authors().Count, result.Count());

	}

}


