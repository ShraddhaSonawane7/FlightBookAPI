using System;
using System.Collections.Generic;

namespace FlightBookAPI.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int FlightId { get; set; }

    public string ReferenceNumber { get; set; } = null!;

    public string SeatType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? BookingDate { get; set; }

    public string ClassType { get; set; } = null!;

    public int Adults { get; set; }

    public int Children { get; set; }

    public int Seniors { get; set; }

    public decimal TotalPrice { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public virtual ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();

    public virtual Flight Flight { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
