using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Route = Simplifly.Models.Route;

namespace Simplifly.Services
{
    public class RouteService : IRouteFlightOwnerService
    {
        private readonly IRepository<int, Route> _routeRepository;
        private readonly ILogger<RouteService> _logger;
        public RouteService(IRepository<int, Route> routeRepository, ILogger<RouteService> logger)
        {
            _routeRepository = routeRepository;
            _logger = logger;

        }
        public async Task<Route> AddRoute(Route route)
        {
            var existingRoutes = await GetAllRoutes();
            var existingRoute=existingRoutes.FirstOrDefault(s=>s.SourceAirportId==route.SourceAirportId
             && s.DestinationAirportId==route.DestinationAirportId);

            if (existingRoute==null)
            {
                route = await _routeRepository.Add(route);
                return route;
            }
            throw new RouteAlreadyPresentException();
        }

        public async Task<List<Route>> GetAllRoutes()
        {
            var routes = await _routeRepository.GetAsync();
            return routes;
        }

        public async Task<Route> RemoveRoute(int sourceAirportId, int destinationAirportId)
        {
            var routes = await _routeRepository.GetAsync();
            var route = routes.FirstOrDefault(e => e.SourceAirportId == sourceAirportId
            && e.DestinationAirportId == destinationAirportId);

            if (route != null)
            {
                int routeId = route.Id;
                route = await _routeRepository.Delete(routeId);
                return route;
            }
            throw new NoSuchRouteException();

        }

        public Task<List<Route>> UpdateRoute(Route route)
        {
            throw new NotImplementedException();
        }
    }
}
