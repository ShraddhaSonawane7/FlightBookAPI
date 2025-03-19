namespace FlightBookAPI
{
    public class BookingDto
    {
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public string ReferenceNumber { get; set; }
        public string SeatType { get; set; }
        public DateTime? BookingDate { get; set; }
        public string ClassType { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Seniors { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
