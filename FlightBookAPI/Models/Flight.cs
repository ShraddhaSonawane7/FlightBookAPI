using System;
using System.Collections.Generic;

namespace FlightBookAPI.Models;

public partial class Flight
{
    public int FlightId { get; set; }

    public string FlightNumber { get; set; } = null!;

    public string Airline { get; set; } = null!;

    public string Source { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public decimal WindowSeatPrice { get; set; }

    public decimal AisleSeatPrice { get; set; }

    public decimal MiddleSeatPrice { get; set; }

    public int TotalSeats { get; set; }

    public int AvailableSeats { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? FlightImageUrl { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
