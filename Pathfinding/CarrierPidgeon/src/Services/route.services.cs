using CarrierPidgeon.Models;
using CarrierPidgeon.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarrierPidgeon.Services
{
    public class RouteService
    {
        private readonly RouteRepository _routeRepository;

        public RouteService(RouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<List<Route>> GetAllRoutesAsync()
        {
            return await _routeRepository.GetAllRoutesAsync();
        }

        public async Task<Route> GetRouteByIdAsync(string id)
        {
            return await _routeRepository.GetRouteByIdAsync(id);
        }

        public async Task CreateRouteAsync(Route route)
        {
            await _routeRepository.CreateRouteAsync(route);
        }

        public async Task UpdateRouteAsync(string id, Route route)
        {
            await _routeRepository.UpdateRouteAsync(id, route);
        }

        public async Task DeleteRouteAsync(string id)
        {
            await _routeRepository.DeleteRouteAsync(id);
        }
    }
}