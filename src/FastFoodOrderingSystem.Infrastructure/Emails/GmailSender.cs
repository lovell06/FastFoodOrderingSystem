using System.Net;
using System.Net.Mail;
using FastFoodOrderingSystem.Application.Abstractions.Emails;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace FastFoodOrderingSystem.Infrastructure.Emails;

public sealed class GmailSender : IEmailSender
{
    private readonly EmailOption _option;
    public GmailSender(IOptions<EmailOption> options)
    {
        _option = options.Value;
    }

    public async Task SendAsync(EmailContent content)
    {
        using var client = new SmtpClient();
        client.Host = _option.Host;
        client.Port = _option.Port;
        client.Credentials = new NetworkCredential(_option.UserName, _option.Password);
        client.EnableSsl = true;

        var mailMessage = new MailMessage()
        {
            From = new MailAddress(content.From.Value, _option.DisplayName),
            Subject = content.Subject,
            Body = content.Body
        };
        mailMessage.To.Add(content.To.Value);

        await client.SendMailAsync(mailMessage);
    }
}