using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApiBestPractices.Endpoints.Endpoints.Authors;
using BackendData.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace APIProjectTests.Endpoints;

// Do everything in the test method
// Works, but results in a lot of duplicate when you have more than one test
public class GetById_Endpoint
{
	private readonly ITestOutputHelper _testOutputHelper;
	private readonly ILogger _logger;
	private readonly HttpClient _client;
	private readonly int _testAuthorId = 1;

	public GetById_Endpoint(ITestOutputHelper testOutputHelper)
	{
		_testOutputHelper = testOutputHelper;
		_logger = XUnitLogger.CreateLogger<Swagger>(_testOutputHelper);
		_client = new WebApiApplication(_testOutputHelper).CreateClient();
	}

	[Fact]
	public async Task ReturnsSeededAuthorWithId1()
	{
		// Arrange (done in WebApiApplication)

		// Act
		var response = await _client.GetAsync(Routes.Authors.Get(_testAuthorId));
		response.EnsureSuccessStatusCode();
		var stringResult = await response.Content.ReadAsStringAsync();
		_logger.LogInformation(stringResult);
		var result = JsonSerializer.Deserialize<AuthorDto>(stringResult,
			JsonOptionConstants.SerializerOptions);

		// Assert
		Assert.NotNull(result);
		Assert.Equal("Steve Smith", result.Name);
	}
}
