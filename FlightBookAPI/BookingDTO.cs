namespace FlightBookAPI
{
    public class BookingDTO
    {
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public string SeatType { get; set; } = null!;
        public string ClassType { get; set; } = null!;
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Seniors { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
