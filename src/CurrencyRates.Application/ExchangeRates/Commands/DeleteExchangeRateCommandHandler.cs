using System.Threading;
using System.Threading.Tasks;
using Common.Repository.Repository;
using CurrencyRates.Application.Exceptions;
using CurrencyRates.Domain.Entities.ExchangeRates;
using CurrencyRates.Domain.Entities.ExchangeRates.Commands;
using MediatR;

namespace CurrencyRates.Application.ExchangeRates.Commands;

public class DeleteExchangeRateCommandHandler
    (IRepository<ExchangeRate> repository) : IRequestHandler<DeleteExchangeRateCommand>
{
    public async Task Handle(DeleteExchangeRateCommand command, CancellationToken cancellationToken)
    {
        var exchangeRate =
            await repository.GetForUpdateAsync(x => x.Id == command.Id,
                cancellationToken: cancellationToken)
            ?? throw new ObjectNotFoundException(nameof(ExchangeRate), nameof(ExchangeRate.Id),
                command.Id);

        await repository.DeleteAsync(exchangeRate, cancellationToken);
    }
}