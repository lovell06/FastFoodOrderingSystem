using System.Diagnostics;
using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Application.Abstractions.Emails;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;
using FastFoodOrderingSystem.Infrastructure.Eventing.IntegrationEvents.Customers;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Infrastructure.Eventing.IntegrationEventHandlers.Customers;

public class SendWelcomeEmailHandler : IEventHandler<IntegrationUserRegisteredEvent>
{
    private readonly ILogger<SendWelcomeEmailHandler> _logger;
    private readonly IEmailSender _emailSender;
    private readonly IEmailConfiguration _emailConfiguration;
    public SendWelcomeEmailHandler(
        ILogger<SendWelcomeEmailHandler> logger, 
        IEmailSender emailSender, 
        IEmailConfiguration emailConfiguration)
    {
        _logger = logger;
        _emailSender = emailSender;
        _emailConfiguration = emailConfiguration;
    }
    public async Task HandleAsync(IntegrationUserRegisteredEvent e, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"User with {e.UserId} registered. Occurred at {e.OccurredAtUtc}");

        var senderAddressResult = Email.Create(_emailConfiguration.SenderAddress);
        if (senderAddressResult.IsFailure)
        {
            var err = senderAddressResult.Error!;
            throw new InvalidOperationException(
                $"Sender email address invalid. {err.Code}. {err.Message}. {e.OccurredAtUtc}");
        }

        _logger.LogInformation($"Sending welcome to {e.UserEmail}...");

        var sw = new Stopwatch();
        
        sw.Start();

        var emailResult = Email.Create(e.UserEmail);
        if (emailResult.IsFailure)
            throw new InvalidOperationException("Email create failed.");
        
        var content = EmailContent.Create(
            senderAddressResult.Value!,
            emailResult.Value!,
            "Welcome to fast food.",
            "You was been register successful.");

        await _emailSender.SendAsync(content);
        sw.Stop();
        
        _logger.LogInformation($"Welcome was been send. {sw.ElapsedMilliseconds}ms.");
    }
}