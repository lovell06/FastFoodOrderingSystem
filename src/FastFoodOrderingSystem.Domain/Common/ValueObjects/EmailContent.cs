namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public sealed record EmailContent
{
    public Email From { get; init; }
    public Email To { get; init; }
    public string Body { get; init; }

    private EmailContent(Email from, Email to, string body)
    {
        From = from;
        To = to;
        Body = body;
    }

    public static EmailContent Create(string from, string to, string body)
    {
        return new(
            Email.Create(from),
            Email.Create(to),
            body);
    }
}