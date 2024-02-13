﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;
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
        [Authorize(Roles ="f    ")]
        public async Task<ActionResult<List<Route>>> GetAllRoute()
        {
            try
            {
                var routes = await _routeFlightOwnerService.GetAllRoutes();
                return routes;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return NotFound(ex.Message);
            }
            
        }

        [Route("AddAirport")]
        [HttpPost]
        public async Task<ActionResult<Airport>> AddAirport(Airport airport)
        {
            try
            {
                airport=await _routeFlightOwnerService.AddAirport(airport);
                return airport;
            }
            catch(AirportAlreadyPresentException aape)
            {
                _logger.LogInformation(aape.Message);
                return NotFound(aape.Message);
            }
        }


        [Route("AddRoute")]
        [HttpPost]
        [Authorize(Roles = "flightOwner")]
        public async Task<ActionResult<Route>> AddRoute(Route route)
        {
            try
            {
                route = await _routeFlightOwnerService.AddRoute(route);
                return route;
            }
            catch(RouteAlreadyPresentException nape)
            {
                _logger.LogInformation(nape.Message);
                return NotFound(nape.Message);
            }
            
        }

        [HttpDelete]
        [Authorize(Roles = "flightOwner")]
        public async Task<ActionResult<Route>> RemoveRoute(RemoveRouteDTO routeDTO)
        {
            try
            {
                var route = await _routeFlightOwnerService.RemoveRoute(routeDTO.sourceAirportId, routeDTO.destinationAirportId);
                return route;
            }
            catch (NoSuchRouteException nsre)
            {
                _logger.LogInformation(nsre.Message);
                return NotFound(nsre.Message);
            }
            
        }
    }
}
