using EFFloristry.Data;
using EFFloristry.Models;
using Microsoft.EntityFrameworkCore;

namespace EFFloristry.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly FloristryContext _context;

        public ClientRepository(FloristryContext context)
        {
            _context = context;
        }

        public async Task Create(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Client client)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Client>> GetAll()
        {
            return await _context.Clients
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Client?> GetById(int id)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(p => p.ClientId == id);
        }

        public async Task<List<Client>> GetByName(string name)
        {
            return await _context.Clients
                .Where(p => p.Name!.ToLower().Contains(name.ToLower()))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task Update(Client client)
        {
            try
            {
                // Verificar se o cliente existe
                var existingClient = await _context.Clients.FindAsync(client.ClientId);
                if (existingClient == null)
                {
                    throw new InvalidOperationException("Cliente não encontrado.");
                }

                // Atualizar apenas as propriedades que foram modificadas
                _context.Entry(existingClient).CurrentValues.SetValues(client);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ClientExists(client.ClientId))
                {
                    throw new InvalidOperationException("Cliente não encontrado para atualização.");
                }
                throw;
            }
        }

        private async Task<bool> ClientExists(int id)
        {
            return await _context.Clients.AnyAsync(p => p.ClientId == id);
        }
    }
}