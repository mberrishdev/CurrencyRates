using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Repository.Repository;
using CurrencyRates.Application.Exceptions;
using CurrencyRates.Application.ExchangeRates.Queries.Models;
using CurrencyRates.Domain.Entities.ExchangeRates;
using MediatR;

namespace CurrencyRates.Application.ExchangeRates.Queries;

public class ListExchangeRateQuery : IRequest<List<ExchangeRateModel>>
{
}

public class ListExchangeRateQueryHandler
    (IQueryRepository<ExchangeRate> repository, IMapper mapper) : IRequestHandler<ListExchangeRateQuery,
        List<ExchangeRateModel>>
{
    public async Task<List<ExchangeRateModel>> Handle(ListExchangeRateQuery query, CancellationToken cancellationToken)
    {
        var exchangeRates =
            await repository.GetListAsync(
                cancellationToken: cancellationToken);

        return mapper.Map<List<ExchangeRateModel>>(exchangeRates);
    }
}