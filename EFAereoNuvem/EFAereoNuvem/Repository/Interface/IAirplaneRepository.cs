using EFAereoNuvem.Models;

namespace EFAereoNuvem.Repository.Interface
{
    public interface IAirplaneRepository
    {
        Task CreateAsync(Airplane airplane);
        Task UpdateAsync(Airplane airplane);
        Task DeleteAsync(Guid id);
        Task<Airplane> GetById(Guid id);
        Task<Airplane?> GetByPrefix(string prefix);
        Task<List<Airplane>> GetAll();
        Task<bool> PrefixExists(string prefix);
    }
}