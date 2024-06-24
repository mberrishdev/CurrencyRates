using CurrencyRates.Domain.Primitives;

namespace CurrencyRates.Domain.Entities.ExchangeRates.Commands;

public class CreateExchangeRateCommand : CommandBase<int>
{
    public required string CurrencyFrom { get; set; }
    public required string CurrencyTo { get; set; }
    public decimal Rate { get; set; }
}