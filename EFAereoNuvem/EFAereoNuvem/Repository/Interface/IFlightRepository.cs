using EFAereoNuvem.Models;

namespace EFAereoNuvem.Repository.Interface;
public interface IFlightRepository
{
    public Task Create(Flight flight);
    public Task Update(Flight flight);
    public Task Delete(Flight flight);
    public Task<Flight> GetById(int Id);
    public Task<Flight?> GetByCode(string codeFlight);
    public Task<List<Flight>> GetByDestination(string Destination);
    public Task<List<Flight>> GetAll();
    public Task<Flight> GetByIdWithScales(int id); 
}