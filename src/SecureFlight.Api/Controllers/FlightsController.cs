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

    public FlightsController(IService<Flight> flightService, IMapper mapper)
        : base(mapper)
    {
        _flightService = flightService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FlightDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> Get()
    {
        var flights = await _flightService.GetAllAsync();
        return GetResult<IReadOnlyList<Flight>, IReadOnlyList<FlightDataTransferObject>>(flights);
    }
    
}