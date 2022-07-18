using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackendData;
using controllers.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace APIProjectTests;

/// <summary>
/// Don't do this - it doesn't test filters, routing, etc.
/// </summary>
public class ControllerUnitTests
{
	[Fact]
	public async Task TestControllerAction()
	{
		var mockLogger = new Mock<ILogger<AuthorsController>>();
		var mockRepo = new Mock<IAsyncRepository<Author>>();
		mockRepo
			.Setup(c => c.ListAllAsync(It.IsAny<CancellationToken>()))
			.ReturnsAsync(new List<Author>());
		var controller = new AuthorsController(mockLogger.Object, mockRepo.Object);

		var result = await controller.Index(CancellationToken.None);
		
		Assert.NotNull(result);
		Assert.IsType<OkObjectResult>(result);
	}
}
