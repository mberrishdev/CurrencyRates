using System.Linq;
using CurrencyRates.Domain.Validators;

namespace CurrencyRates.Domain.Primitives;

public class CommandBaseValidator : ICommandBase
{
    public void Validate()
    {
        if (this.GetType()
                .GetCustomAttributes(typeof(CommandValidationAttribute), true)
                .FirstOrDefault() is not CommandValidationAttribute commandValidationAttribute)
            return;

        var type = commandValidationAttribute.ValidorType;
        CommandValidator.Validate(type, this);
    }
}