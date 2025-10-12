using EFFloristry.Models;

namespace EFFloristry.Repository
{
    public interface IProductRepository
    {
        public Task Create(Product product);
        public Task Update(Product product);
        public Task Delete(Product product);
        public Task<Product?> GetById(int id);
        public Task<List<Product>> GetAll();
        public Task<List<Product>> GetByName(string name);
        public Task<List<Product>> GetAllWithOrders();
    }
}
