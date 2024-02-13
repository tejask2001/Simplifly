using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;
using Route = Simplifly.Models.Route;

namespace Simplifly.Services
{
    public class RouteService : IRouteFlightOwnerService
    {
        private readonly IRepository<int, Route> _routeRepository;
        private readonly ILogger<RouteService> _logger;
        private readonly IRepository<int, Airport> _airportRepository;
        /// <summary>
        /// Constructor to initialize the objects
        /// </summary>
        /// <param name="routeRepository"></param>
        /// <param name="logger"></param>
        public RouteService(IRepository<int, Route> routeRepository, ILogger<RouteService> logger)
        {
            _routeRepository = routeRepository;
            _logger = logger;

        }
        public RouteService(IRepository<int, Route> routeRepository, ILogger<RouteService> logger, IRepository<int, Airport> airportRepository)
        {
            _airportRepository = airportRepository;
            _routeRepository = routeRepository;
            _logger = logger;

        }

        public async Task<Airport> AddAirport(Models.Airport airport)
        {
            var airports= await _airportRepository.GetAsync();
            var existingAirport=airports.FirstOrDefault(e=>e.Name==airport.Name && e.City==airport.City);
            if(existingAirport==null)
            {
                airport = await _airportRepository.Add(airport);
                return airport;
            }
            throw new AirportAlreadyPresentException();
        }

        /// <summary>
        ///Service class method to add route 
        /// </summary>
        /// <param name="route">Object of route</param>
        /// <returns>route object</returns>
        /// <exception cref="RouteAlreadyPresentException">Throw when route is already present</exception>
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


        /// <summary>
        /// Service class method to get all Routes
        /// </summary>
        /// <returns>List of route</returns>
        public async Task<List<Route>> GetAllRoutes()
        {
            var routes = await _routeRepository.GetAsync();
            return routes;
        }

        public async Task<Route> GetRouteById(int id)
        {
            var route=await _routeRepository.GetAsync(id);
            if (route != null)
            {
                return route;
            }
            throw new NoSuchRouteException();
        }

        /// <summary>
        /// Service class method to remove route
        /// </summary>
        /// <param name="sourceAirportId">Source airport id in int</param>
        /// <param name="destinationAirportId">Destination airport id in int</param>
        /// <returns>Object of route</returns>
        /// <exception cref="NoSuchRouteException">throw when route is not present</exception>
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
        public async Task<bool> RemoveRouteById(int routeId)
        {
            if( await _routeRepository.Delete(routeId) != null)
            {
                return true;
            };
            return false;
        }

        public Task<List<Route>> UpdateRoute(Route route)
        {
            throw new NotImplementedException();
        }
    }
}
