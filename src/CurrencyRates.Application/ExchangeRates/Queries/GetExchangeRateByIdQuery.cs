using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Repository.Repository;
using CurrencyRates.Application.Exceptions;
using CurrencyRates.Application.ExchangeRates.Queries.Models;
using CurrencyRates.Domain.Entities.ExchangeRates;
using MediatR;

namespace CurrencyRates.Application.ExchangeRates.Queries;

public class GetExchangeRateByIdQuery : IRequest<ExchangeRateModel>
{
    public required int Id { get; set; }
}

public class GetExchangeRateByIdQueryHandler
    (IQueryRepository<ExchangeRate> repository, IMapper mapper) : IRequestHandler<GetExchangeRateByIdQuery,
        ExchangeRateModel>
{
    public async Task<ExchangeRateModel> Handle(GetExchangeRateByIdQuery query, CancellationToken cancellationToken)
    {
        var exchangeRate =
            await repository.GetAsync(
                x => x.Id == query.Id,
                cancellationToken: cancellationToken)
            ?? throw new ObjectNotFoundException(nameof(ExchangeRate), nameof(ExchangeRate.Id),
                $"{query.Id}");

        return mapper.Map<ExchangeRateModel>(exchangeRate);
    }
}