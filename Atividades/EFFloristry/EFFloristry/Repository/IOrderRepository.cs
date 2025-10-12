using EFFloristry.Models;

namespace EFFloristry.Repository
{
    public interface IOrderRepository
    {
        Task Create(Order order);
        Task Update(Order order);
        Task Delete(Order order);
        Task<Order?> GetById(int id);
        Task<List<Order>> GetAll();
        Task<List<Order>> GetByClientId(int clientId);
        Task<List<Order>> GetByDateRange(DateTime startDate, DateTime endDate);
    }
}