using Arta.Base.Core.Exceptions;
using Arta.Base.Core.Validators;
using FluentValidation;

namespace Restaurant.Presentation.Validators;

public class ValidatorAttribute : ValidatorAttributeBase
{
    private readonly Type _validatorType;

    public ValidatorAttribute(Type validatorType, string parameterName)
        : base(parameterName)
    {
        _validatorType = validatorType;
    }

    protected override void Validate(object value)
    {
        var validator = (IValidator)Activator.CreateInstance(_validatorType)!;

        var context = new ValidationContext<object>(value);

        var result = validator.Validate(context);

        if (!result.IsValid)
            throw new ValidatorExecption("Validation Error", result);
    }
}