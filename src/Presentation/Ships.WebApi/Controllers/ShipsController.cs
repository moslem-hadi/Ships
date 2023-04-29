
using Microsoft.AspNetCore.Mvc;
using Ships.Application.Common.Models;
using Ships.Application.ShipsQR.Queries.GetShips;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ships.WebApi.Controllers;

public class ShipsController : BaseApiController
{
    [HttpGet]
    public async Task<PaginatedList<ShipDto>> GetAll([FromQuery] GetShipsQuery query)
        => await Mediator.Send(query);
}
