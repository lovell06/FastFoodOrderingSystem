using FastFoodOrderingSystem.Domain.Users.ValueObjects;

namespace FastFoodOrderingSystem.Application.Abstractions.Emails;

public sealed record EmailContent
{
    public Email From { get; init; }
    public Email To { get; init; }
    public string Subject { get; init; }
    public string Body { get; init; }

    private EmailContent(Email from, Email to, string subject, string body)
    {
        From = from;
        To = to;
        Subject = subject;
        Body = body;
    }

    public static EmailContent Create(Email from, Email to, string subject, string body)
    {
        return new(
            from: from,
            to: to,
            subject: subject,
            body: body);
    }
}