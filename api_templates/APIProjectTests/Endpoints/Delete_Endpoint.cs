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
		var token = await GetAuthToken();

		_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
		var result = await _client.DeleteAsync(Routes.Authors.Delete(1));

		Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
	}

	// Option 1: Get a real token using the real API
	// Option 2: Build a fake token: https://medium.com/asos-techblog/testing-authorization-scenarios-in-asp-net-core-web-api-484bc95d5f6f
	private async Task<string> GetAuthToken()
	{
		const string testUserEmail = "test@test.com";
		const string testUserPassword = "123456";

		// register, login, and return token
		var registerCommand = new RegisterUserCommand { Email = testUserEmail, Password = testUserPassword };
		var serializedCommand = JsonSerializer.Serialize(registerCommand);
		var registerContent = new StringContent(serializedCommand, Encoding.UTF8, "application/json");
		string registerRoute = Routes.Account.Register();
		var response = await _client.PostAsync(registerRoute, registerContent);

		//HttpRequestMessage registerRequest = new HttpRequestMessage(HttpMethod.Post, registerRoute)
		//{
		//	Content = new StringContent(JsonSerializer.Serialize(registerCommand),
		//		Encoding.UTF8,
		//		"application/json")
		//};
		//var registerResponse = await _client.SendAsync(registerRequest);
		response.EnsureSuccessStatusCode();

		var loginCommand = new LoginUserCommand { Email = testUserEmail, Password = testUserPassword };
		var loginJsonContent = new StringContent(JsonSerializer.Serialize(registerCommand), Encoding.UTF8, "application/json");
		var loginResult = await _client.PostAsync(Routes.Account.Register(), loginJsonContent);

		return "";
	}

}
