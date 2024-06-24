using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Repository.Repository;
using Common.Repository.UnitOfWork;
using CurrencyRates.Application.Exceptions;
using CurrencyRates.Domain.Entities.ExchangeRates;
using CurrencyRates.Domain.Entities.ExchangeRates.Commands;
using MediatR;

namespace CurrencyRates.Application.ExchangeRates.Commands;

public class UpdateExchangeRateCommandHandler(IRepository<ExchangeRate> exchangeRateRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateExchangeRateCommand>
{
    public async Task Handle(UpdateExchangeRateCommand command, CancellationToken cancellationToken)
    {
        using var scope = await unitOfWork.CreateScopeAsync(cancellationToken);
        var exchangeRate =
            await exchangeRateRepository.GetForUpdateAsync(x => x.Id == command.Id,
                cancellationToken: cancellationToken) ?? throw new ObjectNotFoundException(nameof(ExchangeRate),
                nameof(ExchangeRate.Id),
                command.Id);

        exchangeRate.Rate = command.Rate;
        exchangeRate.UpdateDate = DateTime.Now;

        await exchangeRateRepository.UpdateAsync(exchangeRate, cancellationToken);

        // var history = new ExchangeRateHistory
        // {
        //     CurrencyFrom = exchangeRate.CurrencyFrom,
        //     CurrencyTo = exchangeRate.CurrencyTo,
        //     Rate = command.Rate,
        //     From = DateTime.Now
        // };
        //
        // await historyRepository.InsertAsync(history, cancellationToken);

        await scope.CompletAsync(cancellationToken);
    }
}