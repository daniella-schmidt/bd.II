using EFFloristry.Data;
using EFFloristry.Models;
using Microsoft.EntityFrameworkCore;

namespace EFFloristry.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly FloristryContext _context;

        public ProductRepository(FloristryContext context)
        {
            _context = context;
        }

        public async Task Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products
                .OrderBy(p => p.ProductDescription)
                .ToListAsync();
        }

        public async Task<List<Product>> GetByName(string name)
        {
            return await _context.Products
                .Where(p => p.ProductDescription!.ToLower().Contains(name.ToLower()))
                .OrderBy(p => p.ProductDescription)
                .ToListAsync();
        }

        public async Task<List<Product>> GetAllWithOrders()
        {
            return await _context.Products
                .Include(p => p.OrderItems!)
                    .ThenInclude(oi => oi.Order)
                    .ThenInclude(o => o!.Client)
                .OrderBy(p => p.ProductDescription)
                .ToListAsync();
        }
    }
}