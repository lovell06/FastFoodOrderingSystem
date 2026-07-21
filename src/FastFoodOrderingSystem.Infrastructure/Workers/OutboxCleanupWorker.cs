using FastFoodOrderingSystem.Infrastructure.Options;
using FastFoodOrderingSystem.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FastFoodOrderingSystem.Infrastructure.Workers;

public sealed class OutboxCleanupWorker : BackgroundService
{
    private readonly ILogger<OutboxCleanupWorker> _logger;
    private readonly PeriodicTimer _timer;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly OutboxCleanupWorkerOption _options;
    
    public OutboxCleanupWorker(
        IOptions<OutboxCleanupWorkerOption> options,
        ILogger<OutboxCleanupWorker> logger, 
        IServiceScopeFactory scopeFactory)
    {
        _options = options.Value;
        _logger = logger;
        _timer = new PeriodicTimer(TimeSpan.FromHours(_options.Interval.Hours));
        _scopeFactory = scopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(OutboxCleanupWorker)} started.");
        
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await context.OutboxMessages
                    .Where(o => 
                        o.ProcessedAtUtc != null && 
                        o.ProcessedAtUtc.Value.AddDays(_options.Retention.Days) < DateTime.UtcNow)
                    .ExecuteDeleteAsync(stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Cleanup outbox message failed.");
            }
        }
        
        _logger.LogInformation($"{nameof(OutboxCleanupWorker)} stopped.");
    }
}