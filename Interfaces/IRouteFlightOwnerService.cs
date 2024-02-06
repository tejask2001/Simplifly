using Simplifly.Models;
using Route = Simplifly.Models.Route;

namespace Simplifly.Interfaces
{
    public interface IRouteFlightOwnerService
    {
        public Task<Route> AddRoute(Route route);
        public Task<Route> RemoveRoute(Route route);
        public Task<List<Route>> GetAllRoutes();
        public Task<List<Route>> UpdateFlight(Route route);
    }
}
