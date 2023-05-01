
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ships.Application.Common.Exceptions;
using Ships.Application.Common.Models;
using Ships.Application.ShipsQR.Commands;
using Ships.Application.ShipsQR.Queries;

namespace Ships.WebApi.Controllers;

[Authorize]
public class ShipsController : BaseApiController
{
    [HttpGet]
    public async Task<PaginatedList<ShipDto>> GetAll([FromQuery] GetShipsQuery query)
        => await Mediator.Send(query);

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ShipDto> GetById(int id)
        => (await Mediator.Send(new GetShipByIdQuery(id))) ?? throw new NotFoundException();


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<int> Create( CreateShipCommand createCommand)
        => await Mediator.Send(createCommand);


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update([FromRoute]int id,[FromBody] UpdateShipCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteShipCommand(id));

        return NoContent();
    }
}
