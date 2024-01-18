using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;
using System;
using System.Linq;
using Xunit;

namespace SecureFlight.Test
{
    public class AirportTests
    {

        private readonly IRepository<Airport> _airportRepository;

        public AirportTests(IRepository<Airport> airportRepository)
        {
            _airportRepository = airportRepository;
        }


        [Fact]
        public void Update_Succeeds()
        {
            var objectToUpdate = this._airportRepository.FilterAsync(x => x.Code == "JFK").Result.FirstOrDefault();
            objectToUpdate.Country = "CRC";
            this._airportRepository.Update(objectToUpdate);
            var updatedObject = this._airportRepository.FilterAsync(x => x.Code == "JFK").Result.FirstOrDefault();

            Assert.NotEqual(objectToUpdate.Country, updatedObject.Country);


        }
    }
}
