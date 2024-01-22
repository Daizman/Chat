using FluentValidation;

namespace Chat.Identity.Filters;

public class LoginValidationFilter(IValidator<LoginViewModel> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, 
        EndpointFilterDelegate next)
    {
        var login = context.Arguments
            .FirstOrDefault(arg => arg!.GetType() == typeof(LoginViewModel)) as LoginViewModel;
        var res = await validator.ValidateAsync(login!);
        if (!res.IsValid) return Results.Json(res.Errors, statusCode: StatusCodes.Status400BadRequest);

        return await next(context);
    }
}