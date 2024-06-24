using System.Collections.Generic;
using System.Linq;
using CurrencyRates.Domain.Validators;

namespace CurrencyRates.Domain.Exceptions;

public class CommandValidationException : DomainException
{
    public CommandValidationException(IEnumerable<CommandValidationError> messages) : base(
        messages.Select(x => x.ErrorMessage))
    {
    }
}