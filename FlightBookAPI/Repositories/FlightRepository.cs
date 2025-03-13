using FlightBookAPI.Interfaces;
using FlightBookAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookAPI.Repositories
{
    public class FlightRepository : IFlight
    {
        private readonly FlightDemoContext _context;

        public FlightRepository(FlightDemoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Flight>> SearchFlights(string source, string destination, DateTime date)
        {
            return await _context.Flights
                .Where(f => f.Source == source && f.Destination == destination && f.DepartureTime.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> SearchFlightsBySourceDestination(string source, string destination)
        {
            return await _context.Flights
                .Where(f => f.Source == source && f.Destination == destination)
                .ToListAsync();
        }

        public async Task<Flight> GetFlightById(int flightId)
        {
            return await _context.Flights.FindAsync(flightId);
        }

        public async Task<IEnumerable<Flight>> GetAllFlight()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task<Flight> AddFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        public async Task<Flight> UpdateFlight(Flight flight)
        {
            _context.Flights.Update(flight);
            await _context.SaveChangesAsync();
            return flight;
        }


        public async Task<bool> DeleteFlight(int flightId)
        {
            var flight = await _context.Flights.FindAsync(flightId);
            if (flight == null) return false;

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Flight>> FilterByAirline(string airline)
        {
            return await _context.Flights
                .Where(f => f.Airline.Contains(airline)) // Filters by airline name
                .ToListAsync();
        }
    }
}
