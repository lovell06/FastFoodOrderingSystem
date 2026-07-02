using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Application.Abstractions.Emails;

public interface IEmailSender
{
    Task SendAsync(EmailContent content);
}