﻿using EFAereoNuvem.Data;
using EFAereoNuvem.Models;
using EFAereoNuvem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EFAereoNuvem.Repository
{
    public class ClientRepository(AppDBContext context) : IClientRepository
    {
        private readonly AppDBContext _context = context;

        // ==================== CREATE ====================
        public async Task Create(Client client)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Salva o CurrentAdress primeiro
                await _context.Adresses.AddAsync(client.CurrentAdress);
                await _context.SaveChangesAsync(); // Gera o ID do Address

                // 2. Atualiza a FK do cliente
                client.CurrentAdressId = client.CurrentAdress.Id;

                // 3. Se tiver FutureAdress, salva também
                if (client.FutureAdress != null)
                {
                    await _context.Adresses.AddAsync(client.FutureAdress);
                    await _context.SaveChangesAsync();
                    client.FutureAdressId = client.FutureAdress.Id;
                }

                // 4. Agora salva o cliente
                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // ==================== READ - Consultas Básicas ====================
        public async Task<List<Client>> GetAll()
        {
            return await _context.Clients
                .Include(c => c.CurrentAdress)
                .Include(c => c.FutureAdress)
                .Include(c => c.ClientStatus)
                .Include(c => c.Reservations)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Client?> GetById(int id)
        {
            return await _context.Clients
                .Include(c => c.CurrentAdress)
                .Include(c => c.FutureAdress)
                .Include(c => c.ClientStatus)
                .Include(c => c.Reservations)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client?> GetByCpf(string cpf)
        {
            return await _context.Clients
                .Include(c => c.CurrentAdress)
                .Include(c => c.FutureAdress)
                .Include(c => c.ClientStatus)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<Client?> GetByEmail(string email)
        {
            return await _context.Clients
                .Include(c => c.CurrentAdress)
                .Include(c => c.FutureAdress)
                .Include(c => c.ClientStatus)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<List<Client>> GetByName(string name)
        {
            return await _context.Clients
                .Include(c => c.CurrentAdress)
                .Include(c => c.ClientStatus)
                .Where(c => c.Name.Contains(name))
                .AsNoTracking()
                .ToListAsync();
        }

        // ==================== READ - Consultas Específicas ====================
        public async Task<List<Client>> GetByStatus(int statusId)
        {
            return await _context.Clients
                .Include(c => c.CurrentAdress)
                .Include(c => c.ClientStatus)
                .Where(c => c.ClientStatus != null && c.ClientStatus.Id == statusId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Client>> GetClientsWithReservations()
        {
            return await _context.Clients
                .Include(c => c.CurrentAdress)
                .Include(c => c.ClientStatus)
                .Include(c => c.Reservations)
                .Where(c => c.Reservations.Any())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Client>> GetByCity(string city)
        {
            return await _context.Clients
                .Include(c => c.CurrentAdress)
                .Include(c => c.ClientStatus)
                .Where(c => c.CurrentAdress.City.Contains(city))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Client>> GetByState(string state)
        {
            return await _context.Clients
                .Include(c => c.CurrentAdress)
                .Include(c => c.ClientStatus)
                .Where(c => c.CurrentAdress.State == state)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Client>> GetBirthdayClientsOfMonth(int month)
        {
            return await _context.Clients
                .Include(c => c.CurrentAdress)
                .Include(c => c.ClientStatus)
                .Where(c => c.BornDate.Month == month)
                .AsNoTracking()
                .ToListAsync();
        }

        // ==================== READ - Validações ====================
        public async Task<bool> CpfExists(string cpf)
        {
            return await _context.Clients.AnyAsync(c => c.Cpf == cpf);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Clients.AnyAsync(c => c.Email == email);
        }

        // ==================== UPDATE ====================
        public async Task Update(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCurrentAddress(int clientId, Adress newAddress)
        {
            var client = await _context.Clients.FindAsync(clientId);

            if (client == null)
                throw new InvalidOperationException($"Cliente com Id {clientId} não encontrado.");

            // Adiciona o novo endereço
            await _context.Adresses.AddAsync(newAddress);
            await _context.SaveChangesAsync();

            // Atualiza a referência do cliente
            client.CurrentAdressId = newAddress.Id;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFutureAddress(int clientId, Adress? newAddress)
        {
            var client = await _context.Clients.FindAsync(clientId);

            if (client == null)
                throw new InvalidOperationException($"Cliente com Id {clientId} não encontrado.");

            if (newAddress != null)
            {
                await _context.Adresses.AddAsync(newAddress);
                await _context.SaveChangesAsync();
                client.FutureAdressId = newAddress.Id;
            }
            else
            {
                client.FutureAdressId = null;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientStatus(int clientId, int statusId)
        {
            var client = await _context.Clients.FindAsync(clientId);

            if (client == null)
                throw new InvalidOperationException($"Cliente com Id {clientId} não encontrado.");

            var status = await _context.ClientStatus.FindAsync(statusId);

            if (status == null)
                throw new InvalidOperationException($"Status com Id {statusId} não encontrado.");

            client.ClientStatus = status;
            await _context.SaveChangesAsync();
        }

        // ==================== DELETE ====================
        public async Task Delete(Client client)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
                throw new InvalidOperationException($"Cliente com Id {id} não encontrado.");

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        // ==================== UTILITY ====================
        public async Task<int> Count()
        {
            return await _context.Clients.CountAsync();
        }

        public async Task<List<Client>> GetPaginated(int pageNumber, int pageSize)
        {
            return await _context.Clients
                .Include(c => c.CurrentAdress)
                .Include(c => c.ClientStatus)
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}