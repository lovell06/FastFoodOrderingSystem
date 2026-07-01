using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace FastFoodOrderingSystem.Infrastructure.Authentication;

public class JwtProvider
{
    private readonly JwtOption _option;

    public JwtProvider(IOptions<JwtOption> option)
    {
        _option = option.Value;
    }

    
}