using Simplifly.Models;
using Route = Simplifly.Models.Route;

namespace Simplifly.Interfaces
{
    public interface IRouteFlightOwnerService
    {
        public Task<Route> AddRoute(Route route);
        public Task<Route> RemoveRoute(int sourceAirportId,int destinationAirportId);
        public Task<List<Route>> GetAllRoutes();
        public Task<Route> GetRouteById(int id);
        public Task<List<Route>> UpdateRoute(Route route);

        public Task<bool> RemoveRouteById(int routeId);
    }
}
