using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models.DTO_s;
using Route = Simplifly.Models.Route;

namespace Simplifly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteFlightOwnerService _routeFlightOwnerService;
        private readonly ILogger<RouteController> _logger;
        public RouteController(IRouteFlightOwnerService routeFlightOwnerService, ILogger<RouteController> logger)
        {
            _routeFlightOwnerService = routeFlightOwnerService;
            _logger = logger;

        }

        [HttpGet]
        public async Task<List<Route>> GetAllRoute()
        {
            var routes = await _routeFlightOwnerService.GetAllRoutes();
            return routes;
        }

        [HttpPost]
        public async Task<Route> AddRoute(Route route)
        {
            route=await _routeFlightOwnerService.AddRoute(route);
            return route;
        }

        [HttpDelete]
        public async Task<Route> RemoveRoute(RemoveRouteDTO routeDTO)
        {
            var route=await _routeFlightOwnerService.RemoveRoute(routeDTO.sourceAirportId,routeDTO.destinationAirportId);
            return route;
        }
    }
}
