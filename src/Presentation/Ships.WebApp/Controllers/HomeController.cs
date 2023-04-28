using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ships.Application.ShipsQR.Queries.GetShips;
using System.Diagnostics;

namespace Ships.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISender? _mediator;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ISender? mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var ships = await _mediator.Send(new GetShipsQuery());
            return View(ships);
        }

    }
}