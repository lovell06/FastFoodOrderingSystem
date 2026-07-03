using Scalar.AspNetCore;

namespace FastFoodOrderingSystem.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UsePresentation(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.MapControllers();

        app.UseHttpsRedirection();

        return app;
    }
}