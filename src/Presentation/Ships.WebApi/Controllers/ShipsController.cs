
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ships.Application.Common.Exceptions;
using Ships.Application.Common.Models;
using Ships.Application.ShipsQR.Commands.CreateTodoList;
using Ships.Application.ShipsQR.Queries.GetShip;
using Ships.Application.ShipsQR.Queries.GetShips;

namespace Ships.WebApi.Controllers;

[Authorize]
public class ShipsController : BaseApiController
{
    [HttpGet]
    public async Task<PaginatedList<ShipDto>> GetAll([FromQuery] GetShipsQuery query)
        => await Mediator.Send(query);

    [HttpGet("{id}")]
    public async Task<ShipDto> GetById(int id)
        => (await Mediator.Send(new GetShipByIdQuery(id))) ?? throw new NotFoundException();
    [HttpPost]
    public async Task<int> Create( CreateShipCommand createCommand)
        => await Mediator.Send(createCommand);
}
