using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ships.Application.ShipsQR.Queries;

namespace Ships.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISender? _mediator;

        public HomeController( ISender? mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<IActionResult> Index([FromQuery] GetShipsQuery query, string currentFilter,CancellationToken cancellationToken)
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

            var ships = await _mediator!.Send(query, cancellationToken);
            return View(ships);
        }


    }
}