using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace True.Code.ToDoListAPI.Infrastructure.Filters;

public class ValidateModelStateFilter : ActionFilterAttribute
{
    private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
      
        var keys = context.ModelState.Keys;
        
        string [] validationErrors = context.ModelState
            .Keys
            .SelectMany(k => context.ModelState[k]?.Errors!
            ).Select(e => e.ErrorMessage)
            .ToArray();

        JsonErrorResponse json = new() { Messages = validationErrors };

        context.Result = new BadRequestObjectResult(json);
    }
}