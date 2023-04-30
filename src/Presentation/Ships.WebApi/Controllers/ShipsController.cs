
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ships.Application.Common.Models;
using Ships.Application.ShipsQR.Queries.GetShips;

namespace Ships.WebApi.Controllers;

[Authorize]
public class ShipsController : BaseApiController
{
    [HttpGet]
    public async Task<PaginatedList<ShipDto>> GetAll([FromQuery] GetShipsQuery query)
        => await Mediator.Send(query);
}
