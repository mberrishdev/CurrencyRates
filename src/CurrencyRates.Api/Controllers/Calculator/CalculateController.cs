using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.Application.ExchangeRates.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyRates.Api.Controllers.Calculator;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class CalculateController : ApiControllerBase
{
    public CalculateController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<decimal>> Calculate([Required, FromQuery] string currencyFrom,
        [Required, FromQuery] string currencyTo, [Required, FromQuery] decimal amount,
        CancellationToken cancellationToken)
    {
        var query = new GetExchangeRateQuery { CurrencyFrom = currencyFrom, CurrencyTo = currencyTo };
        var exchangeRate = await Mediator.Send(query, cancellationToken);

        var result = amount * exchangeRate.Rate;
        return Ok(result);
    }
}