using FlightBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightBookAPI.Interfaces
{
    public interface IFlight
    {
        Task<IEnumerable<Flight>> SearchFlights(string source, string destination, DateTime date);
        Task<IEnumerable<Flight>> GetAllFlight();
        Task<IEnumerable<Flight>> SearchFlightsBySourceDestination(string source, string destination);
        Task<Flight> GetFlightById(int flightId);
        Task<Flight> AddFlight(Flight flight);
        Task<Flight> UpdateFlight(Flight flight);
        Task<bool> DeleteFlight(int flightId);
        Task<IEnumerable<Flight>> FilterByAirline(string airline);
     
    }
}
