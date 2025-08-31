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

        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products
                .OrderBy(p => p.ProductDescription)
                .ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetByName(string name)
        {
            return await _context.Products
                .Where(p => p.ProductDescription!.ToLower().Contains(name.ToLower()))
                .OrderBy(p => p.ProductDescription)
                .ToListAsync();
        }

        public async Task Update(Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verifica se o produto ainda existe
                if (!await ProductExists(product.Id))
                {
                    throw new InvalidOperationException("Produto não encontrado para atualização.");
                }
                throw;
            }
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        // Métodos adicionais úteis
        public async Task<List<Product>> GetByCategory(string category)
        {
            return await _context.Products
                .Where(p => p.Category == category)
                .OrderBy(p => p.ProductDescription)
                .ToListAsync();
        }

        public async Task<List<Product>> GetLowStockProducts(int threshold = 5)
        {
            return await _context.Products
                .Where(p => p.Stock <= threshold)
                .OrderBy(p => p.Stock)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalValue()
        {
            return await _context.Products
                .SumAsync(p => p.Price * (p.Stock ?? 0));
        }

        public async Task<int> GetTotalStock()
        {
            return await _context.Products
                .Where(p => p.Stock.HasValue)
                .SumAsync(p => p.Stock!.Value);
        }
    }
}