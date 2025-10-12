using EFFloristry.Models;

namespace EFFloristry.Repository
{
    public interface IClientRepository
    {
        Task Create(Client client);
        Task Update(Client client);
        Task Delete(Client client);
        Task<Client?> GetById(int id);
        Task<List<Client>> GetAll();
        Task<List<Client>> GetByName(string name);
    }
}