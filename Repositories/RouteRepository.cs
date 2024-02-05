using Microsoft.EntityFrameworkCore;
using Simplifly.Context;
using Simplifly.Exceptions;
using Simplifly.Interfaces;

namespace Simplifly.Repositories
{
    public class RouteRepository : IRepository<int, Models.Route>
    {
        RequestTrackerContext _context;
        public RouteRepository(RequestTrackerContext context)
        {
            _context = context;
        }
        public async Task<Models.Route> Add(Models.Route items)
        {
            _context.Add(items);
            _context.SaveChanges();
            return items;
        }

        public Task<Models.Route> Delete(Models.Route items)
        {
            var route = GetAsync(items.Id);
            if (route != null)
            {
                _context.Remove(route);
                _context.SaveChanges();
                return route;
            }
            throw new NoSuchRouteException();
        }

        public async Task<Models.Route> GetAsync(int key)
        {
            var routes= await GetAsync();
            var route = routes.FirstOrDefault(e => e.Id == key);
            if (route != null)
            {
                return route;
            }
            throw new NoSuchRouteException();
        }

        public async Task<List<Models.Route>> GetAsync()
        {
            var routes = _context.Routes.ToList();
            return routes;
        }

        public async Task<Models.Route> Update(Models.Route items)
        {
            var route= await GetAsync(items.Id);
            if (route != null)
            {
                _context.Entry<Models.Route>(route).State=EntityState.Modified;
                return route;
            }
            throw new NoSuchRouteException();
        }
    }
}
