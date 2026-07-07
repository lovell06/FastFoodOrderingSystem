using FastFoodOrderingSystem.Domain.Common.DomainResults;

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

    public static DomainResult<EmailContent> Create(string from, string to, string subject, string body)
    {
        var senderAddress = Email.Create(from);
        var recepientAddress = Email.Create(to);

        if (senderAddress.IsFailure)
            return DomainResult<EmailContent>.Failure(senderAddress.Error!);
        if (recepientAddress.IsFailure)
            return DomainResult<EmailContent>.Failure(recepientAddress.Error!);
        
        return DomainResult<EmailContent>.Success(new(
            senderAddress.Value!,
            recepientAddress.Value!,
            subject,
            body));
    }
}