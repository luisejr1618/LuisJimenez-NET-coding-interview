using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Models;
using SecureFlight.Api.Utils;
using SecureFlight.Core;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;

namespace SecureFlight.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PassengersController : SecureFlightBaseController
{
    private readonly IService<Passenger> _personService;
    private readonly IRepository<Passenger> _passengerRepository;
    private readonly IMapper _mapper;

    public PassengersController(IService<Passenger> personService, IRepository<Passenger> passengerRepository, IMapper mapper)
        : base(mapper)
    {
        _personService = personService;
        _passengerRepository = passengerRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PassengerDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> Get()
    {
        var passengers = await _personService.GetAllAsync();
        return GetResult<IReadOnlyList<Passenger>, IReadOnlyList<PassengerDataTransferObject>>(passengers);
    }
    
    [HttpGet("{flightId}")]
    [ProducesResponseType(typeof(IEnumerable<PassengerDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> GetPassengersByFlight(long flightId)
    {
        var passengers = await _passengerRepository.FilterAsync(p => p.Flights.Any(x => x.Id == flightId));
        if (!passengers.Any())
        {
            return new ErrorResponseActionResult
            {
                Result = new ErrorResponse
                {
                    Error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"No passengers were found for the flight {flightId}"
                    }
                }
            };
        }
        return Ok(_mapper.Map<IReadOnlyList<PassengerDataTransferObject>>(passengers));
    }
}