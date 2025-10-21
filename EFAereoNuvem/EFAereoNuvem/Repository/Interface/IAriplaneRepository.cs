using EFAereoNuvem.Models;

namespace EFAereoNuvem.Repository.Interface
{
    public interface IAirplaneRepository
    {
        Task Create(Airplane airplane);
        Task Update(Airplane airplane);
        Task Delete(int id);
        Task<Airplane> GetById(int id);
        Task<Airplane?> GetByPrefix(string prefix);
        Task<List<Airplane>> GetAll();
        Task<bool> PrefixExists(string prefix);
    }
}