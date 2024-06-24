using CurrencyRates.Domain.Primitives;

namespace CurrencyRates.Domain.Entities.ExchangeRates.Commands;

public class DeleteExchangeRateCommand : CommandBase
{
    public int Id { get; set; }
}