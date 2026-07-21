using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;
using FastFoodOrderingSystem.Infrastructure.Eventing.JsonSerializers;
using FastFoodOrderingSystem.Infrastructure.Options;
using FastFoodOrderingSystem.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FastFoodOrderingSystem.Infrastructure.Workers;

public sealed class OutboxWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<OutboxWorker> _logger;
    private readonly PeriodicTimer _timer;
    private readonly OutboxWorkerOption _options;

    public OutboxWorker(
        IOptions<OutboxWorkerOption> options,
        IServiceScopeFactory serviceScopeFactory, 
        ILogger<OutboxWorker> logger)
    {
        _options = options.Value;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(_options.Interval.Milliseconds));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(OutboxWorker)} started.");
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                using var serviceScope = _serviceScopeFactory.CreateScope();

                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var outboxMessages = await context
                    .OutboxMessages
                    .Where(o => o.ProcessedAtUtc == null)
                    .OrderBy(o => o.OccurredAtUtc)
                    .Take(50)
                    .ToListAsync(stoppingToken);

                var dispatcher = serviceScope.ServiceProvider.GetRequiredService<IEventDispatcher>();
                var clock = serviceScope.ServiceProvider.GetRequiredService<IDateTimeProvider>();

                foreach (var outboxMessage in outboxMessages)
                {
                    try
                    {
                        var e = OutboxMessagePayloadSerializer.Deserialize(outboxMessage.Payload, outboxMessage.Type);
                        await dispatcher.DispatchAsync(e, stoppingToken);
                        outboxMessage.ProcessedAtUtc = clock.UtcNow;
                        outboxMessage.Error = null;
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError(
                            exception,
                            "Handle message failed. Message id: {0}. Message type: {1}",
                            outboxMessage.Id,
                            outboxMessage.Type);
                        outboxMessage.Error = exception.ToString();
                    }
                    finally
                    {
                        ++outboxMessage.RetryCount;
                    }
                }
                
                await context.SaveChangesAsync(stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Outbox worker failed.");
            }
        }
        
        _logger.LogInformation($"{nameof(OutboxWorker)} stopped.");
    }
}