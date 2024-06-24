using Common.Repository.Repository;
using Common.Repository.UnitOfWork;
using CurrencyRates.Domain.Entities.ExchangeRates;
using CurrencyRates.Domain.Entities.ExchangeRates.Commands;
using MediatR;

namespace CurrencyRates.Application.ExchangeRates.Commands;

public class CreateExchangeRateCommandHandler
(IRepository<ExchangeRate> exchangeRateRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateExchangeRateCommand, int>
{
    public async Task<int> Handle(CreateExchangeRateCommand command, CancellationToken cancellationToken)
    {
        command.CurrencyFrom = command.CurrencyFrom.ToUpper();
        command.CurrencyTo = command.CurrencyTo.ToUpper();

        using var scope = await unitOfWork.CreateScopeAsync(cancellationToken);
        var exchangeRate = new ExchangeRate
        {
            CurrencyFrom = command.CurrencyFrom,
            CurrencyTo = command.CurrencyTo,
            Rate = command.Rate,
            CreateDate = DateTime.Now
        };

        await exchangeRateRepository.InsertAsync(exchangeRate, cancellationToken);

        // var history = new ExchangeRateHistory
        // {
        //     CurrencyFrom = exchangeRate.CurrencyFrom,
        //     CurrencyTo = exchangeRate.CurrencyTo,
        //     Rate = command.Rate,
        //     From = DateTime.Now
        // };
        //
        // await exchangeRateHistoryRepository.InsertAsync(history, cancellationToken);

        await scope.CompletAsync(cancellationToken);
        return exchangeRate.Id;
    }
}