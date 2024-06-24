namespace CurrencyRates.Application.ExchangeRates.Queries.Models;

public class ExchangeRateModel
{
    public required string CurrencyFrom { get; set; }
    public required string CurrencyTo { get; set; }
    public required decimal Rate { get; set; }
}