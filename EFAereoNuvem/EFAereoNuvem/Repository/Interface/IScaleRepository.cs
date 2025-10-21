using EFAereoNuvem.Models;

namespace EFAereoNuvem.Repository.Interface
{
    public interface IScaleRepository
    {
        Task Create(Scale scale);
        Task Update(Scale scale);
        Task Delete(int id);
        Task DeleteByFlightId(int flightId);
        Task<Scale> GetById(int id);
        Task<List<Scale>> GetByFlightId(int flightId);
        Task<List<Scale>> GetAll();
    }
}