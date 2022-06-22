using BackendData;

namespace ApiBestPractices.Endpoints;

// makes sure data in the database stays properly formatted
public class DataConsistencyWorker : BackgroundService
{
	public DataConsistencyWorker(IServiceProvider services)
	{
		Services = services;
	}

	public IServiceProvider Services { get; }

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await DoWork(stoppingToken);
	}

	private async Task DoWork(CancellationToken stoppingToken)
	{
		using (var scope = Services.CreateScope())
		{
			var scopedProcessingService =
					scope.ServiceProvider
							.GetRequiredService<IScopedProcessingService>();

			await scopedProcessingService.DoWork(stoppingToken);
		}
	}
}

internal interface IScopedProcessingService
{
	Task DoWork(CancellationToken stoppingToken);
}

internal class ScopedProcessingService : IScopedProcessingService
{
	private int executionCount = 0;
	private readonly ILogger _logger;
	private readonly IAsyncRepository<Author> _authorRepository;

	public ScopedProcessingService(ILogger<ScopedProcessingService> logger,
		IAsyncRepository<Author> authorRepository)
	{
		_logger = logger;
		_authorRepository = authorRepository;
	}

	public async Task DoWork(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			_logger.LogInformation(
					"Checking database for issues...");

			var authorsWithInvalidTwitterAlias = (await _authorRepository
				.ListAllAsync(stoppingToken))
				.Where(author => author.TwitterAlias.Length > 0 && 
							!author.TwitterAlias.StartsWith('@'))
				.ToList();

			_logger.LogInformation($"Found {authorsWithInvalidTwitterAlias.Count} records to fix.");

			foreach (var author in authorsWithInvalidTwitterAlias)
			{
				author.TwitterAlias = "@" + author.TwitterAlias;
				await _authorRepository.UpdateAsync(author, stoppingToken);
			}

			_logger.LogInformation($"Fixed {authorsWithInvalidTwitterAlias.Count} records.");

			await Task.Delay(30_000, stoppingToken);
		}
	}
}
