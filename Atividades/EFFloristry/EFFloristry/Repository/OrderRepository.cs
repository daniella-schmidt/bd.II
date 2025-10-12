using EFFloristry.Data;
using EFFloristry.Models;
using Microsoft.EntityFrameworkCore;

namespace EFFloristry.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FloristryContext _context;

        public OrderRepository(FloristryContext context)
        {
            _context = context;
        }

        public async Task Create(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order?> GetById(int id)
        {
            return await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetByClientId(int clientId)
        {
            return await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => o.ClientId == clientId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<Order>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task Update(Order order)
        {
            try
            {
                // Buscar o pedido existente com seus itens
                var existingOrder = await _context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == order.Id);

                if (existingOrder == null)
                {
                    throw new InvalidOperationException("Pedido não encontrado.");
                }

                // Atualizar propriedades básicas
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.Description = order.Description;
                existingOrder.ClientId = order.ClientId;

                // Limpar itens existentes
                _context.OrderItems.RemoveRange(existingOrder.OrderItems);

                // Adicionar novos itens
                foreach (var item in order.OrderItems)
                {
                    // CORREÇÃO: Usar Add em vez de AddRange para ICollection
                    existingOrder.OrderItems.Add(new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    });
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderExists(order.Id))
                {
                    throw new InvalidOperationException("Pedido não encontrado para atualização.");
                }
                throw;
            }
        }

        private async Task<bool> OrderExists(int id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id);
        }
    }
}