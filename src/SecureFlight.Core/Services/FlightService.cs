using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureFlight.Core.Services
{
    public class FlightService : IService<Flight>
    {
        private readonly IRepository<Flight> _repository;

        public FlightService(IRepository<Flight> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<Flight>> FilterFlights(string origen, string destination)
        {
            return await this._repository.FilterAsync(x => 
            (!string.IsNullOrEmpty(origen) ? x.OriginAirport == origen : true) 
            || (!string.IsNullOrEmpty(destination) ? x.DestinationAirport == destination : true));
        }
        public Task<OperationResult<IReadOnlyList<Flight>>> GetAllAsync()
        {
            return this.GetAllAsync();
        }
    }
}
