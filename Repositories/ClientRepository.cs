using MyGymProject.Server.Models;
using MyGymProject.Server.Repositories.Interfaces;
using MyGymProject.Server.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace MyGymProject.Server.Repositories
{
    public class ClientRepository: IClientRepository
    {
        private readonly DataBaseConnection _context;

        public ClientRepository(DataBaseConnection context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetByLoginAsync(string login)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Login == login); 
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client?> GetClientByPhoneAsync(string phone)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Phone == phone);
        }

        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Client client)
        {
            this._context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AddTrainingToClientAsync(int clientId, Training training)
        {
            var client = await _context.Clients.Include(c => c.Trainings)
                                            .FirstOrDefaultAsync(c => c.Id == clientId);
            if (client == null)
                throw new Exception("Клиент не найден");

            client.Trainings.Add(training);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
