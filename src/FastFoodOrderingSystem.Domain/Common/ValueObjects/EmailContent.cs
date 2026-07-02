namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

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

    public static EmailContent Create(string from, string to, string subject, string body)
    {
        return new(
            Email.Create(from),
            Email.Create(to),
            subject,
            body);
    }
}