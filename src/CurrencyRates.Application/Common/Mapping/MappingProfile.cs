using AutoMapper;
using CurrencyRates.Application.ExchangeRates.Queries.Models;
using CurrencyRates.Domain.Entities.ExchangeRates;

namespace CurrencyRates.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ExchangeRate, ExchangeRateModel>().ReverseMap();
    }
}