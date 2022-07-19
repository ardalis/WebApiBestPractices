using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ApiBestPractices.Endpoints.Endpoints.Account;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace APIProjectTests.Endpoints;

public class Delete_Endpoint
{
	private readonly ITestOutputHelper _testOutputHelper;
	private readonly ILogger _logger;
	private readonly HttpClient _client;
	private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase
	};

	public Delete_Endpoint(ITestOutputHelper testOutputHelper)
	{
		_testOutputHelper = testOutputHelper;
		_logger = XUnitLogger.CreateLogger<Swagger>(_testOutputHelper);
		_client = new WebApiApplication(_testOutputHelper)
			.CreateClient(); // (new() { BaseAddress = new Uri("https://localhost")} );
	}

	[Fact]
	public async Task Returns401GivenNoToken()
	{
		var result = await _client.DeleteAsync(Routes.Authors.Delete(1));

		Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
	}

	[Fact]
	public async Task Returns204NotContentGivenValidAuthToken()
	{
		// create a new user then login, get a token, then delete the user

		var token = await GetAuthToken();

		_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
		var result = await _client.DeleteAsync(Routes.Authors.Delete(1));

		Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
	}


	// Option 1: Get a real token using the real API
	// Option 2: Build a fake token: https://medium.com/asos-techblog/testing-authorization-scenarios-in-asp-net-core-web-api-484bc95d5f6f
	private async Task<string> GetAuthToken()
	{
		string testUserEmail = "test@test.com" + Guid.NewGuid().ToString();
		const string testUserPassword = "123456";

		await Register(testUserEmail, testUserPassword);

		return await LoginGetToken(testUserEmail, testUserPassword);
	}

	private async Task Register(string email, string password)
	{
		var registerCommand = new RegisterUserCommand { Email = email, Password = password };
		var registerContent = new StringContent(JsonSerializer.Serialize(registerCommand, _serializerOptions), Encoding.UTF8, "application/json");
		var response = await _client.PostAsync(Routes.Account.Register(), registerContent);
		response.EnsureSuccessStatusCode();
	}

	private async Task<string> LoginGetToken(string email, string password)
	{
		var loginCommand = new LoginUserCommand { Email = email, Password = password };
		var loginJsonContent = new StringContent(JsonSerializer.Serialize(loginCommand, _serializerOptions), Encoding.UTF8, "application/json");
		var loginResponse = await _client.PostAsync(Routes.Account.Login(), loginJsonContent);
		loginResponse.EnsureSuccessStatusCode();

		var loginStringResult = await loginResponse.Content.ReadAsStringAsync();
		var tokenResult = JsonSerializer.Deserialize<TokenResponse>(loginStringResult, _serializerOptions);

		return tokenResult.AccessToken.TokenString;
	}
}
