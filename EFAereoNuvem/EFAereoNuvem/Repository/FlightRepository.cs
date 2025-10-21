using EFAereoNuvem.Data;
using EFAereoNuvem.Models;
using EFAereoNuvem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EFAereoNuvem.Repository;
public class FlightRepository(AppDBContext context) : IFlightRepository
{
    private readonly AppDBContext _context = context; //primary constructor 

    public async Task Create(Flight flight)
    {
        await _context.Flights.AddAsync(flight);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Flight flight)
    {
        _context.Flights.Remove(flight);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Flight>> GetAll() //paginação
    {
        var data = await _context.Flights.ToListAsync();
        return data;
    }

    public async Task<Flight?> GetByCode(string codeFlight)
    {
        var flight = await _context.Flights
            .Where(x => x.CodeFlight == codeFlight)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return flight;
    }

    public async Task<List<Flight>> GetByDestination(string destination)
    {
        var flights = await _context.Flights
            .Where(x => x.Destination == destination)
            .AsNoTracking()
            .ToListAsync();

        return flights;
    }

    public async Task<Flight> GetById(int Id)
    {
        var flight = await _context.Flights.Where(x => x.Id == Id).FirstOrDefaultAsync();

        return flight ?? throw new InvalidOperationException($"Voo com Id {Id} não encontrado.");
    }

    public async Task Update(Flight flight)
    {
        _context.Flights.Update(flight);
        await _context.SaveChangesAsync();
    }

    public async Task<Flight> GetByIdWithScales(int id)
    {
        return await _context.Flights
            .Include(f => f.Airplane)
            .Include(f => f.Scales)
            .Include(f => f.Reservations)
            .FirstOrDefaultAsync(f => f.Id == id)
            ?? throw new InvalidOperationException($"Voo com Id {id} não encontrado.");
    }
}
