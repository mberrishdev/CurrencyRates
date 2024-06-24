using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Repository.Repository;
using CurrencyRates.Application.Exceptions;
using CurrencyRates.Application.ExchangeRates.Queries.Models;
using CurrencyRates.Domain.Entities.ExchangeRates;
using MediatR;

namespace CurrencyRates.Application.ExchangeRates.Queries;

public class GetExchangeRateQuery : IRequest<ExchangeRateModel>
{
    public required string CurrencyFrom { get; set; }
    public required string CurrencyTo { get; set; }
}

public class GetExchangeRateQueryHandler
    (IQueryRepository<ExchangeRate> repository, IMapper mapper) : IRequestHandler<GetExchangeRateQuery,
        ExchangeRateModel>
{
    public async Task<ExchangeRateModel> Handle(GetExchangeRateQuery query, CancellationToken cancellationToken)
    {
        query.CurrencyFrom = query.CurrencyFrom.ToUpper();
        query.CurrencyTo = query.CurrencyTo.ToUpper();
        var exchangeRate =
            await repository.GetAsync(
                x => x.CurrencyFrom.Equals(query.CurrencyFrom) && x.CurrencyTo.Equals(query.CurrencyTo),
                cancellationToken: cancellationToken)
            ?? throw new ObjectNotFoundException(nameof(ExchangeRate), "Currencies",
                $"{query.CurrencyFrom} {query.CurrencyTo}");

        return mapper.Map<ExchangeRateModel>(exchangeRate);
    }
}