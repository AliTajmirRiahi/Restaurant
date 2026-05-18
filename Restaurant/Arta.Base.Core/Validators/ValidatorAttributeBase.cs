using Microsoft.AspNetCore.Mvc.Filters;

namespace Arta.Base.Core.Validators;

[AttributeUsage(AttributeTargets.Method)]
public abstract class ValidatorAttributeBase : ActionFilterAttribute
{
    protected readonly string ParameterName;

    protected ValidatorAttributeBase(string parameterName)
    {
        ParameterName = parameterName;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ActionArguments.TryGetValue(ParameterName, out var value))
            return;

        Validate(value!);
    }

    protected abstract void Validate(object value);
}

