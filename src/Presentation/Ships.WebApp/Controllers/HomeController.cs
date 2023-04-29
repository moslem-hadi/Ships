using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ships.Application.Common.Models;
using Ships.Application.ShipsQR.Queries.GetShips;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        //public async Task<IActionResult> Index([FromQuery]GetShipsQuery query)
        //{
        //    var ships = await _mediator.Send(query);
        //    return View(ships);
        //}


        public async Task<IActionResult> Index([FromQuery] GetShipsQuery query,
            string currentFilter)
        {
            ViewData["CurrentSort"] = query.Sort;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(query.Sort) ? "name_desc" : "";
            ViewData["LengthSortParm"] = query.Sort == "length" ? "length_desc" : "length";
            ViewData["WidthSortParm"] = query.Sort == "width" ? "width_desc" : "width";

            if (query.Filter != null)
            {
                query.Page = 1;
            }
            else
            {
                query.Filter = currentFilter;
            }

            ViewData["CurrentFilter"] = query.Filter;

            var ships = await _mediator.Send(query);
            return View(ships);
        }


    }
}