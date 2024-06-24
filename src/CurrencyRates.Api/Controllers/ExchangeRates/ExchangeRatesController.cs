using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.Application.ExchangeRates.Queries;
using CurrencyRates.Domain.Entities.ExchangeRates;
using CurrencyRates.Domain.Entities.ExchangeRates.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyRates.Api.Controllers.ExchangeRates;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class ExchangeRatesController : ApiControllerBase
{
    public ExchangeRatesController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Gets the exchange rate for the specified currencies.
    /// </summary>
    /// <param name="currencyFrom">The source currency.</param>
    /// <param name="currencyTo">The target currency.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The exchange rate.</returns>
    [HttpGet]
    public async Task<ActionResult<ExchangeRate>> GetExchangeRate([Required, FromQuery] string currencyFrom,
        [Required, FromQuery] string currencyTo, CancellationToken cancellationToken)
    {
        var query = new GetExchangeRateQuery() { CurrencyFrom = currencyFrom, CurrencyTo = currencyTo };
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets the list of all exchange rates.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The list of exchange rates.</returns>
    [HttpGet("List")]
    public async Task<ActionResult<IEnumerable<ExchangeRate>>> GetExchangeRates(CancellationToken cancellationToken)
    {
        var query = new ListExchangeRateQuery() { };
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Creates a new exchange rate.
    /// </summary>
    /// <param name="command">The create exchange rate command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created exchange rate.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateExchangeRate([FromBody] CreateExchangeRateCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Updates an existing exchange rate.
    /// </summary>
    /// <param name="id">The ID of the exchange rate to update.</param>
    /// <param name="command">The update exchange rate command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExchangeRate([FromRoute] int id,
        [FromBody] UpdateExchangeRateCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        await Mediator.Send(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Deletes an exchange rate.
    /// </summary>
    /// <param name="id">The ID of the exchange rate to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExchangeRate(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteExchangeRateCommand { Id = id };
        await Mediator.Send(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Checks if an exchange rate exists.
    /// </summary>
    /// <param name="id">The ID of the exchange rate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>HTTP headers with no body.</returns>
    [HttpHead("{id}")]
    public async Task<IActionResult> HeadExchangeRate(int id, CancellationToken cancellationToken)
    {
        var query = new GetExchangeRateByIdQuery { Id = id };
        var result = await Mediator.Send(query, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Gets the supported HTTP methods.
    /// </summary>
    /// <returns>Supported HTTP methods.</returns>
    [HttpOptions]
    public IActionResult Options()
    {
        Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, HEAD, OPTIONS");
        return Ok();
    }
}