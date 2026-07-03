using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace FastFoodOrderingSystem.Infrastructure.Configurations;

public class GmailConfiguration : IEmailConfiguration
{
    public GmailConfiguration(IOptions<EmailOption> options)
    {
        SenderAddress = options.Value.Email;
    }

    public string SenderAddress { get; }
}