using System;
using System.Collections.Generic;

namespace FlightBookAPI.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int FlightId { get; set; }

    public string? ReferenceNumber { get; set; }

    public string SeatType { get; set; } = null!;

    public DateTime? BookingDate { get; set; }

    public string ClassType { get; set; } = null!;

    public int Adults { get; set; }

    public int Children { get; set; }

    public int Seniors { get; set; }

    public decimal TotalPrice { get; set; }

    
}
