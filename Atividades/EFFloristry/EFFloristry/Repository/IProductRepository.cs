using EFFloristry.Models;

namespace EFFloristry.Repository
{
    public interface IProductRepository
    {
        public Task Create(Product product);
        public Task Update(Product student);
        public Task Delete(Product student);
        public Task<Product?> GetById(int id);
        public Task<List<Product>> GetAll();
        public Task<List<Product>> GetByName(string name);


    }
}
