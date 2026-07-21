using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Common.Errors;

public class SystemError
{
    public static readonly Error Unexpected = 
        Error.Failure(
            "system_error.unexpected",
        "An unexpected error occured. Please try again later.");
}