using System;

namespace SecureFlight.Api.Models;

public class FlightDataTransferObject
{
    public long Id { get; set; }
        
    public string Code { get; set; }

    public string OriginAirport { get; set; }

    public string DestinationAirport { get; set; }

    public int FlightStatusId { get; set; }

    public DateTime DepartureDateTime { get; set; }

    public DateTime ArrivalDateTime { get; set; }
}