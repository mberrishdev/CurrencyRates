using System.Text.Json.Serialization;
using CurrencyRates.Domain.Primitives;

namespace CurrencyRates.Domain.Entities.ExchangeRates.Commands;

public class UpdateExchangeRateCommand : CommandBase
{
    [JsonIgnore] public int Id { get; set; }
    public decimal Rate { get; set; }
}