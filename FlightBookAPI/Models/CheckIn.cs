using System;
using System.Collections.Generic;

namespace FlightBookAPI.Models;

public partial class CheckIn
{
    public int CheckInId { get; set; }

    public int BookingId { get; set; }

    public string SeatNumber { get; set; } = null!;

    public string CheckInStatus { get; set; } = null!;

    public DateTime? CheckInTime { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
