using System;
using System.ComponentModel.DataAnnotations;
using CurrencyRates.Domain.Primitives;

namespace CurrencyRates.Domain.Entities.ExchangeRates;

public class ExchangeRate : Entity<int>
{
    [Required, MaxLength(3)] public required string CurrencyFrom { get; set; }
    [Required, MaxLength(3)] public required string CurrencyTo { get; set; }
    public required decimal Rate { get; set; }
    public required DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}