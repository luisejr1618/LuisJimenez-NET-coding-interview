using System.Collections.Generic;

namespace SecureFlight.Core.Entities;

public class Airport
{
    public Airport()
    {
        this.OriginFlights = new HashSet<Flight>();
        this.DestinationFlights = new HashSet<Flight>();
    }
    public string Code { get; set; }

    public string Name { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public ICollection<Flight> OriginFlights { get; set; }

    public ICollection<Flight> DestinationFlights { get; set; }
}