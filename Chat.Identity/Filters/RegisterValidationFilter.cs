using FluentValidation;

namespace Chat.Identity.Filters;

public class RegisterValidationFilter(IValidator<RegisterViewModel> validator): IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var register = context.Arguments
            .FirstOrDefault(arg => arg!.GetType() == typeof(RegisterViewModel)) as RegisterViewModel;
        var res = await validator.ValidateAsync(register!);
        if (!res.IsValid) return Results.Json(res.Errors, statusCode: StatusCodes.Status400BadRequest);

        return await next(context);
    }
}