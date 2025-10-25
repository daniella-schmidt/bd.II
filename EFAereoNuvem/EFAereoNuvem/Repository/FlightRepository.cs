using EFAereoNuvem.Data;
using EFAereoNuvem.Models;
using EFAereoNuvem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EFAereoNuvem.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AppDBContext _context;
        public FlightRepository(AppDBContext context) => _context = context;

        public async Task Create(Flight flight)
        {
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Flight flight)
        {
            _context.Flights.Update(flight);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Flight flight)
        {
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
        }

        public async Task<Flight> GetById(int Id) =>
            await _context.Flights.FindAsync(Id) ?? throw new KeyNotFoundException();

        public async Task<Flight?> GetByCode(string codeFlight) =>
            await _context.Flights.FirstOrDefaultAsync(f => f.CodeFlight == codeFlight);

        public async Task<List<Flight>> GetByDestination(string Destination) =>
            await _context.Flights.Where(f => f.Destination == Destination).ToListAsync();

        public async Task<List<Flight>> GetAll() =>
            await _context.Flights.Include(f => f.Airplane).ToListAsync();

        public async Task<Flight?> GetByIdWithScales(int id) =>
            await _context.Flights.Include(f => f.Scales).FirstOrDefaultAsync(f => f.Id == id);
    }
}