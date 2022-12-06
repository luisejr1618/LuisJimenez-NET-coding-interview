using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Models;
using SecureFlight.Api.Utils;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;

namespace SecureFlight.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController : SecureFlightBaseController
{
    private readonly IService<Flight> _flightService;
    private readonly IRepository<PassengerFlight> _passengerFlightRepository;
    private readonly IRepository<Passenger> _passengerRepository;

    public FlightsController(IService<Flight> flightService, 
        IRepository<PassengerFlight> repo, 
        IRepository<Passenger> passangerRepo, 
        IMapper mapper)
        : base(mapper)
    {
        _flightService = flightService;
        _passengerFlightRepository = repo;
        _passengerRepository = passangerRepo;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FlightDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> Get()
    {
        var flights = await _flightService.GetAllAsync();
        return GetResult<IReadOnlyList<Flight>, IReadOnlyList<FlightDataTransferObject>>(flights);
    }

    [HttpPost("add-passanger-to-flight/{flightId}")]
    [ProducesResponseType(typeof(PassengerDataTransferObject), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public IActionResult AddPassengerToFlight(long flightId, [FromBody] PassengerDataTransferObject passengerData)
    {
        var result = _passengerFlightRepository.Add(new PassengerFlight
        {
            FlightId = flightId,
            PassengerId = passengerData.Id,
        });

        return Ok(result);
    }

    [HttpDelete("passanger-from-flight/{passangerId}")]
    [ProducesResponseType(typeof(PassengerDataTransferObject), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> DeletePassengerFromFlight(string passangerId, [FromBody] FlightDataTransferObject flightObject)
    {
        var passangerFlights = await _passengerFlightRepository.FilterAsync(x => x.PassengerId == passangerId);
        var flightToDelete = passangerFlights.FirstOrDefault(x => x.FlightId == flightObject.Id);
        if (flightToDelete != null) 
        {
            _passengerFlightRepository.Remove(flightToDelete);
            var hasFlight = passangerFlights.Except(Enumerable.Repeat(flightToDelete, 1)).Any();
            if (!hasFlight) 
            {
                _passengerRepository.Remove(new Passenger
                {
                    Id = passangerId,
                });
            }
        }

        return Ok();
    }

}